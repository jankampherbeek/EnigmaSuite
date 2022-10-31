// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Research.ControlGroups;
using Enigma.Research.Interfaces;

namespace Enigma.Test.Research.ControlGroups;

[TestFixture]


public class TestControlDataCalendar
{
    private IControlDataCalendar _cdCal;


    [SetUp]
    public void SetUp()
    {
        _cdCal = new ControlDataCalendar();
    }

    [Test]
    public void TestHappyFlow()
    {
        Assert.IsTrue(_cdCal.DayFitsInMonth(25, 10, 2022));
    }

    [Test]
    public void TestDay31Correct()
    {
        Assert.IsTrue(_cdCal.DayFitsInMonth(31, 10, 2022));
    }

    [Test]
    public void TestDay31NotCorrect()
    {
        Assert.IsFalse(_cdCal.DayFitsInMonth(31, 11, 2022));
    }

    [Test]
    public void TestFebruaryNotCorrect()
    {
        Assert.IsFalse(_cdCal.DayFitsInMonth(29, 02, 2022));
    }

    [Test]
    public void TestLeapYear()
    {
        Assert.IsTrue(_cdCal.DayFitsInMonth(29, 02, 2024));
    }

    [Test]
    public void TestLeapYear400()
    {
        Assert.IsTrue(_cdCal.DayFitsInMonth(29, 02, 2000));
    }

    [Test]
    public void TestNoLeapYear100()
    {
        Assert.IsFalse(_cdCal.DayFitsInMonth(29, 02, 1900));
    }

}