// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enigma.Api.Interfaces;
using Enigma.Domain.Constants;
using Enigma.Domain.Dtos;
using Enigma.Domain.Exceptions;
using Enigma.Domain.References;
using Enigma.Domain.Requests;
using Enigma.Domain.Research;
using Enigma.Domain.Responses;
using Enigma.Frontend.Ui.State;
using Serilog;

namespace Enigma.Frontend.Ui.Models;

/// <summary>Model for research result</summary>
public class ResearchResultModel
{
    private const int COLUMN_SIZE = 7;
    private const int LARGE_COLUMN_SIZE = 20;
    private const int START_COLUMN_ASPECTS_SIZE = 50;
    private const int MAX_LINE_SIZE = 104;
    private const int MIDPOINT_SEPARATOR_SIZE = 80;
    private const string SPACES = "                    "; // 20 spaces
    private const string SEPARATOR_LINE = "--------------------------------------------------"; // 50 positions
    private const string HEADER_SIGNS =
        "                    ARI    TAU    GEM    CAN    LEO    VIR    LIB    SCO    SAG    CAP    AQU    PIS";
    private const string HEADER_HOUSES =
        "                    1      2      3      4      5      6      7      8      9      10     11     12 ";
    private const string HARMONIC_CONJUNCTIONS = "Harmonic conjunctions. Harmonic number: ";
    private const string OCCUPIED_MIDPOINTS = "Occupied midpoints for dial division";
    private const string CHARTS_WITHOUT_ASPECTS = "Number of charts without aspects";
    private const string ASPECT_TOTALS = "Totals of aspects";
    private const string ORB = "Orb";
    private const string RESULTS_SAVED_AT = "These results have been saved at : ";
 
    private readonly DataVaultResearch _dataVaultResearch = DataVaultResearch.Instance;
    private readonly IFileAccessApi _fileAccessApi;
    private readonly IResearchPathApi _researchPathApi;
    
    public string ProjectName { get; } = string.Empty;
    public string MethodName { get; }
    public string TestResult { get; private set; } = string.Empty;
    public string ControlResult { get; private set; } = string.Empty;
    private string TestResultText { get; set; } = string.Empty;
    private string ControlResultText { get; set; } = string.Empty;
    
    public ResearchResultModel(IFileAccessApi fileAccessApi, IResearchPathApi researchPathApi)
    {
        _fileAccessApi = fileAccessApi;
        _researchPathApi = researchPathApi;
        ResearchProject? project = _dataVaultResearch.CurrentProject;
        if (project != null)
        {
            ProjectName = project.Name;     
        }
        ResearchMethods method = _dataVaultResearch.ResearchMethod;
        MethodName = method.GetDetails().Text;
        MethodResponse? responseTest = _dataVaultResearch.ResponseTest;
        MethodResponse? responseCg = _dataVaultResearch.ResponseCg;
        if (responseTest != null && responseCg != null)
        {
            SetMethodResponses(responseTest, responseCg);
        }


    }

    private void SetMethodResponses(MethodResponse responseTest, MethodResponse responseControl)
    {
        switch (responseTest)
        {
            case CountOfAspectsResponse:
                DefineAspectsResultTexts(responseTest, responseControl);
                break;
            case CountOfUnaspectedResponse:
                DefineUnaspectedResultTexts(responseTest, responseControl);
                break;
            case CountOfOccupiedMidpointsResponse:
                DefineOccupiedMidpointsResultTexts(responseTest, responseControl);
                break;
            case CountHarmonicConjunctionsResponse:
                DefineHarmonicConjunctionsResultTexts(responseTest, responseControl);
                break;
            case CountOfPartsResponse:
                DefinePartsResultTexts(responseTest, responseControl);
                break;
        }
    }


