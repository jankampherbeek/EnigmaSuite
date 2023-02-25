// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Core.Handlers.Interfaces;
using Enigma.Domain.Calc;
using Enigma.Domain.Calc.ChartItems;
using Enigma.Domain.Calc.ChartItems.Coordinates;
using Enigma.Domain.Calc.Specials;
using Enigma.Domain.Charts;
using Enigma.Domain.Points;
using Enigma.Facades.Interfaces;
using Enigma.Facades.Se;

namespace Enigma.Core.Handlers.Calc.CelestialPoints;


/// <inheritdoc/>
public sealed class CelPointsHandler : ICelPointsHandler
{
    private readonly ISeFlags _seFlags;
    private readonly ICelPointSECalc _celPointSECalc;
    private readonly ICelPointsElementsCalc _celPointElementsCalc;
    private readonly ICoTransFacade _coordinateConversionFacade;
    private readonly IObliquityHandler _obliquityHandler;
    private readonly IHorizontalHandler _horizontalHandler;
    private readonly IChartPointsMapping _chartPointsMapping;


    public CelPointsHandler(ISeFlags seFlags,
                               ICelPointSECalc positionCelPointSECalc,
                               ICelPointsElementsCalc posCelPointsElementsCalc,
                               ICoTransFacade coordinateConversionFacade,
                               IObliquityHandler obliquityHandler,
                               IHorizontalHandler horizontalHandler,
                               IChartPointsMapping chartPointsMapping)
    {
        _seFlags = seFlags;
        _celPointSECalc = positionCelPointSECalc;
        _celPointElementsCalc = posCelPointsElementsCalc;
        _coordinateConversionFacade = coordinateConversionFacade;
        _obliquityHandler = obliquityHandler;
        _horizontalHandler = horizontalHandler;
        _chartPointsMapping = chartPointsMapping;
    }


    public Dictionary<ChartPoints, FullPointPos> CalcCommonPoints(CelPointsRequest request)
    {
        double julDay = request.JulianDayUt;
        double previousJd = julDay - 0.5;
        double futureJd = julDay + 0.5;
        Location location = request.Location;
        var obliquityRequest = new ObliquityRequest(julDay, true);

        double obliquity = _obliquityHandler.CalcObliquity(obliquityRequest);
        if (request.CalculationPreferences.ActualZodiacType == ZodiacTypes.Sidereal)
        {
            int idAyanamsa = request.CalculationPreferences.ActualAyanamsha.GetDetails().SeId;
            SeInitializer.SetAyanamsha(idAyanamsa);
        }
        if (request.CalculationPreferences.ActualObserverPosition == ObserverPositions.TopoCentric)
        {
            SeInitializer.SetTopocentric(request.Location.GeoLong, request.Location.GeoLat, 0.0);  // TODO backlog optionally replace 0.0 with value for altitude above sealevel in meters. 
        }


        int flagsEcliptical = _seFlags.DefineFlags(CoordinateSystems.Ecliptical, request.CalculationPreferences.ActualObserverPosition, request.CalculationPreferences.ActualZodiacType);
        int flagsEquatorial = _seFlags.DefineFlags(CoordinateSystems.Equatorial, request.CalculationPreferences.ActualObserverPosition, request.CalculationPreferences.ActualZodiacType);
        var commonPoints = new Dictionary<ChartPoints, FullPointPos>();
        foreach (ChartPoints celPoint in request.CalculationPreferences.ActualChartPoints)
        {
            CalculationCats calculationCat = _chartPointsMapping.CalculationTypeForPoint(celPoint);
            if (calculationCat == CalculationCats.CommonSE)
            {
                KeyValuePair<ChartPoints, FullPointPos> fullPointPos = CreatePosForSePoint(celPoint, julDay, location, flagsEcliptical, flagsEquatorial);
                commonPoints.Add(fullPointPos.Key, fullPointPos.Value);
            }
            else if (calculationCat == CalculationCats.CommonElements)
            {
                double[][] positions = CreatePosForElementBasedPoint(celPoint, julDay, obliquity);
                double[][] previousPositions = CreatePosForElementBasedPoint(celPoint, previousJd, obliquity);
                double[][] futurePositions = CreatePosForElementBasedPoint(celPoint, futureJd, obliquity);
                PosSpeed longPosSpeed = new(positions[0][0], futurePositions[0][0] - previousPositions[0][0]);
                PosSpeed latPosSpeed = new(positions[0][1], futurePositions[0][1] - previousPositions[0][1]);
                PosSpeed distPosSpeed = new(positions[0][2], futurePositions[0][2] - previousPositions[0][2]);
                PosSpeed raPosSpeed = new(positions[1][0], futurePositions[1][0] - previousPositions[1][0]);
                PosSpeed declPosSpeed = new(positions[1][1], futurePositions[1][1] - previousPositions[1][1]);
                EclipticCoordinates eclCoordinates = new(positions[0][0], positions[0][1]);
                HorizontalRequest horizontalRequest = new(julDay, request.Location, eclCoordinates);
                HorizontalCoordinates horCoord = _horizontalHandler.CalcHorizontal(horizontalRequest);
                PosSpeed aziPosSpeed = new(horCoord.Azimuth, 0.0);
                PosSpeed altPosSpeed = new(horCoord.Altitude, 0.0);
                PointPosSpeeds ppsEcliptical = new(longPosSpeed, latPosSpeed, distPosSpeed);
                PointPosSpeeds ppsEquatorial = new(raPosSpeed, declPosSpeed, distPosSpeed);
                PointPosSpeeds ppsHorizontal = new(aziPosSpeed, altPosSpeed, distPosSpeed);
                FullPointPos fullPointPos = new(ppsEcliptical, ppsEquatorial, ppsHorizontal);
                commonPoints.Add(celPoint, fullPointPos);
            } 
            // TODO add branch for numeric calculations
            else
            {
               // throw new EnigmaException("Unrecognized calculationtype for chartpoint : " + celPoint);
            }
        }

        return commonPoints;
    }


