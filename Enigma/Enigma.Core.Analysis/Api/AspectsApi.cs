// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Ardalis.GuardClauses;
using Enigma.Core.Analysis.Interfaces;
using Enigma.Domain.Analysis;
using Enigma.Domain.Charts;

namespace Enigma.Core.Analysis.Api;


public record AspectRequest
{
    public CalculatedChart CalcChart { get; }

    public AspectRequest (CalculatedChart calculatedChart)
    {
        CalcChart = calculatedChart;
    }
}





public class AspectsApi : IAspectsApi
{

    private readonly IAspectChecker _aspectChecker;

    public AspectsApi(IAspectChecker aspectChecker)
    {
        _aspectChecker = aspectChecker;
    }

    public List<EffectiveAspect> AspectsForMundanePoints(AspectRequest request)
    {
        Guard.Against.Null(request);
        Guard.Against.Null(request.CalcChart);
        return _aspectChecker.FindAspectsForMundanePoints(request.CalcChart);
    }

    public List<EffectiveAspect> AspectsForSolSysPoints(AspectRequest request)
    {
        Guard.Against.Null(request);
        Guard.Against.Null(request.CalcChart);
        return _aspectChecker.FindAspectsForSolSysPoints(request.CalcChart);
    }
}

