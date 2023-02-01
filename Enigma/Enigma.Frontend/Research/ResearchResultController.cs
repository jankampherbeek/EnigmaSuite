// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Api.Interfaces;
using Enigma.Domain.Analysis;
using Enigma.Domain.Analysis.Aspects;
using Enigma.Domain.Constants;
using Enigma.Domain.Points;
using Enigma.Domain.Research;
using Enigma.Frontend.Helpers.Support;
using Enigma.Frontend.Ui.Support;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace Enigma.Frontend.Ui.Research;

public class ResearchResultController
{

    private readonly IFileAccessApi _fileAccessApi;
    private readonly IResearchPathApi _researchPathApi;

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

   // public void SetMethodResponses(CountOfUnaspectedResponse responseTest, CountOfUnaspectedResponse responseControl)
   // {
   //     DefineUnaspectedResultTexts(responseTest, responseControl);
   // }

    public void SetMethodResponses(CountOfOccupiedMidpointsResponse responseTest, CountOfOccupiedMidpointsResponse responseControl)
    {
        DefineOccupiedMidpointsResultTexts(responseTest, responseControl);
    }

    public void SetMethodResponses(CountHarmonicConjunctionsResponse responseTst, CountHarmonicConjunctionsResponse responseControl)
    {
        DefineHarmonicConjunctionsResultTexts(responseTst, responseControl);
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



    private static string CreatePartsResultData(MethodResponse response)
    {
        StringBuilder resultData = new();
        CountOfPartsResponse? qualifiedResponse = response as CountOfPartsResponse;
        if (qualifiedResponse != null)
        {
            List<CountOfParts> countOfParts = qualifiedResponse.Counts;
            List<int> totals = qualifiedResponse.Totals;
            string spaces = "                    ";  // 20 spaces
            string headerLine = string.Empty;
            string separatorLine = "--------------------------------------------------------------------------------------------------------";

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
            resultData.AppendLine(separatorLine);

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
            resultData.AppendLine(separatorLine);
            resultData.Append(spaces);
            foreach (int total in totals)
            {
                resultData.Append((total.ToString() + spaces)[..7]);
            }
            resultData.AppendLine();
        }
        return resultData.ToString();
    }


    private static string CreateAspectsResultData(MethodResponse response)
    {
        StringBuilder resultData = new();
        CountOfAspectsResponse? qualifiedResponse = response as CountOfAspectsResponse;
        if (qualifiedResponse != null)
        {
            string spaces = "                                                  ";               // 50 spaces
            string separatorLine = "--------------------------------------------------";        // 50 positions
            string separatorFragment = "-------";                                               // 7 positions
            StringBuilder headerLine = new();
            headerLine.Append(spaces);   // 50 spaces offset to create space for two names for points.
            foreach (AspectTypes asp in qualifiedResponse.AspectsUsed)
            {
                headerLine.Append((((int)asp.GetDetails().Angle).ToString() + spaces)[..7]);
                separatorLine += separatorFragment;
            }
            headerLine.Append("Totals");                // TODO 0.1 replce with text from RB
            separatorLine += separatorFragment;
            resultData.AppendLine(headerLine.ToString());
            resultData.AppendLine(separatorLine);
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
                    detailLine.Append(Rosetta.TextForId(qualifiedResponse.PointsUsed[i].GetDetails().TextId).PadRight(25));
                    detailLine.Append(Rosetta.TextForId(qualifiedResponse.PointsUsed[j].GetDetails().TextId).PadRight(25));
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
            detailLine.Append(("Totals" + spaces)[..50]);
            foreach (int count in qualifiedResponse.TotalsPerAspect)
            {
                detailLine.Append((count.ToString() + spaces)[..7]);
                totalOverall += count;
            }
            detailLine.Append((totalOverall.ToString() + spaces)[..7]);
            resultData.AppendLine(separatorLine);
            resultData.AppendLine(detailLine.ToString());
        }
        return resultData.ToString();
    }


    private static string CreateUnaspectedResultData(MethodResponse response)
    {
        StringBuilder resultData = new();
        CountOfUnaspectedResponse? qualifiedResponse = response as CountOfUnaspectedResponse;
        if (qualifiedResponse != null)
        {
            string spaces = "                    ";                                             // 20 spaces
            string separatorLine = "--------------------------------------------------";        // 50 positions
            StringBuilder headerLine = new();
            headerLine.Append(spaces);
            headerLine.AppendLine("Nr of charts without aspects.");   // TODO 0.1 use RB
            resultData.AppendLine(headerLine.ToString());
            resultData.AppendLine(separatorLine);
            foreach (SimpleCount simpleCount in qualifiedResponse.Counts)
            {
                resultData.AppendLine((Rosetta.TextForId(simpleCount.Point.GetDetails().TextId) + spaces)[..20] + simpleCount.Count.ToString());
            }
        }
        return resultData.ToString();
    }

    private static string CreateOccupiedMidpointsResultData(MethodResponse response)
    {
        StringBuilder resultData = new();
        CountOfOccupiedMidpointsResponse? qualifiedResponse = response as CountOfOccupiedMidpointsResponse;
        if (qualifiedResponse != null )
        {
            Dictionary<OccupiedMidpointStructure, int> allCounts = qualifiedResponse.AllCounts;
            string spaces = "                    ";                                             // 20 spaces
            string separatorLine = "--------------------------------------------------";        // 50 positions
            StringBuilder headerLine = new();
            headerLine.Append(spaces);
            headerLine.AppendLine("Occupied midpoints.");   // TODO 0.1 use RB
            resultData.AppendLine(headerLine.ToString());
            resultData.AppendLine(separatorLine);

            foreach (KeyValuePair<OccupiedMidpointStructure, int> midpoint in allCounts)
            {
                if (midpoint.Value > 0)
                {
                    string firstPointName = Rosetta.TextForId(midpoint.Key.FirstPoint.GetDetails().TextId);
                    string secondPointName = Rosetta.TextForId(midpoint.Key.SecondPoint.GetDetails().TextId);
                    string occPointName = Rosetta.TextForId(midpoint.Key.OccupyingPoint.GetDetails().TextId);
                    string midpointCount = midpoint.Value.ToString();
                    resultData.AppendLine((firstPointName + spaces)[..20] + " / " + (secondPointName + spaces)[..20] + " = " + (occPointName + spaces)[..20] + " " + midpointCount);
                }
            }
        }
        return resultData.ToString();
    }

    private static string CreateHarmonicConjunctionsResultData(MethodResponse response)
    {
        StringBuilder resultData = new();

        CountHarmonicConjunctionsResponse? qualifiedResponse = response as CountHarmonicConjunctionsResponse;
        if (qualifiedResponse != null)
        {
            Dictionary<TwoPointStructure, int> allCounts = qualifiedResponse.AllCounts;

            string spaces = "                    ";                                             // 20 spaces
            string separatorLine = "--------------------------------------------------";        // 50 positions
            StringBuilder headerLine = new();
            headerLine.Append(spaces);
            headerLine.AppendLine("Harmonic conjunctions.");   // TODO 0.1 use RB
            resultData.AppendLine(headerLine.ToString());
            resultData.AppendLine(separatorLine);
            foreach (KeyValuePair<TwoPointStructure, int> harmConj in allCounts)
            {
                if (harmConj.Value > 0)
                {
                    string firstPointName = Rosetta.TextForId(harmConj.Key.Point.GetDetails().TextId);
                    string secondPointName = Rosetta.TextForId(harmConj.Key.Point2.GetDetails().TextId);
                    string harmonicCount = harmConj.Value.ToString();
                    resultData.AppendLine(("Harmonic " + firstPointName + spaces)[..20] + " / " + (" Radix " + secondPointName + spaces)[..20] + " " + harmonicCount);    // TODO 0.1 use RB
                }
            }
        }
        return resultData.ToString();
    }


    public void ShowHelp()
    {
        HelpWindow helpWindow = new();
        helpWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        helpWindow.SetHelpPage("ResearchResults");
        helpWindow.ShowDialog();
    }
}