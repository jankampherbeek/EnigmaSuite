// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Research.ControlGroups;

namespace Enigma.Test.Research.ControlGroups;

[TestFixture]
public class TestControlGroupRng
{
    private IControlGroupRng _rng;

    [SetUp]
    public void SetUp()
    {
        _rng = new ControlGroupRng();
    }

    [Test]
    public void TestGetIntegersRangeHappyFlow()
    {
        int start = 10;
        int end = 50;
        int count = 100;
        List<int> result = _rng.GetIntegers(start, end, count);
        Assert.That(result.Count, Is.EqualTo(count));
        for (int i = 0; i < count; i++)
        {
            Assert.GreaterOrEqual(result[i], start);
            Assert.Less(result[i], end);
        }
    }

    [Test]
    public void TestGetIntegersDifferenceHappyFlow()
    {
        int start = 10;
        int end = 50;
        int count = 100;
        List<int> result1 = _rng.GetIntegers(start, end, count);
        List<int> result2 = _rng.GetIntegers(start, end, count);
        bool listsAreEqual = true;
        for (int i = 0; i < count; i++)
        {
            if (result1[i] != result2[i]) listsAreEqual = false;
        }
        Assert.IsFalse(listsAreEqual);
    }


    [Test]
    public void TestGetIntegersWrongSequenceOfParameters()
    {
        List<int> result = _rng.GetIntegers(10, 2, 30);
        Assert.That(result.Count, Is.EqualTo(0));
    }

    [Test]
    public void TestGetIntegersInvalidCount()
    {
        List<int> result = _rng.GetIntegers(10, 20, -1);
        Assert.That(result.Count, Is.EqualTo(0));
    }

    [Test]
    public void TestGetIntegersZeroBasedRangeHappyFlow()
    {
        int start = 0; 
        int end = 50;
        int count = 100;
        List<int> result = _rng.GetIntegers(end, count);
        Assert.That(result.Count, Is.EqualTo(count));
        for (int i = 0; i < count; i++)
        {
            Assert.GreaterOrEqual(result[i], start);
            Assert.Less(result[i], end);
        }
    }

    [Test]
    public void TestGetIntegersZeroBasedDifferenceHappyFlow()
    {
        int start = 0;
        int end = 50;
        int count = 100;
        List<int> result1 = _rng.GetIntegers(end, count);
        List<int> result2 = _rng.GetIntegers(end, count);
        bool listsAreEqual = true;
        for (int i = 0; i < count; i++)
        {
            if (result1[i] != result2[i]) listsAreEqual = false;
        }
        Assert.IsFalse(listsAreEqual);
    }


    [Test]
    public void TestGetIntegersZeroBasedMaxIsNegative()
    {
        List<int> result = _rng.GetIntegers(-2, 30);
        Assert.That(result.Count, Is.EqualTo(0));
    }

    [Test]
    public void TestGetIntegersZeroBasedInvalidCount()
    {
        List<int> result = _rng.GetIntegers(20, -1);
        Assert.That(result.Count, Is.EqualTo(0));
    }


    [Test]
    public void TestShuffleIntList()
    {
        int[] dataItems = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
        List<int> data = new();
        data.AddRange(dataItems);
        _rng.ShuffleList(data);
        Assert.That(data.Count, Is.EqualTo(dataItems.Length));
        Assert.That(data[0] != 1 || data[1] != 2 || data[2] != 3 || data[3] != 4 || data[4] != 5  || data[5] != 6);
    }

    [Test]
    public void TestShuffleDoubleList()
    {
        double[] dataItems = { 1.1, 2.2, 3.3, 4.4, 5.5, 6.6, 7.7, 8.8, 9.9, 10.0, 11.1, 12.2 };
        List<double> data = new();
        data.AddRange(dataItems);
        _rng.ShuffleList(data);
        Assert.That(data.Count, Is.EqualTo(dataItems.Length));
        Assert.That(data[0] != 1.1 || data[1] != 2.2 || data[2] != 3.3 || data[3] != 4.4 || data[4] != 5.5 || data[5] != 6.6);
    }
}