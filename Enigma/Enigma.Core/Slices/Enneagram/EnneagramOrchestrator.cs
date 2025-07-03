// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Calc;
using Enigma.Core.Slices.SingleCoordinateCalc;
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
        
        var calcUtFacade = new CalcUtFacade();
        var seFlags = new SeFlags();
        var positionCalculator = new SinglePositionCalculator(calcUtFacade);
        var orchestrator = new SingleCoordinateOrchestrator(positionCalculator, seFlags);
        
        var housesFacade = new HousesFacade();
        _calcHouses = new CalcHousesForEnneagram(housesFacade);
        
        _enneagramCalc = new EnneagramCalc();
        _dataCreation = new DataCreation();
    }

    /// <summary>
    /// Handle the calculation of strengths for the 9 Enneagram types
    /// </summary>
    /// <remarks>
    /// Prompt: Calculate the strengths for 9 Enneagramtypes. Calculate the longitudes for the ChartPoints Sun, Moon,
    /// Mercury, Venus, Mars, Jupiter, Saturn, Uranus, Neptune, Pluto, Chiron, True node, ApogeeMean, using
    /// CalcChartsHousesForEnneagram. Retrieve the info from request. Calculate the houses using CalcHousesForEnneagram.
    /// Use the results to calculate the strengths for the Enneagram types, using EnneagramCalc. 
    /// </remarks>
    /// <param name="request">Request with the data for then calculation</param>
    /// <returns>List with indexex for the type and the factors</returns>
    public List<KeyValuePair<int, double[]>> CalcEnneagramStrengths(EnneagramRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);
        
        // Define the chart points to calculate (as specified in the prompt)
        var chartPoints = new List<ChartPoints>
        {
            ChartPoints.Sun,
            ChartPoints.Moon,
            ChartPoints.Mercury,
            ChartPoints.Venus,
            ChartPoints.Mars,
            ChartPoints.Jupiter,
            ChartPoints.Saturn,
            ChartPoints.Uranus,
            ChartPoints.Neptune,
            ChartPoints.Pluto,
            ChartPoints.Chiron,
            ChartPoints.TrueNode,
            ChartPoints.ApogeeMean
        };
        
        // Calculate chart points longitudes
        var chartPointPositions = _calcChartPoints.CalcChartPoints(chartPoints, request.JulianDay);
        
        // Calculate houses
        var houses = _calcHouses.CalcHouses(request.JulianDay, request.GeoLon, request.GeoLat);
        
        // Read the signs and houses data
        var signsData = _dataCreation.ReadDataForSigns();
        var housesData = _dataCreation.ReadDataForHouses();
        
        // Use the values from the request
        var timeIsKnown = request.IsTimeKnown;
        var plutoDouble = request.IsDoublePluto;
        
        // Calculate Enneagram strengths
        var strengths = _enneagramCalc.CalcEnneagramStrengths(
            signsData, 
            housesData, 
            chartPointPositions, 
            houses, 
            timeIsKnown, 
            plutoDouble);
        
        // Convert the result to the expected format (List<KeyValuePair<int, double[]>>)
        // Since EnneagramCalc returns List<KeyValuePair<int, double>>, we need to convert
        // each double to a double[] with one element
        var result = new List<KeyValuePair<int, double[]>>();
        foreach (var strength in strengths)
        {
            result.Add(new KeyValuePair<int, double[]>(strength.Key, new double[] { strength.Value }));
        }
        
        return result;
    }
}