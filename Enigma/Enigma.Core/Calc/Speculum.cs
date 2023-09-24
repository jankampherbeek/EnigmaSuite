// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Interfaces;
using Enigma.Domain.Dtos;
using Enigma.Domain.References;

namespace Enigma.Core.Calc;


public sealed class Speculum: ISpeculum
{
    public PrimaryDirMethods PrimaryDirMethod { get; }

    public double GeoLat { get; }

    public double RaMc { get; }

    public double RaIc { get; }

    public double OblAscAscendant { get; }

    public double OblDescDescendant { get; }

    public List<ISpeculumItem> SpeculumItems { get; }

    public Speculum(PrimaryDirMethods primDirMethod, CalculatedChart calcChart, List<ChartPoints> promissors, List<ChartPoints> significators)
    {
        PrimaryDirMethod = primDirMethod;
        GeoLat = calcChart.InputtedChartData.Location.GeoLat;

        foreach (KeyValuePair<ChartPoints, FullPointPos> position 
                 in calcChart.Positions.Where(position => position.Key == ChartPoints.Mc))
        {
            RaMc = position.Value.Equatorial.MainPosSpeed.Position;
            RaIc = RangeUtil.ValueToRange(RaMc + 180.0, 0.0, 360.0);
            OblAscAscendant = RangeUtil.ValueToRange(RaMc + 90.0, 0.0, 360.0);
            OblDescDescendant = RangeUtil.ValueToRange(RaMc - 90.0, 0.0, 360.0);
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
    public double AscensionalDifference { get; }

    /// <summary>Oblique ascension of promissor.</summary>
    public double ObliqueAscensionPromissor { get; }

    /// <summary>Horizontal distance</summary>
    public double HorizontalDistance => Hd;
    /// <summary>Diurnal semi-arc</summary>
    public double DiurnalSemiArc => Dsa;
    /// <summary>Diurnal semi-arc</summary>
    public double NocturnalSemiArc => Nsa;


    protected readonly double RaPlanet;
    protected readonly double DeclPlanet;
    protected readonly double Mdmc;                       // meridian distance from mc
    protected readonly double Mdic;                       // meridian distance from ic
    protected readonly double GeoLat;
    protected readonly double Hd;                         // horizontal distance
    protected readonly double Dsa;                        // diurnal semi-arc
    protected readonly double Nsa;                        // nocturnal semi-arc
    protected readonly bool East;
    private readonly bool _north;

    protected SpeculumItem(double geoLat, double raMc, double raIc, double oaAsc, double odDesc, double raPlanet, double declPlanet)
    {
        RaPlanet = raPlanet;
        DeclPlanet = declPlanet;
        GeoLat = geoLat;
        Mdmc = RangeUtil.ValueToRange(RaPlanet - raMc, 0.0, 360.0);
        Mdic = RangeUtil.ValueToRange(RaPlanet - raIc, 0.0, 360.0);
        AscensionalDifference = MathExtra.AscensionalDifference(DeclPlanet, geoLat);
        _north = GeoLat >= 0.0;
        East = MathExtra.IsEasternHemiSphere(RaPlanet, raMc);
        ObliqueAscensionPromissor = MathExtra.ObliqueAscension(RaPlanet, AscensionalDifference, East, _north);
        if (East)
        {
            Hd = ObliqueAscensionPromissor - oaAsc;
        } else
        {
            Hd = RangeUtil.ValueToRange(ObliqueAscensionPromissor + 180.0, 0.0, 360.0) - odDesc;
        }
        Dsa = RangeUtil.ValueToRange(Math.Abs(Hd) + Math.Abs(Mdmc), 0.0, 360.0);
        Nsa = RangeUtil.ValueToRange(Math.Abs(Hd) + Math.Abs(Mdic), 0.0, 360.0);

    }
}

public class SpeculumItemPlacidus : SpeculumItem
{

    /// <summary>Proportional semi arc</summary>
    public double ProportionalSemiArc { get; }

    public SpeculumItemPlacidus(double geoLat, double raMc, double raIc, double oaAsc, double odDesc, double raPlanet, double declPlanet) :
        base(geoLat, raMc, raIc, oaAsc, odDesc, raPlanet, declPlanet)
    {
        ProportionalSemiArc = (Hd >= 0.0) ? Mdmc / Dsa : Mdic / Nsa;
    }

}

public class SpeculumItemRegiomontanus: SpeculumItem
{
    public double Pole { get; private set; }

    public double AdPole { get; private set; }

    public double OadPole { get; private set; }

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
        Pole = MathExtra.RadToDeg(poleRad);
        double adPoleRad = Math.Asin(Math.Tan(declPlRad) * Math.Tan(poleRad));
        AdPole = MathExtra.RadToDeg(adPoleRad);
        OadPole = East ? RaPlanet - AdPole : RaPlanet + AdPole;

        // moving point needs additional calculation
    }

}


