﻿// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Core.Calc;
using Enigma.Domain.Dtos;
using Enigma.Domain.References;
using Enigma.Domain.Requests;
using Enigma.Facades.Se;

namespace Enigma.Core.Handlers;

/// <summary>
/// Handler for the calculation of one or more celestial points.
/// </summary>
public interface ICelPointsHandler
{
    public Dictionary<ChartPoints, FullPointPos> CalcCommonPoints(double jdUt, double obliquity, double ayanamshaOffset, 
        double armc, Location? location, CalculationPreferences prefs);

    /// <summary>Calculate a single point, can only be used for SE calculations.</summary>
    /// <param name="point">The chart point.</param>
    /// <param name="jdUt">Julian Day.</param>
    /// <param name="location">Location, only relevant for topocentric positions.</param>
    /// <param name="prefs">Calculation preferences.</param>
    /// <returns>Full point position for the given chart point.</returns>
    public FullPointPos CalcSinglePointWithSe(ChartPoints point, double jdUt, Location location, CalculationPreferences prefs);
}

// ===================================== Implementation ============================================


/// <inheritdoc/>
public sealed class CelPointsHandler : ICelPointsHandler
{
    private const double ZERO = 0.0;
    private readonly ISeFlags _seFlags;
    private readonly ICelPointSeCalc _celPointSeCalc;
    private readonly ICelPointsElementsCalc _celPointElementsCalc;
    private readonly ICelPointFormulaCalc _celPointFormulaCalc;
    private readonly ICoTransFacade _coordinateConversionFacade;
    private readonly IHorizontalHandler _horizontalHandler;
    private readonly IChartPointsMapping _chartPointsMapping;
    private readonly IObliqueLongitudeHandler _obliqueLongitudeHandler;
    private readonly IFullPointPosFactory _fullPointPosFactory;
    private readonly IPeriodSupportChecker _periodSupportChecker;


    public CelPointsHandler(ISeFlags seFlags,
                               ICelPointSeCalc positionCelPointSeCalc,
                               ICelPointsElementsCalc posCelPointsElementsCalc,
                               ICelPointFormulaCalc celPointFormulaCalc,
                               ICoTransFacade coordinateConversionFacade,
                               IHorizontalHandler horizontalHandler,
                               IChartPointsMapping chartPointsMapping,
                               IObliqueLongitudeHandler obliqueLongitudeHandler,
                               IFullPointPosFactory fullPointPosFactory,
                               IPeriodSupportChecker periodSupportChecker)
    {
        _seFlags = seFlags;
        _celPointSeCalc = positionCelPointSeCalc;
        _celPointElementsCalc = posCelPointsElementsCalc;
        _celPointFormulaCalc = celPointFormulaCalc;
        _coordinateConversionFacade = coordinateConversionFacade;
        _horizontalHandler = horizontalHandler;
        _chartPointsMapping = chartPointsMapping;
        _obliqueLongitudeHandler = obliqueLongitudeHandler;
        _fullPointPosFactory = fullPointPosFactory;
        _periodSupportChecker = periodSupportChecker;
    }


