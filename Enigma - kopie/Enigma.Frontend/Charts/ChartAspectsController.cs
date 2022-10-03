// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Core.Analysis.Api;
using Enigma.Domain;
using Enigma.Domain.Analysis;
using Enigma.Frontend.PresentationFactories;
using Enigma.Frontend.State;
using Enigma.Frontend.Support;
using Enigma.Frontend.UiDomain;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Windows;

namespace Enigma.Frontend.Charts;

/// <summary>Controller (according to MVC pattern) for the view ChartAspectsWindow.</summary>
public class ChartAspectsController
{
    private readonly IAspectForDataGridFactory _aspectForDataGridFactory;
    private readonly IAspectsApi _aspectsApi;
    private readonly DataVault _dataVault;


    public ChartAspectsController(IAspectForDataGridFactory aspectForDataGridFactory, IAspectsApi aspectsApi)
    {
        _dataVault = DataVault.Instance;
        _aspectForDataGridFactory = aspectForDataGridFactory;
        _aspectsApi = aspectsApi;

    }

    public string GetChartIdName()
    {
        var chart = _dataVault.GetLastChart();
        return chart == null ? "" : chart.InputtedChartData.ChartMetaData.Name;
    }

    public List<PresentableAspects> GetPresentableAspectsForSolSysPoints()
    {
        List<EffectiveAspect> effAspects = _aspectsApi.AspectsForSolSysPoints(CreateRequest());
        return _aspectForDataGridFactory.CreateAspectForDataGrid(effAspects);
    }

    public List<PresentableAspects> GetPresentableAspectsForMundanePoints()
    {
        List<EffectiveAspect> effAspects = _aspectsApi.AspectsForMundanePoints(CreateRequest());
        return _aspectForDataGridFactory.CreateAspectForDataGrid(effAspects);
    }

    public void ShowHelp()
    {
        HelpWindow? helpWindow = App.ServiceProvider.GetService<HelpWindow>();
        if (helpWindow != null)
        {
            helpWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            helpWindow.SetHelpPage("ChartsAspects");
            helpWindow.ShowDialog();
        }
    }


    private AspectRequest CreateRequest()
    {
        CalculatedChart? currentChart = _dataVault.GetLastChart() as CalculatedChart;
        return new AspectRequest(currentChart);
    }

}
