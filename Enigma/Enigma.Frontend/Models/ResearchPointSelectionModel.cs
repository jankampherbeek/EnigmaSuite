// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.Generic;
using Enigma.Domain.Dtos;
using Enigma.Domain.References;
using Enigma.Frontend.Ui.State;
using Enigma.Frontend.Ui.Support;

namespace Enigma.Frontend.Ui.Models;

/// <summary>Model for point selection</summary>
public class ResearchPointSelectionModel
{

    public bool EnableCusps;
    public int MinimalNrOfPoints;
    private AstroConfig? _astroConfig;
    private List<SelectableChartPointDetails> _selCpDetails = new();
    private readonly IPointsExclusionManager _pointsExclusionManager;
    
    public ResearchPointSelectionModel(IPointsExclusionManager pointsExclusionManager)
    {
        _pointsExclusionManager = pointsExclusionManager;
    }

    public List<SelectableChartPointDetails> GetAllCelPointDetails()
    {
        ResearchMethods method = DataVaultResearch.Instance.ResearchMethod;
        return DefineChartPoints(method);
    }

    
    
    public bool IncludeCusps()
    {
        return _astroConfig!.UseCuspsForAspects;
    }

    
    private List<SelectableChartPointDetails> DefineChartPoints(ResearchMethods method)
    {
        MinimalNrOfPoints = method.GetDetails().MinNumberOfPoints;
        
        _astroConfig = CurrentConfig.Instance.GetConfig();
        _selCpDetails = new List<SelectableChartPointDetails>();
        PointsToExclude pointsToExclude = _pointsExclusionManager.DefineExclusions(method);
        EnableCusps = !pointsToExclude.ExcludeCusps;
        foreach (KeyValuePair<ChartPoints, ChartPointConfigSpecs?> currentCpSpec in _astroConfig.ChartPoints)
        {
            if (!currentCpSpec.Value.IsUsed || pointsToExclude.ExcludedPoints.Contains(currentCpSpec.Key) ||
                currentCpSpec.Key.GetDetails().PointCat == PointCats.Cusp && pointsToExclude.ExcludeCusps) continue;
            PointDetails cpDetails = currentCpSpec.Key.GetDetails();
            char glyph = currentCpSpec.Value.Glyph;
            _selCpDetails.Add(new SelectableChartPointDetails { Selected = false, ChartPoint = cpDetails.Point, Glyph = glyph, Name = cpDetails.Text});
        }
        return _selCpDetails;
    }
    

}

