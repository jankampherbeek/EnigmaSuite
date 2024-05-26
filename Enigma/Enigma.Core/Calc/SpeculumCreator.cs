// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Dtos;
using Enigma.Domain.References;
using Enigma.Domain.Requests;

namespace Enigma.Core.Calc;

public interface ISpeculumCreator
{
    public PlacidusSpeculum CreatePlacidusSpeculum(PrimDirRequest request);
}


public class SpeculumCreator: ISpeculumCreator
{
    private ICoordinateConversionCalc _coordinateConversionCalc;
    
    private const double HALF_CIRCLE = 180.0;

    public SpeculumCreator(ICoordinateConversionCalc coordinateConversionCalc)
    {
        _coordinateConversionCalc = coordinateConversionCalc;
    }
    
    
    public PlacidusSpeculum CreatePlacidusSpeculum(PrimDirRequest request)
    {
        SpeculumBase specBase = CreateBase(request);
        List<PlacidusPointDetails> placidusPointDetails = new();

        foreach (var pointPos in request.Chart.Positions)
        {
            bool significator = request.Significators.Contains(pointPos.Key);
            bool promissor = request.Promissors.Contains(pointPos.Key);
            if (significator || promissor)
            {

                placidusPointDetails.Add(CreatePlacidusPointDetails(pointPos.Key, request, significator, promissor));
            }
        }
        
        
        
        return null;
    }


    private SpeculumBase CreateBase(PrimDirRequest request)
    {
        CalculatedChart chart = request.Chart;
        double geoLat = chart.InputtedChartData.Location.GeoLat;
        double raMc = chart.Positions[ChartPoints.Mc].Equatorial.MainPosSpeed.Position;
        double raIc = raMc <= HALF_CIRCLE ? raMc + HALF_CIRCLE : raMc - HALF_CIRCLE;
        double obliquity = chart.Obliquity;
        double mc = chart.Positions[ChartPoints.Mc].Ecliptical.MainPosSpeed.Position;
        double ascendant = chart.Positions[ChartPoints.Ascendant].Ecliptical.MainPosSpeed.Position;
        bool inMundo = request.InMundo;
        return new SpeculumBase(inMundo, geoLat, raMc, raIc, obliquity, mc, ascendant);
    }

    private PlacidusPointDetails CreatePlacidusPointDetails(
            ChartPoints point, 
            PrimDirRequest request, 
            bool significator, 
            bool promissor)
    {
        double LongMc = request.Chart.Positions[ChartPoints.Mc].Ecliptical.MainPosSpeed.Position;
        double raMc = request.Chart.Positions[ChartPoints.Mc].Equatorial.MainPosSpeed.Position;
        double raIc = RangeUtil.ValueToRange(raMc - 180.0, 0.0, 360.0);
        double oblAscAscendant = raMc - 90.0;
        if (oblAscAscendant < 0.0) oblAscAscendant += 360.0;
        double lonAsc = request.Chart.Positions[ChartPoints.Ascendant].Ecliptical.MainPosSpeed.Position;
        double obliquity = request.Chart.Obliquity;
        double geoLat = request.Chart.InputtedChartData.Location.GeoLat;
        bool inMundo = request.InMundo;
        FullPointPos fullPointPos = request.Chart.Positions[point];
        double lonPoint = fullPointPos.Ecliptical.MainPosSpeed.Position;
        double latPoint = fullPointPos.Ecliptical.DeviationPosSpeed.Position;
        EclipticCoordinates eclCoord = new EclipticCoordinates(lonPoint, 0.0);
        double declInZodiaco = _coordinateConversionCalc.PerformConversion(eclCoord, obliquity).Declination;
        double declInMundo = fullPointPos.Equatorial.DeviationPosSpeed.Position;
        double declPoint = inMundo ? declInMundo : declInZodiaco;
        double raPoint = fullPointPos.Equatorial.MainPosSpeed.Position;
        double ascDiff = MathExtra.AscensionalDifference(declPoint, geoLat);
        bool easternHemisphere = MathExtra.IsEasternHemiSphere(raPoint, raMc);
        // todo create separate function in MathExtra
        bool northernHemisphere = fullPointPos.Horizontal.DeviationPosSpeed.Position >= 0.0;   // altitude
        if (!inMundo)
        {
            double pointToCheck = lonPoint;
            double angle = lonAsc - pointToCheck;
            if (angle < -180.0) pointToCheck -= 360.0;
            if (angle > 180.0) pointToCheck += 360.0;
            northernHemisphere = angle > 0 && angle <= 180.0;
        }
        double oblAsc = MathExtra.ObliqueAscension(raPoint, ascDiff, easternHemisphere, northernHemisphere);
        double horDist = MathExtra.HorizontalDistance(oblAsc, oblAscAscendant, easternHemisphere);
        double merDist = raPoint - raMc;
        if (!northernHemisphere) merDist = raPoint - raIc;
        double semiArc = horDist + Math.Abs(merDist);           // check : horDist als ABS?
        double propD = merDist / semiArc;               // only ton be used for significator 
        return new PlacidusPointDetails(significator, promissor, easternHemisphere, northernHemisphere, lonPoint, latPoint, declPoint, ascDiff, oblAsc, horDist, merDist, semiArc, propD);
    }
    
    
}