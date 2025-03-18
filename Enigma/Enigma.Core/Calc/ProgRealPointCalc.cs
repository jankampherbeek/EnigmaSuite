// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023, 2024, 2025.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Handlers;
using Enigma.Domain.Dtos;
using Enigma.Domain.References;
using Enigma.Domain.Requests;
using Enigma.Domain.Responses;
using Enigma.Facades.Se;

namespace Enigma.Core.Calc;


/// <summary>Calculator for progressive points based on real movements, e.g. transits and secondary directions.</summary>
public interface IProgRealPointCalc
{
    /// <summary>Calculates progressive positions.</summary>
    /// <param name="ayanamsha">Ayanamsha to use, 'None' for tropical calculations.</param>
    /// <param name="observerPos">Position of observer.</param>
    /// <param name="location">Location.</param>
    /// <param name="julianDayUt">Julian day number.</param>
    /// <param name="progPoints">Dictionary with supported points.</param>
    /// <returns></returns>
    public ProgRealPointsResponse CalculateTransits(Ayanamshas ayanamsha, ObserverPositions observerPos,
        Location? location, double julianDayUt, Dictionary<ChartPoints, ProgPointConfigSpecs> progPoints);
}

public class ProgRealPointCalc: IProgRealPointCalc
{
    private readonly ISeFlags _seFlags;
    private readonly IPeriodSupportChecker _periodSupportChecker;
    private readonly IChartPointsMapping _chartPointsMapping;
    private readonly ICelPointSeCalc _celPointSeCalc;
    private readonly ICelPointsElementsCalc _celPointsElementsCalc;    
    private readonly ICoTransFacade _coordinateConversionFacade;
    private readonly IObliquityHandler _obliquityHandler;
    
    public ProgRealPointCalc(ISeFlags seFlags, 
        IPeriodSupportChecker periodSupportChecker,
        IChartPointsMapping chartPointsMapping,
        ICelPointSeCalc celPointSeCalc,
        ICelPointsElementsCalc celPointsElementsCalc,
        ICoTransFacade coordinateConversionFacade,
        IObliquityHandler obliquityHandler)
    {
        _seFlags = seFlags;
        _periodSupportChecker = periodSupportChecker;
        _chartPointsMapping = chartPointsMapping;
        _celPointSeCalc = celPointSeCalc;
        _celPointsElementsCalc = celPointsElementsCalc;
        _coordinateConversionFacade = coordinateConversionFacade;
        _obliquityHandler = obliquityHandler;
    }
       
    public ProgRealPointsResponse CalculateTransits(Ayanamshas ayanamsha, ObserverPositions observerPos, 
        Location? location, double julianDayUt, Dictionary<ChartPoints, ProgPointConfigSpecs> progPoints)
    {
        ZodiacTypes zodiacType = ayanamsha == Ayanamshas.None ? ZodiacTypes.Tropical : ZodiacTypes.Sidereal;
        int flagsEcliptical = _seFlags.DefineFlags(CoordinateSystems.Ecliptical, observerPos, zodiacType);
        int flagsEquatorial = _seFlags.DefineFlags(CoordinateSystems.Equatorial, observerPos, zodiacType);

        Dictionary<ChartPoints, ProgPointConfigSpecs> allCelPoints = progPoints;
        List<ChartPoints> celPoints = (from configPoint in allCelPoints
            where configPoint.Value.IsUsed && _periodSupportChecker.IsSupported(configPoint.Key, julianDayUt)
            select configPoint.Key).ToList();

        ObliquityRequest obliquityRequest = new(julianDayUt, true);
        double obliquity = _obliquityHandler.CalcObliquity(obliquityRequest);

        if (observerPos == ObserverPositions.TopoCentric)
        {
            SeInitializer.SetTopocentric(location.GeoLong, location.GeoLat, 0.0);
        }

        var pointsInTransit = new Dictionary<ChartPoints, ProgPositions>();
        foreach (ChartPoints celPoint in celPoints)
        {
            CalculationCats calculationCat = _chartPointsMapping.CalculationTypeForPoint(celPoint);
            switch (calculationCat)
            {
                case CalculationCats.CommonSe:
                {
                    KeyValuePair<ChartPoints, ProgPositions> calcResult = CreatePosForSePoint(celPoint,
                        julianDayUt, location, flagsEcliptical, flagsEquatorial);
                    pointsInTransit.Add(calcResult.Key, calcResult.Value);
                    break;
                }
                case CalculationCats.CommonElements:
                {
                    KeyValuePair<ChartPoints, ProgPositions> calcResult =
                        CreatePosForElementBasedPoint(celPoint, julianDayUt, obliquity, observerPos);
                    pointsInTransit.Add(calcResult.Key, calcResult.Value);
                    break;
                }
            }
        }
        return new ProgRealPointsResponse(pointsInTransit, 0);
    }

    private KeyValuePair<ChartPoints, ProgPositions> CreatePosForSePoint(ChartPoints celPoint, double julDay, 
        Location? location, int flagsEcl, int flagsEq)
    {
        PosSpeed[] eclipticPosSpeed = _celPointSeCalc.CalculateCelPoint((int)celPoint, julDay, location, flagsEcl);
        PosSpeed[] equatorialPosSpeed = _celPointSeCalc.CalculateCelPoint((int)celPoint, julDay, location, flagsEq);
        ProgPositions progPos = new(eclipticPosSpeed[0].Position, eclipticPosSpeed[1].Position,
            equatorialPosSpeed[0].Position, equatorialPosSpeed[1].Position);
        return new KeyValuePair<ChartPoints, ProgPositions>(celPoint, progPos);
    }
    
    private KeyValuePair<ChartPoints, ProgPositions> CreatePosForElementBasedPoint(ChartPoints celPoint, double julDay, 
        double obliquity, ObserverPositions observerPosition)
    {
        double[] eclipticPos = _celPointsElementsCalc.Calculate(celPoint, julDay, observerPosition);
        double[] equatorialPos = _coordinateConversionFacade.EclipticToEquatorial(new[]
        {
            eclipticPos[0], eclipticPos[1]
        }, obliquity);
        return new KeyValuePair<ChartPoints, ProgPositions>(celPoint,
            new ProgPositions(eclipticPos[0], eclipticPos[1], equatorialPos[0], equatorialPos[1]));
    }    
    
}