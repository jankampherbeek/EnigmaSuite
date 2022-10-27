// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Analysis;
using Enigma.Domain.CalcVars;
using Enigma.Frontend.UiDomain;
using System.Collections.Generic;

namespace Enigma.Frontend.PresentationFactories;

public interface IAspectForWheelFactory
{
    /// <summary>Builds a drawable aspect between two solar system points, that can be used in a wheel.</summary>
    /// <param name="aspects">Calculated aspects.</param>
    /// <returns>Drawable aspects.</returns>
    List<DrawableSolSysPointAspect> CreateSolSysAspectForWheel(List<EffectiveAspect> aspects);

    /// <summary>Builds a drawable aspect between a mundane point and a solar system point, that can be used in a wheel.</summary>
    /// <param name="aspects">Calculated aspects.</param>
    /// <returns>Drawable aspects.</returns>
    List<DrawableMundaneAspect> CreateMundaneAspectForWheel(List<EffectiveAspect> aspects);
}


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
            drawables.Add( new DrawableMundaneAspect(effAspect.MundanePoint, effAspect.SolSysPoint2, exactness, effAspect.EffAspectDetails.Aspect));
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
