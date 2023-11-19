// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.Generic;
using Enigma.Domain.Dtos;
using Enigma.Domain.Interfaces;
using Enigma.Domain.References;
using Enigma.Frontend.Ui.State;
using Enigma.Frontend.Ui.Support;

namespace Enigma.Frontend.Ui.Models;

/// <summary>Model for selection of celestial objects</summary>
public class CelestialObjectsSelectionModel
{
    public int MinimalNrOfPoints;
    private AstroConfig? _astroConfig;
    public List<SelectableChartPointDetails> SelCpDetails { get; set; } = new();
    private readonly IPointsExclusionManager _pointsExclusionManager;

    public CelestialObjectsSelectionModel(IPointsExclusionManager pointsExclusionManager)
    {
        _pointsExclusionManager = pointsExclusionManager;
        // define minimal nr of points (msg)
        // define conditions for celpoints (msg)
        
        SelCpDetails = DefineChartPoints();
    }
    
    private List<SelectableChartPointDetails> DefineChartPoints()
    {
     //   MinimalNrOfPoints = method.GetDetails().MinNumberOfPoints;
        
        _astroConfig = CurrentConfig.Instance.GetConfig();
        var _selCpDetails = new List<SelectableChartPointDetails>();
        PointsToExclude pointsToExclude = _pointsExclusionManager.DefineHelioExclusions();
        PointsToExclude housesToExclude = _pointsExclusionManager.DefineCycleExclusions();
        // todo combine exclusions
        foreach (KeyValuePair<ChartPoints, ChartPointConfigSpecs> currentCpSpec in _astroConfig.ChartPoints)
        {
            if (!currentCpSpec.Value.IsUsed || pointsToExclude.ExcludedPoints.Contains(currentCpSpec.Key) ||
                currentCpSpec.Key.GetDetails().PointCat == PointCats.Cusp && pointsToExclude.ExcludeCusps) continue;
            PointDetails cpDetails = currentCpSpec.Key.GetDetails();
            char glyph = currentCpSpec.Value.Glyph;
            _selCpDetails.Add(new SelectableChartPointDetails
            {
                Selected = false, ChartPoint = cpDetails.Point, Glyph = glyph, Name = cpDetails.Text
            });
        }
        return _selCpDetails;
    }
}

