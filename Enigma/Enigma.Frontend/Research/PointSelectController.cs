// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Domain.Configuration;
using Enigma.Domain.Points;
using Enigma.Frontend.Helpers.Support;
using Enigma.Frontend.Ui.State;
using Enigma.Frontend.Ui.Support;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Windows;

namespace Enigma.Frontend.Ui.Research;
public sealed class PointSelectController
{


    private AstroConfig? _astroConfig;
    private List<SelectableCelPointDetails> _selCPDetails = new();
    private List<SelectableMundanePointDetails> _selMPDetails = new();
    private readonly HelpWindow _helpWindow = App.ServiceProvider.GetRequiredService<HelpWindow>();


    public List<SelectableCelPointDetails> GetAllCelPointDetails()
    {
        DefineCelPoints();
        return _selCPDetails;
    }

    public List<SelectableMundanePointDetails> GetAllMundanePointDetails()
    {
        DefineMundanePoints();
        return _selMPDetails;
    }

    public bool IncludeCuspsForAspects()
    {
        return _astroConfig!.UseCuspsForAspects;
    }

    public class SelectableCelPointDetails
    {
        public ChartPoints ChartPoint { get; set; }
        public char? Glyph { get; set; }
        public string? Name { get; set; }
    }

    public class SelectableMundanePointDetails
    {
        public ChartPoints MundanePoint { get; set; }
        public string? Name { get; set; }
    }


    private void DefineCelPoints()
    {
        _astroConfig = CurrentConfig.Instance.GetConfig();
        _selCPDetails = new();
        foreach (ChartPointConfigSpecs currentCPSpec in _astroConfig.ChartPoints)
        {
            PointCats cat = currentCPSpec.Point.GetDetails().PointCat;
            if (currentCPSpec.IsUsed && cat != PointCats.Angle && cat != PointCats.Cusp)
            {
                PointDetails cpDetails = currentCPSpec.Point.GetDetails();
                char glyph = currentCPSpec.Glyph;
                _selCPDetails.Add(new SelectableCelPointDetails() { ChartPoint = cpDetails.Point, Glyph = glyph, Name = Rosetta.TextForId(cpDetails.TextId) });
            }
        }
    }

    private void DefineMundanePoints()
    {
        _astroConfig = CurrentConfig.Instance.GetConfig();
        _selMPDetails = new();
        foreach (ChartPointConfigSpecs currentSpec in _astroConfig.ChartPoints)
        {
            if (currentSpec.IsUsed && currentSpec.Point.GetDetails().PointCat == PointCats.Angle)
            {
                PointDetails mpDetails = currentSpec.Point.GetDetails();
                _selMPDetails.Add(new SelectableMundanePointDetails() { MundanePoint = mpDetails.Point, Name = Rosetta.TextForId(mpDetails.TextId) });
            }
        }
    }

    public void ShowHelp()
    {
        _helpWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        _helpWindow.SetHelpPage("SelectPointsForTest");
        _helpWindow.ShowDialog();
    }
}