// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System;
using System.Collections.Generic;
using Enigma.Api.Interfaces;
using Enigma.Domain.Analysis;
using Enigma.Domain.Charts;
using Enigma.Frontend.Helpers.Interfaces;
using Enigma.Frontend.Ui.Interfaces;
using Enigma.Frontend.Ui.State;

namespace Enigma.Frontend.Ui.Models;

/// <summary>Model for midpoints in radix</summary>
public sealed class RadixMidpointsModel
{
    private readonly IMidpointsApi _midpointsApi;
    private readonly IMidpointForDataGridFactory _midpointForDataGridFactory;
    private readonly IDoubleToDmsConversions _doubleToDmsConversions;
    private readonly IDescriptiveChartText _descriptiveChartText;
    private readonly DataVault _dataVault;


    public RadixMidpointsModel(IMidpointsApi midpointsApi, 
        IMidpointForDataGridFactory midpointForDataGridFactory, 
        IDoubleToDmsConversions doubleToDmsConversions, 
        IDescriptiveChartText descriptiveChartText)
    {
        _dataVault = DataVault.Instance;
        _midpointsApi = midpointsApi;
        _midpointForDataGridFactory = midpointForDataGridFactory;
        _doubleToDmsConversions = doubleToDmsConversions;
        _descriptiveChartText = descriptiveChartText;
    }

    public string RetrieveChartName()
    {
        var chart = _dataVault.GetCurrentChart();
        return chart != null ? chart.InputtedChartData.MetaData.Name : "";
    }
    
  /// <summary>Calculate midpoints in radix</summary>
  /// <param name="dialSize">The size of the 'dial' to use.</param>
  /// <returns>A tuple with a list of all midpoints and a list with occupied midpoints.
  /// Takes the dialsize into account for the occupied midpoints.</returns>
    public Tuple<List<PresentableMidpoint>, List<PresentableOccupiedMidpoint>> RetrieveAndFormatMidpoints(double dialSize)
    {
        var chart = _dataVault.GetCurrentChart();
        List<PresentableMidpoint> presMidpoints = new();
        List<PresentableOccupiedMidpoint> presOccMidpoints = new();
        if (chart == null)
            return new Tuple<List<PresentableMidpoint>, List<PresentableOccupiedMidpoint>>(presMidpoints, presOccMidpoints);
        double orb = CurrentConfig.Instance.GetConfig().BaseOrbMidpoints;
        List<BaseMidpoint> baseMidpoints = _midpointsApi.AllMidpoints(chart);
        presMidpoints = _midpointForDataGridFactory.CreateMidpointsDataGrid(baseMidpoints);
        List<OccupiedMidpoint> occupiedMidpoints = _midpointsApi.OccupiedMidpoints(chart, dialSize, orb);
        presOccMidpoints = _midpointForDataGridFactory.CreateMidpointsDataGrid(occupiedMidpoints);
        return new Tuple<List<PresentableMidpoint>, List<PresentableOccupiedMidpoint>>(presMidpoints, presOccMidpoints);
    }

  /// <summary>A description of the most relevant settings for this chart</summary>
  /// <returns>Textual description</returns>
    public string DescriptiveText()
    {
        string descText = "";
        var chart = _dataVault.GetCurrentChart();
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