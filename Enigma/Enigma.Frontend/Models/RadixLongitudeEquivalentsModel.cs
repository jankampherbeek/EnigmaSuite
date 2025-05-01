// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System;
using System.Collections.Generic;
using System.Linq;
using Enigma.Api.Analysis;
using Enigma.Domain.Dtos;
using Enigma.Domain.Presentables;
using Enigma.Domain.References;
using Enigma.Domain.Requests;
using Enigma.Frontend.Ui.PresentationFactories;
using Enigma.Frontend.Ui.State;
using Enigma.Frontend.Ui.Support;
using Serilog;

namespace Enigma.Frontend.Ui.Models;


/// <summary>Model for Longitude Equivalents.</summary>
public class RadixLongitudeEquivalentsModel(
    IAnalysisSingleValuesApi analysisSingleValuesApi,
    ILongitudeEquivalentsForDataGridFactory longitudeEquivalentsForDataGridFactory,
    IDescriptiveChartText descriptiveChartText)
{
    private readonly DataVaultCharts _dataVaultCharts = DataVaultCharts.Instance;

    /// <summary>Name/id for chart</summary>
    /// <returns>Name/id as entered by user.</returns>
    public string GetChartIdName()
    {
        var chart = _dataVaultCharts.GetCurrentChart();
        return chart == null ? "" : chart.InputtedChartData.MetaData.Name;
    }
    
    /// <summary>Text with a short description of the name/id and main settings for a chart</summary>
    /// <returns>The text with the description</returns>
    public string DescriptiveText()
    {
        var descText = "";
        var chart = _dataVaultCharts.GetCurrentChart();
        Log.Information("RadixLongitudEquivalentsModel.DescriptiveText(): Retrieving config from CurrentConfig");
        var config = CurrentConfig.Instance.GetConfig();
        if (chart != null)
        {
            descText = descriptiveChartText.ShortDescriptiveText(config, chart.InputtedChartData.MetaData);
        }
        return descText;
    }
    
    /// <summary>Longitude equivalents in presentable format.</summary>
    /// <returns>Actual longitude equivalents formatted as PresentableLongitudeEquivalents.</returns>
    public List<PresentableLongitudeEquivalent> GetPresentableLongitudeEquivalents()
    {
        var currentChart = _dataVaultCharts.GetCurrentChart();
        var equivalents = analysisSingleValuesApi.CalculateLongitudeEquivalents(CreateRequest(currentChart));
        return longitudeEquivalentsForDataGridFactory.CreateLongitudeEquivalentsForDataGrid(
            equivalents, currentChart.Positions, currentChart.Obliquity);
    }
    
    
    private LongitudeEquivalentRequest CreateRequest(CalculatedChart currentChart)
    {
        var jd = currentChart.InputtedChartData.FullDateTime.JulianDayForEt;
        List<Tuple<ChartPoints, double, double>> pointsPosLongDecl = 
            (from pointPos in currentChart.Positions 
                let currentPoint = pointPos.Key 
                where currentPoint.GetDetails().PointCat == PointCats.Common 
                let longitude = pointPos.Value.Ecliptical.MainPosSpeed.Position 
                let declination = pointPos.Value.Equatorial.DeviationPosSpeed.Position 
                select new Tuple<ChartPoints, double, double>(currentPoint, longitude, declination)).ToList();
        return new LongitudeEquivalentRequest(jd, pointsPosLongDecl);
    }
    
    
}