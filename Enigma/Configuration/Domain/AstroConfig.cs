// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Domain.Analysis;
using Enigma.Domain.CalcVars;

namespace Enigma.Configuration.Domain;

public class AstroConfig
{
    public HouseSystems HouseSystem { get; set; }
    public Ayanamshas Ayanamsha { get; set; }
    public ObserverPositions ObserverPosition { get; set; }
    public ZodiacTypes ZodiacType { get; set; }
    public ProjectionTypes ProjectionType { get; set; }
    public List<CelPointSpecs>? CelPoints { get; set; }
    public List<AspectSpecs>? Aspects { get; set; }
    public OrbMethods OrbMethod { get; set; }
}



public record CelPointSpecs
{
    public SolarSystemPoints SolarSystemPoint { get; set; }
    public double FactorAspectOrb { get; set; }
    public bool IsUsed { get; set; }

    public CelPointSpecs(SolarSystemPoints solarSystemPoint, double factorAspectOrb, bool isUsed)
    {
        SolarSystemPoint = solarSystemPoint;
        FactorAspectOrb = factorAspectOrb;
        IsUsed = isUsed;
    }
}

public record AspectSpecs
{
    public AspectTypes AspectType { get; set; }
    public double FactorOrb { get; set; }

    public AspectSpecs(AspectTypes aspectType, double factorOrb)
    {
        AspectType = aspectType;
        FactorOrb = factorOrb;
    }
}




