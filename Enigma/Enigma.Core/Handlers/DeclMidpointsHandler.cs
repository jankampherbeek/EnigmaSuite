// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Analysis;
using Enigma.Domain.Dtos;

namespace Enigma.Core.Handlers;

/// <summary>Handler for midpoints in declination.</summary>
public interface IDeclMidpointsHandler
{
    /// <summary>Retrieve list with all occupied midpoints in declination.</summary>
    /// <param name="chart">Calculated chart with positions.</param>
    /// <param name="orb">Orb from configuration.</param>
    /// <returns>All occupied midpoints in declination.</returns>
    public IEnumerable<OccupiedMidpoint> RetrieveOccupiedMidpoints(CalculatedChart chart, double orb);
}


////////// Implementation //////////

public class DeclMidpointsHandler : IDeclMidpointsHandler
{
    private readonly IOccupiedMidpointsFinder _occupiedMidpoints;

    public DeclMidpointsHandler(IOccupiedMidpointsFinder occupiedMidpoints)
    {
        _occupiedMidpoints = occupiedMidpoints;
    }
    
    public IEnumerable<OccupiedMidpoint> RetrieveOccupiedMidpoints(CalculatedChart chart, double orb)
    {
        return _occupiedMidpoints.CalculateOccupiedMidpointsInDeclination(chart, orb);
        
        
        
    }
}