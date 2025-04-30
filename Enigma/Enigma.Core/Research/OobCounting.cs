// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Handlers;
using Enigma.Core.Persistency;
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


/// <inheritdoc/>
public sealed class OobCounting(
    IResearchPaths researchPaths,
    IFilePersistencyHandler filePersistencyHandler,
    IResearchMethodUtils researchMethodUtils,
    IPointsMapping pointsMapping,
    IProjectDao projectDao)
    : IOobCounting
{
    private readonly IResearchPaths _researchPaths = researchPaths;
    private readonly IFilePersistencyHandler _filePersistencyHandler = filePersistencyHandler;

    /// <inheritdoc/>
    public CountOobResponse CountOob(List<CalculatedResearchChart> charts, GeneralResearchRequest request)
    {
        return PerformCounts(charts, request);
    }

    private CountOobResponse PerformCounts(List<CalculatedResearchChart> charts, GeneralResearchRequest request)
    {
        var ctrlGroupFactor = projectDao.ReadProject(request.ProjectName)!.MultiFactor;
        var selectedCelPointSize = request.PointSelection.SelectedPoints.Count;
        var allCounts = new int[selectedCelPointSize, charts.Count];
        var chartIndex = 0;
        foreach (var calcResearchChart in charts)
        {
            var obliquity = calcResearchChart.Obliquity;
            var relevantChartPointPositions = researchMethodUtils.DefineSelectedPointPositions(calcResearchChart, request.PointSelection);
            var posPoints = pointsMapping.MapFullPointPos2PositionedPoint(relevantChartPointPositions, CoordinateSystems.Equatorial, false);
            foreach (var (point, position) in posPoints)
            {
                var oob = Math.Abs(position) > obliquity;
                if (!oob) continue;
                var oobIndex = 0;
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
        var selectedPoints = request.PointSelection.SelectedPoints;
        var i = 0;
        foreach (var point in selectedPoints)
        {
            var oobCount = 0;
            for (var j = 0; j < charts.Count; j++)
            {
                oobCount += allCounts[i, j];
            }
            resultingCounts.Add(new SimpleCount(point, oobCount));
            i++;
        }
        return new CountOobResponse(request, ctrlGroupFactor, resultingCounts);
    }
    
}