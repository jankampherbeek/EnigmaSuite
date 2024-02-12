// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.Generic;
using System.Linq;
using Enigma.Domain.Dtos;
using Enigma.Domain.Graphics;

namespace Enigma.Frontend.Ui.PresentationFactories;


public interface IAspectForWheelFactory
{
    /// <summary>Builds a drawable aspect between two celestial points, that can be used in a wheel.</summary>
    /// <param name="aspects">Calculated aspects.</param>
    /// <returns>Drawable aspects.</returns>
    List<DrawableCelPointAspect> CreateCelPointAspectForWheel(IEnumerable<DefinedAspect> aspects);

    /// <summary>Builds a drawable aspect between a mundane point and a celestial point, that can be used in a wheel.</summary>
    /// <param name="aspects">Calculated aspects.</param>
    /// <returns>Drawable aspects.</returns>
    List<DrawableMundaneAspect> CreateMundaneAspectForWheel(IEnumerable<DefinedAspect> aspects);
}

/// <inheritdoc/>
public class AspectForWheelFactory : IAspectForWheelFactory
{
    /// <inheritdoc/>
    public List<DrawableMundaneAspect> CreateMundaneAspectForWheel(IEnumerable<DefinedAspect> aspects)
    {
        return (from defAspect in aspects 
            let exactness = 100 - (defAspect.ActualOrb / defAspect.MaxOrb * 100) 
            select new DrawableMundaneAspect(defAspect.Point1, defAspect.Point2, exactness, defAspect.Aspect.Aspect)).ToList();
    }

    /// <inheritdoc/>
    public List<DrawableCelPointAspect> CreateCelPointAspectForWheel(IEnumerable<DefinedAspect> aspects)
    {
        return (from defAspect in aspects 
            let exactness = 100 - (defAspect.ActualOrb / defAspect.MaxOrb * 100) 
            select new DrawableCelPointAspect(defAspect.Point1, defAspect.Point2, exactness, defAspect.Aspect.Aspect)).ToList();
    }

}
