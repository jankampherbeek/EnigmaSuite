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

    public double OaAscendant { get; }

    public double OdDescendant { get; }

    public Dictionary<ChartPoints, SpeculumItem> SpeculumItems { get; }

    public Speculum(PrimaryDirMethods primDirMethod, CalculatedChart calcChart, List<ChartPoints> promissors, List<ChartPoints> significators)
    {
        // TODO support different methods, for now use SA mundane
        
        PrimaryDirMethod = primDirMethod;
        GeoLat = calcChart.InputtedChartData.Location.GeoLat;

        foreach (KeyValuePair<ChartPoints, FullPointPos> position 
                 in calcChart.Positions.Where(position => position.Key == ChartPoints.Mc))
        {
            RaMc = position.Value.Equatorial.MainPosSpeed.Position;
            RaIc = RangeUtil.ValueToRange(RaMc + 180.0, 0.0, 360.0);
            OaAscendant = RangeUtil.ValueToRange(RaMc + 90.0, 0.0, 360.0);
            OdDescendant = RangeUtil.ValueToRange(RaMc - 90.0, 0.0, 360.0);
        }

        Dictionary<ChartPoints, SpeculumItem> speculumItems = new();
        foreach (KeyValuePair<ChartPoints, FullPointPos> position in calcChart.Positions)
        {
            ChartPoints point = position.Key;
            SpeculumItem specItem = CreateSpeculumItemPlacidus(position.Value);
            speculumItems.Add(point, specItem);
        }

    }
    
    private SpeculumItem CreateSpeculumItemPlacidus(FullPointPos fullPointPos)
    {
        double ra = fullPointPos.Equatorial.MainPosSpeed.Position;
        double decl = fullPointPos.Equatorial.DeviationPosSpeed.Position;
        bool easternHemisphere = MathExtra.IsEasternHemiSphere(ra, RaMc);
        double mdmc = RangeUtil.ValueToRange(ra - RaMc, 0.0, 360.0);
        double mdic = RangeUtil.ValueToRange(ra - RaIc, 0.0, 360.0);
        double ad = MathExtra.AscensionalDifference(decl, GeoLat);
        bool northLatitude = GeoLat > 0.0;
        double oa = MathExtra.ObliqueAscension(ra, ad, easternHemisphere, northLatitude);
        double hd = easternHemisphere
            ? oa - OaAscendant
            : RangeUtil.ValueToRange(oa + 180.0, 0.0, 360.0) - OdDescendant;
        double dsa = RangeUtil.ValueToRange(Math.Abs(hd) + Math.Abs(mdmc), 0.0, 360.0);
        double nsa = RangeUtil.ValueToRange(Math.Abs(hd) + Math.Abs(mdic), 0.0, 360.0);
        double propSa = (hd >= 0.0) ? mdmc / dsa : mdic / nsa;

        return new SpeculumItem(ra, decl, mdmc, mdic, ad, oa, hd, dsa, nsa, propSa);
    }
}



/// <summary>Speculumitem with astronomical details for a specific point as used in SA directions.</summary>
/// <param name="Ra">Right ascension.</param>
/// <param name="Declination">Declination.</param>
/// <param name="MdMc">Meridian distance to Mc.</param>
/// <param name="MdIc">Meridian distance to Ic.</param>
/// <param name="Ad">Ascensional difference.</param>
/// <param name="Oa">Oblique ascension.</param>
/// <param name="Hd">Horizontal difference.</param>
/// <param name="Dsa">Diurnal semi-arc.</param>
/// <param name="Nsa">Nocturnal semi-arc.</param>
/// <param name="PropSa">Proportional semi-arc.</param>
public record SpeculumItem(double Ra, double Declination, double MdMc, double MdIc, double Ad, double Oa, double Hd,
    double Dsa, double Nsa, double PropSa);


/*
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
*/

