// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Domain.Analysis;
using Enigma.Domain.Charts;
using Enigma.Domain.Interfaces;
using Enigma.Frontend.Helpers.Interfaces;
using Enigma.Frontend.Ui.Interfaces;
using System.Collections.Generic;

namespace Enigma.Frontend.Ui.PresentationFactories;


public class MidpointForDataGridFactory : IMidpointForDataGridFactory
{
    private readonly IDoubleToDmsConversions _doubleToDmsConversions;
    private readonly ISolarSystemPointSpecifications _solarSystemPointSpecifications;

    public MidpointForDataGridFactory(IDoubleToDmsConversions doubleToDmsConversions, 
        ISolarSystemPointSpecifications solarSystemPointSpecifications)
    {
        _doubleToDmsConversions = doubleToDmsConversions;
        _solarSystemPointSpecifications = solarSystemPointSpecifications;
    }

    /// <inheritdoc/>
    public List<PresentableMidpoint> CreateMidpointsDataGrid(List<EffectiveMidpoint> midpoints)
    {
        List<PresentableMidpoint> presentableMidpoints = new();
        foreach (var midpoint in midpoints)
        {
            presentableMidpoints.Add(CreatePresMidpoint(midpoint));
        }
        return presentableMidpoints;
    }

    /// <inheritdoc/>
    public List<PresentableOccupiedMidpoint> CreateMidpointsDataGrid(List<EffOccupiedMidpoint> midpoints)
    {
        List<PresentableOccupiedMidpoint> presentableOccMidpoints = new();
        foreach (var midpoint in midpoints)
        {
            presentableOccMidpoints.Add(CreatePresMidpoint(midpoint));
        }
        return presentableOccMidpoints;
    }

    private PresentableMidpoint CreatePresMidpoint(EffectiveMidpoint midpoint) {
        string point1Glyph = midpoint.Point1.Glyph;
        string point2Glyph = midpoint.Point2.Glyph;
        var (longTxt, glyph) = _doubleToDmsConversions.ConvertDoubleToDmsWithGlyph(midpoint.Position);
        string position = longTxt;
        string signGlyph = glyph;
        return new PresentableMidpoint(point1Glyph, point2Glyph, signGlyph, position);

    }

    private PresentableOccupiedMidpoint CreatePresMidpoint(EffOccupiedMidpoint midpoint)
    {
        string point1Glyph = midpoint.EffMidpoint.Point1.Glyph;
        string point2Glyph = midpoint.EffMidpoint.Point2.Glyph;
        string pointOccGlyph = midpoint.OccupyingPoint.Glyph;
        string orbText = _doubleToDmsConversions.ConvertDoubleToPositionsDmsText(midpoint.Orb);
        string exactnessText = System.Math.Floor(midpoint.Exactness).ToString() + " %"; 
        return new PresentableOccupiedMidpoint(point1Glyph, point2Glyph, pointOccGlyph, orbText, exactnessText);
    }
}


