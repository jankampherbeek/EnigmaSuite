// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Api.Interfaces;
using Enigma.Domain.Constants;
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
    private readonly Rosetta _rosetta = Rosetta.Instance;


    public ResearchResultController(IFileAccessApi fileAccessApi, IResearchPathApi researchPathApi)
    {
        _fileAccessApi = fileAccessApi;
        _researchPathApi = researchPathApi;
    }

    public void SetMethodResponses(CountOfPartsResponse responseTest, CountOfPartsResponse responseControl)
    {
        DefineResultTexts(responseTest, responseControl);
    }


    public void DefineResultTexts(CountOfPartsResponse responseTest, CountOfPartsResponse responseControl)
    {
        CreateResultHeaders(responseTest);

        TestResultText = CreateResultData(responseTest);
        ControlResultText = CreateResultData(responseControl);
        string projName = responseTest.Request.ProjectName;
        string methodName = responseTest.Request.Method.ToString();
        bool useControlGroup = false;
        string pathTest = _researchPathApi.SummedResultsPath(projName, methodName, useControlGroup);
        useControlGroup = true;
        string pathControl = _researchPathApi.SummedResultsPath(projName, methodName, useControlGroup);
        _fileAccessApi.WriteFile(pathTest, TestResultText);
        _fileAccessApi.WriteFile(pathControl, ControlResultText);
        TestResultText += EnigmaConstants.NEW_LINE + _rosetta.TextForId("researchresultwindow.pathinfo") + EnigmaConstants.NEW_LINE + pathTest;
        ControlResultText += EnigmaConstants.NEW_LINE + _rosetta.TextForId("researchresultwindow.pathinfo") + EnigmaConstants.NEW_LINE + pathControl;

    }

    private void CreateResultHeaders(CountOfPartsResponse response)
    {
        ProjectText = response.Request.ProjectName;
        string methodText = _rosetta.TextForId(response.Request.Method.GetDetails().TextId);
        TestMethodText = methodText + " - test data";
        ControlMethodText = methodText + " - control data";
    }

    private static string CreateResultData(CountOfPartsResponse response)
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


    public void ShowHelp()
    {
        HelpWindow helpWindow = App.ServiceProvider.GetRequiredService<HelpWindow>();
        helpWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        helpWindow.SetHelpPage("ResearchResults");
        helpWindow.ShowDialog();
    }
}