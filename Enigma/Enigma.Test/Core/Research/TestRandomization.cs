// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Research.Helpers;
using Enigma.Core.Research.Interfaces;

namespace Enigma.Test.Core.Research;

[TestFixture]
public class TestControlGroupRng
{
    private IControlGroupRng? _rng;
    private const double DELTA = 0.00000001;
    
    [SetUp]
    public void SetUp()
    {
        _rng = new ControlGroupRng();
    }

    [Test]
    public void TestGetIntegersRangeHappyFlow()
    {
        const int start = 10;
        const int end = 50;
        const int count = 100;
        List<int> result = _rng!.GetIntegers(start, end, count);
        Assert.That(result, Has.Count.EqualTo(count));
        for (int i = 0; i < count; i++)
        {
            Assert.That(result[i], Is.GreaterThanOrEqualTo(start));
            Assert.That(result[i], Is.LessThan(end));
        }
    }

    [Test]
    public void TestGetIntegersDifferenceHappyFlow()
    {
        const int start = 10;
        const int end = 50;
        const int count = 100;
        List<int> result1 = _rng!.GetIntegers(start, end, count);
        List<int> result2 = _rng!.GetIntegers(start, end, count);
        bool listsAreEqual = true;
        for (int i = 0; i < count; i++)
        {
            if (result1[i] != result2[i]) listsAreEqual = false;
        }
        Assert.That(listsAreEqual, Is.False);
    }


    [Test]
    public void TestGetIntegersWrongSequenceOfParameters()
    {
        List<int> result = _rng!.GetIntegers(10, 2, 30);
        Assert.That(result, Is.Empty);
    }

    [Test]
    public void TestGetIntegersInvalidCount()
    {
        List<int> result = _rng!.GetIntegers(10, 20, -1);
        Assert.That(result, Is.Empty);
    }

    [Test]
    public void TestGetIntegersZeroBasedRangeHappyFlow()
    {
        const int start = 0;
        const int end = 50;
        const int count = 100;
        List<int> result = _rng!.GetIntegers(end, count);
        Assert.That(result, Has.Count.EqualTo(count));
        for (int i = 0; i < count; i++)
        {
            Assert.That(result[i], Is.GreaterThanOrEqualTo(start));
            Assert.That(result[i], Is.LessThan(end));
        }
    }

    [Test]
    public void TestGetIntegersZeroBasedDifferenceHappyFlow()
    {
        const int end = 50;
        const int count = 100;
        List<int> result1 = _rng!.GetIntegers(end, count);
        List<int> result2 = _rng!.GetIntegers(end, count);
        bool listsAreEqual = true;
        for (int i = 0; i < count; i++)
        {
            if (result1[i] != result2[i]) listsAreEqual = false;
        }
        Assert.That(listsAreEqual, Is.False);
    }


    [Test]
    public void TestGetIntegersZeroBasedMaxIsNegative()
    {
        List<int> result = _rng!.GetIntegers(-2, 30);
        Assert.That(result, Is.Empty);
    }

    [Test]
    public void TestGetIntegersZeroBasedInvalidCount()
    {
        List<int> result = _rng!.GetIntegers(20, -1);
        Assert.That(result, Is.Empty);
    }


    [Test]
    public void TestShuffleIntList()
    {
        int[] dataItems = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
        List<int> data = new();
        data.AddRange(dataItems);
        _rng!.ShuffleList(data);
        Assert.That(data, Has.Count.EqualTo(dataItems.Length));
        Assert.That(data[0] != 1 || data[1] != 2 || data[2] != 3 || data[3] != 4 || data[4] != 5 || data[5] != 6);
    }

    [Test]
    public void TestShuffleDoubleList()
    {
        double[] dataItems = { 1.1, 2.2, 3.3, 4.4, 5.5, 6.6, 7.7, 8.8, 9.9, 10.0, 11.1, 12.2 };
        List<double> data = new();
        data.AddRange(dataItems);
        _rng!.ShuffleList(data);
        Assert.Multiple(() =>
        {
            Assert.That(data, Has.Count.EqualTo(dataItems.Length));
            Assert.That(Math.Abs(data[0] - 1.1) > DELTA
                        || Math.Abs(data[1] - 2.2) > DELTA
                        || Math.Abs(data[2] - 3.3) > DELTA
                        || Math.Abs(data[3] - 4.4) > DELTA
                        || Math.Abs(data[4] - 5.5) > DELTA
                        || Math.Abs(data[5] - 6.6) > DELTA);
        });
    }
}