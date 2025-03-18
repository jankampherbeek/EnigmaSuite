// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Dtos;
using Enigma.Domain.References;

namespace Enigma.Test.Domain.Dtos;

[TestFixture]
public class TestCalculationPreferencesCreator
{
    private readonly ICalculationPreferencesCreator _creator = new CalculationPreferencesCreator();

    [Test]
    public void TestCalcPrefsStandard()
    {
        AstroConfig? config = CreateConfig();
        CalculationPreferences prefs = _creator.CreatePrefs(config, CoordinateSystems.Ecliptical);
        Assert.Multiple(() =>
        {
            Assert.That(prefs.ActualObserverPosition, Is.EqualTo(ObserverPositions.TopoCentric));
            Assert.That(prefs.ActualAyanamsha, Is.EqualTo(Ayanamshas.None));
            Assert.That(prefs.CoordinateSystem, Is.EqualTo(CoordinateSystems.Ecliptical));
            Assert.That(prefs.ActualHouseSystem, Is.EqualTo(HouseSystems.Campanus));
            Assert.That(prefs.ActualChartPoints, Has.Count.EqualTo(3));
        });
    }

    [Test]
    public void TestCalcPrefsSingleChartPoint()
    {
        AstroConfig? config = CreateConfig();
        CalculationPreferences prefs =
            _creator.CreatePrefsForSinglePoint(ChartPoints.Moon, config, CoordinateSystems.Equatorial);
        Assert.Multiple(() =>
        {
            Assert.That(prefs.CoordinateSystem, Is.EqualTo(CoordinateSystems.Equatorial));
            Assert.That(prefs.ActualChartPoints, Has.Count.EqualTo(1));
            Assert.That(prefs.ActualChartPoints[0], Is.EqualTo(ChartPoints.Moon));
        });

    }

    private AstroConfig? CreateConfig()
    {
        ChartPointConfigSpecs sunSpecs = new(true, 'a', 100, true);
        ChartPointConfigSpecs moonSpecs = new(true, 'b', 90, true);
        ChartPointConfigSpecs mercSpecs = new(true, 'c', 70, true);
        ChartPointConfigSpecs chiSpecs = new(false, 'z', 10, false);
        Dictionary<ChartPoints, ChartPointConfigSpecs> chartPoints = new()
        {
            { ChartPoints.Sun, sunSpecs },
            { ChartPoints.Moon, moonSpecs },
            { ChartPoints.Mercury, mercSpecs },
            { ChartPoints.Chiron, chiSpecs }
        };
        
        AspectConfigSpecs conSpecs = new(true, '1', 100, true);
        AspectConfigSpecs oppSpecs = new(true, '2', 90, true);
        AspectConfigSpecs triSpecs = new(true, '3', 80, true);
        AspectConfigSpecs sepSpecs = new(false, '9', 10, false);
        Dictionary<AspectTypes, AspectConfigSpecs> aspects = new()
        {
            { AspectTypes.Conjunction, conSpecs },
            { AspectTypes.Opposition, oppSpecs },
            { AspectTypes.Triangle, triSpecs },
            { AspectTypes.Septile, sepSpecs }
        };
        
        Dictionary<AspectTypes, string> aspectColors = new()
        {
            { AspectTypes.Conjunction, "Blue" },
            { AspectTypes.Opposition, "Red" },
            { AspectTypes.Triangle, "Green" },
            { AspectTypes.Septile, "Gray" }
        };
        
        return new AstroConfig(
            HouseSystems.Campanus, 
            Ayanamshas.None, 
            ObserverPositions.TopoCentric, 
            ZodiacTypes.Tropical, 
            ProjectionTypes.TwoDimensional, 
            OrbMethods.Weighted,
            chartPoints,
            aspects,
            aspectColors,
            10.0,
            1.6,
            1.0,
            0.5,
            false,
            ApogeeTypes.Corrected,
            false);
    }
    
}