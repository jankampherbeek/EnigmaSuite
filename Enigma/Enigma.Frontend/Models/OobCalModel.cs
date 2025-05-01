// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.Generic;
using Enigma.Api.Analysis;
using Enigma.Domain.Presentables;
using Enigma.Domain.References;
using Enigma.Domain.Requests;
using Enigma.Frontend.Ui.PresentationFactories;
using Enigma.Frontend.Ui.State;
using Enigma.Frontend.Ui.Support;

namespace Enigma.Frontend.Ui.Models;

/// <summary>Model for Out of Bounds calendar.</summary>
public sealed class OobCalModel(
    IDescriptiveChartText descriptiveChartText,
    IOobCalApi oobCalApi,
    IOobEventForDataGridFactory oobEventForDataGridFactory)
{
    private readonly DataVaultCharts _dataVaultCharts = DataVaultCharts.Instance;

    public List<PresentableOobEvents> GetOobEvents()
    {
        List<PresentableOobEvents> allEvents = [];
        var chart = _dataVaultCharts.GetCurrentChart();
        if (chart is null) return allEvents;
        var jd = chart.InputtedChartData.FullDateTime.JulianDayForEt;
        var timeOffset = 0.0;    // TODO, get rid of time offset
        var cal = Calendars.Gregorian;
        var location = chart.InputtedChartData.Location;
        var config = CurrentConfig.Instance.GetConfig();
        OobCalRequest request = new(jd, timeOffset, cal, location, config);
        var events = oobCalApi.CreateOobCalendar(request);
        allEvents = oobEventForDataGridFactory.CreateOobEventForDataGrid(events);
        return allEvents;
    }
    
    
    
    /// <summary>Text with a short description of the name/id and main settings for a chart</summary>
    /// <returns>The text with the description</returns>
    public string DescriptiveText()
    {
        var descText = "";
        var chart = _dataVaultCharts.GetCurrentChart();
        var config = CurrentConfig.Instance.GetConfig();
        if (chart != null)
        {
            descText = descriptiveChartText.ShortDescriptiveText(config, chart.InputtedChartData.MetaData);
        }
        return descText;
    }
    
}