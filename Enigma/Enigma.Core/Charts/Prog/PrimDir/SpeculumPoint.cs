// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Calc;
using Enigma.Domain.Charts.Prog.PrimDir;
using Enigma.Domain.Dtos;
using Enigma.Domain.References;
using Serilog;

namespace Enigma.Core.Charts.Prog.PrimDir;

/// <summary>Point to be used in primary directions.</summary>
public interface ISpeculumPoint
{
    public SpeculumPointBase PointBase { get; }
}

/// <summary>Basic values for a speculum. All values that are relevant for each point.</summary>
public class SpeculumPointBase
{
    public bool IsSignificator { get; private set; }
    public bool IsPromissor { get; private set; }
    public bool ChartLeft { get; private set; }
    public bool ChartTop { get; private set; }
    public double Azimuth { get; private set; }
    public double Altitude { get; private set; }
    public double Lon { get; private set; }
    public double Lat { get; private set; }
    public double Ra { get; private set; }
    public double Decl { get; private set; }
    public AspectTypes aspect { get; private set; }


    /// <summary>Basic values for a point to be used in primary directions.
    /// Each point needs these elements but using different values.</summary>
    /// <param name="point">The point.</param>
    /// <param name="pointPos">All positions for the point.</param>
    /// <param name="request">The original request.</param>
    /// <param name="specBase">The base values for the speculum.</param>
    /// <param name="aspect">Type of aspect, either conjunction or opposition. If aspect != opposition, a conjuction is assumed.</param>
    public SpeculumPointBase(ChartPoints point, FullPointPos pointPos, PrimDirRequest request, SpeculumBase specBase, AspectTypes aspect)
    {
        IsSignificator = request.Significators.Contains(point);
        IsPromissor = request.Promissors.Contains(point);
        // Use ra positions for in mundo, lon positions for in zodiaco.
        ChartLeft = request.Approach == PrimDirApproaches.Mundane
            ? PrimDirCalcAssist.IsChartLeft(pointPos.Equatorial.MainPosSpeed.Position, specBase.RaMc)
            : PrimDirCalcAssist.IsChartLeft(pointPos.Ecliptical.MainPosSpeed.Position, specBase.LonMc);
        ChartTop = request.Approach == PrimDirApproaches.Mundane
            ? PrimDirCalcAssist.IsChartTop(pointPos.Equatorial.MainPosSpeed.Position, specBase.RaAsc)
            : PrimDirCalcAssist.IsChartTop(pointPos.Ecliptical.MainPosSpeed.Position, specBase.LonAsc);
        Azimuth = pointPos.Horizontal.MainPosSpeed.Position;
        Altitude = pointPos.Horizontal.DeviationPosSpeed.Position;
        Lon = pointPos.Ecliptical.MainPosSpeed.Position;
        Lat = pointPos.Ecliptical.DeviationPosSpeed.Position;
        Ra = pointPos.Equatorial.MainPosSpeed.Position;
        Decl = pointPos.Equatorial.DeviationPosSpeed.Position;
        if (request.Approach == PrimDirApproaches.Zodiacal)
        {
            double obl = request.Chart.Obliquity;
            Decl = PrimDirCalcAssist.DeclFromLongNoLat(Lon, obl);
            Ra = PrimDirCalcAssist.RightAscFromLongNoLat(Lon, obl);
        }
        
        
        if (aspect != AspectTypes.Opposition) return;
        // handle opposition
        ChartLeft = !ChartLeft;
        ChartTop = !ChartTop;
        Azimuth = RangeUtil.ValueToRange(Azimuth + 180.0, 0.0, 360.0);
        Altitude *= -1;
        Lon = RangeUtil.ValueToRange(Lon + 180.0, 0.0, 360.0);
        Lat *= -1;
        Ra = RangeUtil.ValueToRange(Ra + 180.0, 0.0, 360.0);
        Decl *= -1;
    }
}



