// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Api.Interfaces;
using Enigma.Domain.Analysis;
using Enigma.Domain.Analysis.Aspects;
using Enigma.Domain.Constants;
using Enigma.Domain.Exceptions;
using Enigma.Domain.Points;
using Enigma.Domain.Research;
using Enigma.Frontend.Helpers.Support;
using Enigma.Frontend.Ui.Support;
using Serilog;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using Enigma.Frontend.Ui.State;
using Enigma.Frontend.Ui.Views;

namespace Enigma.Frontend.Ui.Research;

public class ResearchResultController
{

    private readonly IFileAccessApi _fileAccessApi;
    private readonly IResearchPathApi _researchPathApi;
    private readonly string spaces = "                    ";                                            // 20 spaces
    private readonly string separatorLine = "--------------------------------------------------";       // 50 positions

    public string ProjectText { get; set; } = string.Empty;
    public string TestMethodText { get; set; } = string.Empty;
    public string ControlMethodText { get; set; } = string.Empty;
    public string TestResultText { get; set; } = string.Empty;
    public string ControlResultText { get; set; } = string.Empty;




    public ResearchResultController(IFileAccessApi fileAccessApi, IResearchPathApi researchPathApi)
    {
        _fileAccessApi = fileAccessApi;
        _researchPathApi = researchPathApi;
    }

    public void SetMethodResponses(MethodResponse responseTest, MethodResponse responseControl)
    {
        if (responseTest is CountOfAspectsResponse)
        {
            DefineAspectsResultTexts(responseTest, responseControl);
        }
        else if (responseTest is CountOfUnaspectedResponse)
        {
            DefineUnaspectedResultTexts(responseTest, responseControl);
        }
        else if (responseTest is CountOfOccupiedMidpointsResponse)
        {
            DefineOccupiedMidpointsResultTexts(responseTest, responseControl);
        }
        else if (responseTest is CountHarmonicConjunctionsResponse)
        {
            DefineHarmonicConjunctionsResultTexts(responseTest, responseControl);
        }
        else if (responseTest is CountOfPartsResponse)
        {
            DefinePartsResultTexts(responseTest, responseControl);
        }
    }


    public void SetMethodResponses(CountOfPartsResponse responseTest, CountOfPartsResponse responseControl)
    {
        DefinePartsResultTexts(responseTest, responseControl);
    }

    public void SetMethodResponses(CountOfOccupiedMidpointsResponse responseTest, CountOfOccupiedMidpointsResponse responseControl)
    {
        DefineOccupiedMidpointsResultTexts(responseTest, responseControl);
    }

    public void SetMethodResponses(CountHarmonicConjunctionsResponse responseTest, CountHarmonicConjunctionsResponse responseControl)
    {
        DefineHarmonicConjunctionsResultTexts(responseTest, responseControl);
    }



    public void DefineUnaspectedResultTexts(MethodResponse responseTest, MethodResponse responseControl)
    {
        TestResultText = CreateUnaspectedResultData(responseTest);
        ControlResultText = CreateUnaspectedResultData(responseControl);
        CreateResultHeaders(responseTest.Request);
        WriteResults(responseTest.Request);
    }

    public void DefineAspectsResultTexts(MethodResponse responseTest, MethodResponse responseControl)
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