    private void DefineUnaspectedResultTexts(MethodResponse responseTest, MethodResponse responseControl)
    {
        TestResultText = CreateUnaspectedResultData(responseTest);
        ControlResultText = CreateUnaspectedResultData(responseControl);
        CreateResultHeaders(responseTest.Request);
        WriteResults(responseTest.Request);
    }

    private void DefineAspectsResultTexts(MethodResponse responseTest, MethodResponse responseControl)
    {
        TestResultText = CreateAspectsResultData(responseTest);
        ControlResultText = CreateAspectsResultData(responseControl);
        CreateResultHeaders(responseTest.Request);
        WriteResults(responseTest.Request);
    }

    private void DefineOccupiedMidpointsResultTexts(MethodResponse responseTest, MethodResponse responseControl)
    {
        TestResultText = CreateOccupiedMidpointsResultData(responseTest);
        ControlResultText = CreateOccupiedMidpointsResultData(responseControl);
        CreateResultHeaders(responseTest.Request);
        WriteResults(responseTest.Request);
    }

    private void DefineHarmonicConjunctionsResultTexts(MethodResponse responseTest, MethodResponse responseControl)
    {
        TestResultText = CreateHarmonicConjunctionsResultData(responseTest);
        ControlResultText = CreateHarmonicConjunctionsResultData(responseControl);
        CreateResultHeaders(responseTest.Request);
        WriteResults(responseTest.Request);
    }


    private void DefinePartsResultTexts(MethodResponse responseTest, MethodResponse responseControl)
    {
        TestResultText = CreatePartsResultData(responseTest);
        ControlResultText = CreatePartsResultData(responseControl);
        CreateResultHeaders(responseTest.Request);
        WriteResults(responseTest.Request);
    }

    private void WriteResults(GeneralResearchRequest request)
    {
        string projName = request.ProjectName;
        string methodName = request.Method.ToString();
        bool useControlGroup = false;
        string pathTest = _researchPathApi.SummedResultsPath(projName, methodName, useControlGroup);
        useControlGroup = true;
        string pathControl = _researchPathApi.SummedResultsPath(projName, methodName, useControlGroup);
        TestResultText += EnigmaConstants.NEW_LINE + RESULTS_SAVED_AT + EnigmaConstants.NEW_LINE + pathTest;
        ControlResultText += EnigmaConstants.NEW_LINE + RESULTS_SAVED_AT + EnigmaConstants.NEW_LINE + pathControl;
        _fileAccessApi.WriteFile(pathTest, TestResultText);
        _fileAccessApi.WriteFile(pathControl, ControlResultText);
        TestResult = TestResultText;
        ControlResult = ControlResultText;
    }


    private static void CreateResultHeaders(GeneralResearchRequest request)
    {
        _ = request.Method.GetDetails().Text;
    }



    private static string CreatePartsResultData(MethodResponse response)
    {
        StringBuilder resultData = new();
        if (response is CountOfPartsResponse(_, var countOfParts, var totals))
        {
            string headerLine = string.Empty;
            string longSeparatorLine = (SEPARATOR_LINE + SEPARATOR_LINE + SEPARATOR_LINE)[..MAX_LINE_SIZE];

            headerLine = response.Request.Method switch
            {
                ResearchMethods.CountPosInSigns => HEADER_SIGNS,
                ResearchMethods.CountPosInHouses => HEADER_HOUSES,
                _ => headerLine
            };
            resultData.AppendLine(headerLine);
            resultData.AppendLine(longSeparatorLine);

            foreach (CountOfParts cop in countOfParts)
            {
                string name = cop.Point + SPACES;
                resultData.Append(name[..LARGE_COLUMN_SIZE]);
                foreach (int count in cop.Counts)
                {
                    resultData.Append((count + SPACES)[..COLUMN_SIZE]);
                }
                resultData.AppendLine();
            }
            resultData.AppendLine(longSeparatorLine);
            resultData.Append(SPACES);
            foreach (int total in totals)
            {
                resultData.Append((total + SPACES)[..COLUMN_SIZE]);
            }
            resultData.AppendLine();
        }
        else
        {
            Log.Error("ResearchResultModel.CreatePartsResultData() used a wrong response : {Response}", response);
            throw new EnigmaException("Wrong response in ResearchResultModel.CreatePartsResultData");
        }
        return resultData.ToString();
    }


