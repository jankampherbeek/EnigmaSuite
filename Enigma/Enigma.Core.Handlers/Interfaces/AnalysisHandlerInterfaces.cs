// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Domain.Analysis;
using Enigma.Domain.RequestResponse;

namespace Enigma.Core.Handlers.Interfaces;

/// <summary>Handkler for aspects.</summary>
public interface IAspectsHandler
{
    /// <summary>Find aspects to mundane points.</summary>
    /// <param name="request">Request with positions.</param>
    /// <returns>Aspects found.</returns>
    public List<EffectiveAspect> AspectsForMundanePoints(AspectRequest request);

    /// <summary>Find aspects between celestial points (excluding mundane points).</summary>
    /// <param name="request">Request with positions.</param>
    /// <returns>Aspects found.</returns>
    public List<EffectiveAspect> AspectsForSolSysPoints(AspectRequest request);

}