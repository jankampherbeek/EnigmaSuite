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
    public string ChartName { get; set; } = "";
    
    public void DefineAltChart(ChartPoints offsetPoint)
    {
        var currentChart = _dataVaultCharts.GetCurrentChart();
        if (currentChart == null) return;
        ChartName = currentChart.InputtedChartData.MetaData.Name ?? "";
        AltChart = AltZodiacStartOrchestrator.ChangeStartingPoint(currentChart, offsetPoint);
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
}