    private static string CreateAspectsResultData(MethodResponse response)
    {
        StringBuilder resultData = new();
        if (response is CountOfAspectsResponse(var generalResearchRequest, var allCounts, 
            var totalsPerPointCombi, var totalsPerAspect, var chartPointsList, var aspectTypesList))
        {
            string aspectSpaces = (SPACES + SPACES + SPACES)[..START_COLUMN_ASPECTS_SIZE];
            string aspectSeparatorLine = SEPARATOR_LINE;
            string separatorFragment = SEPARATOR_LINE[..COLUMN_SIZE];
            StringBuilder headerLine = new();
            headerLine.Append(aspectSpaces);
            foreach (AspectTypes asp in aspectTypesList)
            {
                headerLine.Append((((int)asp.GetDetails().Angle) + SPACES)[..COLUMN_SIZE]);
                aspectSeparatorLine += separatorFragment;
            }
            headerLine.Append(ASPECT_TOTALS);
            aspectSeparatorLine += separatorFragment;
            resultData.AppendLine(headerLine.ToString());
            resultData.AppendLine(aspectSeparatorLine);
            StringBuilder detailLine;
            int nrOfCelPoints = chartPointsList.Count(item => item.GetDetails().PointCat != PointCats.Cusp);
            for (int i = 0; i < nrOfCelPoints; i++)
            {
                for (int j = i + 1; j < chartPointsList.Count; j++)
                {
                    detailLine = new StringBuilder();
                    detailLine.Append(chartPointsList[i].GetDetails().Text.PadRight(25));
                    detailLine.Append(chartPointsList[j].GetDetails().Text.PadRight(25));
                    for (int k = 0; k < aspectTypesList.Count; k++)
                    {
                        detailLine.Append((allCounts[i, j, k] + SPACES)[..COLUMN_SIZE]);
                    }
                    detailLine.Append((totalsPerPointCombi[i, j] + SPACES)[..COLUMN_SIZE]);
                    resultData.AppendLine(detailLine.ToString());
                }
            }
            int totalOverall = 0;
            detailLine = new StringBuilder();
            detailLine.Append(("Totals of aspects" + SPACES + SPACES + SPACES)[..START_COLUMN_ASPECTS_SIZE]);
            foreach (int count in totalsPerAspect)
            {
                detailLine.Append((count + SPACES)[..COLUMN_SIZE]);
                totalOverall += count;
            }
            detailLine.Append((totalOverall + SPACES)[..COLUMN_SIZE]);
            resultData.AppendLine(aspectSeparatorLine);
            resultData.AppendLine(detailLine.ToString());
        }
        else
        {
            Log.Error("ResearchResultModel.CreateAspectsResultData() used a wrong response : {Response}", response);
            throw new EnigmaException("Wrong result in ResearchResultModel");
        }
        return resultData.ToString();
    }


    private static string CreateUnaspectedResultData(MethodResponse response)
    {
        StringBuilder resultData = new();
        if (response is CountOfUnaspectedResponse qualifiedResponse)
        {
            resultData.AppendLine(CHARTS_WITHOUT_ASPECTS);
            resultData.AppendLine(SEPARATOR_LINE);
            foreach (SimpleCount simpleCount in qualifiedResponse.Counts)
            {
                resultData.AppendLine((simpleCount.Point.GetDetails().Text + SPACES)[..LARGE_COLUMN_SIZE] + simpleCount.Count);
            }
        }
        else
        {
            Log.Error("ResearchResultController.CreateUnaspectedResultData() used a wrong response : {Response}", response);
            throw new EnigmaException("ResearchResultController.CreateUnaspectedResultData() used a wrong response");
        }
        return resultData.ToString();
    }

