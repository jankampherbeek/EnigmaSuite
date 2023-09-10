// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Api.Interfaces;
using Enigma.Core.Interfaces;
using Enigma.Domain.Calc.ChartItems;
using Enigma.Domain.Points;
using Enigma.Domain.References;
using Enigma.Domain.RequestResponse;

namespace Enigma.Api.Prog;

/// <inheritdoc/>
public class CalcTransitsEventApi: ICalcTransitsEventApi
{
    private readonly IChartAllPositionsHandler _handler;
    
    public CalcTransitsEventApi(IChartAllPositionsHandler handler) => _handler = handler;
    
    /// <inheritdoc/>
    public Dictionary<ChartPoints, FullPointPos> CalculateTransits(TransitsEventRequest request)
    {
        CelPointsRequest cpRequest = new(request.JulianDayUt, request.Location, request.CalculationPreferences);
        return _handler.CalcFullChart(cpRequest);        
    }
}