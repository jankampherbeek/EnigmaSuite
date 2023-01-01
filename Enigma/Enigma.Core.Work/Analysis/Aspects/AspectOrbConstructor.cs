// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Work.Analysis.Interfaces;
using Enigma.Domain.Analysis.Aspects;
using Enigma.Domain.Interfaces;
using Enigma.Domain.Points;

namespace Enigma.Core.Work.Analysis.Aspects;


public class AspectOrbConstructor : IAspectOrbConstructor
{

    private readonly double _baseOrb = 10.0;   // todo make baseorb configurable

    private readonly IOrbDefinitions _orbDefinitions;

    public AspectOrbConstructor(IOrbDefinitions orbDefinitions)
    {
        _orbDefinitions = orbDefinitions;
    }

    public double DefineOrb(CelPoints point1, CelPoints point2, AspectDetails aspectDetails)
    {
        double factor1 = _orbDefinitions.DefineCelPointOrb(point1).OrbFactor;
        double factor2 = _orbDefinitions.DefineCelPointOrb(point2).OrbFactor;
        double aspectFactor = aspectDetails.OrbFactor;
        return Math.Max(factor1, factor2) * aspectFactor * _baseOrb;
    }

    public double DefineOrb(string mundanePoint, CelPoints celPoint, AspectDetails aspectDetails)
    {
        double mundaneFactor = _orbDefinitions.DefineMundanePointOrb(mundanePoint).OrbFactor;
        double celPointFactor = _orbDefinitions.DefineCelPointOrb(celPoint).OrbFactor;
        double aspectFactor = aspectDetails.OrbFactor;
        return Math.Max(mundaneFactor, celPointFactor) * aspectFactor * _baseOrb;
    }
}