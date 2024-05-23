// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.Generic;
using System.Linq;
using Enigma.Domain.Dtos;
using Enigma.Domain.Presentables;
using Enigma.Domain.References;
using Enigma.Frontend.Ui.Support;
using Enigma.Frontend.Ui.Support.Conversions;

namespace Enigma.Frontend.Ui.PresentationFactories;

public interface IAspectForDataGridFactory
{
    /// <summary>Builds a presentable aspect to be used in a grid.</summary>
    /// <param name="aspects">Calculated aspects.</param>
    /// <returns>Presentable aspects.</returns>
    List<PresentableAspects> CreateAspectForDataGrid(IEnumerable<DefinedAspect> aspects);
}

public class AspectForDataGridFactory : IAspectForDataGridFactory
{
    private readonly IDoubleToDmsConversions _doubleToDmsConversions;
    private readonly GlyphsForChartPoints _glyphsForChartPoints;            // TODO replace GlyphsForChartPoints with a solution that uses the configuration.
    private readonly Rosetta _rosetta = Rosetta.Instance;
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
        string aspectText = _rosetta.GetText(effAspect.Aspect.RbKey);
        string orb = _doubleToDmsConversions.ConvertDoubleToPositionsDmsText(effAspect.ActualOrb);
        double exactnessValue = 100 - (effAspect.ActualOrb / effAspect.MaxOrb * 100);
        return new PresentableAspects(point1Text, point1Glyph, aspectText, aspectGlyph, point2Text, point2Glyph, orb, exactnessValue);
    }




}