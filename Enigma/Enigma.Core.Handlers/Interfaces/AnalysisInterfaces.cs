// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Analysis;
using Enigma.Domain.Analysis.Aspects;
using Enigma.Domain.Charts;
using Enigma.Domain.Points;
using Enigma.Domain.RequestResponse;

namespace Enigma.Core.Handlers.Interfaces;


/// <summary>Helper class for the calculation of distances.</summary>
public interface IDistanceCalculator
{
    /// <summary>Calculates the shortest distance bwetween two points.</summary>
    /// <param name="allPoints">List of points.</param>
    /// <returns>List of shortest distances between each pair of points.</returns>
    public List<DistanceBetween2Points> FindShortestDistances(List<PositionedPoint> allPoints);
}


/// <summary>Handler for aspects.</summary>
public interface IAspectsHandler
{
    /// <summary>Find aspects to mundane points.</summary>
    /// <param name="request">Request with positions.</param>
    /// <returns>Aspects found.</returns>
    public List<DefinedAspect> AspectsForMundanePoints(AspectRequest request);

    /// <summary>Find aspects to mundane points.</summary>
    /// <param name="aspectDetails">Supported aspects.</param>
    /// <param name="calculatedChart">Calculated chart.</param>
    /// <returns>Aspects found.</returns>
    public List<DefinedAspect> AspectsForMundanePoints(List<AspectDetails> aspectDetails, CalculatedChart calculatedChart);

    /// <summary>Find aspects between celestial points (excluding mundane points).</summary>
    /// <param name="request">Request with positions.</param>
    /// <returns>Aspects found.</returns>
    public List<DefinedAspect> AspectsForCelPoints(AspectRequest request);

    /// <summary>Find aspects between celestial points (excluding mundane points).</summary>
    /// <param name="aspectDetails">Supported aspects.</param>
    /// <param name="fullCelPointPositions">Supported celestial points.</param>
    /// <returns>Aspects found.</returns>
    public List<DefinedAspect> AspectsForCelPoints(List<AspectDetails> aspectDetails, List<FullChartPointPos> fullCelPointPositions);
}



/// <summary>Handler for harmonics.</summary>
public interface IHarmonicsHandler
{
    /// <summary>Define the harmonics for all positions in CalcualtedChart.</summary>
    /// <param name="chart">Chart with all positions.</param>
    /// <param name="harmonicNumber">The harmonic number, this can also be a fractional number.</param>
    /// <returns>The calculated harmonic positions, all celestial points followed by Mc, Asc, Vertex, Eastpoint in that sequence.</returns>
    public List<double> RetrieveHarmonicPositions(CalculatedChart chart, double harmonicNumber);
}

/// <summary>Handler for midpoints.</summary>
public interface IMidpointsHandler
{
    /// <summary>Retrieve list with all base midpoints between two items, regardless if the midpoint is occupied.</summary>
    /// <param name="chart">Calculated chart with positions.</param>
    /// <returns>All base midpoints.</returns>
    public List<BaseMidpoint> RetrieveBaseMidpoints(CalculatedChart chart);

    /// <summary>Retrieve list with all occupied midpoints for a specifed dial.</summary>
    /// <param name="chart">Calculated chart with positions.</param>
    /// <param name="dialSize">Degrees for specified dial.</param>
    /// <returns>All occupied midpoints.</returns>
    public List<OccupiedMidpoint> RetrieveOccupiedMidpoints(CalculatedChart chart, double dialSize);
}



/// <summary>Calculator for harmonics.</summary>
public interface IHarmonicsCalculator
{
    /// <summary>Calculate harmonics for a list of positions using a specified harmonic number.</summary>
    /// <param name="originalPositions">List of original positions.</param>
    /// <param name="harmonicNumber">The number for the harmonic to calculate.</param>
    /// <returns>List with harmonic positions in the same sequence as the original positions.</returns>
    public List<double> CalculateHarmonics(List<double> originalPositions, double harmonicNumber);
}



/// <summary>Create list with all base midpoints.</summary>
public interface IBaseMidpointsCreator
{
    /// <summary>Find basemidpoints for list with AnalysisPoint.</summary>
    /// <param name="positionedPoints">Positions that will be combined to midpoints.</param>
    /// <returns>List with base midpoints.</returns>
    public List<BaseMidpoint> CreateBaseMidpoints(List<PositionedPoint> positionedPoints);

    /// <summary>Convert list with BaseMidpoint to a list with the same midpoints but with the position converted to a specific dial.</summary>
    /// <param name="baseMidpoints">The original base midpoints.</param>
    /// <param name="dialSize">Degrees of the dial.</param>
    /// <returns>Midpoints with a position that fits in the specified dial.</returns>
    public List<BaseMidpoint> ConvertBaseMidpointsToDial(List<BaseMidpoint> baseMidpoints, double dialSize);
}


/// <summary>Handle points to be used for the calculation of midpoints.</summary>
public interface IPointsForMidpoints
{
    /// <summary>Create analysispoints for a specific dial.</summary>
    /// <param name="chart">Calculated chart.</param>
    /// <param name="dialSize">Dial size in degrees.</param>
    /// <returns>Constructed AnalysisPoints.</returns>
    public List<PositionedPoint> CreateAnalysisPoints(CalculatedChart chart, double dialSize);
}

/// <summary>Handle the calculartion of occupied midpoints.</summary>
public interface IOccupiedMidpoints
{
    /// <summary>Calculate occupied midpoints for a specific dial.</summary>
    /// <param name="chart">Calculated chart.</param>
    /// <param name="dialSize">Dial size in degrees.</param>
    /// <returns>Calculated occupied midpoints.</returns>
    public List<OccupiedMidpoint> CalculateOccupiedMidpoints(CalculatedChart chart, double dialSize);
}





