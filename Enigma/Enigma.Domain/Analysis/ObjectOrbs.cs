// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Interfaces;
using Enigma.Domain.Points;

namespace Enigma.Domain.Analysis;


/// <summary>Default orb factor for a chart point.</summary>
/// <param name="Point">The chart point.</param>
/// <param name="OrbFactor">Factor for the orb.</param>
public record ChartPointOrb(ChartPoints Point, double OrbFactor);


public class OrbDefinitions : IOrbDefinitions
{

    public ChartPointOrb DefineChartPointOrb(ChartPoints chartPoint)            // TODO read orb from configuration (current values are not consistent with default config).
    {
        return chartPoint switch
        {
            ChartPoints.Sun => new ChartPointOrb(chartPoint, 1.0),
            ChartPoints.Moon => new ChartPointOrb(chartPoint, 1.0),
            ChartPoints.Mercury => new ChartPointOrb(chartPoint, 0.9),
            ChartPoints.Venus => new ChartPointOrb(chartPoint, 0.9),
            ChartPoints.Earth => new ChartPointOrb(chartPoint, 1.0),
            ChartPoints.Mars => new ChartPointOrb(chartPoint, 0.9),
            ChartPoints.Jupiter => new ChartPointOrb(chartPoint, 0.7),
            ChartPoints.Saturn => new ChartPointOrb(chartPoint, 0.7),
            ChartPoints.Uranus => new ChartPointOrb(chartPoint, 0.6),
            ChartPoints.Neptune => new ChartPointOrb(chartPoint, 0.6),
            ChartPoints.Pluto => new ChartPointOrb(chartPoint, 0.6),
            ChartPoints.MeanNode => new ChartPointOrb(chartPoint, 0.3),
            ChartPoints.TrueNode => new ChartPointOrb(chartPoint, 0.3),
            ChartPoints.Chiron => new ChartPointOrb(chartPoint, 0.3),
            ChartPoints.PersephoneRam => new ChartPointOrb(chartPoint, 0.0),
            ChartPoints.HermesRam => new ChartPointOrb(chartPoint, 0.0),
            ChartPoints.DemeterRam => new ChartPointOrb(chartPoint, 0.0),
            ChartPoints.CupidoUra => new ChartPointOrb(chartPoint, 0.0),
            ChartPoints.HadesUra => new ChartPointOrb(chartPoint, 0.0),
            ChartPoints.ZeusUra => new ChartPointOrb(chartPoint, 0.0),
            ChartPoints.KronosUra => new ChartPointOrb(chartPoint, 0.0),
            ChartPoints.ApollonUra => new ChartPointOrb(chartPoint, 0.0),
            ChartPoints.AdmetosUra => new ChartPointOrb(chartPoint, 0.0),
            ChartPoints.VulcanusUra => new ChartPointOrb(chartPoint, 0.0),
            ChartPoints.PoseidonUra => new ChartPointOrb(chartPoint, 0.0),
            ChartPoints.Eris => new ChartPointOrb(chartPoint, 0.3),
            ChartPoints.Pholus => new ChartPointOrb(chartPoint, 0.0),
            ChartPoints.Ceres => new ChartPointOrb(chartPoint, 0.0),
            ChartPoints.Pallas => new ChartPointOrb(chartPoint, 0.0),
            ChartPoints.Juno => new ChartPointOrb(chartPoint, 0.0),
            ChartPoints.Vesta => new ChartPointOrb(chartPoint, 0.0),
            ChartPoints.Isis => new ChartPointOrb(chartPoint, 0.0),
            ChartPoints.Nessus => new ChartPointOrb(chartPoint, 0.0),
            ChartPoints.Huya => new ChartPointOrb(chartPoint, 0.0),
            ChartPoints.Varuna => new ChartPointOrb(chartPoint, 0.0),
            ChartPoints.Ixion => new ChartPointOrb(chartPoint, 0.0),
            ChartPoints.Quaoar => new ChartPointOrb(chartPoint, 0.0),
            ChartPoints.Haumea => new ChartPointOrb(chartPoint, 0.0),
            ChartPoints.Orcus => new ChartPointOrb(chartPoint, 0.0),
            ChartPoints.Makemake => new ChartPointOrb(chartPoint, 0.0),
            ChartPoints.Sedna => new ChartPointOrb(chartPoint, 0.0),
            ChartPoints.Hygieia => new ChartPointOrb(chartPoint, 0.0),
            ChartPoints.Astraea => new ChartPointOrb(chartPoint, 0.0),
            ChartPoints.ApogeeMean => new ChartPointOrb(chartPoint, 0.0),
            ChartPoints.ApogeeCorrected => new ChartPointOrb(chartPoint, 0.0),
            ChartPoints.ApogeeInterpolated => new ChartPointOrb(chartPoint, 0.0),
            ChartPoints.ApogeeDuval => new ChartPointOrb(chartPoint, 0.0),
            ChartPoints.PersephoneCarteret => new ChartPointOrb(chartPoint, 0.0),
            ChartPoints.VulcanusCarteret => new ChartPointOrb(chartPoint, 0.0),
            ChartPoints.Ascendant => new ChartPointOrb(chartPoint, 1.0),
            ChartPoints.Mc => new ChartPointOrb(chartPoint, 1.0),
            ChartPoints.EastPoint => new ChartPointOrb(chartPoint, 0.0),
            ChartPoints.Vertex => new ChartPointOrb(chartPoint, 0.0),
            ChartPoints.Cusp1 => new ChartPointOrb(chartPoint, 0.0),
            ChartPoints.Cusp2 => new ChartPointOrb(chartPoint, 0.0),
            ChartPoints.Cusp3 => new ChartPointOrb(chartPoint, 0.0),
            ChartPoints.Cusp4 => new ChartPointOrb(chartPoint, 0.0),
            ChartPoints.Cusp5 => new ChartPointOrb(chartPoint, 0.0),
            ChartPoints.Cusp6 => new ChartPointOrb(chartPoint, 0.0),
            ChartPoints.Cusp7 => new ChartPointOrb(chartPoint, 0.0),
            ChartPoints.Cusp8 => new ChartPointOrb(chartPoint, 0.0),
            ChartPoints.Cusp9 => new ChartPointOrb(chartPoint, 0.0),
            ChartPoints.Cusp10 => new ChartPointOrb(chartPoint, 0.0),
            ChartPoints.Cusp11 => new ChartPointOrb(chartPoint, 0.0),
            ChartPoints.Cusp12 => new ChartPointOrb(chartPoint, 0.0),
            ChartPoints.Cusp13 => new ChartPointOrb(chartPoint, 0.0),
            ChartPoints.Cusp14 => new ChartPointOrb(chartPoint, 0.0),
            ChartPoints.Cusp15 => new ChartPointOrb(chartPoint, 0.0),
            ChartPoints.Cusp16 => new ChartPointOrb(chartPoint, 0.0),
            ChartPoints.Cusp17 => new ChartPointOrb(chartPoint, 0.0),
            ChartPoints.Cusp18 => new ChartPointOrb(chartPoint, 0.0),
            ChartPoints.Cusp19 => new ChartPointOrb(chartPoint, 0.0),
            ChartPoints.Cusp20 => new ChartPointOrb(chartPoint, 0.0),
            ChartPoints.Cusp21 => new ChartPointOrb(chartPoint, 0.0),
            ChartPoints.Cusp22 => new ChartPointOrb(chartPoint, 0.0),
            ChartPoints.Cusp23 => new ChartPointOrb(chartPoint, 0.0),
            ChartPoints.Cusp24 => new ChartPointOrb(chartPoint, 0.0),
            ChartPoints.Cusp25 => new ChartPointOrb(chartPoint, 0.0),
            ChartPoints.Cusp26 => new ChartPointOrb(chartPoint, 0.0),
            ChartPoints.Cusp27 => new ChartPointOrb(chartPoint, 0.0),
            ChartPoints.Cusp28 => new ChartPointOrb(chartPoint, 0.0),
            ChartPoints.Cusp29 => new ChartPointOrb(chartPoint, 0.0),
            ChartPoints.Cusp30 => new ChartPointOrb(chartPoint, 0.0),
            ChartPoints.Cusp31 => new ChartPointOrb(chartPoint, 0.0),
            ChartPoints.Cusp32 => new ChartPointOrb(chartPoint, 0.0),
            ChartPoints.Cusp33 => new ChartPointOrb(chartPoint, 0.0),
            ChartPoints.Cusp34 => new ChartPointOrb(chartPoint, 0.0),
            ChartPoints.Cusp35 => new ChartPointOrb(chartPoint, 0.0),
            ChartPoints.Cusp36 => new ChartPointOrb(chartPoint, 0.0),
            ChartPoints.ZeroAries => new ChartPointOrb(chartPoint, 0.0),
            ChartPoints.ZeroCancer => new ChartPointOrb(chartPoint, 0.0),
            ChartPoints.FortunaSect => new ChartPointOrb(chartPoint, 0.0),
            ChartPoints.FortunaNoSect => new ChartPointOrb(chartPoint, 0.0),
            _ => throw new ArgumentException("Orb definition for chart point unknown : " + chartPoint.ToString())
        };
    }
}


