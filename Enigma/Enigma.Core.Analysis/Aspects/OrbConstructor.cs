// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Analysis;
using Enigma.Domain.CalcVars;

namespace Enigma.Core.Analysis.Aspects;

public interface IOrbConstructor
{
    public double DefineOrb(SolarSystemPoints point1, SolarSystemPoints point2, AspectDetails aspectDetails);
    public double DefineOrb(string mundanePoint, SolarSystemPoints solSysPoint, AspectDetails aspectDetails);
}

public class OrbConstructor: IOrbConstructor
{

    private IOrbDefinitions _orbDefinitions;

    public OrbConstructor(IOrbDefinitions orbDefinitions)
    {
        _orbDefinitions = orbDefinitions;
    }

    public double DefineOrb(SolarSystemPoints point1, SolarSystemPoints point2, AspectDetails aspectDetails)
    {
        double factor1 = _orbDefinitions.DefineSolSysPointOrb(point1).OrbFactor;
        double factor2 = _orbDefinitions.DefineSolSysPointOrb(point2).OrbFactor;
        double aspectFactor = aspectDetails.OrbFactor;
        return factor1 * factor2 * aspectFactor;
    }

    public double DefineOrb(string mundanePoint, SolarSystemPoints solSysPoint, AspectDetails aspectDetails)
    {
        double mundaneFactor = _orbDefinitions.DefineMundanePointOrb(mundanePoint).OrbFactor;
        double solSysFactor = _orbDefinitions.DefineSolSysPointOrb(solSysPoint).OrbFactor;
        double aspectFactor = aspectDetails.OrbFactor;
        return mundaneFactor * solSysFactor * aspectFactor;
    }
}