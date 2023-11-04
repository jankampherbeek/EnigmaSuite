// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Interfaces;
using Enigma.Domain.Dtos;
using Enigma.Domain.References;

namespace Enigma.Core.Calc;

/// <inheritdoc/>
public sealed class SpeculumCreator: ISpeculumCreator
{

    /// <inheritdoc/>
    public Speculum CreateSpeculum(PrimaryDirMethods primDirMethod, CalculatedChart calcChart, 
        List<ChartPoints> promissors, List<ChartPoints> significators)
    {
        double geoLat = calcChart.InputtedChartData.Location.GeoLat;
        calcChart.Positions.TryGetValue(ChartPoints.Mc, out FullPointPos posMc);  
        double raMc = posMc.Equatorial.MainPosSpeed.Position;
        double raIc = RangeUtil.ValueToRange(raMc + 180.0, 0.0, 360.0);
        double oaAscendant = RangeUtil.ValueToRange(raMc + 90.0, 0.0, 360.0);
        double odDescendant = RangeUtil.ValueToRange(raMc - 90.0, 0.0, 360.0);       

        Dictionary<ChartPoints, SpeculumItem> speculumItems = new();
        List<double> aspects = new()
        {
            0.0,
            180.0
        };

        foreach ((ChartPoints point, FullPointPos? value) in calcChart.Positions)
        {
            foreach (SpeculumItem specItem in aspects.Select(aspect => CreateSpeculumItem(aspect, value, raMc, 
                         raIc, geoLat, oaAscendant, odDescendant)))
            {
                speculumItems.Add(point, specItem);
            }
        }
        return new Speculum(primDirMethod, geoLat, raMc, raIc, oaAscendant, odDescendant, speculumItems);
    }
    
    
    private SpeculumItem CreateSpeculumItem(double aspect, FullPointPos fullPointPos, double raMc, double raIc, double geoLat,
        double oaAscendant, double odDescendant)
    {
        double ra = fullPointPos.Equatorial.MainPosSpeed.Position;
        double decl = fullPointPos.Equatorial.DeviationPosSpeed.Position;
        if (aspect is > 179.9 and < 180.1)
        {
            ra = RangeUtil.ValueToRange(ra + aspect, 0.0, 360.0);
            decl = -decl;
        }
        // For now only conjunctions and oppositions are supported.
        // For other aspects the method by Bianchini needs to be implemented.
        
        bool easternHemisphere = MathExtra.IsEasternHemiSphere(ra, raMc);
        double mdmc = RangeUtil.ValueToRange(ra - raMc, 0.0, 360.0);
        double mdic = RangeUtil.ValueToRange(ra - raIc, 0.0, 360.0);
        double ad = MathExtra.AscensionalDifference(decl, geoLat);
        bool northLatitude = geoLat > 0.0;
        double oa = MathExtra.ObliqueAscension(ra, ad, easternHemisphere, northLatitude);
        double hd = easternHemisphere
            ? oa - oaAscendant
            : RangeUtil.ValueToRange(oa + 180.0, 0.0, 360.0) - odDescendant;
        double dsa = RangeUtil.ValueToRange(Math.Abs(hd) + Math.Abs(mdmc), 0.0, 360.0);
        double nsa = RangeUtil.ValueToRange(Math.Abs(hd) + Math.Abs(mdic), 0.0, 360.0);
        double propSa = (hd >= 0.0) ? mdmc / dsa : mdic / nsa;
        return new SpeculumItem(aspect, ra, decl, mdmc, mdic, ad, oa, hd, dsa, nsa, propSa);
    }
}


