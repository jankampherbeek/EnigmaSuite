// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Analysis;
using Enigma.Domain.Charts;
using Enigma.Domain.Enums;
using Enigma.Frontend.Helpers.Interfaces;
using Enigma.Frontend.Ui.Interfaces;
using System.Collections.Generic;

namespace Enigma.Frontend.Ui.PresentationFactories;



public class AspectForDataGridFactory : IAspectForDataGridFactory
{
    private readonly IDoubleToDmsConversions _doubleToDmsConversions;

    public AspectForDataGridFactory(IDoubleToDmsConversions doubleToDmsConversions)
    {
        _doubleToDmsConversions = doubleToDmsConversions;
    }

    /// <inheritdoc/>
    public List<PresentableAspects> CreateAspectForDataGrid(List<EffectiveAspect> aspects)
    {
        List<PresentableAspects> presentableAspects = new();
        foreach (var aspect in aspects)
        {
            presentableAspects.Add(CreatePresAspect(aspect));
        }
        return presentableAspects;

    }

    private PresentableAspects CreatePresAspect(EffectiveAspect effAspect)
    {
        string firstPoint = "";
        if (effAspect.IsMundane)
        {
            firstPoint = effAspect.MundanePoint;
        }
        else if (effAspect.CelPoint1 != null)
        {
            firstPoint = ((CelPoints)effAspect.CelPoint1).GetDetails().DefaultGlyph;
        }
        string aspectGlyph = effAspect.EffAspectDetails.Aspect.GetDetails().Glyph;
        string secondPoint = effAspect.CelPoint2.GetDetails().DefaultGlyph;

        string orb = _doubleToDmsConversions.ConvertDoubleToPositionsDmsText(effAspect.Orb);
        double exactnessValue = 100 - (effAspect.ActualOrb / effAspect.Orb * 100);
        string exactness = string.Format("{0:N}", exactnessValue).Replace(",", ".");
        return new PresentableAspects(firstPoint, aspectGlyph, secondPoint, orb, exactness);
    }




}