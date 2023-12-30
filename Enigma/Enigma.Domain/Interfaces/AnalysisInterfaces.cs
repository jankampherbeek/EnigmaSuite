// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Domain.Dtos;
using Enigma.Domain.References;

namespace Enigma.Domain.Interfaces;

/// <summary>Definitions for an orb for a ChartPoint.</summary>
public interface IOrbDefinitions
{
    /// <summary>Define the orb for a chart point.</summary>
    /// <param name="chartPoint">The ChartPoint.</param>
    /// <param name="chartPointConfigSpecs">Orbs per chartpoint.</param>
    /// <returns>The defined orb.</returns>
    public KeyValuePair<ChartPoints, double> DefineChartPointOrb(ChartPoints chartPoint, Dictionary<ChartPoints, ChartPointConfigSpecs> chartPointConfigSpecs);

}




