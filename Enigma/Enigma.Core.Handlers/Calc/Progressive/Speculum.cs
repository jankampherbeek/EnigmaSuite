// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Handlers.Calc.Util;
using Enigma.Core.Handlers.Interfaces;
using Enigma.Domain.Calc.Progressive;
using Enigma.Domain.Charts;
using Enigma.Domain.Points;

namespace Enigma.Core.Handlers.Calc.Progressive;


public sealed class Speculum: ISpeculum
{
    public PrimarySystems PrimarySystem { get; }

    public double GeoLat { get; }

    public double RaMc { get; }

    public double RaIc { get; }

    public double OblAscAscendant { get; }

    public double OblDescDescendant { get; }

    public List<ISpeculumItem> SpeculumItems { get; }

    public Speculum(PrimarySystems primSys, CalculatedChart calcChart, List<ChartPoints> promissors, List<ChartPoints> significators)
    {
        PrimarySystem = primSys;
        GeoLat = calcChart.InputtedChartData.Location.GeoLat;

        foreach (var position in calcChart.Positions)
        {
            if (position.Key != ChartPoints.Mc) continue;
            RaMc = position.Value.Equatorial.MainPosSpeed.Position;
            RaIc = RangeUtil.ValueToRange(RaMc + 180.0, 0.0, 360.0);
            OblAscAscendant = RangeUtil.ValueToRange(RaMc + 90.0, 0.0, 360.0);
            OblDescDescendant = RangeUtil.ValueToRange(RaMc - 90.0, 0.0, 360.0);

            // handle promissors/significators
        }

    }
}



/// <summary>Speculum for Placidean directions, using the semi-arc.</summary>
public abstract class SpeculumItem : ISpeculumItem
{
    /// <inheritdoc/>
    public double RightAscension => RaPlanet;
    /// <inheritdoc/>
    public double Declination => DeclPlanet;
    /// <inheritdoc/>
    public double MeridianDistanceMc => Mdmc;
    /// <inheritdoc/>
    public double MeridianDistanceIc => Mdic;
    /// <inheritdoc/>
    public double AscensionalDifference => Ad;
    /// <summary>Oblique ascension of promissor.</summary>
    public double ObliqueAscensionPromissor => Oadpl;
    /// <summary>Horizontal distance</summary>
    public double HorizontalDistance => Hd;
    /// <summary>Diurnal semi-arc</summary>
    public double DiurnalSemiArc => Dsa;
    /// <summary>Diurnal semi-arc</summary>
    public double NocturnalSemiArc => Nsa;


    protected double RaPlanet;
    protected double DeclPlanet;
    protected double Mdmc;                       // meridian distance from mc
    protected double Mdic;                       // meridian distance from ic
    protected double Ad;                         // ascensional difference
    protected double Oadpl;                      // oblique ascension or descension planet
    protected double GeoLat;
    protected double Hd;                         // horizontal distance
    protected double Dsa;                        // diurnal semi-arc
    protected double Nsa;                        // nocturnal semi-arc
    protected bool East;
    protected bool North;

    public SpeculumItem(double geoLat, double raMc, double raIc, double oaAsc, double odDesc, double raPlanet, double declPlanet)
    {
        RaPlanet = raPlanet;
        DeclPlanet = declPlanet;
        GeoLat = geoLat;
        Mdmc = RangeUtil.ValueToRange(RaPlanet - raMc, 0.0, 360.0);
        Mdic = RangeUtil.ValueToRange(RaPlanet - raIc, 0.0, 360.0);
        Ad = MathExtra.AscensionalDifference(DeclPlanet, geoLat);
        North = GeoLat >= 0.0;
        East = MathExtra.IsEasternHemiSphere(RaPlanet, raMc);
        Oadpl = MathExtra.ObliqueAscension(RaPlanet, Ad, East, North);
        if (East)
        {
            Hd = Oadpl - oaAsc;
        } else
        {
            Hd = RangeUtil.ValueToRange(Oadpl + 180.0, 0.0, 360.0) - odDesc;
        }
        Dsa = RangeUtil.ValueToRange(Math.Abs(Hd) + Math.Abs(Mdmc), 0.0, 360.0);
        Nsa = RangeUtil.ValueToRange(Math.Abs(Hd) + Math.Abs(Mdic), 0.0, 360.0);

    }
}

public class SpeculumItemPlacidus : SpeculumItem
{

    /// <summary>Proportional semi arc</summary>
    public double ProportionalSemiArc => _psa;

    private double _psa;

    public SpeculumItemPlacidus(double geoLat, double raMc, double raIc, double oaAsc, double odDesc, double raPlanet, double declPlanet) :
        base(geoLat, raMc, raIc, oaAsc, odDesc, raPlanet, declPlanet)
    {
        _psa = (Hd >= 0.0) ? Mdmc / Dsa : Mdic / Nsa;
    }

}

public class SpeculumItemRegiomontanus: SpeculumItem
{
    public double Pole => _pole;
    public double AdPole => _adPole;
    public double OadPole => _oadPole;

    private double _pole;
    private double _adPole;
    private double _oadPole;

    public SpeculumItemRegiomontanus(double geoLat, double raMc, double raIc, double oaAsc, double odDesc, double raPlanet, double declPlanet) :
        base(geoLat, raMc, raIc, oaAsc, odDesc, raPlanet, declPlanet)
    {
        Calculate();
    }

    private void Calculate()
    {
        double geoLatRad = MathExtra.DegToRad(GeoLat);
        double declPlRad = MathExtra.DegToRad(DeclPlanet);
        double mdMcRad = MathExtra.DegToRad(Mdmc);
        double xRad = Math.Atan(Math.Tan(declPlRad) / Math.Cos(mdMcRad));
        double yRad = geoLatRad - xRad;
        double zRad = Math.Atan(Math.Cos(yRad) / (Math.Tan(mdMcRad) * Math.Cos(xRad)));
        double poleRad = Math.Asin(Math.Sin(geoLatRad) * Math.Cos(zRad));
        _pole = MathExtra.RadToDeg(poleRad);
        double adPoleRad = Math.Asin(Math.Tan(declPlRad) * Math.Tan(poleRad));
        _adPole = MathExtra.RadToDeg(adPoleRad);
        _oadPole = East ? RaPlanet - _adPole : RaPlanet + _adPole;

        // moving point needs additional calculation
    }

}


