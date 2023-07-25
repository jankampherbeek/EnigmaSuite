// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Analysis.Aspects;
using Enigma.Domain.Configuration;
using Enigma.Domain.Points;
using Enigma.Domain.Research;
using Enigma.Frontend.Helpers.Support;
using Enigma.Frontend.Ui.State;
using Enigma.Frontend.Ui.Support;
using System.Collections.Generic;

namespace Enigma.Frontend.Ui.Charts.Progressive;

public class ChartProgPrimInputController
{

    private AstroConfig? _astroConfig;
    private List<SelectableChartPointDetails> _selCPDetails = new();
    private List<SelectableAspectDetails> _selAspectDetails = new();

    public List<SelectableChartPointDetails> GetChartPointDetails()
    {
        DefineChartPoints();
        return _selCPDetails;
    }

    public List<SelectableAspectDetails> GetAspectDetails()
    {
        DefineAspects();
        return _selAspectDetails;
    }


    private void DefineChartPoints()
    {
        _astroConfig = CurrentConfig.Instance.GetConfig();
        _selCPDetails = new();
        foreach (KeyValuePair<ChartPoints, ChartPointConfigSpecs> currentCPSpec in _astroConfig.ChartPoints)
        {
            if (currentCPSpec.Value.IsUsed)
            {
                PointDetails cpDetails = currentCPSpec.Key.GetDetails();
                char glyph = currentCPSpec.Value.Glyph;
                _selCPDetails.Add(new SelectableChartPointDetails() { ChartPoint = cpDetails.Point, Glyph = glyph, Name = Rosetta.TextForId(cpDetails.Text) });
            }
        }
    }

    private void DefineAspects()
    {
        _astroConfig = CurrentConfig.Instance.GetConfig();
        _selAspectDetails = new();
        foreach (KeyValuePair<AspectTypes, AspectConfigSpecs> currentAspectSpec in _astroConfig.Aspects)
        {
            if (currentAspectSpec.Value.IsUsed)
            {
                AspectDetails aspectDetails = currentAspectSpec.Key.GetDetails();
                char glyph = currentAspectSpec.Value.Glyph;
                _selAspectDetails.Add(new SelectableAspectDetails() { Aspect = aspectDetails.Aspect, Glyph = glyph, Name = Rosetta.TextForId(aspectDetails.Text) });
            }
        }
    }




}