/// <summary>A specific point to be used in Placidus directions.</summary>
public class SpeculumPointPlac : ISpeculumPoint
{
    public SpeculumPointBase PointBase { get; private set; }
    public double SemiArc { get; set; }
    public double MerDist { get; set; }
    public double HorDist { get; set; }
    public double Oad { get; set; }
    public double Ad { get; set; }
    public bool IsTop { get; set; }
    public bool ChartLeft { get; set; }
    
    public SpeculumPointPlac(ChartPoints point, FullPointPos pointPos, PrimDirRequest request, SpeculumBase specBase, AspectTypes aspect)
    {
        PointBase = new SpeculumPointBase(point, pointPos, request, specBase, aspect);
        double geoLat = request.Chart.InputtedChartData.Location.GeoLat;
        ChartLeft = PointBase.ChartLeft;
        IsTop = PointBase.ChartTop;
        Ad = PrimDirCalcAssist.AscensionalDifference(PointBase.Decl, geoLat);
        Oad = PrimDirCalcAssist.ObliqueAscDesc(PointBase.Ra, Ad, ChartLeft, geoLat >= 0.0);
        HorDist = PrimDirCalcAssist.HorizontalDistance(Oad, specBase.OaAsc, ChartLeft, geoLat >= 0.0);
        MerDist = PrimDirCalcAssist.MeridianDistance(PointBase.Ra, specBase.RaMc, specBase.RaIc, IsTop);
        //SemiArc = Math.Abs(HorDist + MerDist);
        SemiArc = 90.0 + Ad;
        if (!IsTop) SemiArc = 180.0 - SemiArc;
        
        
        Log.Information("Placidus SA for " + point);
        Log.Information("ChartLeft: " + ChartLeft);
        Log.Information("IsTop: " + IsTop);
        Log.Information("Ad: " + Ad);
        Log.Information("Oad: " + Oad);
        Log.Information("HorDist: " + HorDist);
        Log.Information("MerDist: " + MerDist);
        Log.Information("SemiArc: " + SemiArc);
        Log.Information("GeoLat: " + geoLat);

        
        
    }
}


/// <summary>A specific point to be used in Regiomontanus directions.</summary>
public class SpeculumPointReg : ISpeculumPoint
{
    public SpeculumPointBase PointBase { get; private set; }
    public double ZenithDist { get; private set; }
    
    public double PoleReg { get; private set; }
    public double FactorW { get; private set; }
    public double FactorQ { get; private set; }

    public SpeculumPointReg(ChartPoints point, FullPointPos pointPos, PrimDirRequest request, SpeculumBase specBase, AspectTypes aspect)
    {
        PointBase = new SpeculumPointBase(point, pointPos, request, specBase, aspect);
        bool isTop = request.Approach == PrimDirApproaches.Mundane
            ? PrimDirCalcAssist.IsChartTop(PointBase.Ra, specBase.RaAsc)
            : PrimDirCalcAssist.IsChartTop(PointBase.Lon, specBase.LonAsc);
        double geoLat = request.Chart.InputtedChartData.Location.GeoLat;
        double geoLatRad = MathExtra.DegToRad(geoLat);
        double merDist = PrimDirCalcAssist.MeridianDistance(PointBase.Ra, specBase.RaMc, specBase.RaIc, isTop);
        ZenithDist = PrimDirCalcAssist.ZenithDistReg(PointBase.Decl, merDist, geoLat, isTop);
        double zdRad = MathExtra.DegToRad(ZenithDist);
        PoleReg = MathExtra.RadToDeg(Math.Asin(Math.Sin(geoLatRad) * Math.Sin(zdRad)));
        double poleRad = MathExtra.DegToRad(PoleReg);
        double declRad = MathExtra.DegToRad(PointBase.Decl);
        FactorQ = MathExtra.RadToDeg(Math.Asin(Math.Tan(declRad) * Math.Tan(poleRad)));
        FactorW = PointBase.Ra - FactorQ;
        if (!PointBase.ChartLeft) FactorW = PointBase.Ra + FactorQ;
    }
}

