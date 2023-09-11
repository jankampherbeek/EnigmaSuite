// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.Generic;
using Enigma.Api.Interfaces;
using Enigma.Domain.Charts;
using Enigma.Domain.Configuration;
using Enigma.Domain.Dtos;
using Enigma.Domain.Presentables;
using Enigma.Domain.RequestResponse;
using Enigma.Frontend.Ui.Interfaces;
using Enigma.Frontend.Ui.State;

namespace Enigma.Frontend.Ui.Models;

/// <summary>Model for showing radix aspects</summary>
public sealed class RadixAspectsModel
{
    private readonly IAspectForDataGridFactory _aspectForDataGridFactory;
    private readonly IAspectsApi _aspectsApi;
    private readonly IDescriptiveChartText _descriptiveChartText;
    private readonly DataVault _dataVault;


    public RadixAspectsModel(IAspectForDataGridFactory aspectForDataGridFactory, IAspectsApi aspectsApi,
        IDescriptiveChartText descriptiveChartText)
    {
        _dataVault = DataVault.Instance;
        _aspectForDataGridFactory = aspectForDataGridFactory;
        _aspectsApi = aspectsApi;
        _descriptiveChartText = descriptiveChartText;
    }

    /// <summary>Name/id for chart</summary>
    /// <returns>Name/id as entered by user.</returns>
    public string GetChartIdName()
    {
        var chart = _dataVault.GetCurrentChart();
        return chart == null ? "" : chart.InputtedChartData.MetaData.Name;
    }

    /// <summary>Radix aspects in presentable format</summary>
    /// <returns>Actual radix aspects formatted as Presentableaspects</returns>
    public List<PresentableAspects> GetPresentableAspectsForChartPoints()
    {
        IEnumerable<DefinedAspect> effAspects = _aspectsApi.AspectsForCelPoints(CreateRequest());
        return _aspectForDataGridFactory.CreateAspectForDataGrid(effAspects);
    }

    /// <summary>Text with a short description of the name/id and main settings for a chart</summary>
    /// <returns>The text with the description</returns>
    public string DescriptiveText()
    {
        string descText = "";
        CalculatedChart? chart = _dataVault.GetCurrentChart();
        var config = CurrentConfig.Instance.GetConfig();
        if (chart != null)
        {
            descText = _descriptiveChartText.ShortDescriptiveText(config, chart.InputtedChartData.MetaData);
        }
        return descText;
    }

    private AspectRequest CreateRequest()
    {
        CalculatedChart? currentChart = _dataVault.GetCurrentChart();
        AstroConfig config = CurrentConfig.Instance.GetConfig();
        return new AspectRequest(currentChart!, config);
    }
}