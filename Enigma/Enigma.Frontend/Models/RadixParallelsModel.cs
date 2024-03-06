// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.Generic;
using Enigma.Api;
using Enigma.Domain.Dtos;
using Enigma.Domain.Presentables;
using Enigma.Domain.Requests;
using Enigma.Frontend.Ui.PresentationFactories;
using Enigma.Frontend.Ui.State;
using Enigma.Frontend.Ui.Support;
using Serilog;

namespace Enigma.Frontend.Ui.Models;

public class RadixParallelsModel
{

    
    private readonly IParallelsForDataGridFactory _parallelForDataGridFactory;
    private readonly IParallelsApi _parallelsApi;
    private readonly IDescriptiveChartText _descriptiveChartText;
    private readonly DataVaultCharts _dataVaultCharts;
    
    
    public RadixParallelsModel(IParallelsForDataGridFactory parallelForDataGridFactory, IParallelsApi parallelsApi,
        IDescriptiveChartText descriptiveChartText)
    {
        _dataVaultCharts = DataVaultCharts.Instance;
        _parallelForDataGridFactory = parallelForDataGridFactory;
        _parallelsApi = parallelsApi;
        _descriptiveChartText = descriptiveChartText;
    }

    /// <summary>Name/id for chart</summary>
    /// <returns>Name/id as entered by user.</returns>
    public string GetChartIdName()
    {
        var chart = _dataVaultCharts.GetCurrentChart();
        return chart == null ? "" : chart.InputtedChartData.MetaData.Name;
    }

    /// <summary>Radix parallels in presentable format</summary>
    /// <returns>Actual radix parallels formatted as PresentableParallels</returns>
    public List<PresentableParallels> GetPresentableParallelsForChartPoints()
    {
        IEnumerable<DefinedParallel> effParallels = _parallelsApi.ParallelsForCelPoints(CreateRequest());
        return _parallelForDataGridFactory.CreateParallelsForDataGrid(effParallels);
    }

    /// <summary>Text with a short description of the name/id and main settings for a chart</summary>
    /// <returns>The text with the description</returns>
    public string DescriptiveText()
    {
        string descText = "";
        CalculatedChart? chart = _dataVaultCharts.GetCurrentChart();
        Log.Information("RadixParallelssModel.DescriptiveText(): Retrieving config from CurrentConfig");
        var config = CurrentConfig.Instance.GetConfig();
        if (chart != null)
        {
            descText = _descriptiveChartText.ShortDescriptiveText(config, chart.InputtedChartData.MetaData);
        }
        return descText;
    }

    private ParallelRequest CreateRequest()
    {
        CalculatedChart? currentChart = _dataVaultCharts.GetCurrentChart();
        AstroConfig config = CurrentConfig.Instance.GetConfig();
        return new ParallelRequest(currentChart!, config);
    }
}