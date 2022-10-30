// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Core.Analysis.Api;
using Enigma.Core.Analysis.Interfaces;
using Enigma.Domain.Analysis;
using Enigma.Domain.Charts;
using Enigma.Frontend.Interfaces;
using Enigma.Frontend.State;
using Enigma.Frontend.Support;
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
        HelpWindow helpWindow = App.ServiceProvider.GetRequiredService<HelpWindow>();
        helpWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        helpWindow.SetHelpPage("ChartsAspects");
        helpWindow.ShowDialog();
    }


    private AspectRequest CreateRequest()
    {
        CalculatedChart? currentChart = _dataVault.GetLastChart() as CalculatedChart;
        return new AspectRequest(currentChart);
    }

}
