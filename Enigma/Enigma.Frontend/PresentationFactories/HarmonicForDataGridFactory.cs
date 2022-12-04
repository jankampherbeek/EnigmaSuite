// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.AstronCalculations;
using Enigma.Domain.Charts;
using Enigma.Domain.Enums;
using Enigma.Domain.Interfaces;
using Enigma.Frontend.Helpers.Interfaces;
using Enigma.Frontend.Ui.Interfaces;
using Serilog;
using System.Collections.Generic;

namespace Enigma.Frontend.Ui.PresentationFactories;



public class HarmonicForDataGridFactory : IHarmonicForDataGridFactory
{
    private readonly IDoubleToDmsConversions _doubleToDmsConversions;

    public HarmonicForDataGridFactory(IDoubleToDmsConversions doubleToDmsConversions)
    {
        _doubleToDmsConversions = doubleToDmsConversions;
    }

    /// <inheritdoc/>
    public List<PresentableHarmonic> CreateHarmonicForDataGrid(List<double> harmonicPositions, CalculatedChart chart)
    {
        if (harmonicPositions.Count != chart.CelPointPositions.Count + 4)
        {
            Log.Error("ERROR in HarmonicDataGridFactory. Count for harmonicPositions = " + harmonicPositions.Count + " and count for celestial points = "
                + harmonicPositions.Count + ". The harmonicPositions should have a count that is 4 larger than the count for celestial points");
            throw new System.Exception("Error in Enigma. Please check the log file.");    // TODO use specific exception.
        }
        List<PresentableHarmonic> presentableHarmonics = new();
        List<FullCelPointPos> celPoints = chart.CelPointPositions;
        int counterCelPoints = 0;
        for (int i = 0; i < celPoints.Count; i++)
        {
            counterCelPoints = i;
            string glyph = celPoints[i].CelPoint.GetDetails().DefaultGlyph;
            double radixPos = celPoints[i].Longitude.Position;
            double harmonicPos = harmonicPositions[i];
            presentableHarmonics.Add(CreatePresHarmonic(glyph, radixPos, harmonicPos));
        }
        presentableHarmonics.Add(CreatePresHarmonic("M", chart.FullHousePositions.Mc.Longitude, harmonicPositions[++counterCelPoints]));
        presentableHarmonics.Add(CreatePresHarmonic("A", chart.FullHousePositions.Ascendant.Longitude, harmonicPositions[counterCelPoints]));
        return presentableHarmonics;
    }

    private PresentableHarmonic CreatePresHarmonic(string celPointGlyph, double radixPos, double harmonicPos)
    {
        var radixPosPres = _doubleToDmsConversions.ConvertDoubleToDmsWithGlyph(radixPos);
        string radixSignGlyph = radixPosPres.glyph;
        string radixPosText = radixPosPres.longTxt;
        var harmonicPosPres = _doubleToDmsConversions.ConvertDoubleToDmsWithGlyph(harmonicPos);
        string harmonicSignGlyph = harmonicPosPres.glyph;
        string harmonicPosText = harmonicPosPres.longTxt;

        return new PresentableHarmonic(celPointGlyph, radixPosText, radixSignGlyph, harmonicPosText, harmonicSignGlyph);
    }




}