using System.Collections.Generic;
using CommunityToolkit.Mvvm.Input;
using Enigma.Domain.Configuration;
using Enigma.Domain.Interfaces;
using Enigma.Domain.Points;
using Enigma.Domain.Research;
using Enigma.Frontend.Helpers.Support;
using Enigma.Frontend.Ui.State;
using Enigma.Frontend.Ui.Support;
using Enigma.Frontend.Ui.Views;

namespace Enigma.Frontend.Ui.Models;

public class ResearchPointSelectionModel
{

    public bool enableCusps = false;
    public int minimalNrOfPoints;
    private AstroConfig? _astroConfig;
    private List<SelectableChartPointDetails> _selCpDetails = new();
    private readonly IPointsExclusionManager _pointsExclusionManager;
    
    public ResearchPointSelectionModel(IPointsExclusionManager pointsExclusionManager)
    {
        _pointsExclusionManager = pointsExclusionManager;
    }

    public List<SelectableChartPointDetails> GetAllCelPointDetails()
    {
        ResearchMethods method = DataVault.Instance.ResearchMethod;
        return DefineChartPoints(method);
    }

    
    
    public bool IncludeCusps()
    {
        return _astroConfig!.UseCuspsForAspects;
    }

    
    private List<SelectableChartPointDetails> DefineChartPoints(ResearchMethods method)
    {
        if (method != ResearchMethods.None)
        {
            minimalNrOfPoints = method.GetDetails().MinNumberOfPoints;
        
            _astroConfig = CurrentConfig.Instance.GetConfig();
            _selCpDetails = new();
            PointsToExclude pointsToExclude = _pointsExclusionManager.DefineExclusions(method);
            enableCusps = !pointsToExclude.ExcludeCusps;
            foreach (KeyValuePair<ChartPoints, ChartPointConfigSpecs> currentCpSpec in _astroConfig.ChartPoints)
            {
                if (!currentCpSpec.Value.IsUsed || pointsToExclude.ExcludedPoints.Contains(currentCpSpec.Key) ||
                    currentCpSpec.Key.GetDetails().PointCat == PointCats.Cusp && pointsToExclude.ExcludeCusps) continue;
                PointDetails cpDetails = currentCpSpec.Key.GetDetails();
                char glyph = currentCpSpec.Value.Glyph;
                _selCpDetails.Add(new SelectableChartPointDetails() { Selected = false, ChartPoint = cpDetails.Point, Glyph = glyph, Name = cpDetails.Text});
            }            
        }
        return _selCpDetails;
    }
    

}

