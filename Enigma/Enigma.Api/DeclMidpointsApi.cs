// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Handlers;
using Enigma.Domain.Dtos;

namespace Enigma.Api;

/// <summary>SPI for the calculation of midpoints in declination.</summary>
public interface IDeclMidpointsApi
{

    /// <summary>Return all occupied midpoints for a specific dial.</summary>
    /// <param name="chart">Chart with positions.</param>
    /// <param name="orb">Orb from configuration.</param>
    /// <returns>All occupied midpoints.</returns>
    public IEnumerable<OccupiedMidpoint> OccupiedDeclMidpoints(CalculatedChart chart, double orb);
}



////////// Implementation //////////

public class DeclMidpointsApi : IDeclMidpointsApi
{
    private IDeclMidpointsHandler _handler;

    public DeclMidpointsApi(IDeclMidpointsHandler handler)
    {
        _handler = handler;
    }
    
    public IEnumerable<OccupiedMidpoint> OccupiedDeclMidpoints(CalculatedChart chart, double orb)
    {
        return _handler.RetrieveOccupiedMidpoints(chart, orb);
    }
} 
 
