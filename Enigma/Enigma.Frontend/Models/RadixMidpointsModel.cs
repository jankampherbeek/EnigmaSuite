// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System;
using System.Collections.Generic;
using Enigma.Api.Analysis;
using Enigma.Domain.Dtos;
using Enigma.Domain.Presentables;
using Enigma.Frontend.Ui.PresentationFactories;
using Enigma.Frontend.Ui.State;
using Enigma.Frontend.Ui.Support;
using Enigma.Frontend.Ui.Support.Conversions;

namespace Enigma.Frontend.Ui.Models;

/// <summary>Model for midpoints in radix</summary>
public sealed class RadixMidpointsModel(
    IMidpointsApi midpointsApi,
    IMidpointForDataGridFactory midpointForDataGridFactory,
    IDoubleToDmsConversions doubleToDmsConversions,
    IDescriptiveChartText descriptiveChartText)
{
    private readonly DataVaultCharts _dataVaultCharts = DataVaultCharts.Instance;


    /// <summary>Calculate midpoints in radix</summary>
  /// <param name="dialSize">The size of the 'dial' to use.</param>
  /// <returns>A tuple with a list of all midpoints and a list with occupied midpoints.
  /// Takes the dialsize into account for the occupied midpoints.</returns>
    public Tuple<List<PresentableMidpoint>, List<PresentableOccupiedMidpoint>> RetrieveAndFormatMidpoints(double dialSize)
    {
        var chart = _dataVaultCharts.GetCurrentChart();
        List<PresentableMidpoint> presMidpoints = new();
        List<PresentableOccupiedMidpoint> presOccMidpoints = new();
        if (chart == null)
            return new Tuple<List<PresentableMidpoint>, List<PresentableOccupiedMidpoint>>(presMidpoints, presOccMidpoints);
        double orb = CurrentConfig.Instance.GetConfig().BaseOrbMidpoints;
        IEnumerable<BaseMidpoint> baseMidpoints = midpointsApi.AllMidpoints(chart);
        presMidpoints = midpointForDataGridFactory.CreateMidpointsDataGrid(baseMidpoints);
        IEnumerable<OccupiedMidpoint> occupiedMidpoints = midpointsApi.OccupiedMidpoints(chart, dialSize, orb);
        presOccMidpoints = midpointForDataGridFactory.CreateMidpointsDataGrid(occupiedMidpoints);
        return new Tuple<List<PresentableMidpoint>, List<PresentableOccupiedMidpoint>>(presMidpoints, presOccMidpoints);
    }

  /// <summary>A description of the most relevant settings for this chart</summary>
  /// <returns>Textual description</returns>
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


    public string DegreesToDms(double value)
    {
        return doubleToDmsConversions.ConvertDoubleToPositionsDmsText(value);
    }

}