    private static string CreateOccupiedMidpointsResultData(MethodResponse response)
    {
        StringBuilder resultData = new();
        if (response is CountOfOccupiedMidpointsResponse qualifiedResponse)
        {
            if (response.Request is CountOccupiedMidpointsRequest qualifiedRequest)
            {
                resultData.AppendLine(OCCUPIED_MIDPOINTS + " " + qualifiedRequest.DivisionForDial 
                                      + ". " + ORB + ": " + qualifiedRequest.Orb);
                resultData.AppendLine((SEPARATOR_LINE + SEPARATOR_LINE)[..MIDPOINT_SEPARATOR_SIZE]);
                Dictionary<OccupiedMidpointStructure, int> allCounts = qualifiedResponse.AllCounts;
                foreach (KeyValuePair<OccupiedMidpointStructure, int> midpoint in allCounts)
                {
                    if (midpoint.Value <= 0) continue;
                    string firstPointName = midpoint.Key.FirstPoint.GetDetails().Text;
                    string secondPointName = midpoint.Key.SecondPoint.GetDetails().Text;
                    string occPointName = midpoint.Key.OccupyingPoint.GetDetails().Text;
                    string midpointCount = midpoint.Value.ToString();
                    resultData.AppendLine((firstPointName + SPACES)[..LARGE_COLUMN_SIZE] + " / " + (secondPointName + SPACES)[..COLUMN_SIZE] 
                                          + " = " + (occPointName + SPACES)[..COLUMN_SIZE] + " " + midpointCount);
                }
            }
            else
            {
                Log.Error("ResearchResultModel.CreateOccupiedMidpointsResultData() used a wrong request : {Request}", response.Request);
                throw new EnigmaException("Wrong request in ResearchResultModel");
            }
        }
        else
        {
            Log.Error("ResearchResultModel.CreateOccupiedMidpointsResultData() used a wrong response : {Response}", response);
            throw new EnigmaException("Wrong response in ResearchResultModel");
        }
        return resultData.ToString();
    }

    private static string CreateHarmonicConjunctionsResultData(MethodResponse response)
    {
        StringBuilder resultData = new();
        if (response is CountHarmonicConjunctionsResponse qualifiedResponse)
        {
            if (response.Request is CountHarmonicConjunctionsRequest qualifiedRequest)
            {
                resultData.AppendLine(HARMONIC_CONJUNCTIONS + qualifiedRequest.HarmonicNumber
                    + ". " + ORB + ": " + qualifiedRequest.Orb);
                resultData.AppendLine(SEPARATOR_LINE);
                Dictionary<TwoPointStructure, int> allCounts = qualifiedResponse.AllCounts;
                foreach (KeyValuePair<TwoPointStructure, int> harmConj in allCounts)
                {
                    if (harmConj.Value <= 0) continue;
                    string firstPointName = harmConj.Key.Point.GetDetails().Text;
                    string secondPointName = harmConj.Key.Point2.GetDetails().Text;
                    string harmonicCount = harmConj.Value.ToString();
                    resultData.AppendLine(("Harmonic" + " " + firstPointName + SPACES)[..LARGE_COLUMN_SIZE] + " / "
                        + ("Radix" + " " + secondPointName + SPACES)[..LARGE_COLUMN_SIZE] + " " + harmonicCount);
                }
            }
            else
            {
                Log.Error("ResearchResultModel.CreateHarmonicConjunctionsResultData() used a wrong request : {Request}", response.Request);
                throw new EnigmaException("Wrong request in ResearchResultModel");
            }
        }
        else
        {
            Log.Error("ResearchResultModel.CreateHarmonicConjunctionsResultData() used a wrong response : {Response}", response);
            throw new EnigmaException("Wrong result in ResearchResultModel");
        }
        return resultData.ToString();
    }

    
}