// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Ardalis.GuardClauses;
using E4C.Core.Astron.FullChart;
using E4C.Core.Astron.Houses;
using E4C.Shared.ReqResp;

namespace E4C.Core.Api.Astron;

/// <summary>API for calculation of a fully defined chart.</summary>
public interface IFullChartApi
{
    /// <summary>Api call to calculate a full chart.</summary>
    /// <param name="request"/>
    /// <remarks>Throws ArgumentNullException if the request is null.</remarks>
    /// <returns>Response with instance of FullChart with all positionscoordinates, or an indication of errors that occurred.</returns>
    public FullChartResponse getFullChart(FullChartRequest request);
}


/// <inheritdoc/>
public class FullChartApi: IFullChartApi
{
    private readonly IFullChartHandler _handler;

    /// <param name="handler">Handler for the calculation of the chart.</param>
    public FullChartApi(IFullChartHandler handler) => _handler = handler;

    public FullChartResponse getFullChart(FullChartRequest request)
    {
        Guard.Against.Null(request);
        Guard.Against.Null(request.SolSysPointRequest);
        Guard.Against.Null(request.SolSysPointRequest.Ayanamsha);
        Guard.Against.Null(request.SolSysPointRequest.ChartLocation);
        Guard.Against.Null(request.SolSysPointRequest.ProjectionType);
        Guard.Against.Null(request.SolSysPointRequest.ObserverPosition);
        Guard.Against.Null(request.HouseSystem);

        return _handler.CalcFullChart(request);
    }

}