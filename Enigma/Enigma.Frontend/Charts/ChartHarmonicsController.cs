﻿// Enigma Astrology Research.
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
    private readonly DataVault _dataVault;
    private readonly HelpWindow _helpWindow = App.ServiceProvider.GetRequiredService<HelpWindow>();

    public ChartHarmonicsController(IHarmonicsApi harmonicsApi, IHarmonicForDataGridFactory dataGridFactory)
    {
        _dataVault = DataVault.Instance;
        _harmonicsApi = harmonicsApi;
        _dataGridFactory = dataGridFactory;
    }

    public String RetrieveChartName()
    {
        var chart = _dataVault.GetLastChart();
        if (chart != null)
        {
            return chart.InputtedChartData.MetaData.Name;
        }
        return "";
    }

    public List<PresentableHarmonic> RetrieveAndFormatHarmonics(double harmonicNr)
    {
        var chart = _dataVault.GetLastChart();
        List<PresentableHarmonic> presHarmonics = new();
        if (chart != null)
        {
            List<double> harmonicPositions = _harmonicsApi.Harmonics(chart, harmonicNr);
            presHarmonics = _dataGridFactory.CreateHarmonicForDataGrid(harmonicPositions, chart);
        }
        return presHarmonics;
    }

    public void ShowHelp()
    {
        _helpWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        _helpWindow.SetHelpPage("Harmonics");
        _helpWindow.ShowDialog();
    }
}