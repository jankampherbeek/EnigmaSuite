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

namespace Enigma.Test.Frontend.Ui.Support;

[TestFixture]
public class TestDescriptiveChartText
{
    private readonly IDescriptiveChartText _descriptiveChartText = new DescriptiveChartText();

    private const string NAME = "Some Name";
    private const string DESCRIPTION = "Descriptive text";
    private const string SOURCE = "source";
    private const string LOCATION_NAME = "Somewhere";
    private const ChartCategories CHART_CATEGORY = ChartCategories.Female;
    private const RoddenRatings RATING = RoddenRatings.AA;
    private const HouseSystems HOUSE_SYSTEM = HouseSystems.Alcabitius;
    private const Ayanamshas AYANAMSHA = Ayanamshas.None;
    private const ObserverPositions OBSERVER_POS = ObserverPositions.GeoCentric;
    private const ZodiacTypes ZODIAC_TYPE = ZodiacTypes.Tropical;
    private const ProjectionTypes PROJ_TYPE = ProjectionTypes.TwoDimensional;
    private const OrbMethods ORB_METHOD = OrbMethods.Weighted;
    private readonly Dictionary<ChartPoints, ChartPointConfigSpecs> _chartPoints = new();
    private readonly Dictionary<AspectTypes, AspectConfigSpecs> _aspects = new();
    private const double BASE_ORB_ASPECTS = 8.0;
    private const double BASE_ORB_MIDPOINTS = 1.6;
    private const bool USE_CUSPS_FOR_ASPECTS = false;
    private const int ID = 123;
    private const double GEO_LONG = 12.25;
    private const double GEO_LAT = 30.5;
    private const string DATE_TEXT = "2023/02/22";
    private const string TIME_TEXT = "18:19:00";
    private const double JD_UT = 123456.789;

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
        return new MetaData(NAME, DESCRIPTION, SOURCE, LOCATION_NAME, CHART_CATEGORY, RATING);
    }

    private AstroConfig CreateConfig()
    {
        return new AstroConfig(HOUSE_SYSTEM, AYANAMSHA, OBSERVER_POS, ZODIAC_TYPE, PROJ_TYPE, ORB_METHOD, _chartPoints, _aspects, BASE_ORB_ASPECTS, BASE_ORB_MIDPOINTS, USE_CUSPS_FOR_ASPECTS);
    }


    private static Location CreateLocation()
    {
        return new Location(LOCATION_NAME, GEO_LONG, GEO_LAT);
    }

    private static FullDateTime CreateFullDateTime()
    {
        return new FullDateTime(DATE_TEXT, TIME_TEXT, JD_UT);
    }


    private static ChartData CreateChartData()
    {
        return new ChartData(ID, CreateMetaData(), CreateLocation(), CreateFullDateTime());
    }
}

