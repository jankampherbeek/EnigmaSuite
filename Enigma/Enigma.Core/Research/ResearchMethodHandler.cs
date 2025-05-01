// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Globalization;
using Enigma.Core.Data;
using Enigma.Core.Persistency;
using Enigma.Core.Research;
using Enigma.Domain.Dtos;
using Enigma.Domain.Exceptions;
using Enigma.Domain.References;
using Enigma.Domain.Requests;
using Enigma.Domain.Responses;
using Serilog;

namespace Enigma.Core.Research;

/// <summary>Handlers for performing research methods.</summary>
public interface IResearchMethodHandler
{
    /// <summary>Start running a test.</summary>
    /// <param name="request">Instance of GeneralResearchRequest or one of its children.</param>
    /// <returns>Results of the test as instance of MethodResponse or one of its children.</returns>
    public MethodResponse HandleResearch(GeneralResearchRequest request);

    /// <summary>Event that reports progress of chart processing.</summary>
    event EventHandler<ChartProgressEventArgs> ChartProgress;
}

/// <summary>Event arguments for chart processing progress.</summary>
public class ChartProgressEventArgs : EventArgs
{
    /// <summary>Number of charts processed so far.</summary>
    public int ProcessedCharts { get; }

    /// <summary>Total number of charts to process.</summary>
    public int TotalCharts { get; }

    /// <summary>Create new instance of ChartProgressEventArgs.</summary>
    /// <param name="processedCharts">Number of charts processed so far.</param>
    /// <param name="totalCharts">Total number of charts to process.</param>
    public ChartProgressEventArgs(int processedCharts, int totalCharts)
    {
        ProcessedCharts = processedCharts;
        TotalCharts = totalCharts;
    }
}

