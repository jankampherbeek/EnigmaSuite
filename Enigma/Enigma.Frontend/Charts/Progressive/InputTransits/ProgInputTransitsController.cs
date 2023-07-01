// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Analysis.Aspects;
using Enigma.Domain.Calc.DateTime;
using Enigma.Domain.Configuration;
using Enigma.Domain.Points;
using Enigma.Frontend.Helpers.Support;
using Enigma.Frontend.Ui.State;
using Enigma.Frontend.Ui.Support;
using System.Collections.Generic;

namespace Enigma.Frontend.Ui.Charts.Progressive.InputTransits;

class ProgInputTransitsController
{
    private AstroConfig? _astroConfig;
    private List<SelectableChartPointDetails> _selCPDetails = new();


    public List<SelectableChartPointDetails> GetChartPointDetails()
    {
        DefineChartPoints();
        return _selCPDetails;
    }



    private void DefineChartPoints()
    {
        _astroConfig = CurrentConfig.Instance.GetConfig();
        _selCPDetails = new();
        foreach (KeyValuePair<ChartPoints, ChartPointConfigSpecs> currentCPSpec in _astroConfig.ChartPoints)
        {
            if (currentCPSpec.Value.IsUsed && currentCPSpec.Key.GetDetails().PointCat == PointCats.Common)
            {
                PointDetails cpDetails = currentCPSpec.Key.GetDetails();
                char glyph = currentCPSpec.Value.Glyph;
                _selCPDetails.Add(new SelectableChartPointDetails() { ChartPoint = cpDetails.Point, Glyph = glyph, Name = Rosetta.TextForId(cpDetails.TextId) });
            }
        }
    }



}
