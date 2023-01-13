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

    public ChartPointOrb DefineChartPointOrb(ChartPoints chartPoint)            // TODO read orb from configuration.
    {                                                                           // TODO complete definitions.    
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
            ChartPoints.Mc => new ChartPointOrb(chartPoint, 1.0),
            ChartPoints.Ascendant => new ChartPointOrb(chartPoint, 1.0),

            _ => throw new ArgumentException("Orb definition for chart point unknown : " + chartPoint.ToString())
        };
    }

}

