// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Calc;
using Enigma.Domain.Charts.Prog.PrimDir;
using Enigma.Domain.Dtos;
using Enigma.Domain.References;

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
        HorDist = PrimDirCalcAssist.HorizontalDistance(Oad, specBase.OaAsc, ChartLeft);
        MerDist = PrimDirCalcAssist.MeridianDistance(PointBase.Ra, specBase.RaMc, specBase.RaIc, IsTop);
        SemiArc = Math.Abs(HorDist + MerDist);
    }
}

/// <summary>A specific point to be used in Placidus under the Pole directions.</summary>
public class SpeculumPointPlacPole : ISpeculumPoint
{
    public SpeculumPointBase PointBase { get; private set; }

    public double AdPlacPole { get; private set; }
    public double ElevPole { get; private set; }


    public SpeculumPointPlacPole(ChartPoints point, FullPointPos pointPos, PrimDirRequest request,
        SpeculumBase specBase, AspectTypes aspect)
    {
        PointBase = new SpeculumPointBase(point, pointPos, request, specBase, aspect);
        //       PointSaBase = new SpeculumPointSaBase(point, pointPos, request, specBase);

        double decl = pointPos.Equatorial.DeviationPosSpeed.Position;
        double ra = pointPos.Equatorial.MainPosSpeed.Position;
        double lon = pointPos.Ecliptical.MainPosSpeed.Position;
        double geoLat = request.Chart.InputtedChartData.Location.GeoLat;
        bool chartLeft = request.Approach == PrimDirApproaches.Mundane
            ? PrimDirCalcAssist.IsChartLeft(ra, specBase.RaMc)
            : PrimDirCalcAssist.IsChartLeft(lon, specBase.LonMc);
        bool isTop = request.Approach == PrimDirApproaches.Mundane
            ? PrimDirCalcAssist.IsChartTop(ra, specBase.RaAsc)
            : PrimDirCalcAssist.IsChartTop(lon, specBase.LonAsc);
        double ad = PrimDirCalcAssist.AscensionalDifference(decl, geoLat);
        double oad = PrimDirCalcAssist.ObliqueAscDesc(ra, ad, chartLeft, geoLat >= 0.0);
        double horDist = Math.Abs(PrimDirCalcAssist.HorizontalDistance(oad, specBase.OaAsc, chartLeft));

        double merDist = PrimDirCalcAssist.MeridianDistance(ra, specBase.RaMc, specBase.RaIc, isTop);
        double semiArc = Math.Abs(horDist) + Math.Abs(merDist);

        AdPlacPole = (merDist / semiArc) * ad;  // todo reverse signs for oadUnderPole in southern hemisphere
        ElevPole = PrimDirCalcAssist.ElevationOfThePolePlac(AdPlacPole, decl);
    }
}



/// <summary>A specific point to be used in Regiomontanus directions.</summary>
public class SpeculumPointReg : ISpeculumPoint
{
    public SpeculumPointBase PointBase { get; private set; }
    public double PoleReg { get; private set; }
    public double AdPoleReg { get; set; }
    public double OadPoleReg { get; set; }

    public SpeculumPointReg(ChartPoints point, FullPointPos pointPos, PrimDirRequest request, SpeculumBase specBase, AspectTypes aspect)
    {
        PointBase = new SpeculumPointBase(point, pointPos, request, specBase, aspect);
        
        
        double declRad = MathExtra.DegToRad(PointBase.Decl);
        bool isTop = request.Approach == PrimDirApproaches.Mundane
            ? PrimDirCalcAssist.IsChartTop(PointBase.Ra, specBase.RaAsc)
            : PrimDirCalcAssist.IsChartTop(PointBase.Lon, specBase.LonAsc);
        
        //double signMdUpperRad = MathExtra.DegToRad(signPoint.PointSaBase.MerDist);
        double merDist = PrimDirCalcAssist.MeridianDistance(PointBase.Ra, specBase.RaMc, specBase.RaIc, isTop);
        double PoleReg = PrimDirCalcAssist.PoleRegiomontanus(PointBase.Decl, merDist, specBase.GeoLat);

        double AdPoleReg = PrimDirCalcAssist.AdUnderRegPole(PoleReg, PointBase.Decl);

        double OadPoleReg = PointBase.Ra - AdPoleReg;
    }
}


/// <summary>A specific point to be used in Topocentric directions.</summary>
public class SpeculumPointTopoc : ISpeculumPoint
{
    public SpeculumPointBase PointBase { get; private set; }
    public double PoleTc { get; private set; }
   
    public SpeculumPointTopoc(ChartPoints point, FullPointPos pointPos, PrimDirRequest request, SpeculumBase specBase, AspectTypes aspect)
    {
        PointBase = new SpeculumPointBase(point, pointPos, request, specBase, aspect);
        double geoLat = request.Chart.InputtedChartData.Location.GeoLat;
        double decl = pointPos.Equatorial.DeviationPosSpeed.Position;
        bool isTop = request.Approach == PrimDirApproaches.Mundane
            ? PrimDirCalcAssist.IsChartTop(PointBase.Ra, specBase.RaAsc)
            : PrimDirCalcAssist.IsChartTop(PointBase.Lon, specBase.LonAsc);
        bool chartLeft = request.Approach == PrimDirApproaches.Mundane
            ? PrimDirCalcAssist.IsChartLeft(pointPos.Equatorial.MainPosSpeed.Position, specBase.RaMc)
            : PrimDirCalcAssist.IsChartLeft(pointPos.Ecliptical.MainPosSpeed.Position, specBase.LonMc); 
        double ad = PrimDirCalcAssist.AscensionalDifference(decl, geoLat);
        double oad = PrimDirCalcAssist.ObliqueAscDesc(pointPos.Equatorial.MainPosSpeed.Position, ad, chartLeft, geoLat >= 0.0);        
        double horDist = Math.Abs(PrimDirCalcAssist.HorizontalDistance(oad, specBase.OaAsc, chartLeft));        
        double merDist = PrimDirCalcAssist.MeridianDistance(PointBase.Ra, specBase.RaMc, specBase.RaIc, isTop);
        double semiArc = Math.Abs(horDist) + Math.Abs(merDist);
        PoleTc = PrimDirCalcAssist.TopocPole(merDist, semiArc, decl, geoLat);
        
       
    }
}