// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

// Interfaces for API's that support analysises.


using Enigma.Domain.Analysis;
using Enigma.Domain.RequestResponse;

namespace Enigma.Api.Interfaces;

/// <summary>Api for the analysis of aspects.</summary>
public interface IAspectsApi
{
    /// <summary>Aspects for celestial points.</summary>
    public List<EffectiveAspect> AspectsForSolSysPoints(AspectRequest request);

    /// <summary>Aspects for mundane points.</summary>
    public List<EffectiveAspect> AspectsForMundanePoints(AspectRequest request);
}

/// <summary>Api for the analysis of midpoints.</summary>
public interface IMidpointsApi
{
    /// <summary>All midpoints as list and list of occupied midpoints.</summary>
    /// <param name="request">Request contains chartdata and wheeltype for midpoints.</param>
    /// <returns>Midpoints and occupied midpoints.</returns>
    public MidpointResponse AllMidpoints(MidpointRequest request);
}