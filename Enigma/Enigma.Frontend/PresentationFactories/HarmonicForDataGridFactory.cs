// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Charts;
using Enigma.Domain.Points;
using Enigma.Frontend.Helpers.Interfaces;
using Enigma.Frontend.Helpers.Support;
using Enigma.Frontend.Ui.Interfaces;
using Serilog;
using System.Collections.Generic;

namespace Enigma.Frontend.Ui.PresentationFactories;



public class HarmonicForDataGridFactory : IHarmonicForDataGridFactory
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
        if (harmonicPositions.Count != chart.ChartPointPositions.Count + 4)
        {
            Log.Error("ERROR in HarmonicDataGridFactory. Count for harmonicPositions = " + harmonicPositions.Count + " and count for celestial points = "
                + harmonicPositions.Count + ". The harmonicPositions should have a count that is 4 larger than the count for celestial points");
            throw new System.Exception("Error in Enigma. Please check the log file.");    // TODO use specific exception.
        }
        List<PresentableHarmonic> presentableHarmonics = new();
        List<FullChartPointPos> celPoints = chart.ChartPointPositions;
        int counterCelPoints = 0;
        for (int i = 0; i < celPoints.Count; i++)
        {
            counterCelPoints = i;
            char glyph = _glyphsForChartPoints.FindGlyph(celPoints[i].ChartPoint);
            double radixPos = celPoints[i].PointPos.Longitude.Position;
            double harmonicPos = harmonicPositions[i];
            presentableHarmonics.Add(CreatePresHarmonic(glyph, radixPos, harmonicPos));
        }
        presentableHarmonics.Add(CreatePresHarmonic('M', chart.FullHousePositions.Mc.PointPos.Longitude.Position, harmonicPositions[++counterCelPoints]));
        presentableHarmonics.Add(CreatePresHarmonic('A', chart.FullHousePositions.Ascendant.PointPos.Longitude.Position, harmonicPositions[counterCelPoints]));
        return presentableHarmonics;
    }

    private PresentableHarmonic CreatePresHarmonic(char celPointGlyph, double radixPos, double harmonicPos)
    {
        var (longTxt, glyph) = _doubleToDmsConversions.ConvertDoubleToDmsWithGlyph(radixPos);
        char radixSignGlyph = glyph;
        string radixPosText = longTxt;
        var harmonicPosPres = _doubleToDmsConversions.ConvertDoubleToDmsWithGlyph(harmonicPos);
        char harmonicSignGlyph = harmonicPosPres.glyph;
        string harmonicPosText = harmonicPosPres.longTxt;

        return new PresentableHarmonic(celPointGlyph, radixPosText, radixSignGlyph, harmonicPosText, harmonicSignGlyph);
    }




}