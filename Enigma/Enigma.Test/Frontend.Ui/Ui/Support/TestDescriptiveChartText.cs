// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Analysis;
using Enigma.Domain.Analysis.Aspects;
using Enigma.Domain.Calc.ChartItems;
using Enigma.Domain.Calc.DateTime;
using Enigma.Domain.Charts;
using Enigma.Domain.Configuration;
using Enigma.Domain.Points;
using Enigma.Frontend.Ui.Interfaces;
using Enigma.Frontend.Ui.Support;

namespace Enigma.Test.Frontend.Ui.Ui.Support;

[TestFixture]
public class TestDescriptiveChartText
{
    private readonly IDescriptiveChartText _descriptiveChartText = new DescriptiveChartText();

    private const string Name = "Some Name";
    private const string Description = "Descriptive text";
    private const string Source = "source";
    private const string LocationName = "Somewhere";
    private const ChartCategories ChartCategory = ChartCategories.Female;
    private const RoddenRatings Rating = RoddenRatings.AA;
    private const HouseSystems HouseSystem = HouseSystems.Alcabitius;
    private const Ayanamshas Ayanamsha = Ayanamshas.None;
    private const ObserverPositions ObserverPos = ObserverPositions.GeoCentric;
    private const ZodiacTypes ZodiacType = ZodiacTypes.Tropical;
    private const ProjectionTypes ProjType = ProjectionTypes.TwoDimensional;
    private const OrbMethods OrbMethod = OrbMethods.Weighted;
    private readonly Dictionary<ChartPoints, ChartPointConfigSpecs> _chartPoints = new();
    private readonly Dictionary<AspectTypes, AspectConfigSpecs> _aspects = new();
    private const double BaseOrbAspects = 8.0;
    private const double BaseOrbMidpoints = 1.6;
    private const bool UseCuspsForAspects = false;
    private const int Id = 123;
    private const double GeoLong = 12.25;
    private const double GeoLat = 30.5;
    private const string DateText = "2023/02/22";
    private const string TimeText = "18:19:00";
    private const double JdUt = 123456.789;

    [Test]
    public void TestShortDescriptiveText()
    {
        const string expected = "Some Name, Descriptive text \nAlcabitius Tropical Geocentric Standard (2-dimensional)\n";
        string result = _descriptiveChartText.ShortDescriptiveText(CreateConfig(), CreateMetaData());
        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void TestLongDescriptiveText()
    {
        const string expected = "Some Name, Descriptive text \n2023/02/22 18:19:00 Somewhere\nAlcabitius Tropical Geocentric Standard (2-dimensional)\n";
        string result = _descriptiveChartText.FullDescriptiveText(CreateConfig(), CreateChartData());
        Assert.That(result, Is.EqualTo(expected));
    }

    private static MetaData CreateMetaData()
    {
        return new MetaData(Name, Description, Source, LocationName, ChartCategory, Rating);
    }

    private AstroConfig CreateConfig()
    {
        return new AstroConfig(HouseSystem, Ayanamsha, ObserverPos, ZodiacType, ProjType, OrbMethod, _chartPoints, _aspects, BaseOrbAspects, BaseOrbMidpoints, UseCuspsForAspects);
    }


    private static Location CreateLocation()
    {
        return new Location(LocationName, GeoLong, GeoLat);
    }

    private static FullDateTime CreateFullDateTime()
    {
        return new FullDateTime(DateText, TimeText, JdUt);
    }


    private static ChartData CreateChartData()
    {
        return new ChartData(Id, CreateMetaData(), CreateLocation(), CreateFullDateTime());
    }
}

