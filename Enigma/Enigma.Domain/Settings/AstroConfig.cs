// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Domain.Analysis;
using Enigma.Domain.CalcVars;
using System.Text.Json;

namespace Enigma.Domain.Settings;

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
      public bool UseForAspects { get; set; }
      public bool UseForMidPoints { get; set; }

    public CelPointSpecs(SolarSystemPoints solarSystemPoint, double factorAspectOrb, bool useForAspects, bool useForMidpoints)
    {
        SolarSystemPoint = solarSystemPoint;
        FactorAspectOrb = factorAspectOrb;
        UseForAspects = useForAspects;
        UseForMidPoints = useForMidpoints;
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


public class AstroConfigPersister
{
    public string Marshall(AstroConfig astroConfig)
    {
        var options = new JsonSerializerOptions()
        {
            WriteIndented = true
        };
        string jsonString = JsonSerializer.Serialize(astroConfig, options);

        return jsonString;
    }

    public AstroConfig UnMarshall(string jsonString)
    {
        AstroConfig? astroConfig = JsonSerializer.Deserialize<AstroConfig>(jsonString);
        return astroConfig;
    }

}


