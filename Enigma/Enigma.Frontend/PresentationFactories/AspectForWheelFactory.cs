// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Analysis.Aspects;
using Enigma.Domain.Charts;
using Enigma.Domain.Points;
using Enigma.Frontend.Ui.Interfaces;
using System.Collections.Generic;

namespace Enigma.Frontend.Ui.PresentationFactories;



/// <inheritdoc/>
public class AspectForWheelFactory : IAspectForWheelFactory
{
    /// <inheritdoc/>
    public List<DrawableMundaneAspect> CreateMundaneAspectForWheel(List<DefinedAspect> aspects)
    {
        List<DrawableMundaneAspect> drawables = new();
        foreach (DefinedAspect defAspect in aspects)
        {
            double exactness = 100 - (defAspect.ActualOrb / defAspect.MaxOrb * 100);
            drawables.Add(new DrawableMundaneAspect(defAspect.Point1, defAspect.Point2, exactness, defAspect.Aspect.Aspect));
        }
        return drawables;
    }

    /// <inheritdoc/>
    public List<DrawableCelPointAspect> CreateCelPointAspectForWheel(List<DefinedAspect> aspects)
    {
        List<DrawableCelPointAspect> drawables = new();
        foreach (DefinedAspect defAspect in aspects)
        {
            double exactness = 100 - (defAspect.ActualOrb / defAspect.MaxOrb * 100);
            drawables.Add(new DrawableCelPointAspect((ChartPoints)defAspect.Point1, defAspect.Point2, exactness, defAspect.Aspect.Aspect));
        }
        return drawables;
    }

}
