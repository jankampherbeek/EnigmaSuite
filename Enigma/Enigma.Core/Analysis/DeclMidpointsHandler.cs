// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Dtos;

namespace Enigma.Core.Analysis;

/// <summary>Handler for midpoints in declination.</summary>
public interface IDeclMidpointsHandler
{
    /// <summary>Retrieve list with all occupied midpoints in declination.</summary>
    /// <param name="chart">Calculated chart with positions.</param>
    /// <param name="orb">Orb from configuration.</param>
    /// <returns>All occupied midpoints in declination.</returns>
    public IEnumerable<OccupiedMidpoint> RetrieveOccupiedMidpoints(CalculatedChart chart, double orb);
}


public class DeclMidpointsHandler(IOccupiedMidpointsFinder occupiedMidpoints) : IDeclMidpointsHandler
{
    public IEnumerable<OccupiedMidpoint> RetrieveOccupiedMidpoints(CalculatedChart chart, double orb)
    {
        return occupiedMidpoints.CalculateOccupiedMidpointsInDeclination(chart, orb);
    }
}