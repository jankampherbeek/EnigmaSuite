// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Domain.Analysis;
using Enigma.Domain.Charts;
using Enigma.Domain.Interfaces;

namespace Enigma.Core.Work.Analysis.Midpoints.Interfaces;


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

/// <summary>Create list with all base midpoints.</summary>
public interface IBaseMidpointsCreator
{
    /// <summary>Find basemidpoints for list with AnalysisPoint.</summary>
    /// <param name="analysisPoints">Positions that will be combined to midpoints.</param>
    /// <returns>List with base midpoints.</returns>
    public List<BaseMidpoint> CreateBaseMidpoints(List<AnalysisPoint> analysisPoints);

    /// <summary>Convert list with BaseMidpoint to a list with the same midpoints but with the position converted to a specific dial.</summary>
    /// <param name="baseMidpoints">The original base midpoints.</param>
    /// <param name="dialSize">Degrees of the dial.</param>
    /// <returns>Midpoints with a position that fits in the specified dial.</returns>
    public List<BaseMidpoint> ConvertBaseMidpointsToDial(List<BaseMidpoint> baseMidpoints, double dialSize);
}


/// <summary>Handle analysispoints to be used for the calculation of midpoints.</summary>
public interface IAnalysisPointsForMidpoints
{
    /// <summary>Create analysispoints for a specific dial.</summary>
    /// <param name="chart">Calculated chart.</param>
    /// <param name="dialSize">Dial size in degrees.</param>
    /// <returns>Constructed AnalysisPoints.</returns>
    public List<AnalysisPoint> CreateAnalysisPoints(CalculatedChart chart, double dialSize);
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