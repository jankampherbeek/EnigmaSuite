// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Analysis;
using Enigma.Domain.CalcVars;
using Enigma.InputSupport.Conversions;
using Enigma.Frontend.UiDomain;
using System.Collections.Generic;

namespace Enigma.Frontend.PresentationFactories;


public interface IAspectForDataGridFactory
{
    /// <summary>Builds a presentable aspect to be used in a grid.</summary>
    /// <param name="aspects">Calculated aspects.</param>
    /// <returns>Presentable aspects.</returns>
    List<PresentableAspects> CreateAspectForDataGrid(List<EffectiveAspect> aspects);
}


public class AspectForDataGridFactory : IAspectForDataGridFactory
{
    private readonly IDoubleToDmsConversions _doubleToDmsConversions;
    private readonly ISolarSystemPointSpecifications _solarSystemPointSpecifications;
    private readonly IAspectSpecifications _aspectSpecifications;

    public AspectForDataGridFactory(IDoubleToDmsConversions doubleToDmsConversions, 
        ISolarSystemPointSpecifications solarSystemPointSpecifications,
        IAspectSpecifications aspectSpecifications)
    {
        _doubleToDmsConversions = doubleToDmsConversions;
        _solarSystemPointSpecifications = solarSystemPointSpecifications;
        _aspectSpecifications = aspectSpecifications;
    }

    /// <inheritdoc/>
    public List<PresentableAspects> CreateAspectForDataGrid(List<EffectiveAspect> aspects)
    {
        List<PresentableAspects> presentableAspects = new List<PresentableAspects>();
        foreach (var aspect in aspects)
        {
            presentableAspects.Add(CreatePresAspect(aspect));
        }
        return presentableAspects;

    }

    private PresentableAspects CreatePresAspect(EffectiveAspect effAspect)
    {
        string firstPoint = "";
        if (effAspect.IsMundane )
        {
            firstPoint = effAspect.MundanePoint;
        }
        else if (effAspect.SolSysPoint1 != null)
        {
            firstPoint = _solarSystemPointSpecifications.DetailsForPoint((SolarSystemPoints)effAspect.SolSysPoint1).DefaultGlyph;
        }
        string aspectGlyph = _aspectSpecifications.DetailsForAspect(effAspect.EffAspectDetails.Aspect).Glyph;
        string secondPoint = _solarSystemPointSpecifications.DetailsForPoint(effAspect.SolSysPoint2).DefaultGlyph;

        string orb = _doubleToDmsConversions.ConvertDoubleToPositionsDmsText(effAspect.Orb);
        double exactnessValue = 100 - ((effAspect.ActualOrb / effAspect.Orb) * 100);
        string exactness = string.Format("{0:N}", exactnessValue).Replace(",", "."); 
        return new PresentableAspects(firstPoint, aspectGlyph, secondPoint, orb, exactness);
    }




}