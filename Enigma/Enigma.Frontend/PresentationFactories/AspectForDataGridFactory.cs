// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Analysis.Aspects;
using Enigma.Domain.Charts;
using Enigma.Domain.Points;
using Enigma.Frontend.Helpers.Interfaces;
using Enigma.Frontend.Helpers.Support;
using Enigma.Frontend.Ui.Interfaces;
using System.Collections.Generic;

namespace Enigma.Frontend.Ui.PresentationFactories;



public class AspectForDataGridFactory : IAspectForDataGridFactory
{
    private readonly IDoubleToDmsConversions _doubleToDmsConversions;
    private readonly GlyphsForChartPoints _glyphsForChartPoints;            // TODO 0.3 replace GlyphsForChartPoints with a solution that uses the configuration.

    public AspectForDataGridFactory(IDoubleToDmsConversions doubleToDmsConversions)
    {
        _doubleToDmsConversions = doubleToDmsConversions;
        _glyphsForChartPoints = new GlyphsForChartPoints();
    }

    /// <inheritdoc/>
    public List<PresentableAspects> CreateAspectForDataGrid(List<DefinedAspect> aspects)
    {
        List<PresentableAspects> presentableAspects = new();
        foreach (var aspect in aspects)
        {
            presentableAspects.Add(CreatePresAspect(aspect));
        }
        return presentableAspects;

    }

    private PresentableAspects CreatePresAspect(DefinedAspect effAspect)
    {
        char point1Glyph = _glyphsForChartPoints.FindGlyph(effAspect.Point1);
        char point2Glyph = _glyphsForChartPoints.FindGlyph(effAspect.Point2);
        char aspectGlyph = effAspect.Aspect.Glyph;
        string point1Text = Rosetta.TextForId(effAspect.Point1.GetDetails().Text);
        string point2Text = Rosetta.TextForId(effAspect.Point2.GetDetails().Text);
        string aspectText = Rosetta.TextForId(effAspect.Aspect.Text);
        string orb = _doubleToDmsConversions.ConvertDoubleToPositionsDmsText(effAspect.ActualOrb);
        double exactnessValue = 100 - (effAspect.ActualOrb / effAspect.MaxOrb * 100);
        string exactness = string.Format("{0:N}", exactnessValue).Replace(",", ".");
        return new PresentableAspects(point1Text, point1Glyph, aspectText, aspectGlyph, point2Text, point2Glyph, orb, exactness);
    }




}