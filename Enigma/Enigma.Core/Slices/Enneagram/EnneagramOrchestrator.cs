// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.References;
using Enigma.Facades.Se;

namespace Enigma.Core.Slices.Enneagram;

/// <summary>
/// Orchestrator for the calculation of Enneagram strengths
/// </summary>
public class EnneagramOrchestrator
{
    private readonly CalcChartPointsForEnneagram _calcChartPoints;
    private readonly CalcHousesForEnneagram _calcHouses;
    private readonly EnneagramCalc _enneagramCalc;
    private readonly DataCreation _dataCreation;

    /// <summary>
    /// Initializes a new instance of the EnneagramOrchestrator class
    /// </summary>
    public EnneagramOrchestrator()
    {
        _calcChartPoints = new CalcChartPointsForEnneagram();
        var housesFacade = new HousesFacade();
        _calcHouses = new CalcHousesForEnneagram(housesFacade);
        _enneagramCalc = new EnneagramCalc();
        _dataCreation = new DataCreation();
    }

    /// <summary>
    /// Handle the calculation of strengths for the 9 Enneagram types
    /// </summary>
    /// <remarks>
    /// Prompt: Calculate the strengths for 9 Enneagramtypes. Calculate the longitudes for the points as defined in the
    /// request, using CalcChartsHousesForEnneagram. Retrieve the info from request.
    /// Calculate the houses using CalcHousesForEnneagram.
    /// Use the results to calculate the strengths for the Enneagram types, using EnneagramCalc. 
    /// </remarks>
    /// <param name="request">Request with the data for then calculation</param>
    /// <returns>List with indexex for the type and the factors</returns>
    public List<KeyValuePair<int, double[]>> CalcEnneagramStrengths(EnneagramRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);
        
        // Use the chart points from the request instead of hardcoded list
        var chartPoints = request.Points;
        
        // Validate that the points list is not null or empty
        if (chartPoints == null || chartPoints.Count == 0)
        {
            throw new ArgumentException("Points list cannot be null or empty", nameof(request));
        }
        
        // Calculate chart points longitudes
        var chartPointPositions = _calcChartPoints.CalcChartPoints(chartPoints, request.JulianDay);
        
        // Calculate houses
        var houses = _calcHouses.CalcHouses(request.JulianDay, request.GeoLon, request.GeoLat);
        
        // Read the signs and houses data
        var signsData = _dataCreation.ReadDataForSigns();
        var housesData = _dataCreation.ReadDataForHouses();
        
        // Use the values from the request
        var timeIsKnown = request.UseHouses;
        
        // Pluto logic: if Pluto is not in the list, ignore IsDoublePluto
        var plutoDouble = request.IsDoublePluto && chartPoints.Contains(ChartPoints.Pluto);
        
        // Calculate Enneagram strengths
        var strengths = _enneagramCalc.CalcEnneagramStrengths(
            signsData, 
            housesData, 
            chartPointPositions, 
            houses, 
            timeIsKnown, 
            plutoDouble);
        
        // Convert the result to the expected format (List<KeyValuePair<int, double[]>>)

        return strengths.Select(strength => new KeyValuePair<int, double[]>(strength.Key, [strength.Value])).ToList();
    }
}