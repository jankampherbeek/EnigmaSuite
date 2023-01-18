// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Api.Interfaces;
using Enigma.Domain.Analysis.Aspects;
using Enigma.Domain.Constants;
using Enigma.Domain.Points;
using Enigma.Domain.Research;
using Enigma.Frontend.Helpers.Support;
using Enigma.Frontend.Ui.Support;
using Microsoft.Extensions.DependencyInjection;
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

    public void SetMethodResponses(CountOfAspectsResponse responseTest, CountOfAspectsResponse responseControl)
    {
        DefineAspectsResultTexts(responseTest, responseControl);
    }


    public void SetMethodResponses(CountOfPartsResponse responseTest, CountOfPartsResponse responseControl)
    {
        DefinePartsResultTexts(responseTest, responseControl);
    }


    public void DefineAspectsResultTexts(CountOfAspectsResponse responseTest, CountOfAspectsResponse responseControl)
    {
        CreateResultHeaders(responseTest.Request);
        TestResultText = CreateAspectsResultData(responseTest);
        ControlResultText = CreateAspectsResultData(responseControl);
        string projName = responseTest.Request.ProjectName;
        string methodName = responseTest.Request.Method.ToString();
        bool useControlGroup = false;
        string pathTest = _researchPathApi.SummedResultsPath(projName, methodName, useControlGroup);
        useControlGroup = true;
        string pathControl = _researchPathApi.SummedResultsPath(projName, methodName, useControlGroup);
        _fileAccessApi.WriteFile(pathTest, TestResultText);
        _fileAccessApi.WriteFile(pathControl, ControlResultText);
        TestResultText += EnigmaConstants.NEW_LINE + Rosetta.TextForId("researchresultwindow.pathinfo") + EnigmaConstants.NEW_LINE + pathTest;
        ControlResultText += EnigmaConstants.NEW_LINE + Rosetta.TextForId("researchresultwindow.pathinfo") + EnigmaConstants.NEW_LINE + pathControl;

    }


    public void DefinePartsResultTexts(CountOfPartsResponse responseTest, CountOfPartsResponse responseControl)
    {
        CreateResultHeaders(responseTest.Request);

        TestResultText = CreatePartsResultData(responseTest);
        ControlResultText = CreatePartsResultData(responseControl);
        string projName = responseTest.Request.ProjectName;
        string methodName = responseTest.Request.Method.ToString();
        bool useControlGroup = false;
        string pathTest = _researchPathApi.SummedResultsPath(projName, methodName, useControlGroup);
        useControlGroup = true;
        string pathControl = _researchPathApi.SummedResultsPath(projName, methodName, useControlGroup);
        _fileAccessApi.WriteFile(pathTest, TestResultText);
        _fileAccessApi.WriteFile(pathControl, ControlResultText);
        TestResultText += EnigmaConstants.NEW_LINE + Rosetta.TextForId("researchresultwindow.pathinfo") + EnigmaConstants.NEW_LINE + pathTest;
        ControlResultText += EnigmaConstants.NEW_LINE + Rosetta.TextForId("researchresultwindow.pathinfo") + EnigmaConstants.NEW_LINE + pathControl;

    }

    private void CreateResultHeaders(GeneralResearchRequest request)
    {
        ProjectText = request.ProjectName;
        string methodText = Rosetta.TextForId(request.Method.GetDetails().TextId);
        TestMethodText = methodText + " - test data";
        ControlMethodText = methodText + " - control data";
    }



    private static string CreatePartsResultData(CountOfPartsResponse response)
    {
        List<CountOfParts> countOfParts = response.Counts;
        List<int> totals = response.Totals;
        string spaces = "                    ";  // 20 spaces
        string headerLine = string.Empty;
        string separatorLine = "--------------------------------------------------------------------------------------------------------";
        StringBuilder resultData = new();
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
        return resultData.ToString();
    }


    private static string CreateAspectsResultData(CountOfAspectsResponse response)
    {
        StringBuilder resultData = new();

        string spaces = "                                                  ";               // 50 spaces
        string separatorLine = "--------------------------------------------------";        // 50 positions
        string separatorFragment = "-------";                                               // 7 positions
        StringBuilder headerLine = new();
        headerLine.Append(spaces);   // 50 spaces offset to create space for two names for points.
        foreach (AspectTypes asp in response.AspectsUsed)
        {
            headerLine.Append((((int)asp.GetDetails().Angle).ToString() + spaces)[..7]);
            separatorLine += separatorFragment;
        }
        headerLine.Append("Totals");                // TODO 0.1 replce with text from RB
        separatorLine += separatorFragment;  
        resultData.AppendLine(headerLine.ToString());
        resultData.AppendLine(separatorLine);
        int[,,] allCounts = response.AllCounts;
        StringBuilder detailLine = new();
        int nrOfCelPoints = 0;
        foreach (var item in response.PointsUsed)
        {
            if (item.GetDetails().PointCat != PointCats.Cusp)
            {
                nrOfCelPoints++;
            }
        }


        for (int i = 0; i < nrOfCelPoints; i++)
        {
            for (int j = i+1; j < response.PointsUsed.Count; j++)
            {
                detailLine = new();
                detailLine.Append(Rosetta.TextForId(response.PointsUsed[i].GetDetails().TextId).PadRight(25));
                detailLine.Append(Rosetta.TextForId(response.PointsUsed[j].GetDetails().TextId).PadRight(25));
                for (int k = 0; k < response.AspectsUsed.Count; k++)
                {
                    detailLine.Append((allCounts[i,j,k].ToString() + spaces)[..7]); 
                }
                detailLine.Append((response.TotalsPerPointCombi[i, j].ToString() + spaces)[..7]);
                resultData.AppendLine(detailLine.ToString());
            }
        }
        int totalOverall = 0;
        detailLine = new();
        detailLine.Append(("Totals" + spaces)[..50]);
        foreach (int count in response.TotalsPerAspect)
        {
            detailLine.Append((count.ToString() + spaces)[..7]);
            totalOverall += count;
        }
        detailLine.Append((totalOverall.ToString() + spaces)[..7]);
        resultData.AppendLine(separatorLine);
        resultData.AppendLine(detailLine.ToString());
        return resultData.ToString();
    }



    public void ShowHelp()
    {
        HelpWindow helpWindow = App.ServiceProvider.GetRequiredService<HelpWindow>();
        helpWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        helpWindow.SetHelpPage("ResearchResults");
        helpWindow.ShowDialog();
    }
}