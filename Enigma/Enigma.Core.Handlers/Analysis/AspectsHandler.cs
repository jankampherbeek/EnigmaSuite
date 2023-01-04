// Jan Kampherbeek, (c) 2022, 2023.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Handlers.Interfaces;
using Enigma.Domain.Analysis.Aspects;
using Enigma.Domain.Charts;
using Enigma.Domain.Points;
using Enigma.Domain.RequestResponse;

namespace Enigma.Core.Handlers.Analysis;

/// <inheritdoc/>
public sealed class AspectsHandler : IAspectsHandler
{
    private readonly IAspectChecker _aspectChecker;

    public AspectsHandler(IAspectChecker aspectChecker)
    {
        _aspectChecker = aspectChecker;
    }

    /// <inheritdoc/>
    public List<EffectiveAspect> AspectsForMundanePoints(List<AspectDetails> aspectDetails, CalculatedChart calculatedChart)
    {
        return _aspectChecker.FindAspectsForMundanePoints(aspectDetails, calculatedChart);
    }

    /// <inheritdoc/>
    public List<EffectiveAspect> AspectsForMundanePoints(AspectRequest request)
    {
        return _aspectChecker.FindAspectsForMundanePoints(request.CalcChart, request.Config);
    }

    /// <inheritdoc/>
    public List<EffectiveAspect> AspectsForCelPoints(List<AspectDetails> aspectDetails, List<FullCelPointPos> fullCelPointPositions)
    {
        return _aspectChecker.FindAspectsCelPoints(aspectDetails, fullCelPointPositions);
    }


    /// <inheritdoc/>
    public List<EffectiveAspect> AspectsForCelPoints(AspectRequest request)
    {
        return _aspectChecker.FindAspectsCelPoints(request.CalcChart, request.Config);
    }
}
