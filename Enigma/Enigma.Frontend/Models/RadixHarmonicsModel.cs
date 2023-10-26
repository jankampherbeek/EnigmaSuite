// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.Generic;
using Enigma.Api.Interfaces;
using Enigma.Domain.Presentables;
using Enigma.Frontend.Ui.Interfaces;
using Enigma.Frontend.Ui.State;

namespace Enigma.Frontend.Ui.Models;

/// <summary>Model for harmonics in radix</summary>
public sealed class RadixHarmonicsModel
{
    private readonly IHarmonicsApi _harmonicsApi;
    private readonly IHarmonicForDataGridFactory _dataGridFactory;
    private readonly IDescriptiveChartText _descriptiveChartText;
    private readonly DataVaultCharts _dataVaultCharts;

    public RadixHarmonicsModel(IHarmonicsApi harmonicsApi, IHarmonicForDataGridFactory dataGridFactory, IDescriptiveChartText descriptiveChartText)
    {
        _dataVaultCharts = DataVaultCharts.Instance;
        _harmonicsApi = harmonicsApi;
        _dataGridFactory = dataGridFactory;
        _descriptiveChartText = descriptiveChartText;
    }

    public string RetrieveChartName()
    {
        var chart = _dataVaultCharts.GetCurrentChart();
        return chart != null ? chart.InputtedChartData.MetaData.Name : "";
    }

    public List<PresentableHarmonic> RetrieveAndFormatHarmonics(double harmonicNr)
    {
        var chart = _dataVaultCharts.GetCurrentChart();
        List<PresentableHarmonic> presHarmonics = new();
        if (chart == null) return presHarmonics;
        List<double> harmonicPositions = _harmonicsApi.Harmonics(chart, harmonicNr);
        presHarmonics = _dataGridFactory.CreateHarmonicForDataGrid(harmonicPositions, chart);
        return presHarmonics;
    }

    public string DescriptiveText()
    {
        var chart = _dataVaultCharts.GetCurrentChart();
        var config = CurrentConfig.Instance.GetConfig();
        return chart != null
            ? _descriptiveChartText.ShortDescriptiveText(config, chart.InputtedChartData.MetaData)
            : "";
    }


}