    public Dictionary<ChartPoints, FullPointPos> CalcCommonPoints(double jdUt, double obliquity, double ayanamshaOffset, 
        double armc, Location? location, CalculationPreferences prefs)
    {
        List<ChartPoints> allCelPoints = prefs.ActualChartPoints;
        List<ChartPoints> celPoints = allCelPoints.Where(point => _periodSupportChecker.IsSupported(point, jdUt)).ToList();
        ObserverPositions observerPosition = prefs.ActualObserverPosition;
        double previousJd = jdUt - 0.5;
        double futureJd = jdUt + 0.5;

        if (prefs.ActualObserverPosition == ObserverPositions.TopoCentric)
        {
            SeInitializer.SetTopocentric(location.GeoLong, location.GeoLat, 0.0); 
        }

        int flagsEcliptical = _seFlags.DefineFlags(CoordinateSystems.Ecliptical, prefs.ActualObserverPosition, prefs.ActualZodiacType);
        int flagsEquatorial = _seFlags.DefineFlags(CoordinateSystems.Equatorial, prefs.ActualObserverPosition, prefs.ActualZodiacType);
        var commonPoints = new Dictionary<ChartPoints, FullPointPos>();
        foreach (ChartPoints celPoint in celPoints)   // only handle calculations using CalculationCats.CommonSE and CommonElements
        {
            CalculationCats calculationCat = _chartPointsMapping.CalculationTypeForPoint(celPoint);
            switch (calculationCat)
            {
                case CalculationCats.CommonSe:
                {
                    KeyValuePair<ChartPoints, FullPointPos> fullPointPos = CreatePosForSePoint(celPoint, jdUt, location, flagsEcliptical, flagsEquatorial);
                    commonPoints.Add(fullPointPos.Key, fullPointPos.Value);
                    break;
                }
                case CalculationCats.CommonElements:
                {
                    double[][] positions = CreatePosForElementBasedPoint(celPoint, jdUt, obliquity, observerPosition);
                    double[][] previousPositions = CreatePosForElementBasedPoint(celPoint, previousJd, obliquity, observerPosition);
                    double[][] futurePositions = CreatePosForElementBasedPoint(celPoint, futureJd, obliquity, observerPosition);
                    PosSpeed longPosSpeed = new(positions[0][0] - ayanamshaOffset, futurePositions[0][0] - previousPositions[0][0]);
                    PosSpeed latPosSpeed = new(positions[0][1], futurePositions[0][1] - previousPositions[0][1]);
                    PosSpeed distPosSpeed = new(positions[0][2], futurePositions[0][2] - previousPositions[0][2]);
                    PosSpeed[] eclipticPosSpeeds = { longPosSpeed, latPosSpeed, distPosSpeed };
                    PosSpeed raPosSpeed = new(positions[1][0], futurePositions[1][0] - previousPositions[1][0]);
                    PosSpeed declPosSpeed = new(positions[1][1], futurePositions[1][1] - previousPositions[1][1]);
                    PosSpeed[] equatorialPosSpeeds = { raPosSpeed, declPosSpeed, distPosSpeed };
                    EquatorialCoordinates equCoordinates = new(positions[1][0], positions[1][1]);
                    HorizontalRequest horizontalRequest = new(jdUt, location, equCoordinates);
                    HorizontalCoordinates horCoord = _horizontalHandler.CalcHorizontal(horizontalRequest);

                    FullPointPos fullPointPos = _fullPointPosFactory.CreateFullPointPos(eclipticPosSpeeds, equatorialPosSpeeds, horCoord);
                    commonPoints.Add(celPoint, fullPointPos);
                    break;
                }
                case CalculationCats.CommonFormula:
                {
                    double longitude = CreateLongitudeForFormulaPoint(celPoint, jdUt);
                    List<double> posSpeedValues = new() { longitude, ZERO, ZERO, ZERO, ZERO, ZERO };
                    List<double> emptyPosSpeedValues = new() { ZERO, ZERO, ZERO, ZERO, ZERO, ZERO };
                    PointPosSpeeds posSpeeds = new PointPosSpeeds(posSpeedValues);
                    PointPosSpeeds emptyPosSpeeds = new PointPosSpeeds(emptyPosSpeedValues);
                    FullPointPos fpPos = new FullPointPos(posSpeeds, emptyPosSpeeds, emptyPosSpeeds);
                    commonPoints.Add(celPoint, fpPos);
                    break;
                }
            }
        }

        if (prefs.ActualProjectionType != ProjectionTypes.ObliqueLongitude) return commonPoints;
        ObliqueLongitudeRequest obliqueLongitudeRequest = CreateObliqueLongitudeRequest(commonPoints, armc, obliquity, location, ayanamshaOffset);
        List<NamedEclipticLongitude> obliqueLongitudes = _obliqueLongitudeHandler.CalcObliqueLongitude(obliqueLongitudeRequest);
        Dictionary<ChartPoints, FullPointPos> obliqueLongitudePoints = CreateObliqueLongitudePoints(commonPoints, obliqueLongitudes);
        return obliqueLongitudePoints;
    }

