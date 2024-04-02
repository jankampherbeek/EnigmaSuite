// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System;
using System.Collections.Generic;
using System.Linq;
using Enigma.Api;
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
public class RadixLongitudeEquivalentsModel
{
    private IAnalysisSingleValuesApi _analysisSingleValuesApi;
    private ILongitudeEquivalentsForDataGridFactory _longitudeEquivalentsForDataGridFactory;
    private DataVaultCharts _dataVaultCharts;
    private readonly IDescriptiveChartText _descriptiveChartText;

    public RadixLongitudeEquivalentsModel(IAnalysisSingleValuesApi analysisSingleValuesApi,
        ILongitudeEquivalentsForDataGridFactory longitudeEquivalentsForDataGridFactory,
        IDescriptiveChartText descriptiveChartText)
    {
        _analysisSingleValuesApi = analysisSingleValuesApi;
        _longitudeEquivalentsForDataGridFactory = longitudeEquivalentsForDataGridFactory;
        _dataVaultCharts = DataVaultCharts.Instance;
        _descriptiveChartText = descriptiveChartText;
    }
    
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
        string descText = "";
        CalculatedChart? chart = _dataVaultCharts.GetCurrentChart();
        Log.Information("RadixLongitudEquivalentsModel.DescriptiveText(): Retrieving config from CurrentConfig");
        var config = CurrentConfig.Instance.GetConfig();
        if (chart != null)
        {
            descText = _descriptiveChartText.ShortDescriptiveText(config, chart.InputtedChartData.MetaData);
        }
        return descText;
    }
    
    /// <summary>Longitude equivalents in presentable format.</summary>
    /// <returns>Actual longitude equivalents formatted as PresentableLongitudeEquivalents.</returns>
    public List<PresentableLongitudeEquivalent> GetPresentableLongitudeEquivalents()
    {
        CalculatedChart? currentChart = _dataVaultCharts.GetCurrentChart();
        var equivalents = _analysisSingleValuesApi.CalculateLongitudeEquivalents(CreateRequest(currentChart));
        return _longitudeEquivalentsForDataGridFactory.CreateLongitudeEquivalentsForDataGrid(
            equivalents, currentChart.Positions, currentChart.Obliquity);
    }
    
    
    private LongitudeEquivalentRequest CreateRequest(CalculatedChart currentChart)
    {
        double jd = currentChart.InputtedChartData.FullDateTime.JulianDayForEt;
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