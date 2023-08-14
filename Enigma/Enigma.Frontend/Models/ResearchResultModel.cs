// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enigma.Api.Interfaces;
using Enigma.Domain.Analysis;
using Enigma.Domain.Analysis.Aspects;
using Enigma.Domain.Constants;
using Enigma.Domain.Exceptions;
using Enigma.Domain.Points;
using Enigma.Domain.Research;
using Enigma.Frontend.Ui.State;
using Serilog;

namespace Enigma.Frontend.Ui.Models;

/// <summary>Model for research result</summary>
public class ResearchResultModel
{
    private readonly DataVault _dataVault = DataVault.Instance;
    private readonly IFileAccessApi _fileAccessApi;
    private readonly IResearchPathApi _researchPathApi;
    private const string SPACES = "                    "; // 20 spaces
    private const string SEPARATOR_LINE = "--------------------------------------------------"; // 50 positions
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
        ResearchProject? project = _dataVault.CurrentProject;
        if (project != null)
        {
            ProjectName = project.Name;     
        }
        ResearchMethods method = _dataVault.ResearchMethod;
        MethodName = method.GetDetails().Text;
        MethodResponse? responseTest = _dataVault.ResponseTest;
        MethodResponse? responseCg = _dataVault.ResponseCg;
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
        TestResultText += EnigmaConstants.NewLine + "These results have been saved at : " + EnigmaConstants.NewLine + pathTest;
        ControlResultText += EnigmaConstants.NewLine + "These results have been saved at : " + EnigmaConstants.NewLine + pathControl;
        _fileAccessApi.WriteFile(pathTest, TestResultText);
        _fileAccessApi.WriteFile(pathControl, ControlResultText);
        TestResult = TestResultText;
        ControlResult = ControlResultText;
    }


    private static void CreateResultHeaders(GeneralResearchRequest request)
    {
        string methodText = request.Method.GetDetails().Text;
    }



    private static string CreatePartsResultData(MethodResponse response)
    {
        StringBuilder resultData = new();
        if (response is CountOfPartsResponse(var generalResearchRequest, var countOfParts, var totals))
        {
            string headerLine = string.Empty;
            string longSeparatorLine = (SEPARATOR_LINE + SEPARATOR_LINE + SEPARATOR_LINE)[..104];

            headerLine = response.Request.Method switch
            {
                ResearchMethods.CountPosInSigns =>
                    "                    ARI    TAU    GEM    CAN    LEO    VIR    LIB    SCO    SAG    CAP    AQU    PIS",
                ResearchMethods.CountPosInHouses =>
                    "                    1      2      3      4      5      6      7      8      9      10     11     12 ",
                _ => headerLine
            };
            resultData.AppendLine(headerLine);
            resultData.AppendLine(longSeparatorLine);

            foreach (CountOfParts cop in countOfParts)
            {
                string name = cop.Point + SPACES;
                resultData.Append(name[..20]);
                foreach (int count in cop.Counts)
                {
                    resultData.Append((count + SPACES)[..7]);
                }
                resultData.AppendLine();
            }
            resultData.AppendLine(longSeparatorLine);
            resultData.Append(SPACES);
            foreach (int total in totals)
            {
                resultData.Append((total + SPACES)[..7]);
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
        if (response is CountOfAspectsResponse qualifiedResponse)
        {
            string aspectSpaces = (SPACES + SPACES + SPACES)[..50];
            string aspectSeparatorLine = SEPARATOR_LINE;
            string separatorFragment = SEPARATOR_LINE[..7];
            StringBuilder headerLine = new();
            headerLine.Append(aspectSpaces);
            foreach (AspectTypes asp in qualifiedResponse.AspectsUsed)
            {
                headerLine.Append((((int)asp.GetDetails().Angle) + SPACES)[..7]);
                aspectSeparatorLine += separatorFragment;
            }
            headerLine.Append("Totals of aspects");
            aspectSeparatorLine += separatorFragment;
            resultData.AppendLine(headerLine.ToString());
            resultData.AppendLine(aspectSeparatorLine);
            int[,,] allCounts = qualifiedResponse.AllCounts;
            StringBuilder detailLine;
            int nrOfCelPoints = qualifiedResponse.PointsUsed.Count(item => item.GetDetails().PointCat != PointCats.Cusp);
            for (int i = 0; i < nrOfCelPoints; i++)
            {
                for (int j = i + 1; j < qualifiedResponse.PointsUsed.Count; j++)
                {
                    detailLine = new StringBuilder();
                    detailLine.Append(qualifiedResponse.PointsUsed[i].GetDetails().Text.PadRight(25));
                    detailLine.Append(qualifiedResponse.PointsUsed[j].GetDetails().Text.PadRight(25));
                    for (int k = 0; k < qualifiedResponse.AspectsUsed.Count; k++)
                    {
                        detailLine.Append((allCounts[i, j, k] + SPACES)[..7]);
                    }
                    detailLine.Append((qualifiedResponse.TotalsPerPointCombi[i, j] + SPACES)[..7]);
                    resultData.AppendLine(detailLine.ToString());
                }
            }
            int totalOverall = 0;
            detailLine = new StringBuilder();
            detailLine.Append(("Totals of aspects" + SPACES + SPACES + SPACES)[..50]);
            foreach (int count in qualifiedResponse.TotalsPerAspect)
            {
                detailLine.Append((count + SPACES)[..7]);
                totalOverall += count;
            }
            detailLine.Append((totalOverall + SPACES)[..7]);
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
            resultData.AppendLine("Number of charts without aspects");
            resultData.AppendLine(SEPARATOR_LINE);
            foreach (SimpleCount simpleCount in qualifiedResponse.Counts)
            {
                resultData.AppendLine((simpleCount.Point.GetDetails().Text + SPACES)[..20] + simpleCount.Count);
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
                resultData.AppendLine("Occupied midpoints for dial division" + " " + qualifiedRequest.DivisionForDial 
                                      + ". " + "Orb" + ": " + qualifiedRequest.Orb);
                resultData.AppendLine((SEPARATOR_LINE + SEPARATOR_LINE)[..80]);
                Dictionary<OccupiedMidpointStructure, int> allCounts = qualifiedResponse.AllCounts;
                foreach (KeyValuePair<OccupiedMidpointStructure, int> midpoint in allCounts)
                {
                    if (midpoint.Value <= 0) continue;
                    string firstPointName = midpoint.Key.FirstPoint.GetDetails().Text;
                    string secondPointName = midpoint.Key.SecondPoint.GetDetails().Text;
                    string occPointName = midpoint.Key.OccupyingPoint.GetDetails().Text;
                    string midpointCount = midpoint.Value.ToString();
                    resultData.AppendLine((firstPointName + SPACES)[..20] + " / " + (secondPointName + SPACES)[..20] 
                                          + " = " + (occPointName + SPACES)[..20] + " " + midpointCount);
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
                resultData.AppendLine("Harmonic conjunctions. Harmonic number" + ": " + qualifiedRequest.HarmonicNumber
                    + ". " + "Orb" + ": " + qualifiedRequest.Orb);
                resultData.AppendLine(SEPARATOR_LINE);
                Dictionary<TwoPointStructure, int> allCounts = qualifiedResponse.AllCounts;
                foreach (KeyValuePair<TwoPointStructure, int> harmConj in allCounts)
                {
                    if (harmConj.Value <= 0) continue;
                    string firstPointName = harmConj.Key.Point.GetDetails().Text;
                    string secondPointName = harmConj.Key.Point2.GetDetails().Text;
                    string harmonicCount = harmConj.Value.ToString();
                    resultData.AppendLine(("Harmonic" + " " + firstPointName + SPACES)[..20] + " / "
                        + ("Radix" + " " + secondPointName + SPACES)[..20] + " " + harmonicCount);
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