// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Domain.Analysis;
using Enigma.Domain.Charts;
using Enigma.Frontend.Helpers.Interfaces;
using Enigma.Frontend.Ui.Interfaces;
using System.Collections.Generic;

namespace Enigma.Frontend.Ui.PresentationFactories;

// TODO 0.1 Analysis
public class MidpointForDataGridFactory : IMidpointForDataGridFactory
{
    private readonly IDoubleToDmsConversions _doubleToDmsConversions;

    public MidpointForDataGridFactory(IDoubleToDmsConversions doubleToDmsConversions)
    {
        _doubleToDmsConversions = doubleToDmsConversions;
    }

    /// <inheritdoc/>
    public List<PresentableMidpoint> CreateMidpointsDataGrid(List<BaseMidpoint> midpoints)
    {
        List<PresentableMidpoint> presentableMidpoints = new();
        foreach (var midpoint in midpoints)
        {
            presentableMidpoints.Add(CreatePresMidpoint(midpoint));
        }
        return presentableMidpoints;
    }

    /// <inheritdoc/>
    public List<PresentableOccupiedMidpoint> CreateMidpointsDataGrid(List<OccupiedMidpoint> midpoints)
    {
        List<PresentableOccupiedMidpoint> presentableOccMidpoints = new();
        foreach (var midpoint in midpoints)
        {
            presentableOccMidpoints.Add(CreatePresMidpoint(midpoint));
        }
        return presentableOccMidpoints;
    }

    private PresentableMidpoint CreatePresMidpoint(BaseMidpoint midpoint)
    {
        /*
        string point1Glyph = midpoint.Point1.Glyph;
        string point2Glyph = midpoint.Point2.Glyph; 
        var (longTxt, glyph) = _doubleToDmsConversions.ConvertDoubleToDmsWithGlyph(midpoint.Position);
        string position = longTxt;
        string signGlyph = glyph; */
        char tempGlyph = ' ';
        return new PresentableMidpoint(tempGlyph, tempGlyph, tempGlyph, "positionTxt");

    }

    private PresentableOccupiedMidpoint CreatePresMidpoint(OccupiedMidpoint midpoint)
    {
        /*
              string point1Glyph = midpoint.Midpoint.Point1.Glyph;
          string point2Glyph = midpoint.Midpoint.Point2.Glyph;
          string pointOccGlyph = midpoint.OccupyingPoint.Glyph;
        */
        string orbText = _doubleToDmsConversions.ConvertDoubleToPositionsDmsText(midpoint.Orb);
        string exactnessText = System.Math.Floor(midpoint.Exactness).ToString() + " %";
        string tempGlyph = "";
        return new PresentableOccupiedMidpoint(tempGlyph, tempGlyph, tempGlyph, orbText, exactnessText);
    }
}


