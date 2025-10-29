// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.Generic;
using Enigma.Domain.Dtos;
using Enigma.Domain.References;
using Enigma.Frontend.Ui.State;
using Enigma.Frontend.Ui.Support;

namespace Enigma.Frontend.Ui.Models;

public class PreNatalFactorSelectionModel
{
    public bool EnableCusps = false;
    public int MinimalNrOfPoints = 1;
    private AstroConfig? _astroConfig;
    private List<SelectableChartPointDetails> _selCpDetails = new();
    

    public List<SelectableChartPointDetails> GetAllCelPointDetails()
    {
        
        _astroConfig = CurrentConfig.Instance.GetConfig();
        _selCpDetails = new List<SelectableChartPointDetails>();
        var pointsToExclude = new List<ChartPoints>
            {
                ChartPoints.FortunaNoSect,
                ChartPoints.FortunaSect,
                ChartPoints.EastPoint,
                ChartPoints.Ascendant,
                ChartPoints.Mc,
                ChartPoints.Vertex,
                ChartPoints.ZeroAries
            }
        ;
        foreach (KeyValuePair<ChartPoints, ChartPointConfigSpecs?> currentCpSpec in _astroConfig.ChartPoints)
        {
            if (!currentCpSpec.Value.IsUsed || pointsToExclude.Contains(currentCpSpec.Key) ||
                currentCpSpec.Key.GetDetails().PointCat == PointCats.Cusp) continue;
            PointDetails cpDetails = currentCpSpec.Key.GetDetails();
            char glyph = currentCpSpec.Value.Glyph;
            _selCpDetails.Add(new SelectableChartPointDetails { Selected = false, ChartPoint = cpDetails.Point, Glyph = glyph, Name = cpDetails.Text});
        }
        return _selCpDetails;
    }
}