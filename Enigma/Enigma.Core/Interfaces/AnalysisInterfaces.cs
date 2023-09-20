// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Dtos;
using Enigma.Domain.References;
using Enigma.Domain.Requests;

namespace Enigma.Core.Interfaces;


/// <summary>Helper class for the calculation of distances.</summary>
public interface ICalculatedDistance
{
    /// <summary>Calculates the shortest distance between two positions.</summary>
    /// <remarks>PRE: pos1 and pos2 &gt;= 0.0 and pos1 and pos2 &lt; 360.0</remarks>
    /// <param name="pos1">First position.</param>
    /// <param name="pos2">Second position.</param>
    /// <returns>Shortest distance between pos1 and pos2.</returns>
    public double ShortestDistance(double pos1, double pos2);
    
    /// <summary>Calculates the shortest distance between several combinations of two points.</summary>
    /// <param name="allPoints">List of points.</param>
    /// <returns>List of shortest distances between each pair of points.</returns>
    public List<DistanceBetween2Points> ShortestDistances(List<PositionedPoint> allPoints);

    /// <summary>Calculates the shortest distances between several combinations of two points,
    /// the first point is not a cusp (but could be Mc or Asc), the second point is a cusp.</summary>
    /// <param name="allPoints">All points except the cusps.</param>
    /// <param name="allCusps">The cusps.</param>
    /// <returns>List of shortest distances between a non-cusp (first position in distance) and a cusp (second position).</returns>
    public List<DistanceBetween2Points> ShortestDistanceBetweenPointsAndCusps(IEnumerable<PositionedPoint> allPoints, List<PositionedPoint> allCusps);
}

/// <summary>Helper class for finding aspects for progressions.</summary>
public interface ICheckedProgAspects
{
    /// <summary>Check if a given distance is within orb for one or more aspects.</summary>
    /// <remarks>PRE: Distance positive and distance max 180.0, orb positive and max 30.0,
    /// supportedAspects contains minimal one aspect.</remarks>
    /// <param name="distance">The distance to check.</param>
    /// <param name="orb">The orb to apply.</param>
    /// <param name="supportedAspects">The aspects to check.</param>
    /// <returns>Zero, one or more aspects that are within orb for the given distance.</returns>
    Dictionary<AspectTypes, double> CheckAspects(double distance, double orb, List<AspectTypes> supportedAspects);
}


/// <summary>Selector for points that can be used to calculate aspects.</summary>
public interface IAspectPointSelector
{
    /// <summary>Selects points for aspects.</summary>
    /// <param name="positions">Available chartpoint positions.</param>
    /// <param name="chartPointConfigSpecs">Configuration data for chart points.</param>
    /// <returns>The relevant points for the calculation of aspects.</returns>
    public Dictionary<ChartPoints, FullPointPos> SelectPoints(Dictionary<ChartPoints, FullPointPos> positions, 
           Dictionary<ChartPoints, ChartPointConfigSpecs> chartPointConfigSpecs);
}


/// <summary>Handler for aspects.</summary>
public interface IAspectsHandler
{

    /// <summary>Find aspects between chart points.</summary>
    /// <param name="request">Request with positions.</param>
    /// <returns>Aspects found.</returns>
    public IEnumerable<DefinedAspect> AspectsForChartPoints(AspectRequest request);

    /// <summary>Find aspects between chart points.</summary>
    /// <param name="posPoints">Celestial points with positions.</param>
    /// <param name="cuspPoints">Cusps with positions.</param>
    /// <param name="relevantAspects">Supported aspects as defined in configuration.</param>
    /// <param name="chartPointConfigSpecs">Configuration for chartpoints.</param>
    /// <param name="baseOrb">Base orb for aspects.</param>
    /// <returns>List with aspects between celestial points and between celestial points and cusps. Aspects between cusps are omitted.</returns>
    public List<DefinedAspect> AspectsForPosPoints(List<PositionedPoint> posPoints, List<PositionedPoint> cuspPoints, 
           Dictionary<AspectTypes, AspectConfigSpecs> relevantAspects, 
           Dictionary<ChartPoints, ChartPointConfigSpecs> chartPointConfigSpecs, double baseOrb);
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
    public IEnumerable<BaseMidpoint> RetrieveBaseMidpoints(CalculatedChart chart);

    /// <summary>Retrieve list with all occupied midpoints for a specified dial.</summary>
    /// <param name="chart">Calculated chart with positions.</param>
    /// <param name="dialSize">Degrees for specified dial.</param>
    /// <param name="orb">Base orb from configuration.</param>
    /// <returns>All occupied midpoints.</returns>
    public IEnumerable<OccupiedMidpoint> RetrieveOccupiedMidpoints(CalculatedChart chart, double dialSize, double orb);

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
    public List<double> CalculateHarmonics(IEnumerable<double> originalPositions, double harmonicNumber);

    /// <summary>Calculate harmonics for a single position using a specified harmonic number.</summary>
    /// <param name="originalPosition">OriginalPosition.</param>
    /// <param name="harmonicNumber">The multiplication factor for the harmonic to calculate.</param>
    /// <returns>Value for the harmonic position in the range 0 lt;= value lt; 360.0.</returns>
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
    /// <param name="baseOrb">Base orb from configuration.</param>
    /// <returns>Calculated occupied midpoints.</returns>
    public List<OccupiedMidpoint> CalculateOccupiedMidpoints(CalculatedChart chart, double dialSize, double baseOrb);

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
    public double DefineOrb(ChartPoints point1, ChartPoints point2, double baseOrb, double aspectOrbFactor, Dictionary<ChartPoints, ChartPointConfigSpecs> chartPointConfigSpecs);
}



