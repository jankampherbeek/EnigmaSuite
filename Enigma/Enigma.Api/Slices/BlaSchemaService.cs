// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Slices.BlaSchema;
using Enigma.Domain.Dtos;

namespace Enigma.Api.Slices;

/// <summary>
/// Service to retrieve details from BLA schema
/// </summary>
public class BlaSchemaService(BlaSchemaOrchestrator orchestrator)
{
    /// <summary>
    /// Get BLA schema details
    /// </summary>
    /// <param name="chart">A calculated chart</param>
    /// <returns>Instance of ChartDetails</returns>
    public BlaSchemaDataSheet GetChartDetails(ChartLongitudes chart)
    {
        return orchestrator.CreateBlaSchema(chart);;
    }
}