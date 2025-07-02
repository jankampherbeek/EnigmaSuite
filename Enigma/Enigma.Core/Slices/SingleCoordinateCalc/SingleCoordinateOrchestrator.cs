// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Calc;
using Enigma.Domain.References;

namespace Enigma.Core.Slices.SingleCoordinateCalc;

/// <summary>
/// Orchestrator for the calculation of positions with only one coordinate
/// </summary>
/// <remarks>
/// Supports all coordinate types: Longitude, Latitude, RightAscension, Declination, Azimuth, Altitude.
/// Currently supports geocentric, tropical positions.
/// </remarks>
public class SingleCoordinateOrchestrator
{
    private readonly SinglePositionCalculator _positionCalculator;
    private readonly ISeFlags _seFlags;

    public SingleCoordinateOrchestrator(
        SinglePositionCalculator positionCalculator,
        ISeFlags seFlags)
    {
        _positionCalculator = positionCalculator ?? throw new ArgumentNullException(nameof(positionCalculator));
        _seFlags = seFlags ?? throw new ArgumentNullException(nameof(seFlags));
    }

    /// <summary>
    /// Calculate single coordinate positions for multiple chart points
    /// </summary>
    /// <param name="coordinate">The coordinate type to calculate</param>
    /// <param name="points">List of chart points to calculate positions for</param>
    /// <param name="jd">Julian day number</param>
    /// <returns>List of chart points with their calculated positions</returns>
    public List<KeyValuePair<ChartPoints, double>> CalcSinglePositions(Coordinates coordinate, List<ChartPoints> points, double jd)
    {
        ValidateInputs(coordinate, points, jd);
        
        var coordinateDetails = coordinate.GetDetails();
        var isMainPos = IsMainPosition(coordinate);
        var flags = _seFlags.DefineFlags(coordinateDetails.CoordinateSystem, ObserverPositions.GeoCentric, ZodiacTypes.Tropical);
        
        return points.Select(point => 
        {
            var pointId = point.GetDetails().CalcId;
            var pos = _positionCalculator.CalcSinglePosition(jd, pointId, flags, isMainPos);
            return new KeyValuePair<ChartPoints, double>(point, pos);
        }).ToList();
    }

    /// <summary>
    /// Determines if a coordinate is a main position (longitude, right ascension, azimuth) or deviation (latitude, declination, altitude)
    /// </summary>
    /// <param name="coordinate">The coordinate to check</param>
    /// <returns>True if it's a main position, false if it's a deviation</returns>
    private static bool IsMainPosition(Coordinates coordinate)
    {
        return coordinate switch
        {
            Coordinates.Longitude => true,
            Coordinates.RightAscension => true,
            Coordinates.Azimuth => true,
            Coordinates.Latitude => false,
            Coordinates.Declination => false,
            Coordinates.Altitude => false,
            _ => throw new ArgumentException($"Unsupported coordinate type: {coordinate}")
        };
    }

    /// <summary>
    /// Validates input parameters
    /// </summary>
    /// <param name="coordinate">The coordinate type</param>
    /// <param name="points">List of chart points</param>
    /// <param name="jd">Julian day number</param>
    /// <exception cref="ArgumentException">Thrown when inputs are invalid</exception>
    private static void ValidateInputs(Coordinates coordinate, List<ChartPoints> points, double jd)
    {
        ArgumentNullException.ThrowIfNull(points);

        if (points.Count == 0)
            throw new ArgumentException("Points list cannot be empty", nameof(points));
        
        if (jd <= 0)
            throw new ArgumentException("Julian day must be positive", nameof(jd));
        
        // Validate that the coordinate is supported
        _ = coordinate.GetDetails(); // This will throw if coordinate is invalid
    }
}

