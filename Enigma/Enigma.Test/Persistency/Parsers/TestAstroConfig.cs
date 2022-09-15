// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Analysis;
using Enigma.Domain.CalcVars;
using Enigma.Domain.Settings;
using Enigma.Persistency.Parsers;

namespace Enigma.Test.Persistency.Parsers;

[TestFixture]
public class TestAstroConfig
{
    [Test]
    public void TempTest()
    {
        List<CelPointSpecs> celPoints = new List<CelPointSpecs>();
        celPoints.Add(new CelPointSpecs(SolarSystemPoints.Sun, 1.0, true));
        celPoints.Add(new CelPointSpecs(SolarSystemPoints.Moon, 1.0, true));
        List<AspectSpecs> aspects = new List<AspectSpecs>();
        aspects.Add(new AspectSpecs(AspectTypes.Conjunction, 1.0));
        aspects.Add(new AspectSpecs(AspectTypes.Opposition, 1.0));

        var astroConfig = new AstroConfig
        {
            HouseSystem = HouseSystems.Placidus,
            Ayanamsha = Ayanamshas.None,
            ObserverPosition = ObserverPositions.TopoCentric,
            ZodiacType = ZodiacTypes.Tropical,
            ProjectionType = ProjectionTypes.twoDimensional,
            OrbMethod = OrbMethods.Weighted,
            CelPoints = celPoints,
            Aspects = aspects
        };
        AstroConfigPersister persister = new();
        string result = persister.Marshall(astroConfig);
        Console.WriteLine(result);

        AstroConfig deSerializedConfig = persister.UnMarshall(result);
        Assert.That(deSerializedConfig.HouseSystem, Is.EqualTo(astroConfig.HouseSystem));

    }
}

