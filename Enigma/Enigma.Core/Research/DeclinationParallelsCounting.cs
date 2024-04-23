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

/// <summary>Counting for declination parallels.</summary>
public interface IDeclinationParallelsCounting
{
    /// <summary>Perform a count of declination parallels.</summary>
    /// <param name="charts">The calculated charts to check.</param>
    /// <param name="request">The original request.</param>
    /// <returns>The calculated counts.</returns>
    public CountOfParallelsResponse CountParallels(List<CalculatedResearchChart> charts, GeneralResearchRequest request);
}

// ===================== Implementation ==========================================

/// <inheritdoc/>
public class DeclinationParallelsCounting: IDeclinationParallelsCounting
{
    private readonly IPointsMapping _pointsMapping;
    private readonly IResearchMethodUtils _researchMethodUtils;
    private readonly IParallelsHandler _parallelsHandler;


    public DeclinationParallelsCounting(
        IPointsMapping pointsMapping, 
        IResearchMethodUtils researchMethodUtils,
        IParallelsHandler parallelsHandler)
    {
        _pointsMapping = pointsMapping;
        _researchMethodUtils = researchMethodUtils;
        _parallelsHandler = parallelsHandler;
    }
    
    /// <inheritdoc/>
    public CountOfParallelsResponse CountParallels(List<CalculatedResearchChart> charts, GeneralResearchRequest request)
    {
        return PerformCount(charts, request);
    }
    
    
    private CountOfParallelsResponse PerformCount(List<CalculatedResearchChart> charts, GeneralResearchRequest request)
    {
        AstroConfig config = request.Config;

        Dictionary<ChartPoints, ChartPointConfigSpecs> chartPointConfigSpecs = config.ChartPoints;
        int celPointSize = chartPointConfigSpecs.Count;
        int selectedCelPointSize = 0;
        int parallelSize = 2;    // parallel and contra-parallel
        int[,,] allCounts = new int[celPointSize, celPointSize, parallelSize];
        List<PositionedPoint> allPoints = new();

        foreach (CalculatedResearchChart calcResearchChart in charts)
        {
            Dictionary<ChartPoints, FullPointPos> relevantChartPointPositions = 
                _researchMethodUtils.DefineSelectedPointPositions(calcResearchChart, request.PointSelection);
            List<PositionedPoint> posPoints = 
                _pointsMapping.MapFullPointPos2PositionedPoint(
                    relevantChartPointPositions, CoordinateSystems.Equatorial, false);
            selectedCelPointSize = relevantChartPointPositions.Count;
            allPoints = new List<PositionedPoint>(posPoints.Count);
            allPoints.AddRange(posPoints);



            double orb = 1.0;    // todo use orb from config
            IEnumerable<DefinedParallel> definedParallels =
                _parallelsHandler.ParallelsForPosPoints(allPoints, chartPointConfigSpecs, orb);
            foreach (DefinedParallel defParallel in definedParallels)
            {
                int index1 = _researchMethodUtils.FindIndexForPoint(defParallel.Point1, allPoints);
                int index2 = _researchMethodUtils.FindIndexForPoint(defParallel.Point2, allPoints);
                int index3 = defParallel.OppParallel ? 1 : 0;
                allCounts[index1, index2, index3] += 1;
            }
        }
        return CreateResponse(request, selectedCelPointSize, allCounts, allPoints);
    }



    private static CountOfParallelsResponse CreateResponse(GeneralResearchRequest request, int selectedCelPointSize,
        int[,,] allCounts, IReadOnlyCollection<PositionedPoint> posPoints)
    {
        List<ChartPoints> chartPoints = posPoints.Select(posPoint => posPoint.Point).ToList();
        int[,] totalsPerPointCombi = new int[posPoints.Count, posPoints.Count];
        int[] totalsPerParallel = new int[2];             // parallel and contra-parallel

        for (int i = 0; i < selectedCelPointSize; i++)
        {
            for (int j = 0; j < posPoints.Count; j++)
            {
                int total = 0;
                for (int k = 0; k < 2; k++)        
                {
                    total += allCounts[i, j, k];
                    totalsPerParallel[k] += allCounts[i, j, k];
                }
                totalsPerPointCombi[i, j] += total;
            }
        }
        return new CountOfParallelsResponse(request, allCounts, totalsPerPointCombi, totalsPerParallel, chartPoints);
    }
    
    
    
}