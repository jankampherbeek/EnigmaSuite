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
/// Calculate chart points for Enneagram
/// </summary>
public class CalcChartPointsForEnneagram
{
    private readonly SingleCoordinateOrchestrator _orchestrator;

    /// <summary>
    /// Initializes a new instance of the CalcChartPointsForEnneagram class
    /// </summary>
    public CalcChartPointsForEnneagram()
    {
        var calcUtFacade = new CalcUtFacade();
        var seFlags = new SeFlags();
        var positionCalculator = new SinglePositionCalculator(calcUtFacade);
        _orchestrator = new SingleCoordinateOrchestrator(positionCalculator, seFlags);
    }

    /// <summary>
    /// Calculate a set of predefined chart points for an Enneagram calculation
    /// </summary>
    /// <remarks>
    /// Prompt: call SingelCoordCalcService to calculate all longitudes for the points. Return the calculatedpoints
    /// as a key-value pair with the point and the position, 
    /// </remarks>
    /// <param name="points">All supported points</param>
    /// <param name="julianDay">Julian day</param>
    /// <returns>A list with Chartpoints and positions</returns>
    public List<KeyValuePair<ChartPoints, double>> CalcChartPoints(List<ChartPoints> points, double julianDay)
    {
        ArgumentNullException.ThrowIfNull(points);
        
        if (points.Count == 0)
        {
            throw new ArgumentException("Points list cannot be empty", nameof(points));
        }
        
        if (double.IsNaN(julianDay) || double.IsInfinity(julianDay))
        {
            throw new ArgumentException("Julian day must be a finite number", nameof(julianDay));
        }
        
        return _orchestrator.CalcSinglePositions(Coordinates.Longitude, points, julianDay);
    }
}