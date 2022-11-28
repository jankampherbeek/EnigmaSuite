// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.


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