// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using E4C.Core;
using E4C.Core.Api.Astron;
using E4C.Core.Api.Datetime;
using NUnit.Framework;

namespace E4CTest.Core;


[TestFixture] 
public class TestApiFactory
{
    private ApiFactory _factory = new ApiFactory();

    [Test]
    public void TestGetChartallPositionsApi()
    {
        Assert.True(_factory.GetChartAllPositionsApi() is IChartAllPositionsApi);
    }

    [Test]
    public void TestGetCalcDateTimeApi()
    {
        Assert.True(_factory.GetCalcDateTimeApi() is ICalcDateTimeApi);
    }

    [Test]
    public void TestGetCheckDateTimeApi()
    {
        Assert.True(_factory.GetCheckDateTimeApi() is ICheckDateTimeApi);
    }

    [Test]
    public void TestGetCoordinateConversionApi()
    {
        Assert.True(_factory.GetCoordinateConversionApi() is ICoordinateConversionApi);
    }

    [Test]
    public void TestGetHorizontalApi()
    {
        Assert.True(_factory.GetHorizontalApi() is IHorizontalApi);
    }

    [Test]
    public void TestGetHousesApi()
    {
        Assert.True(_factory.GetHousesApi() is IHousesApi);
    }

    [Test]
    public void TestJulianDayApi()
    {
        Assert.True(_factory.GetJulianDayApi() is IJulianDayApi);
    }

    [Test]
    public void TestObliquityApi()
    {
        Assert.True(_factory.GetObliquityApi() is IObliquityApi);
    }

}