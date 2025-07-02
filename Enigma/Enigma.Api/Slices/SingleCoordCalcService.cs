// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Calc;
using Enigma.Core.Slices.SingleCoordinateCalc;
using Enigma.Domain.References;
using Enigma.Facades.Se;

namespace Enigma.Api.Slices;

/// <summary>
/// Service for the calculation of single coordinates
/// </summary>
public class SingleCoordCalcService
{
    private readonly SingleCoordinateOrchestrator _orchestrator;

    public SingleCoordCalcService()
    {
        var calcUtFacade = new CalcUtFacade();
        var seFlags = new SeFlags();
        var positionCalculator = new SinglePositionCalculator(calcUtFacade);
        _orchestrator = new SingleCoordinateOrchestrator(positionCalculator, seFlags);
    }

    /// <summary>
    /// Calculates single coordinate positions for one or more chart points
    /// </summary>
    /// <remarks>
    /// Prompt: use SingelCoordinateOrchestrator to retrieve positions for the given chartpoints and return these positions.
    /// </remarks>
    /// <param name="request">Request</param>
    /// <returns>List of chartpoints and positions</returns>
    public List<KeyValuePair<ChartPoints, double>> CalcPositions(SingleCoordCalcRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);
        
        return _orchestrator.CalcSinglePositions(
            request.Coordinate, 
            request.Points, 
            request.JulianDay);
    }
}