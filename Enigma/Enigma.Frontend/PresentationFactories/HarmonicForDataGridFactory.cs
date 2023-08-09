// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Domain.Charts;
using Enigma.Domain.Points;
using Enigma.Frontend.Helpers.Interfaces;
using Enigma.Frontend.Helpers.Support;
using Enigma.Frontend.Ui.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Enigma.Frontend.Ui.PresentationFactories;



public sealed class HarmonicForDataGridFactory : IHarmonicForDataGridFactory
{
    private readonly IDoubleToDmsConversions _doubleToDmsConversions;
    private readonly GlyphsForChartPoints _glyphsForChartPoints;

    public HarmonicForDataGridFactory(IDoubleToDmsConversions doubleToDmsConversions)
    {
        _doubleToDmsConversions = doubleToDmsConversions;
        _glyphsForChartPoints = new GlyphsForChartPoints();
    }

    /// <inheritdoc/>
    public List<PresentableHarmonic> CreateHarmonicForDataGrid(List<double> harmonicPositions, CalculatedChart chart)
    {
        Dictionary<ChartPoints, FullPointPos> celPoints = (
            from posPoint in chart.Positions
            where posPoint.Key.GetDetails().PointCat == PointCats.Common || posPoint.Key.GetDetails().PointCat == PointCats.Angle
            select posPoint).ToDictionary(x => x.Key, x => x.Value);
        int counterCelPoints = 0;
        return (from celPoint in celPoints 
            where celPoint.Key != ChartPoints.EastPoint && celPoint.Key != ChartPoints.Vertex 
            let glyph = GlyphsForChartPoints.FindGlyph(celPoint.Key) 
            let radixPos = celPoint.Value.Ecliptical.MainPosSpeed.Position 
            let harmonicPos = harmonicPositions[counterCelPoints++] 
            select CreatePresHarmonic(glyph, radixPos, harmonicPos)).ToList();
    }

    private PresentableHarmonic CreatePresHarmonic(char celPointGlyph, double radixPos, double harmonicPos)
    {
        var (longTxt, glyph) = _doubleToDmsConversions.ConvertDoubleToDmsWithGlyph(radixPos);
        (string? harmonicPosText, char harmonicSignGlyph) = _doubleToDmsConversions.ConvertDoubleToDmsWithGlyph(harmonicPos);

        return new PresentableHarmonic(celPointGlyph, longTxt, glyph, harmonicPosText, harmonicSignGlyph);
    }

}