    private KeyValuePair<ChartPoints,  FullPointPos> CreatePosForSePoint(ChartPoints celPoint, double julDay, Location location, int flagsEcl, int flagsEq)
    {
        PosSpeed[] eclipticPosSpeed = _celPointSECalc.CalculateCelPoint(celPoint, julDay, location, flagsEcl);
        PosSpeed longPosSpeed = eclipticPosSpeed[0];
        PosSpeed latPosSpeed = eclipticPosSpeed[1];
        PosSpeed distPosSpeed = eclipticPosSpeed[2];
        PosSpeed[] equatorialPosSpeed = _celPointSECalc.CalculateCelPoint(celPoint, julDay, location, flagsEq);
        PosSpeed raPosSpeed = equatorialPosSpeed[0];
        PosSpeed declPosSpeed = equatorialPosSpeed[1];
        var eclCoordinates = new EclipticCoordinates(eclipticPosSpeed[0].Position, eclipticPosSpeed[1].Position);
        HorizontalRequest horizontalRequest = new(julDay, location, eclCoordinates);
        HorizontalCoordinates horCoord = _horizontalHandler.CalcHorizontal(horizontalRequest);

        PosSpeed aziPosSpeed = new(horCoord.Azimuth, 0.0);
        PosSpeed altPosSpeed = new(horCoord.Altitude, 0.0);
        PointPosSpeeds ppsEcliptical = new(longPosSpeed, latPosSpeed, distPosSpeed);
        PointPosSpeeds ppsEquatorial = new(raPosSpeed, declPosSpeed, distPosSpeed);
        PointPosSpeeds ppsHorizontal = new(aziPosSpeed, altPosSpeed, distPosSpeed);
        FullPointPos fullPointPos = new(ppsEcliptical, ppsEquatorial, ppsHorizontal);

        return new KeyValuePair<ChartPoints, FullPointPos>(celPoint, fullPointPos);

    }


    private double[][] CreatePosForElementBasedPoint(ChartPoints celPoint, double julDay, double obliquity)
    {
        double[] eclipticPos = _celPointElementsCalc.Calculate(celPoint, julDay);
        double[] equatorialPos = _coordinateConversionFacade.EclipticToEquatorial(new double[] { eclipticPos[0], eclipticPos[1] }, obliquity);
        return new double[][] { eclipticPos, equatorialPos };
    }
}
