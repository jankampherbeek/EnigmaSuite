// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Handlers.Interfaces;
using Enigma.Core.Helpers.Interfaces;
using Enigma.Domain.Analysis;
using Enigma.Domain.RequestResponse;

namespace Enigma.Core.Handlers.Analysis;

/// <inheritdoc/>
class AspectsHandler: IAspectsHandler
{
    private IAspectChecker _aspectChecker;
    
    public AspectsHandler(IAspectChecker aspectChecker)
    {
        _aspectChecker = aspectChecker;
    }

    /// <inheritdoc/>
    public List<EffectiveAspect> AspectsForMundanePoints(AspectRequest request)
    {
        return _aspectChecker.FindAspectsForMundanePoints(request.CalcChart);
    }

    /// <inheritdoc/>
    public List<EffectiveAspect> AspectsForSolSysPoints(AspectRequest request)
    {
        return _aspectChecker.FindAspectsForSolSysPoints(request.CalcChart);
    }
}
