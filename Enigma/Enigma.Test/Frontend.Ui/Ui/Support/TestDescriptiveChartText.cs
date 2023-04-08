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

    private readonly string _name = "Some Name";
    private readonly string _description = "Descriptive text";
    private readonly string _source = "source";
    private readonly string _locationName = "Somewhere";
    private readonly ChartCategories _chartCategory = ChartCategories.Female;
    private readonly RoddenRatings _rating = RoddenRatings.AA;
    private readonly HouseSystems _houseSystem = HouseSystems.Alcabitius;
    private readonly Ayanamshas _ayanamsha = Ayanamshas.None;
    private readonly ObserverPositions _observerPos = ObserverPositions.GeoCentric;
    private readonly ZodiacTypes _zodiacType = ZodiacTypes.Tropical;
    private readonly ProjectionTypes _projType = ProjectionTypes.TwoDimensional;
    private readonly OrbMethods _orbMethod = OrbMethods.Weighted;
    private readonly Dictionary<ChartPoints, ChartPointConfigSpecs> _chartPoints = new();
    private readonly Dictionary<AspectTypes, AspectConfigSpecs> _aspects = new();
    private readonly double _baseOrbAspects = 8.0;
    private readonly double _baseOrbMidpoints = 1.6;
    private readonly bool _useCuspsForAspects = false;
    private readonly int _id = 123;
    private readonly double _geoLong = 12.25;
    private readonly double _geoLat = 30.5;
    private readonly string _dateText = "2023/02/22";
    private readonly string _timeText = "18:19:00";
    private readonly double _jdUt = 123456.789;

    [Test]
    public void TestShortDescriptiveText()
    {
        string expected = "Some Name, Descriptive text \nAlcabitius Tropical Geocentric Standard (two-dimensional)\n";
        string result = _descriptiveChartText.ShortDescriptiveText(CreateConfig(), CreateMetaData());
        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void TestLongDescriptiveText()
    {
        string expected = "Some Name, Descriptive text \n2023/02/22 18:19:00 Somewhere\nAlcabitius Tropical Geocentric Standard (two-dimensional)\n";
        string result = _descriptiveChartText.FullDescriptiveText(CreateConfig(), CreateChartData());
        Assert.That(result, Is.EqualTo(expected));
    }

    private MetaData CreateMetaData()
    {
        return new(_name, _description, _source, _locationName, _chartCategory, _rating);
    }

    private AstroConfig CreateConfig()
    {
        return new(_houseSystem, _ayanamsha, _observerPos, _zodiacType, _projType, _orbMethod, _chartPoints, _aspects, _baseOrbAspects, _baseOrbMidpoints, _useCuspsForAspects);
    }


    private Location CreateLocation()
    {
        return new(_locationName, _geoLong, _geoLat);
    }

    private FullDateTime CreateFullDateTime()
    {
        return new(_dateText, _timeText, _jdUt);
    }


    private ChartData CreateChartData()
    {
        return new ChartData(_id, CreateMetaData(), CreateLocation(), CreateFullDateTime());
    }
}

