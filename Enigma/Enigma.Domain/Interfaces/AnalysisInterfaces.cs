// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Domain.Analysis;
using Enigma.Domain.AstronCalculations;
using Enigma.Domain.Charts;
using Enigma.Domain.Enums;

namespace Enigma.Domain.Interfaces;

public interface IOrbDefinitions
{
    public SolSysPointOrb DefineSolSysPointOrb(SolarSystemPoints solSysPoint);
    public MundanePointOrb DefineMundanePointOrb(string mundanePoint);
}


/// <summary>
/// Mapping to support analysis.
/// </summary>
public interface IAnalysisPointsMapping
{
    /// <summary>
    /// Maps values from a calculated chart to a list of AnalysisPoint.
    /// </summary>
    /// <remarks>Does not yet support fix stars, or horizontal coordiantes. 
    /// For mundane positions only supports Mc and Asc.
    /// For zodiacal points only supports Zero Aries.</remarks>
    public List<AnalysisPoint> ChartToSingLeAnalysisPoints(List<PointGroups> pointGroups, CoordinateSystems coordinateSystem, bool mainCoord, CalculatedChart chart);
}

public interface ISolSysPointToAnalysisPointMap
{
    public AnalysisPoint MapToAnalysisPoint(FullSolSysPointPos solSysPoint, PointGroups pointGroup, CoordinateSystems coordinateSystem, bool mainCoord);
}

public interface IMundanePointToAnalysisPointMap
{
    public AnalysisPoint MapToAnalysisPoint(MundanePoints mundanePoint, CuspFullPos cuspPos, PointGroups pointGroup, CoordinateSystems coordinateSystem, bool mainCoord);
}

