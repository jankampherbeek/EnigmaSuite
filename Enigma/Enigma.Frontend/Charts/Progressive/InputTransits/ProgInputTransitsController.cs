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
    private List<SelectableChartPointDetails> _selCpDetails = new();


    public List<SelectableChartPointDetails> GetChartPointDetails()
    {
        DefineChartPoints();
        return _selCpDetails;
    }



    private void DefineChartPoints()
    {
        _astroConfig = CurrentConfig.Instance.GetConfig();
        _selCpDetails = new List<SelectableChartPointDetails>();
        foreach (KeyValuePair<ChartPoints, ChartPointConfigSpecs> currentCpSpec in _astroConfig.ChartPoints)
        {
            if (!currentCpSpec.Value.IsUsed || currentCpSpec.Key.GetDetails().PointCat != PointCats.Common) continue;
            PointDetails cpDetails = currentCpSpec.Key.GetDetails();
            char glyph = currentCpSpec.Value.Glyph;
            _selCpDetails.Add(new SelectableChartPointDetails() { ChartPoint = cpDetails.Point, Glyph = glyph, Name = Rosetta.TextForId(cpDetails.Text)});
        }
    }



}
