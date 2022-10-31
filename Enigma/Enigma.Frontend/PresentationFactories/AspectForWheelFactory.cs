// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Analysis;
using Enigma.Domain.Charts;
using Enigma.Domain.Enums;
using Enigma.Frontend.Interfaces;
using System.Collections.Generic;

namespace Enigma.Frontend.PresentationFactories;



/// <inheritdoc/>
public class AspectForWheelFactory : IAspectForWheelFactory
{
    /// <inheritdoc/>
    public List<DrawableMundaneAspect> CreateMundaneAspectForWheel(List<EffectiveAspect> aspects)
    {
        List<DrawableMundaneAspect> drawables = new();
        foreach (var effAspect in aspects)
        {
            double exactness = 100 - ((effAspect.ActualOrb / effAspect.Orb) * 100);
            drawables.Add(new DrawableMundaneAspect(effAspect.MundanePoint, effAspect.SolSysPoint2, exactness, effAspect.EffAspectDetails.Aspect));
        }
        return drawables;
    }

    /// <inheritdoc/>
    public List<DrawableSolSysPointAspect> CreateSolSysAspectForWheel(List<EffectiveAspect> aspects)
    {
        List<DrawableSolSysPointAspect> drawables = new();
        foreach (var effAspect in aspects)
        {
            double exactness = 100 - ((effAspect.ActualOrb / effAspect.Orb) * 100);
            if (effAspect.SolSysPoint1 != null)
            {
                drawables.Add(new DrawableSolSysPointAspect((SolarSystemPoints)effAspect.SolSysPoint1, effAspect.SolSysPoint2, exactness, effAspect.EffAspectDetails.Aspect));
            }
        }
        return drawables;
    }

}
