// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.Generic;
using Enigma.Core.Slices.AltZodiacStart;
using Enigma.Domain.Dtos;
using Enigma.Domain.References;
using Enigma.Frontend.Ui.State;
using Enigma.Frontend.Ui.Support;

namespace Enigma.Frontend.Ui.Models;

public class StartZodModel
{
    private readonly DataVaultCharts _dataVaultCharts = DataVaultCharts.Instance;
    public CalculatedChart? AltChart { get; set; }
    public ChartPoints SelectedPoint { get; set; } = ChartPoints.Sun;
    
    public void DefineAltChart(ChartPoints offsetPoint)
    {
        SelectedPoint = offsetPoint;
        var currentChart = _dataVaultCharts.GetCurrentChart();
        if (currentChart == null) return;
        AltChart = AltZodiacStartOrchestrator.ChangeStartingPoint(currentChart, offsetPoint);
        DataVaultCharts.Instance.DefineAltChart(AltChart);
    }

    public List<SelectableChartPointDetails> GetAllCelPointDetails()
    {
        var astroConfig = CurrentConfig.Instance.GetConfig();
        var selCpDetails = new List<SelectableChartPointDetails>();
        foreach (KeyValuePair<ChartPoints, ChartPointConfigSpecs?> currentCpSpec in astroConfig.ChartPoints)
        {
            if (!currentCpSpec.Value.IsUsed || currentCpSpec.Key.GetDetails().PointCat == PointCats.Cusp) continue;
            PointDetails cpDetails = currentCpSpec.Key.GetDetails();
            char glyph = currentCpSpec.Value.Glyph;
            selCpDetails.Add(new SelectableChartPointDetails 
            { 
                Selected = false, 
                ChartPoint = cpDetails.Point, 
                Glyph = glyph, 
                Name = cpDetails.Text
            });
        }
        return selCpDetails;
    }
    
    /// <summary>
    /// Get the current chart name
    /// </summary>
    /// <returns>Name of the current chart or empty string if no chart</returns>
    public string GetCurrentChartName()
    {
        var currentChart = _dataVaultCharts.GetCurrentChart();
        return currentChart?.InputtedChartData.MetaData.Name ?? "";
    }
}