// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2024.
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

public interface IParallelsForDataGridFactory
{
    /// <summary>Builds a presentable parallel to be used in a grid.</summary>
    /// <param name="parallels">Calculated parallels.</param>
    /// <returns>Presentable aspects.</returns>
    List<PresentableParallels> CreateParallelsForDataGrid(IEnumerable<DefinedParallel> parallels);
}

public class ParallelsForDataGridFactory : IParallelsForDataGridFactory
{
    private readonly IDoubleToDmsConversions _doubleToDmsConversions;
    private readonly GlyphsForChartPoints _glyphsForChartPoints;            // TODO replace GlyphsForChartPoints with a solution that uses the configuration.

    public ParallelsForDataGridFactory(IDoubleToDmsConversions doubleToDmsConversions)
    {
        _doubleToDmsConversions = doubleToDmsConversions;
        _glyphsForChartPoints = new GlyphsForChartPoints();
    }

    /// <inheritdoc/>
    public List<PresentableParallels> CreateParallelsForDataGrid(IEnumerable<DefinedParallel> parallels)
    {
        return parallels.Select(CreatePresParallel).ToList();
    }

    private PresentableParallels CreatePresParallel(DefinedParallel effParallel)
    {
        char point1Glyph = GlyphsForChartPoints.FindGlyph(effParallel.Point1);
        char point2Glyph = GlyphsForChartPoints.FindGlyph(effParallel.Point2);
        char typeGlyph = effParallel.OppParallel ? 'P' : 'O';
        string point1Text = effParallel.Point1.GetDetails().Text;
        string point2Text = effParallel.Point2.GetDetails().Text;
        string orb = _doubleToDmsConversions.ConvertDoubleToPositionsDmsText(effParallel.ActualOrb);
        double exactnessValue = 100 - (effParallel.ActualOrb / effParallel.MaxOrb * 100);
        return new PresentableParallels(point1Text, point1Glyph, typeGlyph, point2Text, point2Glyph, orb, exactnessValue);
    }




}