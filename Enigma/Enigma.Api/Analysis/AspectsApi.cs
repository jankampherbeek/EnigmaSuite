// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Ardalis.GuardClauses;
using Enigma.Api.Interfaces;
using Enigma.Core.Handlers.Interfaces;
using Enigma.Domain.Analysis;
using Enigma.Domain.RequestResponse;
using Serilog;

namespace Enigma.Api.Analysis;


/// <inheritdoc/>
public class AspectsApi : IAspectsApi
{

    private readonly IAspectsHandler _aspectHandler;

    public AspectsApi(IAspectsHandler aspectHandler)
    {
        _aspectHandler = aspectHandler;
    }

    /// <inheritdoc/>
    public List<EffectiveAspect> AspectsForMundanePoints(AspectRequest request)
    {
        Guard.Against.Null(request);
        Guard.Against.Null(request.CalcChart);
        Log.Information("AspectsApi: AspectsForMundanePoints for chart " + request.CalcChart.InputtedChartData.ChartMetaData.Name);
        return _aspectHandler.AspectsForMundanePoints(request);
    }

    /// <inheritdoc/>
    public List<EffectiveAspect> AspectsForCelPoints(AspectRequest request)
    {
        Guard.Against.Null(request);
        Guard.Against.Null(request.CalcChart);
        Log.Information("AspectsApi: AspectsForCelPoints for chart " + request.CalcChart.InputtedChartData.ChartMetaData.Name);
        return _aspectHandler.AspectsForCelPoints(request);
    }
}

