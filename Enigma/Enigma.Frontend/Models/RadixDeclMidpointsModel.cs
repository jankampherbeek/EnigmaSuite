// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.Generic;
using Enigma.Api;
using Enigma.Domain.Dtos;
using Enigma.Domain.Presentables;
using Enigma.Frontend.Ui.PresentationFactories;
using Enigma.Frontend.Ui.State;
using Enigma.Frontend.Ui.Support;
using Enigma.Frontend.Ui.Support.Conversions;

namespace Enigma.Frontend.Ui.Models;

public class RadixDeclMidpointsModel
{
    
    private readonly IDeclMidpointsApi _declMidpointsApi;
    private readonly IMidpointForDataGridFactory _midpointForDataGridFactory;
    private readonly IDoubleToDmsConversions _doubleToDmsConversions;
    private readonly IDescriptiveChartText _descriptiveChartText;
    private readonly DataVaultCharts _dataVaultCharts;

    public RadixDeclMidpointsModel(IDeclMidpointsApi declMidpointsApi, 
        IMidpointForDataGridFactory midpointForDataGridFactory, 
        IDoubleToDmsConversions doubleToDmsConversions, 
        IDescriptiveChartText descriptiveChartText)
    {
        _dataVaultCharts = DataVaultCharts.Instance;
        _declMidpointsApi = declMidpointsApi;
        _midpointForDataGridFactory = midpointForDataGridFactory;
        _doubleToDmsConversions = doubleToDmsConversions;
        _descriptiveChartText = descriptiveChartText;
    }

    public IEnumerable<PresentableOccupiedMidpoint> GetPresentableDeclMidpoints()
    {
        List<PresentableMidpoint> presMidpoints = new();
        double orb = CurrentConfig.Instance.GetConfig().OrbMidpointsDecl;
        CalculatedChart chart = _dataVaultCharts.GetCurrentChart();
        IEnumerable<OccupiedMidpoint> occupiedMidpoints = _declMidpointsApi.OccupiedDeclMidpoints(chart, orb);
        IEnumerable<PresentableOccupiedMidpoint> presOccMidpoints = _midpointForDataGridFactory.CreateMidpointsDataGrid(occupiedMidpoints);
        return presOccMidpoints;
    }

    public string GetChartIdName()
    {
        return _dataVaultCharts.GetCurrentChart().InputtedChartData.MetaData.Name;
    }
    
    /// <summary>A description of the most relevant settings for this chart</summary>
    /// <returns>Textual description</returns>
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


    public string DegreesToDms(double value)
    {
        return _doubleToDmsConversions.ConvertDoubleToPositionsDmsText(value);
    }
    
}