// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Domain.CalcChartsRange;
using Enigma.Domain.RequestResponse;

/// <summary>Handle calculation for a full chart with all positions.</summary>
public interface IChartAllPositionsHandler
{
    public ChartAllPositionsResponse CalcFullChart(ChartAllPositionsRequest request);
}

public interface IObliqueLongitudeHandler
{
    public ObliqueLongitudeResponse CalcObliqueLongitude(ObliqueLongitudeRequest request);
}


/// <summary>
/// Handler for the calculation of one or more celestial points.
/// </summary>
public interface ICelPointsHandler
{
    public CelPointsResponse CalcCelPoints(CelPointsRequest request);
}

/// <summary>Handler for the calculation of  range of charts for research purposes.</summary>
public interface ICalcChartsRangeHandler
{
    /// <summary>Calculate a range of charts.</summary>
    /// <param name="request">Request with the data and the settings.</param>
    /// <returns>The calculated result.</returns>
    public List<FullChartForResearchItem> CalculateRange(ChartsRangeRequest request);
}