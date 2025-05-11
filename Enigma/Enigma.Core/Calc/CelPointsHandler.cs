// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Dtos;
using Enigma.Domain.References;
using Enigma.Domain.Requests;
using Enigma.Facades.Se;

namespace Enigma.Core.Calc;

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
    IObliqueLongitudeHandler obliqueLongitudeHandler,
    ICoordinateConversionCalc coordinateConversionCalc,
    IFullPointPosFactory fullPointPosFactory,
    IPeriodSupportChecker periodSupportChecker)
    : ICelPointsHandler
{
    private const double ZERO = 0.0;

    
    // TODO Refactor CalcCommonPoints
    public Dictionary<ChartPoints, FullPointPos> CalcCommonPoints(double jdUt, double obliquity, double ayanamshaOffset, 
        double armc, Location? location, CalculationPreferences prefs)
    {
        var allCelPoints = prefs.ActualChartPoints;
        var celPoints = allCelPoints.Where(point => periodSupportChecker.IsSupported(point, jdUt)).ToList();
        var observerPosition = prefs.ActualObserverPosition;
        var previousJd = jdUt - 0.5;
        var futureJd = jdUt + 0.5;

        if (prefs.ActualObserverPosition == ObserverPositions.TopoCentric)
        {
            if (location != null) SeInitializer.SetTopocentric(location.GeoLong, location.GeoLat, 0.0);
        }

        var flagsEcliptical = seFlags.DefineFlags(CoordinateSystems.Ecliptical, prefs.ActualObserverPosition, prefs.ActualZodiacType);
        var flagsEquatorial = seFlags.DefineFlags(CoordinateSystems.Equatorial, prefs.ActualObserverPosition, prefs.ActualZodiacType);
        var commonPoints = new Dictionary<ChartPoints, FullPointPos>();
        foreach (var celPoint in celPoints)
        {
            var calculationCat = celPoint.GetDetails().CalculationCat;
            if (celPoint == ChartPoints.ApogeeCorrected && prefs.ApogeeType == ApogeeTypes.Duval)
            {
                calculationCat = CalculationCats.CommonFormulaLongitude;
            }
            
            switch (calculationCat)
            {
                case CalculationCats.CommonSe:
                {
                    var actCelPoint = celPoint;
                    if (celPoint == ChartPoints.NorthNode && prefs.Oscillate) actCelPoint = ChartPoints.TrueNode;
                    KeyValuePair<ChartPoints, FullPointPos> fullPointPos =
                        CreatePosForSePoint(actCelPoint, jdUt, location, flagsEcliptical, flagsEquatorial);
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
                    var positions = CreatePosForElementBasedPoint(celPoint, jdUt, obliquity, observerPosition);
                    var previousPositions =
                        CreatePosForElementBasedPoint(celPoint, previousJd, obliquity, observerPosition);
                    var futurePositions =
                        CreatePosForElementBasedPoint(celPoint, futureJd, obliquity, observerPosition);
                    PosSpeed longPosSpeed = new(positions[0][0] - ayanamshaOffset,
                        futurePositions[0][0] - previousPositions[0][0]);
                    PosSpeed latPosSpeed = new(positions[0][1], futurePositions[0][1] - previousPositions[0][1]);
                    PosSpeed distPosSpeed = new(positions[0][2], futurePositions[0][2] - previousPositions[0][2]);
                    PosSpeed[] eclipticPosSpeeds = [longPosSpeed, latPosSpeed, distPosSpeed];
                    PosSpeed raPosSpeed = new(positions[1][0], futurePositions[1][0] - previousPositions[1][0]);
                    PosSpeed declPosSpeed = new(positions[1][1], futurePositions[1][1] - previousPositions[1][1]);
                    PosSpeed[] equatorialPosSpeeds = [raPosSpeed, declPosSpeed, distPosSpeed];
                    EquatorialCoordinates equCoordinates = new(positions[1][0], positions[1][1]);
                    HorizontalRequest horizontalRequest = new(jdUt, location, equCoordinates);
                    var horCoord = horizontalHandler.CalcHorizontal(horizontalRequest);

                    var fullPointPos =
                        fullPointPosFactory.CreateFullPointPos(eclipticPosSpeeds, equatorialPosSpeeds, horCoord);
                    commonPoints.Add(celPoint, fullPointPos);
                    break;
                }
                case CalculationCats.CommonFormulaLongitude:
                {
                    var longitude = celPointFormulaCalc.Calculate(celPoint, jdUt);
                    List<double> posSpeedValues = [longitude, ZERO, ZERO, ZERO, ZERO, ZERO];
                    List<double> emptyPosSpeedValues = [ZERO, ZERO, ZERO, ZERO, ZERO, ZERO];
                    var posSpeeds = new PointPosSpeeds(posSpeedValues);
                    var emptyPosSpeeds = new PointPosSpeeds(emptyPosSpeedValues);
                    var fpPos = new FullPointPos(posSpeeds, emptyPosSpeeds, emptyPosSpeeds);
                    commonPoints.Add(celPoint, fpPos);
                    break;
                }
                case CalculationCats.CommonFormulaFull:
                {
                    if (celPoint is ChartPoints.SouthNode)
                    {
                        var northNode = prefs.Oscillate ? ChartPoints.TrueNode : ChartPoints.NorthNode;
                        var nodePos = CreatePosForSePoint(northNode, jdUt, location, flagsEcliptical, flagsEquatorial);
                        var nodeDistancePos = nodePos.Value.Ecliptical.DistancePosSpeed.Position;
                        var nodeDistanceSpeed = nodePos.Value.Ecliptical.DistancePosSpeed.Speed;
                        var distPosSpeed = new PosSpeed(nodeDistancePos, nodeDistanceSpeed);
                        var nodeLongPos = nodePos.Value.Ecliptical.MainPosSpeed.Position + 180.0;
                        if (nodeLongPos >= 360.0) nodeLongPos -= 360.0;
                        var nodeLongSpeed = nodePos.Value.Ecliptical.MainPosSpeed.Speed;
                        var eclLongPosSpeed = new PosSpeed(nodeLongPos, nodeLongSpeed);
                        var eclLatPosSpeed = new PosSpeed(0.0, 0.0);
                        var eclPosSpeeds = new PointPosSpeeds(eclLongPosSpeed, eclLatPosSpeed, distPosSpeed);
                        
                        var nodeRaPos = nodePos.Value.Equatorial.MainPosSpeed.Position + 180.0;
                        if (nodeRaPos >= 360.0) nodeRaPos -= 360.0;
                        var nodeRaSpeed = nodePos.Value.Equatorial.MainPosSpeed.Speed;
                        var raPosSpeed = new PosSpeed(nodeRaPos, nodeRaSpeed);
                        var nodeDeclPos = nodePos.Value.Equatorial.DeviationPosSpeed.Position * -1.0;
                        var nodeDeclSpeed = nodePos.Value.Equatorial.DeviationPosSpeed.Speed;
                        var declPosSpeed = new PosSpeed(nodeDeclPos, nodeDeclSpeed);
                        var equPosSpeeds = new PointPosSpeeds(raPosSpeed, declPosSpeed, distPosSpeed);
                        
                        var nodeAzimuth = nodePos.Value.Horizontal.MainPosSpeed.Position + 180.0;
                        if (nodeAzimuth >= 360.0) nodeAzimuth -= 360.0;
                        var azimPosSpeed = new PosSpeed(nodeAzimuth, 0.0);
                        var nodeAltitude = nodePos.Value.Horizontal.DeviationPosSpeed.Position * -1.0;
                        var altPosSpeed = new PosSpeed(nodeAltitude, 0.0);
                        var horDistPosSpeed = new PosSpeed(0.0, 0.0);
                        var horPosSpeeds = new PointPosSpeeds(azimPosSpeed, altPosSpeed, horDistPosSpeed);

                        var southNodeFpPos = new FullPointPos(eclPosSpeeds, equPosSpeeds, horPosSpeeds);
                        commonPoints.Add(celPoint, southNodeFpPos);
                    }
                    
                    
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
                            var longitude = celPointFormulaCalc.Calculate(ChartPoints.ApogeeCorrected, jdUt);
                            List<double> posSpeedValues = [longitude, ZERO, ZERO, ZERO, ZERO, ZERO];
                            List<double> emptyPosSpeedValues = [ZERO, ZERO, ZERO, ZERO, ZERO, ZERO];
                            var posSpeeds = new PointPosSpeeds(posSpeedValues);
                            var emptyPosSpeeds = new PointPosSpeeds(emptyPosSpeedValues);
                            var fpPos = new FullPointPos(posSpeeds, emptyPosSpeeds, emptyPosSpeeds);
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
                        var node = prefs.Oscillate ? ChartPoints.TrueNode : ChartPoints.NorthNode;
                        
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
                        var eclipticPosSpeed = new[]
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
                        var equatorialPosSpeed = new[]
                        {
                            new PosSpeed(ra, 0.0),
                            new PosSpeed(decl, 0.0),
                            new PosSpeed(0.0, 0.0)
                        };
                        // Calculate horizontal coordinates
                        HorizontalRequest horizontalRequest = new(jdUt, location, eqCoord);
                        var horCoord = horizontalHandler.CalcHorizontal(horizontalRequest);
                        var fullPointPos =
                            fullPointPosFactory.CreateFullPointPos(eclipticPosSpeed, equatorialPosSpeed, horCoord);
                        commonPoints.Add(celPoint, fullPointPos);
                    }
                    break;
                }
            }
        }

        if (prefs.ActualProjectionType != ProjectionTypes.ObliqueLongitude) return commonPoints;
        var obliqueLongitudeRequest = CreateObliqueLongitudeRequest(commonPoints, armc, obliquity, location, ayanamshaOffset);
        var obliqueLongitudes = obliqueLongitudeHandler.CalcObliqueLongitude(obliqueLongitudeRequest);
        var obliqueLongitudePoints = CreateObliqueLongitudePoints(commonPoints, obliqueLongitudes);
        return obliqueLongitudePoints;
    }

    public FullPointPos CalcSinglePointWithSe(ChartPoints point, double jdUt, Location location, CalculationPreferences prefs)
    {
        if (prefs.ActualObserverPosition == ObserverPositions.TopoCentric)
        {
            SeInitializer.SetTopocentric(location.GeoLong, location.GeoLat, 0.0); 
        }
        var flagsEcliptical = seFlags.DefineFlags(CoordinateSystems.Ecliptical, prefs.ActualObserverPosition, prefs.ActualZodiacType);
        var flagsEquatorial = seFlags.DefineFlags(CoordinateSystems.Equatorial, prefs.ActualObserverPosition, prefs.ActualZodiacType);
        var fullPointPos = CreatePosForSePoint(point, jdUt, location, flagsEcliptical, flagsEquatorial);
        return fullPointPos.Value;
    }

    private static ObliqueLongitudeRequest CreateObliqueLongitudeRequest(Dictionary<ChartPoints, FullPointPos> calculatedPoints, double armc, double obliquity, Location? location, double ayanamshaOffset)
    {
        var coordinates = calculatedPoints.Select(calcPoint 
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
        var eclipticPosSpeed = positionCelPointSeCalc.CalculateCelPoint(seId, julDay, flagsEcl);
        var equatorialPosSpeed = positionCelPointSeCalc.CalculateCelPoint(seId, julDay, flagsEq);
        var equCoordinates = new EquatorialCoordinates(equatorialPosSpeed[0].Position, equatorialPosSpeed[1].Position);
        HorizontalRequest horizontalRequest = new(julDay, location, equCoordinates);
        var horCoord = horizontalHandler.CalcHorizontal(horizontalRequest);
        var fullPointPos = fullPointPosFactory.CreateFullPointPos(eclipticPosSpeed, equatorialPosSpeed, horCoord);
        return new KeyValuePair<ChartPoints, FullPointPos>(celPoint, fullPointPos);

    }

    private KeyValuePair<ChartPoints, FullPointPos> CreatePosForApside(ChartPoints celPoint, double julDay,
        ApsidesMethods method, Location? location, int flagsEcl,  int flagsEq)
    {
        var eclipticPosSpeed = apsideSeCalc.CalculateApside(celPoint, julDay, method, flagsEcl);
        var equatorialPosSpeed = apsideSeCalc.CalculateApside(celPoint, julDay, method, flagsEq);
        var equCoordinates = new EquatorialCoordinates(equatorialPosSpeed[0].Position, equatorialPosSpeed[1].Position);
        HorizontalRequest horizontalRequest = new(julDay, location, equCoordinates);
        var horCoord = horizontalHandler.CalcHorizontal(horizontalRequest);
        var fullPointPos = fullPointPosFactory.CreateFullPointPos(eclipticPosSpeed, equatorialPosSpeed, horCoord);
        return new KeyValuePair<ChartPoints, FullPointPos>(celPoint, fullPointPos);
    }
        
    

    private double[][] CreatePosForElementBasedPoint(ChartPoints celPoint, double julDay, double obliquity, ObserverPositions observerPosition)
    {
        var eclipticPos = posCelPointsElementsCalc.Calculate(celPoint, julDay, observerPosition);
        var equatorialPos = coordinateConversionFacade.EclipticToEquatorial(new[] { eclipticPos[0], eclipticPos[1] }, obliquity);
        return new[] { eclipticPos, equatorialPos };
    }
    
}
