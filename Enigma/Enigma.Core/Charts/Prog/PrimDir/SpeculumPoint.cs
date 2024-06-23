// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Charts.Prog.PrimDir;
using Enigma.Domain.Dtos;
using Enigma.Domain.References;

namespace Enigma.Core.Charts.Prog.PrimDir;

/// <summary>Point to be used in primary directions.</summary>
public interface ISpeculumPoint
{
    public SpeculumPointBase PointBase{ get;  }
}

/// <summary>Basic values for a speculum. All values that are relevant for each point.</summary>
public class SpeculumPointBase
{
    public bool IsSignificator { get; private set; }
    public bool IsPromissor { get; private set; }
    public bool ChartLeft { get; private set; }
    public double Lon { get; private set; }
    public double Lat { get; private set; }
    public double Ra { get; private set; }
    public double Decl { get; private set; }
    
    
    /// <summary>Basic values for a point to be used in primary directions.
    /// Each point needs these elements but using different values.</summary>
    /// <param name="point">The point.</param>
    /// <param name="pointPos">All positions for the point.</param>
    /// <param name="request">The original request.</param>
    /// <param name="specBase">The base values for the speculum.</param>
    public SpeculumPointBase(ChartPoints point, FullPointPos pointPos, PrimDirRequest request, SpeculumBase specBase)
    {
        IsSignificator = request.Significators.Contains(point);
        IsPromissor = request.Promissors.Contains(point);
        // Use ra positions for in mundo, lon positions for in zodiaco.
        ChartLeft = request.Approach == PrimDirApproaches.Mundane 
            ? PrimDirCalcAssist.IsChartLeft(pointPos.Equatorial.MainPosSpeed.Position, specBase.RaMc) 
            : PrimDirCalcAssist.IsChartLeft(pointPos.Ecliptical.MainPosSpeed.Position, specBase.LonMc);
        Lon = pointPos.Ecliptical.MainPosSpeed.Position;
        Lat = pointPos.Ecliptical.DeviationPosSpeed.Position;
        Ra = pointPos.Equatorial.MainPosSpeed.Position;
        Decl = pointPos.Equatorial.DeviationPosSpeed.Position;
    }
    
}

/// <summary>Additional basic values for points in SemiArc based primary directions.</summary>
public class SpeculumPointSaBase
{
    public double Ad { get; private set; }
    public double Oad { get; private set; }
    public double MdU { get; private set; }
    public double MdL { get; private set; }
    public double HorDist { get; private set; }
    public double SaD { get; private set; }
    public double SaN { get; private set; }

    public SpeculumPointSaBase(ChartPoints point, FullPointPos pointPos, PrimDirRequest request, SpeculumBase specBase)
    {
        double decl = pointPos.Equatorial.DeviationPosSpeed.Position;
        double ra = pointPos.Equatorial.MainPosSpeed.Position;
        double lon = pointPos.Ecliptical.MainPosSpeed.Position;
        double geoLat = request.Chart.InputtedChartData.Location.GeoLat;
        bool chartLeft = request.Approach == PrimDirApproaches.Mundane
            ? PrimDirCalcAssist.IsChartLeft(ra, specBase.RaMc)
            : PrimDirCalcAssist.IsChartLeft(lon, specBase.LonMc);
        Oad = PrimDirCalcAssist.ObliqueAscdesc(ra, Ad, chartLeft, geoLat >= 0.0);
        MdU = ra - specBase.RaMc;
        MdL = ra - specBase.RaIc;
        SaD = Math.Abs(HorDist) + Math.Abs(MdU);
        SaN = Math.Abs(HorDist) + Math.Abs(MdL);
        Ad = PrimDirCalcAssist.AscensionalDifference(decl, geoLat);
        HorDist = PrimDirCalcAssist.HorizontalDistance(Oad, specBase.OaAsc, chartLeft);        
    }
}


/// <summary>A specific point to be used in Placidus directions.</summary>
public class SpeculumPointPlac : ISpeculumPoint
{
    public SpeculumPointBase PointBase { get; private set; }
    public SpeculumPointSaBase PointSaBase { get; private set; }

    public SpeculumPointPlac(ChartPoints point, FullPointPos pointPos, PrimDirRequest request, SpeculumBase specBase)
    {
        PointSaBase = new SpeculumPointSaBase(point, pointPos, request, specBase);
    }
}


/// <summary>A specific point to be used in Placidus under the Pole directions.</summary>
public class SpeculumPointPlacPole: ISpeculumPoint
{
    public SpeculumPointBase PointBase { get; private set; }
    public SpeculumPointSaBase PointSaBase { get; private set; }
    
    public double AdPlacPole { get; private set; }
    public double ElevPole { get; private set; }

    public SpeculumPointPlacPole(ChartPoints point, FullPointPos pointPos, PrimDirRequest request,
        SpeculumBase specBase)
    {
        PointSaBase = new SpeculumPointSaBase(point, pointPos, request, specBase);
        double decl = pointPos.Equatorial.DeviationPosSpeed.Position;
        AdPlacPole = PointSaBase.HorDist >= 0.0 
            ? (PointSaBase.MdU / PointSaBase.SaD) * PointSaBase.Ad 
            : (PointSaBase.MdL / PointSaBase.SaN) * PointSaBase.Ad;
        ElevPole = PrimDirCalcAssist.ElevationOfThePolePlac(AdPlacPole, decl);
    }
}


/// <summary>A specific point to be used in Topocentric directions.</summary>
public class SpeculumPointTopoc : ISpeculumPoint
{
    public SpeculumPointBase PointBase { get; private set; }
    public SpeculumPointSaBase PointSaBase { get; private set; }
    public double Mdo { get; private set; }

    public SpeculumPointTopoc(ChartPoints point, FullPointPos pointPos, PrimDirRequest request, SpeculumBase specBase)
    {
        PointSaBase = new SpeculumPointSaBase(point, pointPos, request, specBase);
        double decl = pointPos.Equatorial.DeviationPosSpeed.Position;
        Mdo = PointSaBase.HorDist >= 0.0
            ? (PointSaBase.MdU / PointSaBase.SaD) * 90.0
            : (PointSaBase.MdL / PointSaBase.SaN);
    }
}


/// <summary>A specific point to be used in Regiomontanus directions.</summary>
public class SpeculumPointReg: ISpeculumPoint
{
    public SpeculumPointBase PointBase { get; private set; }
    public double PoleReg { get; private set; }
    public double AdPoleReg { get; private set; }
    
    public SpeculumPointReg(ChartPoints point, FullPointPos pointPos, PrimDirRequest request, SpeculumBase specBase)
    {
        double decl = pointPos.Equatorial.DeviationPosSpeed.Position;
        double mdUpper = pointPos.Equatorial.MainPosSpeed.Position - specBase.RaMc;
        PointBase = new SpeculumPointBase(point, pointPos, request, specBase);
        PoleReg = PrimDirCalcAssist.PoleRegiomontanus(decl, mdUpper, specBase.GeoLat);
        AdPoleReg = PrimDirCalcAssist.AdUnderRegPole(PoleReg, decl);
    }
}