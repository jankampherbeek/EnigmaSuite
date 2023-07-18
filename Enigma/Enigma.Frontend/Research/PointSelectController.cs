// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Domain.Configuration;
using Enigma.Domain.Interfaces;
using Enigma.Domain.Points;
using Enigma.Domain.Research;
using Enigma.Frontend.Helpers.Support;
using Enigma.Frontend.Ui.State;
using Enigma.Frontend.Ui.Support;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Windows;
using Enigma.Frontend.Ui.Views;

namespace Enigma.Frontend.Ui.Research;
public sealed class PointSelectController
{
    private readonly IPointsExclusionManager _pointsExclusionManager;
    public PointSelectController(IPointsExclusionManager pointsExclusionManager)
    {
        _pointsExclusionManager = pointsExclusionManager;
    }

    public bool enableCusps = false;

    private AstroConfig? _astroConfig;
    private List<SelectableChartPointDetails> _selCPDetails = new();


    public List<SelectableChartPointDetails> GetAllCelPointDetails(ResearchMethods method)
    {
        DefineChartPoints(method);
        return _selCPDetails;
    }

    public bool IncludeCusps()
    {
        return _astroConfig!.UseCuspsForAspects;
    }




    private void DefineChartPoints(ResearchMethods method)
    {
        _astroConfig = CurrentConfig.Instance.GetConfig();
        _selCPDetails = new();
        PointsToExclude pointsToExclude = _pointsExclusionManager.DefineExclusions(method);
        enableCusps = !pointsToExclude.ExcludeCusps;
        foreach (KeyValuePair<ChartPoints, ChartPointConfigSpecs> currentCPSpec in _astroConfig.ChartPoints)
        {
            if (currentCPSpec.Value.IsUsed && !pointsToExclude.ExcludedPoints.Contains(currentCPSpec.Key) && !(currentCPSpec.Key.GetDetails().PointCat == PointCats.Cusp && pointsToExclude.ExcludeCusps))
            {
                PointDetails cpDetails = currentCPSpec.Key.GetDetails();
                char glyph = currentCPSpec.Value.Glyph;
                _selCPDetails.Add(new SelectableChartPointDetails() { ChartPoint = cpDetails.Point, Glyph = glyph, Name = Rosetta.TextForId(cpDetails.TextId) });
            }
        }
    }

    public static void ShowHelp()
    {
        DataVault.Instance.CurrentViewBase = "SelectPointsForTest";
        HelpWindow helpWindow = new();
        helpWindow.ShowDialog();
    }

}