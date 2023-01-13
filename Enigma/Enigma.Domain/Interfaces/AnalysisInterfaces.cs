// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Domain.Analysis;
using Enigma.Domain.Points;

namespace Enigma.Domain.Interfaces;

/// <summary>Definitions for an orb for a ChartPoint.</summary>
public interface IOrbDefinitions
{
    /// <summary>Define the orb for a chart point.</summary>
    /// <param name="ChartPoint">The ChartPoint.</param>
    /// <returns>The defined orb.</returns>
    public ChartPointOrb DefineChartPointOrb(ChartPoints ChartPoint);

}




