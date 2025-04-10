// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Research;
using Enigma.Domain.Dtos;
using Enigma.Domain.Exceptions;
using Enigma.Domain.References;
using Enigma.Domain.Requests;
using Enigma.Domain.Responses;
using Newtonsoft.Json;
using Serilog;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace Enigma.Core.Handlers;

/// <summary>Handlers for performing research methods.</summary>
public interface IResearchMethodHandler
{
    /// <summary>Start running a test.</summary>
    /// <param name="request">Instance of GeneralResearchRequest or one of its children.</param>
    /// <returns>Results of the test as instance of MethodResponse or one of its children.</returns>
    public MethodResponse HandleResearch(GeneralResearchRequest request);

}

/// <inheritdoc/>
public sealed class ResearchMethodHandler(
    ICalculatedResearchPositions researchPositions,
    IPointsInPartsCounting pointsInZodiacPartsCounting,
    IFilePersistencyHandler filePersistencyHandler,
    IResearchDataHandler researchDataHandler,
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
    /// <inheritdoc/>
    public MethodResponse HandleResearch(GeneralResearchRequest request)
    {
        var method = request.Method;
        Log.Information("ResearchMethodHandler HandleResearch, using method {M} for project {P}", method, request.ProjectName);
        var allCalculatedResearchCharts = CalculateAllCharts(request.ProjectName, request.UseControlGroup);
        WriteCalculatedChartsToJson(request.ProjectName, method.ToString(), request.UseControlGroup, allCalculatedResearchCharts);
        switch (request)
        {
            case CountHarmonicConjunctionsRequest conjunctionsRequest:
                return harmonicConjunctionsCounting.CountHarmonicConjunctions(allCalculatedResearchCharts, conjunctionsRequest); 
            case CountOccupiedMidpointsRequest midpointsRequest:
                return occupiedMidpointsCounting.CountMidpoints(allCalculatedResearchCharts, midpointsRequest);
            case CountOccupiedMidpointsDeclinationRequest midpointsDeclinationRequest:
                return occupiedMidpointsDeclinationCounting.CountMidpointsInDeclination(allCalculatedResearchCharts, midpointsDeclinationRequest );
                
        }
        // ReSharper disable once SwitchStatementHandlesSomeKnownEnumValuesWithDefault
        switch (method)
        {
            case ResearchMethods.CountUnaspected:
                return unaspectedCounting.CountUnaspected(allCalculatedResearchCharts, request);
            case ResearchMethods.CountAspects:
                return aspectsCounting.CountAspects(allCalculatedResearchCharts, request);
            case ResearchMethods.CountPosInSigns:
                return pointsInZodiacPartsCounting.CountPointsInParts(allCalculatedResearchCharts, request);
            case ResearchMethods.CountPosInHouses:
                return pointsInZodiacPartsCounting.CountPointsInParts(allCalculatedResearchCharts, request);
            case ResearchMethods.CountOob:
                return oobCounting.CountOob(allCalculatedResearchCharts, request);
            case ResearchMethods.CountDeclinationParallels:
                return declinationParallelsCounting.CountParallels(allCalculatedResearchCharts, request);
            default:
                Log.Error("ResearchMethodHandler.HandleResearch() received an unrecognized request : {Request}", request);
                throw new EnigmaException("Unrecognized ResearchMethod in request for ResearchMethodHandler");
        }
    }

    private List<CalculatedResearchChart> CalculateAllCharts(string projectName, bool controlGroup)
    {
        List<CalculatedResearchChart> allCalculatedResearchCharts = [];
        var fullPath = researchPaths.DataPath(projectName, controlGroup);
        Log.Information("Reading Json from path : {Fp}", fullPath);
        var json = filePersistencyHandler.ReadFile(fullPath);
        if (json != "")
        {
            var standardInput = researchDataHandler.GetStandardInputFromJson(json);
            allCalculatedResearchCharts = researchPositions.CalculatePositions(standardInput);
            Log.Information("Calculation completed");            
        }
        else
        {
            Log.Error("Could not find data for project {ProjName}", projectName);
        }
        return allCalculatedResearchCharts;
    }

    private void WriteCalculatedChartsToJson(string projectName, string methodName, bool controlGroup,
        List<CalculatedResearchChart> allCharts)
    {
        var pathForResults = researchPaths.ResultPath(projectName, methodName, controlGroup);
        using (var stream = new FileStream(pathForResults, FileMode.Create))
        using (var writer = new StreamWriter(stream))
        using (var jsonWriter = new JsonTextWriter(writer))
        {
            jsonWriter.Formatting = Formatting.None;
            var serializer = new JsonSerializer();
            serializer.Serialize(jsonWriter, allCharts);
        }

        Log.Information("Json with positions written to {Path}", pathForResults);
    }
}

