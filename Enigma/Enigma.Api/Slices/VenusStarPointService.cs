// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Slices.VenusStarPoint;

namespace Enigma.Api.Slices;

public class VenusStarPointService(VenusStarPointOrchestrator orchestrator)
{

    /// <summary>
    /// Calculate the Venus Star Point for the given request
    /// </summary>
    /// <remarks>
    /// Prompt: check the request, it should not be null. Use the request to call VenusStarPointOrchestrator and
    /// calculate the desired points.
    /// </remarks>
    /// <param name="request">Request with details for calculation</param>
    /// <returns>A calculated list with fully defined positions for a Venus Star Point</returns>
    public List<VenusStarPointPosition> VenusStarPointCalculation(VenusStarPointRequest request)
    {
        // Check the request, it should not be null
        if (request == null)
        {
            throw new ArgumentNullException(nameof(request), "Venus Star Point request cannot be null");
        }
        return orchestrator.CalculateVenusStarPoint(request);
    }
}