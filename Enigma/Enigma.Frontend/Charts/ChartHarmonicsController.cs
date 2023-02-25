// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Api.Interfaces;
using Enigma.Domain.Charts;
using Enigma.Frontend.Ui.Interfaces;
using Enigma.Frontend.Ui.State;
using Enigma.Frontend.Ui.Support;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Windows;

namespace Enigma.Frontend.Ui.Charts;

public sealed class ChartHarmonicsController
{
    private readonly IHarmonicsApi _harmonicsApi;
    private readonly IHarmonicForDataGridFactory _dataGridFactory;
    private readonly IDescriptiveChartText _descriptiveChartText;
    private readonly DataVault _dataVault;

    public ChartHarmonicsController(IHarmonicsApi harmonicsApi, IHarmonicForDataGridFactory dataGridFactory, IDescriptiveChartText descriptiveChartText)
    {
        _dataVault = DataVault.Instance;
        _harmonicsApi = harmonicsApi;
        _dataGridFactory = dataGridFactory;
        _descriptiveChartText = descriptiveChartText;
    }

    public String RetrieveChartName()
    {
        var chart = _dataVault.GetCurrentChart();
        if (chart != null)
        {
            return chart.InputtedChartData.MetaData.Name;
        }
        return "";
    }

    public List<PresentableHarmonic> RetrieveAndFormatHarmonics(double harmonicNr)
    {
        var chart = _dataVault.GetCurrentChart();
        List<PresentableHarmonic> presHarmonics = new();
        if (chart != null)
        {
            List<double> harmonicPositions = _harmonicsApi.Harmonics(chart, harmonicNr);
            presHarmonics = _dataGridFactory.CreateHarmonicForDataGrid(harmonicPositions, chart);
        }
        return presHarmonics;
    }

    public string DescriptiveText()
    {
        string descText = "";
        var chart = _dataVault.GetCurrentChart();
        var config = CurrentConfig.Instance.GetConfig();
        if (chart != null)
        {
            descText = _descriptiveChartText.ShortDescriptiveText(config, chart.InputtedChartData.MetaData);
        }
        return descText;
    }

    public static void ShowHelp()
    {
        HelpWindow helpWindow = App.ServiceProvider.GetRequiredService<HelpWindow>();
        helpWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        helpWindow.SetHelpPage("Harmonics");
        helpWindow.ShowDialog();
    }
}