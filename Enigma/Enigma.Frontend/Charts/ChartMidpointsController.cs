// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Api.Interfaces;
using Enigma.Domain.Analysis;
using Enigma.Domain.Charts;
using Enigma.Frontend.Helpers.Interfaces;
using Enigma.Frontend.Ui.Interfaces;
using Enigma.Frontend.Ui.State;
using Enigma.Frontend.Ui.Support;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Windows;

namespace Enigma.Frontend.Ui.Charts;
public class ChartMidpointsController
{
    private readonly IMidpointsApi _midpointsApi;
    private readonly IMidpointForDataGridFactory _midpointForDataGridFactory;
    private readonly IDoubleToDmsConversions _doubleToDmsConversions;
    private readonly DataVault _dataVault;


    // TODO make it possible to work with a new chart without overwriting screens with data from the previous chart.
    public ChartMidpointsController(IMidpointsApi midpointsApi, IMidpointForDataGridFactory midpointForDataGridFactory, IDoubleToDmsConversions doubleToDmsConversions)
    {
        _dataVault = DataVault.Instance;
        _midpointsApi = midpointsApi;
        _midpointForDataGridFactory = midpointForDataGridFactory;
        _doubleToDmsConversions = doubleToDmsConversions;
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
    public Tuple<List<PresentableMidpoint>, List<PresentableOccupiedMidpoint>> RetrieveAndFormatMidpoints(double dialSize)
    {
        var chart = _dataVault.GetCurrentChart();
        List<PresentableMidpoint> presMidpoints = new();
        List<PresentableOccupiedMidpoint> presOccMidpoints = new();
        if (chart != null)
        {
            List<BaseMidpoint> baseMidpoints = _midpointsApi.AllMidpoints(chart);
            presMidpoints = _midpointForDataGridFactory.CreateMidpointsDataGrid(baseMidpoints);
            List<OccupiedMidpoint> occupiedMidpoints = _midpointsApi.OccupiedMidpoints(chart, dialSize);
            presOccMidpoints = _midpointForDataGridFactory.CreateMidpointsDataGrid(occupiedMidpoints);
        }
        return new Tuple<List<PresentableMidpoint>, List<PresentableOccupiedMidpoint>>(presMidpoints, presOccMidpoints);
    }



    public string DegreesToDms(double value)
    {
        return _doubleToDmsConversions.ConvertDoubleToPositionsDmsText(value);
    }

    public static void ShowHelp()
    {
        HelpWindow helpWindow = App.ServiceProvider.GetRequiredService<HelpWindow>();
        helpWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        helpWindow.SetHelpPage("Midpoints");
        helpWindow.ShowDialog();
    }

}