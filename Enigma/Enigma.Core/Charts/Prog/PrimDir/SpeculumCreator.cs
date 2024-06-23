// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Calc;
using Enigma.Domain.Charts.Prog.PrimDir;
using Enigma.Domain.Dtos;
using Enigma.Domain.References;

namespace Enigma.Core.Charts.Prog.PrimDir;

/// <summary>Creator for speculums for primary directions.</summary>
public interface ISpeculumCreator
{
    
    public PlacidusSpeculum CreatePlacidusSpeculum(PrimDirRequest request);
}


// ============================ Implementation =============================================================

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
        SpeculumBaseOld specBaseOld = CreateBase(request);
        Dictionary<ChartPoints, PlacidusPointDetails> detailLines = new();
        foreach (var pointPos in request.Chart.Positions)
        {
            bool significator = request.Significators.Contains(pointPos.Key);
            bool promissor = request.Promissors.Contains(pointPos.Key);
            if (significator || promissor)
            {
                FullPointPos fullPointPos = request.Chart.Positions[pointPos.Key];
    //            detailLines.Add(pointPos.Key, CreatePlacidusPointDetails(pointPos.Key, request, significator, promissor));
                detailLines.Add(pointPos.Key, CreatePlacidusPointDetails(fullPointPos, specBaseOld, significator));
            }
        }
        return new PlacidusSpeculum(specBaseOld, detailLines);
    }


    private SpeculumBaseOld CreateBase(PrimDirRequest request)
    {
        CalculatedChart chart = request.Chart;
        double jdRadix = request.Chart.InputtedChartData.FullDateTime.JulianDayForEt;
        double geoLat = chart.InputtedChartData.Location.GeoLat;
        double raMc = chart.Positions[ChartPoints.Mc].Equatorial.MainPosSpeed.Position;
        double raIc = raMc <= HALF_CIRCLE ? raMc + HALF_CIRCLE : raMc - HALF_CIRCLE;
        double obl = chart.Obliquity;
        double lonMc = chart.Positions[ChartPoints.Mc].Ecliptical.MainPosSpeed.Position;
        double lonAsc = chart.Positions[ChartPoints.Ascendant].Ecliptical.MainPosSpeed.Position;
        double oaAsc = raMc + 90.0;
        if (oaAsc >= 360.0) oaAsc -= 360.0;
        bool inMundo = request.Approach == PrimDirApproaches.Mundane;
        return new SpeculumBaseOld(inMundo, geoLat, jdRadix, raMc, raIc, obl, lonMc, lonAsc, oaAsc);
    }

    private PlacidusPointDetails CreatePlacidusPointDetails(
        FullPointPos fullPointPos, 
        SpeculumBaseOld specBaseOld, 
        bool significator)
    {
        double lonPoint = fullPointPos.Ecliptical.MainPosSpeed.Position;
        double latPoint = fullPointPos.Ecliptical.DeviationPosSpeed.Position;
        EclipticCoordinates eclCoord = new EclipticCoordinates(lonPoint, 0.0);
        double declZod = _coordinateConversionCalc.PerformConversion(eclCoord, specBaseOld.Obl).Declination;
        double declMun = fullPointPos.Equatorial.DeviationPosSpeed.Position;
        double declPoint = specBaseOld.InMundo ? declMun : declZod;
        double raPoint = fullPointPos.Equatorial.MainPosSpeed.Position;
        double aD = PrimDirCalcAssist.AscensionalDifference(declPoint, specBaseOld.GeoLat);
        
        bool chartL = specBaseOld.InMundo ? PrimDirCalcAssist.IsChartLeft(raPoint, specBaseOld.RaMc) : 
            PrimDirCalcAssist.IsChartLeft(lonPoint, specBaseOld.LonMc);

        bool hemN = specBaseOld.GeoLat > 0.0;
        if (!specBaseOld.InMundo)
        {
            double pointToCheck = lonPoint;
            double angle = specBaseOld.LonAsc - pointToCheck;
            if (angle < -180.0) pointToCheck -= 360.0;
            if (angle > 180.0) pointToCheck += 360.0;
            hemN = angle is > 0 and <= 180.0;
        }
        double oA = PrimDirCalcAssist.ObliqueAscdesc(raPoint, aD, chartL, hemN);
        double horDist = PrimDirCalcAssist.HorizontalDistance(oA, specBaseOld.OaAsc, chartL);
        double mD = raPoint - specBaseOld.RaMc;
        if (!hemN) mD = raPoint - specBaseOld.RaIc;
        double sA = Math.Abs(horDist) + Math.Abs(mD);
        
        double adPolePc = (mD / sA) * aD;
        double elevPolePc = PrimDirCalcAssist.ElevationOfThePolePlac(adPolePc, declPoint);
        
        double propD = mD / sA;               // only to be used for significator 
        return new PlacidusPointDetails(significator, !significator, chartL, 
            hemN, lonPoint, latPoint, raPoint, declPoint,  aD, oA, 
            horDist, mD, sA, adPolePc, elevPolePc, propD);
    }
    
    
    
    
    
}