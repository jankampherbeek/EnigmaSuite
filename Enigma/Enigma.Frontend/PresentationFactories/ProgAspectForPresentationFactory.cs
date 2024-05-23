// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023, 2024.
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

/// <summary>Conversions for presentable progressive aspects.</summary>
public interface IProgAspectForPresentationFactory
{
    /// <summary>Convert Defined Aspects to PresentableProgaspects.</summary>
    /// <param name="definedAspects">Aspect definitions.</param>
    /// <returns>The resulting PresentableProgAspects.</returns>
    public List<PresentableProgAspect> CreatePresProgAspect(List<DefinedAspect> definedAspects);
}

/// <inheritdoc/>
public sealed class ProgAspectForPresentationFactory: IProgAspectForPresentationFactory
{
    private readonly IDoubleToDmsConversions _doubleToDmsConversions;
    private readonly Rosetta _rosetta = Rosetta.Instance;

    public ProgAspectForPresentationFactory(IDoubleToDmsConversions doubleToDmsConversions)
    {
        _doubleToDmsConversions = doubleToDmsConversions;
    }
    
    /// <inheritdoc/>
    public List<PresentableProgAspect> CreatePresProgAspect(List<DefinedAspect> definedAspects)
    {
        return definedAspects.Select(ConvertToPresentable).ToList();
    }


    private PresentableProgAspect ConvertToPresentable(DefinedAspect definedAspect)
    {
        char aspectGlyph = definedAspect.Aspect.Glyph;
        char progGlyph = GlyphsForChartPoints.FindGlyph(definedAspect.Point1); 
        // TODO 0.3 replace GlyphsForChartPoints with a solution that uses the configuration.
        char radixGlyph = GlyphsForChartPoints.FindGlyph(definedAspect.Point2);
        string aspectName = _rosetta.GetText(definedAspect.Aspect.RbKey);
        string progName = definedAspect.Point1.GetDetails().Text;
        string radixName = definedAspect.Point2.GetDetails().Text;
        string orbText = _doubleToDmsConversions.ConvertDoubleToPositionsDmsText(definedAspect.ActualOrb);
        return new PresentableProgAspect(radixGlyph, radixName, aspectGlyph, aspectName, progGlyph, progName, orbText);
    }
}
