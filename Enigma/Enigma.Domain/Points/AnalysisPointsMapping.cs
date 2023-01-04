// Jan Kampherbeek, (c) 2022, 2023.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Domain.AstronCalculations;
using Enigma.Domain.Charts;
using Enigma.Domain.Enums;
using Enigma.Domain.Interfaces;

namespace Enigma.Domain.Points;


/// <inheritdoc/>
public class AnalysisPointsMapping : IAnalysisPointsMapping
{
    private readonly ICelPointToAnalysisPointMap _celPointMap;
    private readonly IMundanePointToAnalysisPointMap _mundanePointMap;

    public AnalysisPointsMapping(ICelPointToAnalysisPointMap celPointMap, IMundanePointToAnalysisPointMap mundanePointMap)
    {
        _celPointMap = celPointMap;
        _mundanePointMap = mundanePointMap;
    }


    /// <inheritdoc/>
    public List<AnalysisPoint> ChartToSingleAnalysisPoints(List<PointGroups> pointGroups, CoordinateSystems coordinateSystem, bool mainCoord, CalculatedChart chart)
    {
        List<AnalysisPoint> mappedPoints = new();
        foreach (var pointGroup in pointGroups)
        {
            if (pointGroup == PointGroups.CelPoints)
            {
                foreach (var fulCelPointPos in chart.CelPointPositions)
                {
                    mappedPoints.Add(_celPointMap.MapToAnalysisPoint(fulCelPointPos, pointGroup, coordinateSystem, mainCoord));
                }
            }
            if (pointGroup == PointGroups.MundanePoints)
            {
                mappedPoints.Add(_mundanePointMap.MapToAnalysisPoint(MundanePoints.Mc, chart.FullHousePositions.Mc, pointGroup, coordinateSystem, mainCoord));
                mappedPoints.Add(_mundanePointMap.MapToAnalysisPoint(MundanePoints.Ascendant, chart.FullHousePositions.Ascendant, pointGroup, coordinateSystem, mainCoord));
            }
            if (pointGroup == PointGroups.ZodiacalPoints)
            {
                double pos0Aries = 0.0;  // correct value for 0 Aries all ecliptical and equatorial coordinates.
                string glyph = "1";
                mappedPoints.Add(new AnalysisPoint(pointGroup, (int)ZodiacPoints.ZeroAries, pos0Aries, glyph));
            }
        }
        return mappedPoints;
    }

}

public class CelPointToAnalysisPointMap : ICelPointToAnalysisPointMap
{

    public AnalysisPoint MapToAnalysisPoint(FullCelPointPos fullCelPointPos, PointGroups pointGroup, CoordinateSystems coordinateSystem, bool mainCoord)
    {
        double position = 0.0;
        CelPointDetails sspDetails = fullCelPointPos.CelPoint.GetDetails();
        string glyph = sspDetails.DefaultGlyph;
        if (coordinateSystem == CoordinateSystems.Ecliptical)
        {
            position = mainCoord ? fullCelPointPos.GeneralPointPos.Longitude.Position : fullCelPointPos.GeneralPointPos.Latitude.Position;
        }
        if (coordinateSystem == CoordinateSystems.Equatorial)
        {
            position = mainCoord ? fullCelPointPos.GeneralPointPos.RightAscension.Position : fullCelPointPos.GeneralPointPos.Declination.Position;
        }
        int idPoint = (int)fullCelPointPos.CelPoint;
        return new AnalysisPoint(pointGroup, idPoint, position, glyph);
    }
}

public class MundanePointToAnalysisPointMap : IMundanePointToAnalysisPointMap
{
    public AnalysisPoint MapToAnalysisPoint(MundanePoints mundanePoint, CuspFullPos cuspPos, PointGroups pointGroup, CoordinateSystems coordinateSystem, bool mainCoord)
    {
        double position = 0.0;
        string glyph = "";
        if (mundanePoint == MundanePoints.Mc) glyph = "M";
        if (mundanePoint == MundanePoints.Ascendant) glyph = "A";
        if (coordinateSystem == CoordinateSystems.Ecliptical)
        {
            position = mainCoord ? cuspPos.Longitude : 0.0;     // latitude for cusp is always zero.
        }
        if (coordinateSystem == CoordinateSystems.Equatorial)
        {
            position = mainCoord ? cuspPos.RaDecl.RightAscension : cuspPos.RaDecl.Declination;
        }
        int idPoint = (int)mundanePoint;
        return new AnalysisPoint(pointGroup, idPoint, position, glyph);
    }

}