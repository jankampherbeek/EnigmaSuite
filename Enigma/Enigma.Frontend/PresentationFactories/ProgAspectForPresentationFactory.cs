// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.Generic;
using System.Linq;
using Enigma.Domain.Dtos;
using Enigma.Domain.Presentables;
using Enigma.Domain.References;
using Enigma.Frontend.Helpers.Interfaces;
using Enigma.Frontend.Ui.Interfaces;
using Enigma.Frontend.Ui.Support;

namespace Enigma.Frontend.Ui.PresentationFactories;

/// <inheritdoc/>
public sealed class ProgAspectForPresentationFactory: IProgAspectForPresentationFactory
{
    private readonly IDoubleToDmsConversions _doubleToDmsConversions;

    public ProgAspectForPresentationFactory(IDoubleToDmsConversions doubleToDmsConversions)
    {
        _doubleToDmsConversions = doubleToDmsConversions;
    }
    
    /// <inheritdoc/>
    public List<PresentableProgAspect> CreatePresProgAspect(List<DefinedAspect> definedAspects)
    {
        return definedAspects.Select(definedAspect => ConvertToPresentable(definedAspect)).ToList();
    }


    private PresentableProgAspect ConvertToPresentable(DefinedAspect definedAspect)
    {
        char aspectGlyph = definedAspect.Aspect.Glyph;
        char progGlyph = GlyphsForChartPoints.FindGlyph(definedAspect.Point1); 
        // TODO replace GlyphsForChartPoints with a solution that uses the configuration.
        char radixGlyph = GlyphsForChartPoints.FindGlyph(definedAspect.Point2);
        string aspectName = definedAspect.Aspect.Text;
        string progName = definedAspect.Point1.GetDetails().Text;
        string radixName = definedAspect.Point2.GetDetails().Text;
        string orbText = _doubleToDmsConversions.ConvertDoubleToPositionsDmsText(definedAspect.ActualOrb);
        return new PresentableProgAspect(radixGlyph, radixName, aspectGlyph, aspectName, progGlyph, progName, orbText);
    }
}

