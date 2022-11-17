// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Domain.AstronCalculations;
using Enigma.Domain.Charts;
using Enigma.Domain.Enums;
using Enigma.Domain.Interfaces;

namespace Enigma.Domain.Analysis;


/// <inheritdoc/>
public class AnalysisPointsMapping : IAnalysisPointsMapping
{
    private readonly ISolSysPointToAnalysisPointMap _solSysPointMap;
    private readonly IMundanePointToAnalysisPointMap _mundanePointMap;

    public AnalysisPointsMapping(ISolSysPointToAnalysisPointMap solSysPointMap, IMundanePointToAnalysisPointMap mundanePointMap)
    {
        _solSysPointMap = solSysPointMap;
        _mundanePointMap = mundanePointMap;
    }


    /// <inheritdoc/>
    public List<AnalysisPoint> ChartToSingLeAnalysisPoints(List<PointGroups> pointGroups, CoordinateSystems coordinateSystem, bool mainCoord, CalculatedChart chart)
    {
        List<AnalysisPoint> mappedPoints = new();
        foreach (var pointGroup in pointGroups)
        {
            if (pointGroup == PointGroups.SolarSystemPoints)
            {
                foreach (var fullSolSysPointPos in chart.SolSysPointPositions)
                {
                    mappedPoints.Add(_solSysPointMap.MapToAnalysisPoint(fullSolSysPointPos, pointGroup, coordinateSystem, mainCoord));
                }
            }
            if (pointGroup == PointGroups.MundanePoints)
            {
                mappedPoints.Add(_mundanePointMap.MapToAnalysisPoint(MundanePoints.Mc, chart.FullHousePositions.Mc, pointGroup, coordinateSystem, mainCoord));
                mappedPoints.Add(_mundanePointMap.MapToAnalysisPoint(MundanePoints.Ascendant, chart.FullHousePositions.Ascendant, pointGroup, coordinateSystem, mainCoord));
            }
            if (pointGroup != PointGroups.ZodiacalPoints)
            {
                double pos0Aries = 0.0;  // correct value for 0 Aries all ecliptical and equatorial coordinates.
                mappedPoints.Add(new AnalysisPoint(pointGroup, (int)ZodiacalPoints.ZeroAries, pos0Aries));
            }
        }
        return mappedPoints;
    }

}

public class SolSysPointToAnalysisPointMap : ISolSysPointToAnalysisPointMap
{
    public AnalysisPoint MapToAnalysisPoint(FullSolSysPointPos solSysPoint, PointGroups pointGroup, CoordinateSystems coordinateSystem, bool mainCoord)
    {
        double position = 0.0;
        if (coordinateSystem == CoordinateSystems.Ecliptical)
        {
            position = mainCoord ? solSysPoint.Longitude.Position : solSysPoint.Latitude.Position;
        }
        if (coordinateSystem == CoordinateSystems.Equatorial)
        {
            position = mainCoord ? solSysPoint.RightAscension.Position : solSysPoint.Declination.Position;
        }
        int idPoint = (int)solSysPoint.SolarSystemPoint;
        return new AnalysisPoint(pointGroup, idPoint, position);
    }
}

public class MundanePointToAnalysisPointMap : IMundanePointToAnalysisPointMap
{
    public AnalysisPoint MapToAnalysisPoint(MundanePoints mundanePoint, CuspFullPos cuspPos, PointGroups pointGroup, CoordinateSystems coordinateSystem, bool mainCoord)
    {
        double position = 0.0;
        if (coordinateSystem == CoordinateSystems.Ecliptical)
        {
            position = mainCoord ? cuspPos.Longitude : 0.0;     // latitude for cusp is always zero.
        }
        if (coordinateSystem == CoordinateSystems.Equatorial)
        {
            position = mainCoord ? cuspPos.RaDecl.RightAscension : cuspPos.RaDecl.Declination;
        }
        int idPoint = (int)mundanePoint;
        return new AnalysisPoint(pointGroup, idPoint, position);
    }

}