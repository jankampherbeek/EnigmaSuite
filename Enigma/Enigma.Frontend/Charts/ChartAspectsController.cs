// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Api.Interfaces;
using Enigma.Domain.Analysis.Aspects;
using Enigma.Domain.Charts;
using Enigma.Domain.Configuration;
using Enigma.Domain.RequestResponse;
using Enigma.Frontend.Ui.Interfaces;
using Enigma.Frontend.Ui.State;
using Enigma.Frontend.Ui.Support;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Windows;

namespace Enigma.Frontend.Ui.Charts;

/// <summary>Controller (according to MVC pattern) for the view ChartAspectsWindow.</summary>
public sealed class ChartAspectsController
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
        var chart = _dataVault.GetCurrentChart();
        return chart == null ? "" : chart.InputtedChartData.MetaData.Name;
    }

    public List<PresentableAspects> GetPresentableAspectsForChartPoints()
    {
        List<DefinedAspect> effAspects = _aspectsApi.AspectsForCelPoints(CreateRequest());
        return _aspectForDataGridFactory.CreateAspectForDataGrid(effAspects);
    }

    public static void ShowHelp()
    {
        HelpWindow helpWindow = App.ServiceProvider.GetRequiredService<HelpWindow>();
        helpWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        helpWindow.SetHelpPage("ChartsAspects");
        helpWindow.ShowDialog();
    }


    private AspectRequest CreateRequest()
    {
        CalculatedChart? currentChart = _dataVault.GetCurrentChart();
        AstroConfig config = CurrentConfig.Instance.GetConfig();
        return new AspectRequest(currentChart!, config);
    }

}
