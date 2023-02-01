// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Analysis;
using Enigma.Domain.Analysis.Aspects;
using Enigma.Domain.Charts;
using Enigma.Domain.Configuration;
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

    /// <summary>Calculates the shortest distances between two points, the first point is not a cusp (but could be Mc or Asc), the second point is a cusp.</summary>
    /// <param name="allPoints">All points except the cusps.</param>
    /// <param name="allCusps">The cusps.</param>
    /// <returns>List of shortest distances between a non-cusp (first position in distance) and a cusp (second position).</returns>
    public List<DistanceBetween2Points> FindShortestDistanceBetweenPointsAndCusps(List<PositionedPoint> allPoints, List<PositionedPoint> allCusps);
}

/// <summary>Selector for points that can be used to calculate aspects.</summary>
public interface IAspectPointSelector
{
    /// <summary>Selects points for aspects.</summary>
    /// <param name="chartPointPositions">Available chartpoint positions.</param>
    /// <param name="anglePositions">Available mundane positions for angles.</param>
    /// <param name="chartPointConfigSpecs">Configuration data for chart points.</param>
    /// <returns>The relevant points for the calculation of aspects.</returns>
    public Dictionary<ChartPoints, FullPointPos> SelectPoints(Dictionary<ChartPoints, FullPointPos> chartPointPositions, Dictionary<ChartPoints, FullPointPos> anglePositions, List<ChartPointConfigSpecs> chartPointConfigSpecs);
}


/// <summary>Handler for aspects.</summary>
public interface IAspectsHandler
{
  
    /// <summary>Find aspects between chart points.</summary>
    /// <param name="request">Request with positions.</param>
    /// <returns>Aspects found.</returns>
    public List<DefinedAspect> AspectsForChartPoints(AspectRequest request);

    /// <summary>Find aspects between chart points.</summary>
    /// <param name="posPoints">Celestial points with positions.</param>
    /// <param name="cuspPoints">Cusps with positions.</param>
    /// <param name="relevantAspects">Supported aspects as defined in configuration.</param>
    /// <param name="baseOrb">Base orb for aspects.</param>
    /// <returns>List with aspects between celestial points and between celestial points and cusps. Aspects between cusps are omitted.</returns>
    public List<DefinedAspect> AspectsForPosPoints(List<PositionedPoint> posPoints, List<PositionedPoint> cuspPoints, List<AspectConfigSpecs> relevantAspects, double baseOrb);
}



/// <summary>Handler for harmonics.</summary>
public interface IHarmonicsHandler
{
    /// <summary>Define the harmonics for all positions in CalculatedChart.</summary>
    /// <param name="chart">Chart with all positions.</param>
    /// <param name="harmonicNumber">The harmonic number, this can also be a fractional number.</param>
    /// <returns>The calculated harmonic positions, all celestial points followed by Mc, Asc, Vertex, Eastpoint in that sequence.</returns>
    public List<double> RetrieveHarmonicPositions(CalculatedChart chart, double harmonicNumber);

    /// <summary>Define the harmonics a list of PositionedPoint.</summary>
    /// <param name="posPoints">The points to calculate.</param>
    /// <param name="harmonicNumber">The multiplication factor for the harmonic.</param>
    /// <returns>The calculated results.</returns>
    public Dictionary<ChartPoints, double> RetrieveHarmonicPositions(List<PositionedPoint> posPoints, double harmonicNumber);
}

/// <summary>Handler for midpoints.</summary>
public interface IMidpointsHandler
{
    /// <summary>Retrieve list with all base midpoints between two items, regardless if the midpoint is occupied.</summary>
    /// <param name="chart">Calculated chart with positions.</param>
    /// <returns>All base midpoints.</returns>
    public List<BaseMidpoint> RetrieveBaseMidpoints(CalculatedChart chart);

    /// <summary>Retrieve list with all occupied midpoints for a specified dial.</summary>
    /// <param name="chart">Calculated chart with positions.</param>
    /// <param name="dialSize">Degrees for specified dial.</param>
    /// <returns>All occupied midpoints.</returns>
    public List<OccupiedMidpoint> RetrieveOccupiedMidpoints(CalculatedChart chart, double dialSize);

    /// <summary>Retrieve list with occupied midpoints for a given set of points and for a specified dial.</summary>
    /// <param name="posPoints">List with points.</param>
    /// <param name="dialSize">Degrees for specified dial.</param>
    /// <param name="orb">User defined orb.</param>
    /// <returns>Occupied midpoints for the given set of points.</returns>
    public List<OccupiedMidpoint> RetrieveOccupiedMidpoints(List<PositionedPoint> posPoints, double dialSize, double orb);
}



/// <summary>Calculator for harmonics.</summary>
public interface IHarmonicsCalculator
{
    /// <summary>Calculate harmonics for a list of positions using a specified harmonic number.</summary>
    /// <param name="originalPositions">List of original positions.</param>
    /// <param name="harmonicNumber">The multiplication factor for the harmonic to calculate.</param>
    /// <returns>List with harmonic positions in the same sequence as the original positions.</returns>
    public List<double> CalculateHarmonics(List<double> originalPositions, double harmonicNumber);

    /// <summary>Calculate harmonics for a single position using a specified harmonic number.</summary>
    /// <param name="originalPosition">OriginalPosition.</param>
    /// <param name="harmonicNumber">The multiplication factor for the harmonic to calculate.</param>
    /// <returns>Value for the harmonic position in the range 0 <= value < 360.0.</returns>
    public double CalculateHarmonic(double originalPosition, double harmonicNumber);
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
public interface IOccupiedMidpointsFinder
{
    /// <summary>Calculate occupied midpoints for a specific dial.</summary>
    /// <param name="chart">Calculated chart.</param>
    /// <param name="dialSize">Dial size in degrees.</param>
    /// <returns>Calculated occupied midpoints.</returns>
    public List<OccupiedMidpoint> CalculateOccupiedMidpoints(CalculatedChart chart, double dialSize);

    /// <summary>Calculate occupied midpoints for a specific dial.</summary>
    /// <param name="posPoints">List with points.</param>
    /// <param name="dialSize">Dial size in degrees.</param>
    /// <param name="orb">Orb.</param>
    /// <returns>Calculated occupied midpoints.</returns>
    public List<OccupiedMidpoint> CalculateOccupiedMidpoints(List<PositionedPoint> posPoints, double dialSize, double orb);
}

/// <summary>
/// Define actual orb for an aspect.
/// </summary>
public interface IAspectOrbConstructor
{
    /// <summary>Define orb between two celestial points for a given aspect.</summary>
    public double DefineOrb(ChartPoints point1, ChartPoints point2, double baseOrb, double aspectOrbFactor);
}



