// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Points;
using Enigma.Frontend.Helpers.Interfaces;
using Enigma.Frontend.Ui.Interfaces;
using System.Collections.Generic;
using System.Linq;
using Enigma.Domain.Dtos;
using Enigma.Domain.Presentables;
using Enigma.Domain.References;
using Enigma.Frontend.Ui.Support;

namespace Enigma.Frontend.Ui.PresentationFactories;



public class AspectForDataGridFactory : IAspectForDataGridFactory
{
    private readonly IDoubleToDmsConversions _doubleToDmsConversions;
    private readonly GlyphsForChartPoints _glyphsForChartPoints;            // TODO replace GlyphsForChartPoints with a solution that uses the configuration.

    public AspectForDataGridFactory(IDoubleToDmsConversions doubleToDmsConversions)
    {
        _doubleToDmsConversions = doubleToDmsConversions;
        _glyphsForChartPoints = new GlyphsForChartPoints();
    }

    /// <inheritdoc/>
    public List<PresentableAspects> CreateAspectForDataGrid(IEnumerable<DefinedAspect> aspects)
    {
        return aspects.Select(CreatePresAspect).ToList();
    }

    private PresentableAspects CreatePresAspect(DefinedAspect effAspect)
    {
        char point1Glyph = GlyphsForChartPoints.FindGlyph(effAspect.Point1);
        char point2Glyph = GlyphsForChartPoints.FindGlyph(effAspect.Point2);
        char aspectGlyph = effAspect.Aspect.Glyph;
        string point1Text = effAspect.Point1.GetDetails().Text;
        string point2Text = effAspect.Point2.GetDetails().Text;
        string aspectText = effAspect.Aspect.Text;
        string orb = _doubleToDmsConversions.ConvertDoubleToPositionsDmsText(effAspect.ActualOrb);
        double exactnessValue = 100 - (effAspect.ActualOrb / effAspect.MaxOrb * 100);
        string exactness = $"{exactnessValue:N}".Replace(",", ".");
        return new PresentableAspects(point1Text, point1Glyph, aspectText, aspectGlyph, point2Text, point2Glyph, orb, exactness);
    }




}