/// <inheritdoc/>
public sealed class ResearchMethodHandler(
    ICsvStandardDataReader csvStandardDataReader,
    ICsvExporter csvExporter,
    ISettingsDao settingsDao,
    ICalculatedResearchPositions researchPositions,
    IPointsInPartsCounting pointsInZodiacPartsCounting,
    IResearchPaths researchPaths,
    IAspectsCounting aspectsCounting,
    IUnaspectedCounting unaspectedCounting,
    IOccupiedMidpointsCounting occupiedMidpointsCounting,
    IHarmonicConjunctionsCounting harmonicConjunctionsCounting,
    IOobCounting oobCounting,
    IOccupiedMidpointsDeclinationCounting occupiedMidpointsDeclinationCounting,
    IDeclinationParallelsCounting declinationParallelsCounting)
    : IResearchMethodHandler
{
    private bool _isProcessing;

    /// <inheritdoc/>
    public event EventHandler<ChartProgressEventArgs>? ChartProgress;

    /// <inheritdoc/>
    public MethodResponse HandleResearch(GeneralResearchRequest request)
    {
        var method = request.Method;
        Log.Information("ResearchMethodHandler HandleResearch, using method {M} for project {P}", method,
            request.ProjectName);

        var fullPath = researchPaths.DataPath(request.ProjectName, request.UseControlGroup);
        Log.Information("Reading csv from path : {Fp}", fullPath);
        var standardInput = csvStandardDataReader.ReadStandardInputData(fullPath);

        const int batchSize = 2000;
        var totalCharts = standardInput.Count;
        var processedCharts = 0;
        List<MethodResponse> orderedResponses = [];

        Log.Information("Starting research with {TotalCharts} charts", totalCharts);
        _isProcessing = true;

        while (processedCharts < totalCharts)
        {
            var remainingCharts = totalCharts - processedCharts;
            var currentBatchSize = Math.Min(batchSize, remainingCharts);
            var batchInput = standardInput.Skip(processedCharts).Take(currentBatchSize).ToList();
            var batchCharts = researchPositions.CalculatePositions(batchInput);
            AddChartsToCsv(batchCharts, request.ProjectName, request.Method, request.UseControlGroup,
                request.PointSelection);
            
            processedCharts += currentBatchSize;
            
            // Only raise progress event if we're processing
            if (_isProcessing)
            {
                ChartProgress?.Invoke(this, new ChartProgressEventArgs(processedCharts, totalCharts));
            }

            var batchResponse = ProcessBatch(request, batchCharts);
            orderedResponses.Add(batchResponse);
        }

        _isProcessing = false;
        // Combine all responses in order
        return CombineOrderedResponses(orderedResponses);
    }

    private MethodResponse ProcessBatch(GeneralResearchRequest request, List<CalculatedResearchChart> batchCharts)
    {
        switch (request)
        {
            case CountHarmonicConjunctionsRequest conjunctionsRequest:
                return harmonicConjunctionsCounting.CountHarmonicConjunctions(batchCharts, conjunctionsRequest);
            case CountOccupiedMidpointsRequest midpointsRequest:
                return occupiedMidpointsCounting.CountMidpoints(batchCharts, midpointsRequest);
            case CountOccupiedMidpointsDeclinationRequest midpointsDeclinationRequest:
                return occupiedMidpointsDeclinationCounting.CountMidpointsInDeclination(batchCharts,
                    midpointsDeclinationRequest);
        }

        switch (request.Method)
        {
            case ResearchMethods.CountUnaspected:
                return unaspectedCounting.CountUnaspected(batchCharts, request);
            case ResearchMethods.CountAspects:
                return aspectsCounting.CountAspects(batchCharts, request);
            case ResearchMethods.CountPosInSigns:
                return pointsInZodiacPartsCounting.CountPointsInParts(batchCharts, request);
            case ResearchMethods.CountPosInHouses:
                return pointsInZodiacPartsCounting.CountPointsInParts(batchCharts, request);
            case ResearchMethods.CountOob:
                return oobCounting.CountOob(batchCharts, request);
            case ResearchMethods.CountDeclinationParallels:
                return declinationParallelsCounting.CountParallels(batchCharts, request);
            case ResearchMethods.CountOccupiedMidpoints:
            case ResearchMethods.CountHarmonicConjunctions:
            case ResearchMethods.CountDeclinationMidpoints:
            default:
                Log.Error("ResearchMethodHandler.ProcessBatch() received an unrecognized request : {Request}", request);
                throw new EnigmaException("Unrecognized ResearchMethod in request for ResearchMethodHandler");
        }
    }

    private void AddChartsToCsv(List<CalculatedResearchChart> charts, string projName, ResearchMethods method,
        bool isControlGroup, ResearchPointSelection selection)
    {
        var workFolder = settingsDao.ReadSetting("workfolder");
        var coord = "Longitude";
        if (method is ResearchMethods.CountOob or ResearchMethods.CountDeclinationMidpoints
            or ResearchMethods.CountDeclinationParallels)
        {
            coord = "Declination";
        }

        var dateTime = DateTime.Now.ToString(CultureInfo.InvariantCulture).Replace("/","-").Replace(" ","-").Replace(":","-");
        var sep = Path.DirectorySeparatorChar;
        var typeOfTest = isControlGroup ? "Control" : "Test";
        var fullPath = workFolder + sep + "projects" + sep + projName + sep + "results" + sep + typeOfTest + "-" 
            + coord + "-" + dateTime + ".csv";

        var persistableCharts = charts.Select(chart => new ResearchPositionsForChart
        {
            Id = chart.InputItem.Id,
            Positions = chart.Positions
                .Where(pos => selection.SelectedPoints.Contains(pos.Key) ||
                              (selection.IncludeCusps && pos.Key.GetDetails().PointCat == PointCats.Cusp))
                .Select(pos => new ResearchPosition
                {
                    Abbrev = pos.Key.GetDetails().Abbr,
                    Position = coord == "Longitude"
                        ? pos.Value.Ecliptical.MainPosSpeed.Position
                        : pos.Value.Equatorial.MainPosSpeed.Position
                }).ToList()
        }).ToList();
        csvExporter.WriteResearchPositionsToCsv(persistableCharts, fullPath, CultureInfo.CurrentCulture);
    }


    private MethodResponse CombineOrderedResponses(List<MethodResponse> orderedResponses)
    {
        switch (orderedResponses.Count)
        {
            case 0:
                throw new EnigmaException("No results were generated");
            case 1:
                return orderedResponses[0];
        }

        // Verify all responses are of the same type
        var responseType = orderedResponses[0].GetType();
        if (orderedResponses.Any(r => r.GetType() != responseType))
        {
            throw new EnigmaException("Cannot combine responses of different types");
        }

        // Combine responses in order
        var combinedResponse = orderedResponses[0];
        for (var i = 1; i < orderedResponses.Count; i++)
        {
            combinedResponse = CombineTwoResponses(combinedResponse, orderedResponses[i]);
        }

        return combinedResponse;
    }

    private static MethodResponse CombineTwoResponses(MethodResponse response1, MethodResponse response2)
    {
        return response1 switch
        {
            CountOfAspectsResponse aspects1 when response2 is CountOfAspectsResponse aspects2 =>
                new CountOfAspectsResponse(
                    aspects1.Request,
                    aspects1.CtrlGroupFactor,
                    CombineArrays(aspects1.AllCounts, aspects2.AllCounts),
                    CombineArrays(aspects1.TotalsPerPointCombi, aspects2.TotalsPerPointCombi),
                    CombineArrays(aspects1.TotalsPerAspect, aspects2.TotalsPerAspect),
                    aspects1.PointsUsed,
                    aspects1.AspectsUsed),

            CountOfParallelsResponse parallels1 when response2 is CountOfParallelsResponse parallels2 =>
                new CountOfParallelsResponse(
                    parallels1.Request,
                    parallels1.CtrlGroupFactor,
                    CombineArrays(parallels1.AllCounts, parallels2.AllCounts),
                    CombineArrays(parallels1.TotalsPerPointCombi, parallels2.TotalsPerPointCombi),
                    CombineArrays(parallels1.TotalsPerAspect, parallels2.TotalsPerAspect),
                    parallels1.PointsUsed),

            CountOfPartsResponse parts1 when response2 is CountOfPartsResponse parts2 =>
                new CountOfPartsResponse(
                    parts1.Request,
                    parts1.CtrlGroupFactor,
                    CombineCountOfParts(parts1.Counts, parts2.Counts),
                    CombineArrays(parts1.Totals, parts2.Totals)),

            CountOfUnaspectedResponse unaspected1 when response2 is CountOfUnaspectedResponse unaspected2 =>
                new CountOfUnaspectedResponse(
                    unaspected1.Request,
                    unaspected1.CtrlGroupFactor,
                    CombineSimpleCounts(unaspected1.Counts, unaspected2.Counts)),

            CountOfOccupiedMidpointsResponse midpoints1 when response2 is CountOfOccupiedMidpointsResponse midpoints2 =>
                new CountOfOccupiedMidpointsResponse(
                    midpoints1.Request,
                    midpoints1.CtrlGroupFactor,
                    CombineDictionary(midpoints1.AllCounts, midpoints2.AllCounts)),

            CountOfOccupiedMidpointsDeclResponse declMidpoints1 when
                response2 is CountOfOccupiedMidpointsDeclResponse declMidpoints2 =>
                new CountOfOccupiedMidpointsDeclResponse(
                    declMidpoints1.Request,
                    declMidpoints1.CtrlGroupFactor,
                    CombineDictionary(declMidpoints1.AllCounts, declMidpoints2.AllCounts)),

            CountHarmonicConjunctionsResponse conjunctions1 when
                response2 is CountHarmonicConjunctionsResponse conjunctions2 =>
                new CountHarmonicConjunctionsResponse(
                    conjunctions1.Request,
                    conjunctions1.CtrlGroupFactor,
                    CombineDictionary(conjunctions1.AllCounts, conjunctions2.AllCounts)),

            CountOobResponse oob1 when response2 is CountOobResponse oob2 =>
                new CountOobResponse(
                    oob1.Request,
                    oob1.CtrlGroupFactor,
                    CombineSimpleCounts(oob1.Counts, oob2.Counts)),

            _ => throw new EnigmaException($"Combination not implemented for response type {response1.GetType()}")
        };
    }

    private static int[,,] CombineArrays(int[,,] array1, int[,,] array2)
    {
        var dim1 = array1.GetLength(0);
        var dim2 = array1.GetLength(1);
        var dim3 = array1.GetLength(2);
        var result = new int[dim1, dim2, dim3];

        for (var i = 0; i < dim1; i++)
        {
            for (var j = 0; j < dim2; j++)
            {
                for (var k = 0; k < dim3; k++)
                {
                    result[i, j, k] = array1[i, j, k] + array2[i, j, k];
                }
            }
        }

        return result;
    }

    private static int[,] CombineArrays(int[,] array1, int[,] array2)
    {
        var dim1 = array1.GetLength(0);
        var dim2 = array1.GetLength(1);
        var result = new int[dim1, dim2];

        for (var i = 0; i < dim1; i++)
        {
            for (var j = 0; j < dim2; j++)
            {
                result[i, j] = array1[i, j] + array2[i, j];
            }
        }

        return result;
    }

    private static int[] CombineArrays(int[] array1, int[] array2)
    {
        var result = new int[array1.Length];
        for (var i = 0; i < array1.Length; i++)
        {
            result[i] = array1[i] + array2[i];
        }

        return result;
    }

    private static List<int> CombineArrays(List<int> list1, List<int> list2)
    {
        var result = new List<int>(list1.Count);
        for (var i = 0; i < list1.Count; i++)
        {
            result.Add(list1[i] + list2[i]);
        }

        return result;
    }

    private static List<CountOfParts> CombineCountOfParts(List<CountOfParts> parts1, List<CountOfParts> parts2)
    {
        var result = new List<CountOfParts>();
        for (var i = 0; i < parts1.Count; i++)
        {
            var combinedCounts = new List<int>();
            for (var j = 0; j < parts1[i].Counts.Count; j++)
            {
                combinedCounts.Add(parts1[i].Counts[j] + parts2[i].Counts[j]);
            }

            result.Add(new CountOfParts(parts1[i].Point, combinedCounts));
        }

        return result;
    }

    private static List<SimpleCount> CombineSimpleCounts(List<SimpleCount> counts1, List<SimpleCount> counts2)
    {
        var result = new List<SimpleCount>();
        for (var i = 0; i < counts1.Count; i++)
        {
            result.Add(new SimpleCount(counts1[i].Point, counts1[i].Count + counts2[i].Count));
        }

        return result;
    }

    private static Dictionary<T, int> CombineDictionary<T>(Dictionary<T, int> dict1, Dictionary<T, int> dict2)
        where T : notnull
    {
        var result = new Dictionary<T, int>(dict1);
        foreach (var kvp in dict2)
        {
            if (result.ContainsKey(kvp.Key))
            {
                result[kvp.Key] += kvp.Value;
            }
            else
            {
                result[kvp.Key] = kvp.Value;
            }
        }

        return result;
    }
}