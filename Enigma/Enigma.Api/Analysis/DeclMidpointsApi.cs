// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Analysis;
using Enigma.Domain.Dtos;

namespace Enigma.Api.Analysis;

/// <summary>SPI for the calculation of midpoints in declination.</summary>
public interface IDeclMidpointsApi
{

    /// <summary>Return all occupied midpoints for a specific dial.</summary>
    /// <param name="chart">Chart with positions.</param>
    /// <param name="orb">Orb from configuration.</param>
    /// <returns>All occupied midpoints.</returns>
    public IEnumerable<OccupiedMidpoint> OccupiedDeclMidpoints(CalculatedChart chart, double orb);
}


public class DeclMidpointsApi(IDeclMidpointsHandler handler) : IDeclMidpointsApi
{
    public IEnumerable<OccupiedMidpoint> OccupiedDeclMidpoints(CalculatedChart chart, double orb)
    {
        return handler.RetrieveOccupiedMidpoints(chart, orb);
    }
} 
 
