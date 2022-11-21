// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.



using Enigma.Core.Work.Analysis.Midpoints;
using Enigma.Core.Work.Analysis.Midpoints.Interfaces;
using Enigma.Domain.Analysis;
using Enigma.Domain.AstronCalculations;
using Enigma.Domain.Charts;
using Enigma.Domain.Enums;
using Enigma.Domain.Interfaces;
using Moq;

namespace Enigma.Test.Core.Work.Analysis.Midpoints;

[TestFixture]
public class TestAnalysisPointsForMidpoints
{
    private readonly double _delta = 0.00000001;

    [Test]
    public void TestCreateAnalysisPoints360Degree()
    {
        CalculatedChart chart = CreateChart();
        double dialSize = 360.0;
        IAnalysisPointsMapping mockAnalysisPointsMapping = CreateMockForMapping();
        IAnalysisPointsForMidpoints _analysisPointsForMidpoints = new AnalysisPointsForMidpoints(mockAnalysisPointsMapping);
        List<AnalysisPoint> calculatedAnalysisPoints = _analysisPointsForMidpoints.CreateAnalysisPoints(chart, dialSize);
        Assert.Multiple(() =>
        {
            Assert.That(calculatedAnalysisPoints[0].Position, Is.EqualTo(22.0).Within(_delta));
            Assert.That(calculatedAnalysisPoints[1].Position, Is.EqualTo(178.0).Within(_delta));
            Assert.That(calculatedAnalysisPoints[2].Position, Is.EqualTo(302.0).Within(_delta));
            Assert.That(calculatedAnalysisPoints[3].Position, Is.EqualTo(42.0).Within(_delta));
        });
    }

    [Test]
    public void TestCreateAnalysisPoints90Degree()
    {
        CalculatedChart chart = CreateChart();
        double dialSize = 90.0;
        IAnalysisPointsMapping mockAnalysisPointsMapping = CreateMockForMapping();
        IAnalysisPointsForMidpoints _analysisPointsForMidpoints = new AnalysisPointsForMidpoints(mockAnalysisPointsMapping);
        List<AnalysisPoint> calculatedAnalysisPoints = _analysisPointsForMidpoints.CreateAnalysisPoints(chart, dialSize);
        Assert.Multiple(() =>
        {
            Assert.That(calculatedAnalysisPoints[0].Position, Is.EqualTo(22.0).Within(_delta));
            Assert.That(calculatedAnalysisPoints[1].Position, Is.EqualTo(88.0).Within(_delta));
            Assert.That(calculatedAnalysisPoints[2].Position, Is.EqualTo(32.0).Within(_delta));
            Assert.That(calculatedAnalysisPoints[3].Position, Is.EqualTo(42.0).Within(_delta));
        });
    }

    [Test]
    public void TestCreateAnalysisPoints45Degree()
    {
        CalculatedChart chart = CreateChart();
        double dialSize = 45.0;
        IAnalysisPointsMapping mockAnalysisPointsMapping = CreateMockForMapping();
        IAnalysisPointsForMidpoints _analysisPointsForMidpoints = new AnalysisPointsForMidpoints(mockAnalysisPointsMapping);
        List<AnalysisPoint> calculatedAnalysisPoints = _analysisPointsForMidpoints.CreateAnalysisPoints(chart, dialSize);
        Assert.Multiple(() =>
        {
            Assert.That(calculatedAnalysisPoints[0].Position, Is.EqualTo(22.0).Within(_delta));
            Assert.That(calculatedAnalysisPoints[1].Position, Is.EqualTo(43.0).Within(_delta));
            Assert.That(calculatedAnalysisPoints[2].Position, Is.EqualTo(32.0).Within(_delta));
            Assert.That(calculatedAnalysisPoints[3].Position, Is.EqualTo(42.0).Within(_delta));
        });
    }



    private static IAnalysisPointsMapping CreateMockForMapping()
    {
        List<AnalysisPoint> analysisPoints = CreateAnalysisPoints();
        List<PointGroups> pointGroups = new() { PointGroups.SolarSystemPoints, PointGroups.MundanePoints, PointGroups.ZodiacalPoints };
        CoordinateSystems coordinateSystem = CoordinateSystems.Ecliptical;
        bool mainCoordinate = true;
        var mockAnalysisPointsMapping = new Mock<IAnalysisPointsMapping>();
        mockAnalysisPointsMapping.Setup(x => x.ChartToSingleAnalysisPoints(pointGroups, coordinateSystem, mainCoordinate, It.IsAny<CalculatedChart>())).Returns(analysisPoints);
        return mockAnalysisPointsMapping.Object;
    }


    private static List<AnalysisPoint> CreateAnalysisPoints()
    {
        PointGroups pointGroup = PointGroups.ZodiacalPoints;
        AnalysisPoint sun = new(pointGroup, 0, 22.0, "a");
        AnalysisPoint moon = new(pointGroup, 1, 178.0, "b");
        AnalysisPoint mars = new(pointGroup, 5, 302.0, "c");
        AnalysisPoint jupiter = new(pointGroup, 6, 42.0, "d");
        List<AnalysisPoint> analysisPoints = new() { sun, moon, mars, jupiter };
        return analysisPoints;
    }

    private static CalculatedChart CreateChart()
    {
        // the calculated chart is actually used as a mock, but as it is a record is it not possible to mock it with moq.
        List<FullSolSysPointPos> solSysPointPositions = new();
        List<CuspFullPos> cusps = new();
        CuspFullPos mc = CreateCuspFullPos();
        CuspFullPos ascendant= CreateCuspFullPos();
        CuspFullPos vertex= CreateCuspFullPos();
        CuspFullPos eastPoint = CreateCuspFullPos();
        FullHousesPositions fullHousePositions = new(cusps, mc, ascendant, vertex, eastPoint);
        int id = 1;
        int tempId = 2;
        MetaData metaData = new("name", "descr", "source", ChartCategories.Unknown, RoddenRatings.Unknown);
        Location location = new("Somewhere", 0.0, 0.0);
        FullDateTime fullDateTime = new("dateText", "timeText", 1234567.89);
        ChartData inputData = new(id, tempId, metaData, location, fullDateTime);
        return new CalculatedChart(solSysPointPositions, fullHousePositions, inputData);
    }

    private static CuspFullPos CreateCuspFullPos() 
    {
        return new CuspFullPos(0.0, new EquatorialCoordinates(0.0, 0.0), new HorizontalCoordinates(0.0, 0.0));
    }
}