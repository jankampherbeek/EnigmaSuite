// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Handlers;
using Enigma.Domain.Dtos;
using Enigma.Domain.Points;
using Enigma.Domain.References;
using Enigma.Domain.Requests;
using Enigma.Domain.Responses;

namespace Enigma.Core.Research;

/// <summary>Counting for OOB positions.</summary>
public interface IOobCounting
{
    /// <summary>Count positions that are Out Of Bound (OOB).</summary>
    /// <param name="charts">The charts to check.</param>
    /// <param name="request">The request.</param>
    /// <returns>Response with the calculated counts.</returns>
    public CountOobResponse CountOob(List<CalculatedResearchChart> charts, GeneralResearchRequest request);
}

//========== Implementation ===================================================================

/// <inheritdoc/>
public class OobCounting: IOobCounting
{
    private readonly IResearchPaths _researchPaths;
    private readonly IFilePersistencyHandler _filePersistencyHandler;
    private readonly IResearchMethodUtils _researchMethodUtils;
    private readonly IPointsMapping _pointsMapping;

    public OobCounting(IResearchPaths researchPaths, 
        IFilePersistencyHandler filePersistencyHandler, 
        IResearchMethodUtils researchMethodUtils,
        IPointsMapping pointsMapping)
    {
        _researchPaths = researchPaths;
        _filePersistencyHandler = filePersistencyHandler;
        _researchMethodUtils = researchMethodUtils;
        _pointsMapping = pointsMapping;
    }
    
    /// <inheritdoc/>
    public CountOobResponse CountOob(List<CalculatedResearchChart> charts, GeneralResearchRequest request)
    {
        return PerformCounts(charts, request);
    }

    private CountOobResponse PerformCounts(List<CalculatedResearchChart> charts, GeneralResearchRequest request)
    {
        return HandleCount(charts, request);
    }
    
    
     private CountOobResponse HandleCount(List<CalculatedResearchChart> charts, GeneralResearchRequest request)
    {
        AstroConfig config = request.Config;
        
        int selectedCelPointSize = request.PointSelection.SelectedPoints.Count;
        int[,] allCounts = new int[selectedCelPointSize, charts.Count];

        int chartIndex = 0;
        foreach (CalculatedResearchChart calcResearchChart in charts)
        {
            double obliquity = calcResearchChart.Obliquity;
            Dictionary<ChartPoints, FullPointPos> relevantChartPointPositions = _researchMethodUtils.DefineSelectedPointPositions(calcResearchChart, request.PointSelection);
            List<PositionedPoint> posPoints = _pointsMapping.MapFullPointPos2PositionedPoint(relevantChartPointPositions, CoordinateSystems.Equatorial, false);
            foreach (PositionedPoint posPoint in posPoints)
            {
                ChartPoints point = posPoint.Point;
                bool oob = Math.Abs(posPoint.Position) > obliquity;
                if (!oob) continue;
                int oobIndex = 0;
                foreach (var rcpPos in relevantChartPointPositions)
                {
                    if (rcpPos.Key == point)
                    {
                        allCounts[oobIndex, chartIndex]++;
                    }
                    oobIndex++;
                }
            }
            chartIndex++;
        }
        List<SimpleCount> resultingCounts = new();
        List<ChartPoints> selectedPoints = request.PointSelection.SelectedPoints;
        int i = 0;
        foreach (var point in selectedPoints)
        {
            int oobCount = 0;
            for (int j = 0; j < charts.Count; j++)
            {
                oobCount += allCounts[i, j];
            }
            resultingCounts.Add(new SimpleCount(point, oobCount));
            i++;
        }
        return new CountOobResponse(request, resultingCounts);
    }
    
    
}