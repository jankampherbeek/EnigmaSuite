// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Frontend.Helpers.Interfaces;
using Enigma.Frontend.Ui.Support.Conversions;

namespace Enigma.Test.Frontend.Ui.Conversions;


[TestFixture]
public class TestValueRangeConverter
{
    private readonly IValueRangeConverter _converter = new ValueRangeConverter();


    [Test]
    public void TestHappyFlow()
    {
        const string text = "12-14-18";
        const char separator = '-';
        (int[] numbers, bool success) = _converter.ConvertStringRangeToIntRange(text, separator);
        Assert.Multiple(() =>
        {
            Assert.That(success, Is.True);
            Assert.That(numbers, Has.Length.EqualTo(3));
            Assert.That(numbers[0], Is.EqualTo(12));
            Assert.That(numbers[1], Is.EqualTo(14));
            Assert.That(numbers[2], Is.EqualTo(18));
        });
    }

    [Test]
    public void TestSingleItem()
    {
        const string text = "12";
        const char separator = '-';
        (int[] numbers, bool success) = _converter.ConvertStringRangeToIntRange(text, separator);
        Assert.Multiple(() =>
        {
            Assert.That(success, Is.True);
            Assert.That(numbers, Has.Length.EqualTo(1));
            Assert.That(numbers[0], Is.EqualTo(12));
        });
    }

    [Test]
    public void TestEmptyString()
    {
        const string text = "";
        const char separator = '-';
        (_, bool success) = _converter.ConvertStringRangeToIntRange(text, separator);
        Assert.That(success, Is.False);
    }

    [Test]
    public void TestNullString()
    {
        string? text = null;
        const char separator = '-';
        _ = Assert.Throws<NullReferenceException>(() => _converter.ConvertStringRangeToIntRange(text!, separator));
    }

    [Test]
    public void TestNonNumeric()
    {
        const string text = "aa-12-22";
        const char separator = '-';
        (_, bool success) = _converter.ConvertStringRangeToIntRange(text, separator);
        Assert.That(success, Is.False);
    }

    [Test]
    public void TestWrongSeparator()
    {
        const string text = "3-12-22";
        const char separator = '|';
        (_, bool success) = _converter.ConvertStringRangeToIntRange(text, separator);
        Assert.That(success, Is.False);
    }

}