    public void DefinePartsResultTexts(MethodResponse responseTest, MethodResponse responseControl)
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
        TestResultText += EnigmaConstants.NEW_LINE + Rosetta.TextForId("researchresultwindow.pathinfo") + EnigmaConstants.NEW_LINE + pathTest;
        ControlResultText += EnigmaConstants.NEW_LINE + Rosetta.TextForId("researchresultwindow.pathinfo") + EnigmaConstants.NEW_LINE + pathControl;
        _fileAccessApi.WriteFile(pathTest, TestResultText);
        _fileAccessApi.WriteFile(pathControl, ControlResultText);
    }


    private void CreateResultHeaders(GeneralResearchRequest request)
    {
        ProjectText = request.ProjectName;
        string methodText = Rosetta.TextForId(request.Method.GetDetails().TextId);
        TestMethodText = methodText + " - test data";
        ControlMethodText = methodText + " - control data";
    }



    private string CreatePartsResultData(MethodResponse response)
    {
        StringBuilder resultData = new();
        CountOfPartsResponse? qualifiedResponse = response as CountOfPartsResponse;
        if (qualifiedResponse != null)
        {
            List<CountOfParts> countOfParts = qualifiedResponse.Counts;
            List<int> totals = qualifiedResponse.Totals;
            string headerLine = string.Empty;
            string longSeparatorLine = (separatorLine + separatorLine + separatorLine)[..104];

            if (response.Request.Method == ResearchMethods.CountPosInSigns)
            {
                headerLine = "                    ARI    TAU    GEM    CAN    LEO    VIR    LIB    SCO    SAG    CAP    AQU    PIS";
            }
            else
            if (response.Request.Method == ResearchMethods.CountPosInHouses)
            {
                headerLine = "                    1      2      3      4      5      6      7      8      9      10     11     12 ";
            }
            resultData.AppendLine(headerLine);
            resultData.AppendLine(longSeparatorLine);

            foreach (CountOfParts cop in countOfParts)
            {
                string name = cop.Point.ToString() + spaces;
                resultData.Append(name[..20]);
                foreach (int count in cop.Counts)
                {
                    resultData.Append((count.ToString() + spaces)[..7]);
                }
                resultData.AppendLine();
            }
            resultData.AppendLine(longSeparatorLine);
            resultData.Append(spaces);
            foreach (int total in totals)
            {
                resultData.Append((total.ToString() + spaces)[..7]);
            }
            resultData.AppendLine();
        }
        else
        {
            string errorTxt = "ResearchResultController.CreatePartsResultData() used a wrong response : " + response;
            Log.Error(errorTxt);
            throw new EnigmaException(errorTxt);
        }

        return resultData.ToString();
    }


    private string CreateAspectsResultData(MethodResponse response)
    {
        StringBuilder resultData = new();
        CountOfAspectsResponse? qualifiedResponse = response as CountOfAspectsResponse;
        if (qualifiedResponse != null)
        {
            string aspectSpaces = (spaces + spaces + spaces)[..50];
            string aspectSeparatorLine = separatorLine;
            string separatorFragment = separatorLine[..7];
            StringBuilder headerLine = new();
            headerLine.Append(aspectSpaces);
            foreach (AspectTypes asp in qualifiedResponse.AspectsUsed)
            {
                headerLine.Append((((int)asp.GetDetails().Angle).ToString() + spaces)[..7]);
                aspectSeparatorLine += separatorFragment;
            }
            headerLine.Append(Rosetta.TextForId("researchresultwindow.totalsaspects"));
            aspectSeparatorLine += separatorFragment;
            resultData.AppendLine(headerLine.ToString());
            resultData.AppendLine(aspectSeparatorLine);
            int[,,] allCounts = qualifiedResponse.AllCounts;
            StringBuilder detailLine;
            int nrOfCelPoints = 0;
            foreach (var item in qualifiedResponse.PointsUsed)
            {
                if (item.GetDetails().PointCat != PointCats.Cusp)
                {
                    nrOfCelPoints++;
                }
            }
            for (int i = 0; i < nrOfCelPoints; i++)
            {
                for (int j = i + 1; j < qualifiedResponse.PointsUsed.Count; j++)
                {
                    detailLine = new();
                    detailLine.Append(Rosetta.TextForId(qualifiedResponse.PointsUsed[i].GetDetails().Text).PadRight(25));
                    detailLine.Append(Rosetta.TextForId(qualifiedResponse.PointsUsed[j].GetDetails().Text).PadRight(25));
                    for (int k = 0; k < qualifiedResponse.AspectsUsed.Count; k++)
                    {
                        detailLine.Append((allCounts[i, j, k].ToString() + spaces)[..7]);
                    }
                    detailLine.Append((qualifiedResponse.TotalsPerPointCombi[i, j].ToString() + spaces)[..7]);
                    resultData.AppendLine(detailLine.ToString());
                }
            }
            int totalOverall = 0;
            detailLine = new();
            detailLine.Append((Rosetta.TextForId("researchresultwindow.totalsaspects") + spaces + spaces + spaces)[..50]);
            foreach (int count in qualifiedResponse.TotalsPerAspect)
            {
                detailLine.Append((count.ToString() + spaces)[..7]);
                totalOverall += count;
            }
            detailLine.Append((totalOverall.ToString() + spaces)[..7]);
            resultData.AppendLine(aspectSeparatorLine);
            resultData.AppendLine(detailLine.ToString());
        }
        else
        {
            string errorTxt = "ResearchResultController.CreateAspectsResultData() used a wrong response : " + response;
            Log.Error(errorTxt);
            throw new EnigmaException(errorTxt);
        }
        return resultData.ToString();
    }


    private string CreateUnaspectedResultData(MethodResponse response)
    {
        StringBuilder resultData = new();
        CountOfUnaspectedResponse? qualifiedResponse = response as CountOfUnaspectedResponse;
        if (qualifiedResponse != null)
        {
            resultData.AppendLine(Rosetta.TextForId("researchresultwindow.noaspects"));
            resultData.AppendLine(separatorLine);
            foreach (SimpleCount simpleCount in qualifiedResponse.Counts)
            {
                resultData.AppendLine((Rosetta.TextForId(simpleCount.Point.GetDetails().Text) + spaces)[..20] + simpleCount.Count.ToString());
            }
        }
        else
        {
            string errorTxt = "ResearchResultController.CreateUnaspectedResultData() used a wrong response : " + response;
            Log.Error(errorTxt);
            throw new EnigmaException(errorTxt);
        }
        return resultData.ToString();
    }

    private string CreateOccupiedMidpointsResultData(MethodResponse response)
    {
        StringBuilder resultData = new();
        CountOfOccupiedMidpointsResponse? qualifiedResponse = response as CountOfOccupiedMidpointsResponse;
        if (qualifiedResponse != null)
        {
            CountOccupiedMidpointsRequest? qualifiedRequest = response.Request as CountOccupiedMidpointsRequest;
            if (qualifiedRequest != null)
            {
                resultData.AppendLine(Rosetta.TextForId("researchresultwindow.titleoccupiedmidpoints") + " " + qualifiedRequest.DivisionForDial + ". " + Rosetta.TextForId("researchresultwindow.orb") + ": " + qualifiedRequest.Orb);
                resultData.AppendLine((separatorLine + separatorLine)[..80]);
                Dictionary<OccupiedMidpointStructure, int> allCounts = qualifiedResponse.AllCounts;
                foreach (KeyValuePair<OccupiedMidpointStructure, int> midpoint in allCounts)
                {
                    if (midpoint.Value > 0)
                    {
                        string firstPointName = Rosetta.TextForId(midpoint.Key.FirstPoint.GetDetails().Text);
                        string secondPointName = Rosetta.TextForId(midpoint.Key.SecondPoint.GetDetails().Text);
                        string occPointName = Rosetta.TextForId(midpoint.Key.OccupyingPoint.GetDetails().Text);
                        string midpointCount = midpoint.Value.ToString();
                        resultData.AppendLine((firstPointName + spaces)[..20] + " / " + (secondPointName + spaces)[..20] + " = " + (occPointName + spaces)[..20] + " " + midpointCount);
                    }
                }
            }
            else
            {
                string errorTxt = "ResearchResultController.CreateOccupiedMidpointsResultData() used a wrong request : " + response.Request;
                Log.Error(errorTxt);
                throw new EnigmaException(errorTxt);
            }
        }
        else
        {
            string errorTxt = "ResearchResultController.CreateOccupiedMidpointsResultData() used a wrong response : " + response;
            Log.Error(errorTxt);
            throw new EnigmaException(errorTxt);
        }
        return resultData.ToString();
    }

    private string CreateHarmonicConjunctionsResultData(MethodResponse response)
    {
        StringBuilder resultData = new();
        CountHarmonicConjunctionsResponse? qualifiedResponse = response as CountHarmonicConjunctionsResponse;
        if (qualifiedResponse != null)
        {
            CountHarmonicConjunctionsRequest? qualifiedRequest = response.Request as CountHarmonicConjunctionsRequest;
            if (qualifiedRequest != null)
            {
                resultData.AppendLine(Rosetta.TextForId("researchresultwindow.titleharmonicconjunctions") + ": " + qualifiedRequest.HarmonicNumber.ToString()
                    + ". " + Rosetta.TextForId("researchresultwindow.orb") + ": " + qualifiedRequest.Orb.ToString());
                resultData.AppendLine(separatorLine);
                Dictionary<TwoPointStructure, int> allCounts = qualifiedResponse.AllCounts;
                foreach (KeyValuePair<TwoPointStructure, int> harmConj in allCounts)
                {
                    if (harmConj.Value > 0)
                    {
                        string firstPointName = Rosetta.TextForId(harmConj.Key.Point.GetDetails().Text);
                        string secondPointName = Rosetta.TextForId(harmConj.Key.Point2.GetDetails().Text);
                        string harmonicCount = harmConj.Value.ToString();
                        resultData.AppendLine((Rosetta.TextForId("researchresultwindow.posharmonic") + " " + firstPointName + spaces)[..20] + " / "
                            + (Rosetta.TextForId("researchresultwindow.posradix") + " " + secondPointName + spaces)[..20] + " " + harmonicCount);
                    }
                }
            }
            else
            {
                string errorTxt = "ResearchResultController.CreateHarmonicConjunctionsResultData() used a wrong request : " + response.Request;
                Log.Error(errorTxt);
                throw new EnigmaException(errorTxt);
            }
        }
        else
        {
            string errorTxt = "ResearchResultController.CreateHarmonicConjunctionsResultData() used a wrong response : " + response;
            Log.Error(errorTxt);
            throw new EnigmaException(errorTxt);
        }
        return resultData.ToString();
    }

    public static void ShowHelp()
    {
        DataVault.Instance.CurrentViewBase = "ResearchResults";
        HelpWindow helpWindow = new();
        helpWindow.ShowDialog();
    }

}