    public FullPointPos CalcSinglePointWithSe(ChartPoints point, double jdUt, Location location, CalculationPreferences prefs)
    {
        ObserverPositions observerPosition = prefs.ActualObserverPosition;
        if (prefs.ActualObserverPosition == ObserverPositions.TopoCentric)
        {
            SeInitializer.SetTopocentric(location.GeoLong, location.GeoLat, 0.0); 
        }
        int flagsEcliptical = _seFlags.DefineFlags(CoordinateSystems.Ecliptical, prefs.ActualObserverPosition, prefs.ActualZodiacType);
        int flagsEquatorial = _seFlags.DefineFlags(CoordinateSystems.Equatorial, prefs.ActualObserverPosition, prefs.ActualZodiacType);
        KeyValuePair<ChartPoints, FullPointPos> fullPointPos = CreatePosForSePoint(point, jdUt, location, flagsEcliptical, flagsEquatorial);
        return fullPointPos.Value;
    }

    private static ObliqueLongitudeRequest CreateObliqueLongitudeRequest(Dictionary<ChartPoints, FullPointPos> calculatedPoints, double armc, double obliquity, Location? location, double ayanamshaOffset)
    {
        List<NamedEclipticCoordinates> coordinates = calculatedPoints.Select(calcPoint 
            => new NamedEclipticCoordinates(calcPoint.Key, 
                new EclipticCoordinates(calcPoint.Value.Ecliptical.MainPosSpeed.Position, 
                    calcPoint.Value.Ecliptical.DeviationPosSpeed.Position))).ToList();
        return new ObliqueLongitudeRequest(armc, obliquity, location.GeoLat, coordinates, ayanamshaOffset);
    }

    private static Dictionary<ChartPoints, FullPointPos> CreateObliqueLongitudePoints(Dictionary<ChartPoints, 
        FullPointPos> commonPoints, IReadOnlyCollection<NamedEclipticLongitude> obliqueLongitudes)
    {
        Dictionary<ChartPoints, FullPointPos> obliqueLongitudePoints = new();

        foreach (var fullPos in commonPoints)
        {
            foreach (FullPointPos positionValues in 
                     from oblLong in obliqueLongitudes 
                     where fullPos.Key == oblLong.CelPoint 
                     select fullPos.Value.Ecliptical.MainPosSpeed with { Position = oblLong.EclipticLongitude } 
                     into oblEclPosSpeed 
                     select new PointPosSpeeds(oblEclPosSpeed, fullPos.Value.Ecliptical.DeviationPosSpeed, 
                         fullPos.Value.Ecliptical.DistancePosSpeed) 
                     into eclPointPosSpeeds 
                     select fullPos.Value with { Ecliptical = eclPointPosSpeeds })
            {
                obliqueLongitudePoints.Add(fullPos.Key, positionValues);
            }
        }
        return obliqueLongitudePoints;
    }


    private KeyValuePair<ChartPoints, FullPointPos> CreatePosForSePoint(ChartPoints celPoint, double julDay, Location? location, int flagsEcl, int flagsEq)
    {
        PosSpeed[] eclipticPosSpeed = _celPointSeCalc.CalculateCelPoint(celPoint, julDay, location, flagsEcl);
        PosSpeed[] equatorialPosSpeed = _celPointSeCalc.CalculateCelPoint(celPoint, julDay, location, flagsEq);
        var equCoordinates = new EquatorialCoordinates(equatorialPosSpeed[0].Position, equatorialPosSpeed[1].Position);
        HorizontalRequest horizontalRequest = new(julDay, location, equCoordinates);
        HorizontalCoordinates horCoord = _horizontalHandler.CalcHorizontal(horizontalRequest);
        FullPointPos fullPointPos = _fullPointPosFactory.CreateFullPointPos(eclipticPosSpeed, equatorialPosSpeed, horCoord);
        return new KeyValuePair<ChartPoints, FullPointPos>(celPoint, fullPointPos);

    }


    private double[][] CreatePosForElementBasedPoint(ChartPoints celPoint, double julDay, double obliquity, ObserverPositions observerPosition)
    {
        double[] eclipticPos = _celPointElementsCalc.Calculate(celPoint, julDay, observerPosition);
        double[] equatorialPos = _coordinateConversionFacade.EclipticToEquatorial(new[] { eclipticPos[0], eclipticPos[1] }, obliquity);
        return new[] { eclipticPos, equatorialPos };
    }
    
    private double CreateLongitudeForFormulaPoint(ChartPoints celPoint, double julDay)
    {
        return _celPointFormulaCalc.Calculate(celPoint, julDay);

    }
}
