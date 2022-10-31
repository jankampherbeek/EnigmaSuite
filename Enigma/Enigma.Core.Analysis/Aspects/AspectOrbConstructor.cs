// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Analysis.Interfaces;
using Enigma.Domain.Analysis;
using Enigma.Domain.Enums;
using Enigma.Domain.Interfaces;

namespace Enigma.Core.Analysis.Aspects;


public class AspectOrbConstructor : IAspectOrbConstructor
{

    private readonly double _baseOrb = 10.0;   // todo make baseorb configurable

    private readonly IOrbDefinitions _orbDefinitions;

    public AspectOrbConstructor(IOrbDefinitions orbDefinitions)
    {
        _orbDefinitions = orbDefinitions;
    }

    public double DefineOrb(SolarSystemPoints point1, SolarSystemPoints point2, AspectDetails aspectDetails)
    {
        double factor1 = _orbDefinitions.DefineSolSysPointOrb(point1).OrbFactor;
        double factor2 = _orbDefinitions.DefineSolSysPointOrb(point2).OrbFactor;
        double aspectFactor = aspectDetails.OrbFactor;
        return Math.Max(factor1, factor2) * aspectFactor * _baseOrb;
    }

    public double DefineOrb(string mundanePoint, SolarSystemPoints solSysPoint, AspectDetails aspectDetails)
    {
        double mundaneFactor = _orbDefinitions.DefineMundanePointOrb(mundanePoint).OrbFactor;
        double solSysFactor = _orbDefinitions.DefineSolSysPointOrb(solSysPoint).OrbFactor;
        double aspectFactor = aspectDetails.OrbFactor;
        return Math.Max(mundaneFactor, solSysFactor) * aspectFactor * _baseOrb;
    }
}