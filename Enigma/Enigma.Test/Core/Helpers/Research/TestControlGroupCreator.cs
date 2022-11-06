// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Helpers.Research;

namespace Enigma.Test.Core.Helpers.Research;

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
        Assert.That(_cdCal.DayFitsInMonth(25, 10, 2022), Is.True);
    }

    [Test]
    public void TestDay31Correct()
    {
        Assert.That(_cdCal.DayFitsInMonth(31, 10, 2022), Is.True);
    }

    [Test]
    public void TestDay31NotCorrect()
    {
        Assert.That(_cdCal.DayFitsInMonth(31, 11, 2022), Is.False);
    }

    [Test]
    public void TestFebruaryNotCorrect()
    {
        Assert.That(_cdCal.DayFitsInMonth(29, 02, 2022), Is.False);
    }

    [Test]
    public void TestLeapYear()
    {
        Assert.That(_cdCal.DayFitsInMonth(29, 02, 2024), Is.True);
    }

    [Test]
    public void TestLeapYear400()
    {
        Assert.That(_cdCal.DayFitsInMonth(29, 02, 2000), Is.True);
    }

    [Test]
    public void TestNoLeapYear100()
    {
        Assert.That(_cdCal.DayFitsInMonth(29, 02, 1900), Is.False);
    }

}