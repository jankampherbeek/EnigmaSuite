// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.Generic;
using System.Windows.Documents;
using Enigma.Api;
using Enigma.Domain.Dtos;
using Enigma.Domain.Presentables;
using Enigma.Domain.References;
using Enigma.Domain.Requests;
using Enigma.Frontend.Ui.PresentationFactories;
using Enigma.Frontend.Ui.State;
using Enigma.Frontend.Ui.Support;

namespace Enigma.Frontend.Ui.Models;

/// <summary>Model for Out of Bounds calendar.</summary>
public sealed class OobCalModel
{
    private readonly IDescriptiveChartText _descriptiveChartText;
    private readonly IOobCalApi _oobCalApi;
    private readonly IOobEventForDataGridFactory _oobEventForDataGridFactory;
    private readonly DataVaultCharts _dataVaultCharts;

    public OobCalModel(IDescriptiveChartText descriptiveChartText, IOobCalApi oobCalApi, IOobEventForDataGridFactory oobEventForDataGridFactory)
    {
        _dataVaultCharts = DataVaultCharts.Instance;
        _descriptiveChartText = descriptiveChartText;
        _oobCalApi = oobCalApi;
        _oobEventForDataGridFactory = oobEventForDataGridFactory;
    }

    public List<PresentableOobEvents> GetOobEvents()
    {
        List<PresentableOobEvents> allEvents = new();
        CalculatedChart? chart = _dataVaultCharts.GetCurrentChart();
        if (chart is null) return allEvents;
        double jd = chart.InputtedChartData.FullDateTime.JulianDayForEt;
        double timeOffset = 0.0;    // TODO, get rid of time offset
        Calendars cal = Calendars.Gregorian;
        Location location = chart.InputtedChartData.Location;
        AstroConfig config = CurrentConfig.Instance.GetConfig();
        OobCalRequest request = new(jd, timeOffset, cal, location, config);
        List<OobCalEvent> events = _oobCalApi.CreateOobCalendar(request);
        allEvents = _oobEventForDataGridFactory.CreateOobEventForDataGrid(events);
        return allEvents;
    }
    
    
    
    /// <summary>Text with a short description of the name/id and main settings for a chart</summary>
    /// <returns>The text with the description</returns>
    public string DescriptiveText()
    {
        string descText = "";
        CalculatedChart? chart = _dataVaultCharts.GetCurrentChart();
        var config = CurrentConfig.Instance.GetConfig();
        if (chart != null)
        {
            descText = _descriptiveChartText.ShortDescriptiveText(config, chart.InputtedChartData.MetaData);
        }
        return descText;
    }
    
    
}