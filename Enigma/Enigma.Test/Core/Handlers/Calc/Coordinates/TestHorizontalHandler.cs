// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Handlers.Calc.Coordinates;
using Enigma.Core.Handlers.Interfaces;
using Enigma.Domain.Calc.ChartItems;
using Enigma.Domain.Calc.ChartItems.Coordinates;
using Moq;

namespace Enigma.Test.Core.Handlers.Calc.Coordinates;

[TestFixture]
public class TestHorizontalHandler
{
    private readonly double _delta = 0.00000001;
    private readonly double _jdUt = 123456.789;
    private readonly Location _location = new("Anywhere", 50.0, 10.0);
    private readonly EclipticCoordinates _eclipticCoordinates = new(160.0, 3.3);
    private readonly int _flags = 0;
    private readonly double[] _expectedResults = { 222.2, 33.3 };


    [Test]
    public void TestHappyFlow()
    {
        Mock<IHorizontalCalc> calcMock = CreateCalcMock();
        IHorizontalHandler handler = new HorizontalHandler(calcMock.Object);
        HorizontalCoordinates coordinates = handler.CalcHorizontal(new HorizontalRequest(_jdUt, _location, _eclipticCoordinates));
        Assert.Multiple(() =>
        {
            Assert.That(coordinates.Azimuth, Is.EqualTo(_expectedResults[0]).Within(_delta));
            Assert.That(coordinates.Altitude, Is.EqualTo(_expectedResults[1]).Within(_delta));
        });
    }


    private Mock<IHorizontalCalc> CreateCalcMock()
    {
        var mock = new Mock<IHorizontalCalc>();
        mock.Setup(p => p.CalculateHorizontal(_jdUt, _location, _eclipticCoordinates, _flags)).Returns(_expectedResults);
        return mock;
    }

}
