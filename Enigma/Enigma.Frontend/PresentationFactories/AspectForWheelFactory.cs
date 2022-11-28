// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Analysis;
using Enigma.Domain.Charts;
using Enigma.Domain.Enums;
using Enigma.Frontend.Ui.Interfaces;
using System.Collections.Generic;

namespace Enigma.Frontend.Ui.PresentationFactories;



/// <inheritdoc/>
public class AspectForWheelFactory : IAspectForWheelFactory
{
    /// <inheritdoc/>
    public List<DrawableMundaneAspect> CreateMundaneAspectForWheel(List<EffectiveAspect> aspects)
    {
        List<DrawableMundaneAspect> drawables = new();
        foreach (var effAspect in aspects)
        {
            double exactness = 100 - (effAspect.ActualOrb / effAspect.Orb * 100);
            drawables.Add(new DrawableMundaneAspect(effAspect.MundanePoint, effAspect.CelPoint2, exactness, effAspect.EffAspectDetails.Aspect));
        }
        return drawables;
    }

    /// <inheritdoc/>
    public List<DrawableCelPointAspect> CreateCelPointAspectForWheel(List<EffectiveAspect> aspects)
    {
        List<DrawableCelPointAspect> drawables = new();
        foreach (var effAspect in aspects)
        {
            double exactness = 100 - (effAspect.ActualOrb / effAspect.Orb * 100);
            if (effAspect.CelPoint1 != null)
            {
                drawables.Add(new DrawableCelPointAspect((CelPoints)effAspect.CelPoint1, effAspect.CelPoint2, exactness, effAspect.EffAspectDetails.Aspect));
            }
        }
        return drawables;
    }

}
