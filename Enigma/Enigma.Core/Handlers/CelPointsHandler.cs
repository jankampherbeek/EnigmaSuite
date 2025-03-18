// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023, 2024, 2025.
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
public sealed class CelPointsHandler(
    ISeFlags seFlags,
    ICelPointSeCalc positionCelPointSeCalc,
    ICelPointsElementsCalc posCelPointsElementsCalc,
    IApsideSeCalc apsideSeCalc,
    IInclinationCalc inclinationCalc,
    ICelPointFormulaCalc celPointFormulaCalc,
    ICoTransFacade coordinateConversionFacade,
    IHorizontalHandler horizontalHandler,
    IChartPointsMapping chartPointsMapping,
    IObliqueLongitudeHandler obliqueLongitudeHandler,
    ICoordinateConversionCalc coordinateConversionCalc,
    IFullPointPosFactory fullPointPosFactory,
    IPeriodSupportChecker periodSupportChecker)
    : ICelPointsHandler
{
    private const double ZERO = 0.0;

    public Dictionary<ChartPoints, FullPointPos> CalcCommonPoints(double jdUt, double obliquity, double ayanamshaOffset, 
        double armc, Location? location, CalculationPreferences prefs)
    {
        List<ChartPoints> allCelPoints = prefs.ActualChartPoints;
        List<ChartPoints> celPoints = allCelPoints.Where(point => periodSupportChecker.IsSupported(point, jdUt)).ToList();
        ObserverPositions observerPosition = prefs.ActualObserverPosition;
        double previousJd = jdUt - 0.5;
        double futureJd = jdUt + 0.5;

        if (prefs.ActualObserverPosition == ObserverPositions.TopoCentric)
        {
            SeInitializer.SetTopocentric(location.GeoLong, location.GeoLat, 0.0); 
        }

        int flagsEcliptical = seFlags.DefineFlags(CoordinateSystems.Ecliptical, prefs.ActualObserverPosition, prefs.ActualZodiacType);
        int flagsEquatorial = seFlags.DefineFlags(CoordinateSystems.Equatorial, prefs.ActualObserverPosition, prefs.ActualZodiacType);
        var commonPoints = new Dictionary<ChartPoints, FullPointPos>();
        foreach (ChartPoints celPoint in celPoints)
        {
            CalculationCats calculationCat = celPoint.GetDetails().CalculationCat;
            if (celPoint == ChartPoints.ApogeeCorrected && prefs.ApogeeType == ApogeeTypes.Duval)
            {
                calculationCat = CalculationCats.CommonFormulaLongitude;
            }
            
            switch (calculationCat)
            {
                case CalculationCats.CommonSe:
                {
                    KeyValuePair<ChartPoints, FullPointPos> fullPointPos =
                        CreatePosForSePoint(celPoint, jdUt, location, flagsEcliptical, flagsEquatorial);
                    commonPoints.Add(fullPointPos.Key, fullPointPos.Value);
                    break;
                }
                case CalculationCats.Apsides:
                {
                    // Currently only supports Black Sun and Diamond, so no oscillation is used. 
                    var method = ApsidesMethods.mean;
                    KeyValuePair<ChartPoints, FullPointPos> fullPointPos = CreatePosForApside(celPoint, jdUt, method,
                        location, flagsEcliptical, flagsEquatorial);
                    commonPoints.Add(fullPointPos.Key, fullPointPos.Value);
                    break;
                }
                case CalculationCats.CommonElements:
                {
                    double[][] positions = CreatePosForElementBasedPoint(celPoint, jdUt, obliquity, observerPosition);
                    double[][] previousPositions =
                        CreatePosForElementBasedPoint(celPoint, previousJd, obliquity, observerPosition);
                    double[][] futurePositions =
                        CreatePosForElementBasedPoint(celPoint, futureJd, obliquity, observerPosition);
                    PosSpeed longPosSpeed = new(positions[0][0] - ayanamshaOffset,
                        futurePositions[0][0] - previousPositions[0][0]);
                    PosSpeed latPosSpeed = new(positions[0][1], futurePositions[0][1] - previousPositions[0][1]);
                    PosSpeed distPosSpeed = new(positions[0][2], futurePositions[0][2] - previousPositions[0][2]);
                    PosSpeed[] eclipticPosSpeeds = { longPosSpeed, latPosSpeed, distPosSpeed };
                    PosSpeed raPosSpeed = new(positions[1][0], futurePositions[1][0] - previousPositions[1][0]);
                    PosSpeed declPosSpeed = new(positions[1][1], futurePositions[1][1] - previousPositions[1][1]);
                    PosSpeed[] equatorialPosSpeeds = { raPosSpeed, declPosSpeed, distPosSpeed };
                    EquatorialCoordinates equCoordinates = new(positions[1][0], positions[1][1]);
                    HorizontalRequest horizontalRequest = new(jdUt, location, equCoordinates);
                    HorizontalCoordinates horCoord = horizontalHandler.CalcHorizontal(horizontalRequest);

                    FullPointPos fullPointPos =
                        fullPointPosFactory.CreateFullPointPos(eclipticPosSpeeds, equatorialPosSpeeds, horCoord);
                    commonPoints.Add(celPoint, fullPointPos);
                    break;
                }
                case CalculationCats.CommonFormulaLongitude:
                {
                    double longitude = celPointFormulaCalc.Calculate(celPoint, jdUt);
                    List<double> posSpeedValues = new() { longitude, ZERO, ZERO, ZERO, ZERO, ZERO };
                    List<double> emptyPosSpeedValues = new() { ZERO, ZERO, ZERO, ZERO, ZERO, ZERO };
                    PointPosSpeeds posSpeeds = new PointPosSpeeds(posSpeedValues);
                    PointPosSpeeds emptyPosSpeeds = new PointPosSpeeds(emptyPosSpeedValues);
                    FullPointPos fpPos = new FullPointPos(posSpeeds, emptyPosSpeeds, emptyPosSpeeds);
                    commonPoints.Add(celPoint, fpPos);
                    break;
                }
                case CalculationCats.CommonFormulaFull:
                {
                    if (celPoint is ChartPoints.Priapus or ChartPoints.PriapusCorrected)
                    {
                        var apogee = prefs.ApogeeType switch
                        {
                            ApogeeTypes.Corrected => ChartPoints.ApogeeCorrected,
                            ApogeeTypes.Duval => ChartPoints.ApogeeCorrected,
                            ApogeeTypes.Interpolated => ChartPoints.ApogeeInterpolated,
                            _ => ChartPoints.ApogeeMean
                        };
                        if (celPoint == ChartPoints.Priapus) apogee = ChartPoints.ApogeeMean;
                        var fullPointPosApogee =
                            CreatePosForSePoint(apogee, jdUt, location, flagsEcliptical, flagsEquatorial);
                        if (apogee == ChartPoints.ApogeeCorrected && prefs.ApogeeType == ApogeeTypes.Duval)
                        {
                            double longitude = celPointFormulaCalc.Calculate(ChartPoints.ApogeeCorrected, jdUt);
                            List<double> posSpeedValues = new() { longitude, ZERO, ZERO, ZERO, ZERO, ZERO };
                            List<double> emptyPosSpeedValues = new() { ZERO, ZERO, ZERO, ZERO, ZERO, ZERO };
                            PointPosSpeeds posSpeeds = new PointPosSpeeds(posSpeedValues);
                            PointPosSpeeds emptyPosSpeeds = new PointPosSpeeds(emptyPosSpeedValues);
                            FullPointPos fpPos = new FullPointPos(posSpeeds, emptyPosSpeeds, emptyPosSpeeds);
                            fullPointPosApogee = new KeyValuePair<ChartPoints, FullPointPos>(ChartPoints.ApogeeCorrected, fpPos);                       
                        }
                        var eclLong = fullPointPosApogee.Value.Ecliptical.MainPosSpeed.Position + 180.0;
                        if (eclLong >= 360.0) eclLong -= 360.0;
                        var eclipticPositions = new List<double>
                        {
                            eclLong,
                            fullPointPosApogee.Value.Ecliptical.MainPosSpeed.Speed,
                            fullPointPosApogee.Value.Ecliptical.DeviationPosSpeed.Position * -1.0,
                            fullPointPosApogee.Value.Ecliptical.DeviationPosSpeed.Speed,
                            0.0,
                            0.0
                        };
                        var ra = fullPointPosApogee.Value.Equatorial.MainPosSpeed.Position + 180.0;
                        if (ra >= 360.0) ra -= 360.0;
                        var equatorialPositions = new List<double>
                        {
                            ra,
                            fullPointPosApogee.Value.Equatorial.MainPosSpeed.Speed,
                            fullPointPosApogee.Value.Equatorial.DeviationPosSpeed.Position * -1.0,
                            fullPointPosApogee.Value.Equatorial.DeviationPosSpeed.Speed,
                            0.0,
                            0.0
                        };
                        var azimuth = fullPointPosApogee.Value.Horizontal.MainPosSpeed.Position + 180.0;
                        if (azimuth >= 360.0) azimuth -= 360.0;
                        var horizontalPositions = new List<double>
                        {
                            azimuth,
                            0.0,
                            fullPointPosApogee.Value.Horizontal.DeviationPosSpeed.Position * -1.0,
                            0.0,
                            0.0,
                            0.0
                        };
                        var priaPusFullPos =
                            new FullPointPos(new PointPosSpeeds(eclipticPositions),
                                new PointPosSpeeds(equatorialPositions),
                                new PointPosSpeeds(horizontalPositions));
                        commonPoints.Add(celPoint, priaPusFullPos);
                    }

                    if (celPoint is ChartPoints.Dragon or ChartPoints.Beast)
                    {
                        var node = prefs.Oscillate ? ChartPoints.TrueNode : ChartPoints.MeanNode;
                        var fullPointPosNode =
                            CreatePosForSePoint(node, jdUt, location, flagsEcliptical, flagsEquatorial);
                        var eclLongNode = fullPointPosNode.Value.Ecliptical.MainPosSpeed.Position;
                        var inclination =
                            inclinationCalc.CalcInclination(ChartPoints.Moon, jdUt,
                                flagsEcliptical); // TODO check if there is a difference with flagsEquatorial
                        var deltaNode = celPoint is ChartPoints.Dragon ? 90.0 : -90.0;
                        var latitude = celPoint is ChartPoints.Dragon ? inclination : inclination * -1.0;
                        var longitude = eclLongNode + deltaNode;
                        if (longitude >= 360.0) longitude -= 360.0;
                        if (longitude < 0.0) longitude += 360.0;
                        var eclipticPosSpeed = new PosSpeed[]
                        {
                            new PosSpeed(longitude, 0.0),
                            new PosSpeed(latitude, 0.0),
                            new PosSpeed(0.0, 0.0)
                        };
                        // Calculate equatorial coordinates
                        var eqCoord = coordinateConversionCalc.PerformConversion(
                            new EclipticCoordinates(longitude, latitude), obliquity);
                        var ra = eqCoord.RightAscension;
                        var decl = eqCoord.Declination;
                        var equatorialPosSpeed = new PosSpeed[]
                        {
                            new PosSpeed(ra, 0.0),
                            new PosSpeed(decl, 0.0),
                            new PosSpeed(0.0, 0.0)
                        };
                        // Calculate horizontal coordinates
                        HorizontalRequest horizontalRequest = new(jdUt, location, eqCoord);
                        HorizontalCoordinates horCoord = horizontalHandler.CalcHorizontal(horizontalRequest);
                        FullPointPos fullPointPos =
                            fullPointPosFactory.CreateFullPointPos(eclipticPosSpeed, equatorialPosSpeed, horCoord);
                        commonPoints.Add(celPoint, fullPointPos);
                    }
                    break;
                }
            }
        }

        if (prefs.ActualProjectionType != ProjectionTypes.ObliqueLongitude) return commonPoints;
        ObliqueLongitudeRequest obliqueLongitudeRequest = CreateObliqueLongitudeRequest(commonPoints, armc, obliquity, location, ayanamshaOffset);
        List<NamedEclipticLongitude> obliqueLongitudes = obliqueLongitudeHandler.CalcObliqueLongitude(obliqueLongitudeRequest);
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
        int flagsEcliptical = seFlags.DefineFlags(CoordinateSystems.Ecliptical, prefs.ActualObserverPosition, prefs.ActualZodiacType);
        int flagsEquatorial = seFlags.DefineFlags(CoordinateSystems.Equatorial, prefs.ActualObserverPosition, prefs.ActualZodiacType);
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
        var seId = celPoint.GetDetails().CalcId;
        PosSpeed[] eclipticPosSpeed = positionCelPointSeCalc.CalculateCelPoint(seId, julDay, location, flagsEcl);
        PosSpeed[] equatorialPosSpeed = positionCelPointSeCalc.CalculateCelPoint(seId, julDay, location, flagsEq);
        var equCoordinates = new EquatorialCoordinates(equatorialPosSpeed[0].Position, equatorialPosSpeed[1].Position);
        HorizontalRequest horizontalRequest = new(julDay, location, equCoordinates);
        HorizontalCoordinates horCoord = horizontalHandler.CalcHorizontal(horizontalRequest);
        FullPointPos fullPointPos = fullPointPosFactory.CreateFullPointPos(eclipticPosSpeed, equatorialPosSpeed, horCoord);
        return new KeyValuePair<ChartPoints, FullPointPos>(celPoint, fullPointPos);

    }

    private KeyValuePair<ChartPoints, FullPointPos> CreatePosForApside(ChartPoints celPoint, double julDay,
        ApsidesMethods method, Location? location, int flagsEcl,  int flagsEq)
    {
        PosSpeed[] eclipticPosSpeed = apsideSeCalc.CalculateApside(celPoint, julDay, method, flagsEcl);
        PosSpeed[] equatorialPosSpeed = apsideSeCalc.CalculateApside(celPoint, julDay, method, flagsEq);
        var equCoordinates = new EquatorialCoordinates(equatorialPosSpeed[0].Position, equatorialPosSpeed[1].Position);
        HorizontalRequest horizontalRequest = new(julDay, location, equCoordinates);
        HorizontalCoordinates horCoord = horizontalHandler.CalcHorizontal(horizontalRequest);
        FullPointPos fullPointPos = fullPointPosFactory.CreateFullPointPos(eclipticPosSpeed, equatorialPosSpeed, horCoord);
        return new KeyValuePair<ChartPoints, FullPointPos>(celPoint, fullPointPos);
    }
        
    

    private double[][] CreatePosForElementBasedPoint(ChartPoints celPoint, double julDay, double obliquity, ObserverPositions observerPosition)
    {
        double[] eclipticPos = posCelPointsElementsCalc.Calculate(celPoint, julDay, observerPosition);
        double[] equatorialPos = coordinateConversionFacade.EclipticToEquatorial(new[] { eclipticPos[0], eclipticPos[1] }, obliquity);
        return new[] { eclipticPos, equatorialPos };
    }
    
    private double CreateLongitudeForFormulaPoint(ChartPoints celPoint, double julDay)
    {
        return celPointFormulaCalc.Calculate(celPoint, julDay);

    }
}
