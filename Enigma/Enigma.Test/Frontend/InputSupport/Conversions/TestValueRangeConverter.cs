// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Frontend.InputSupport.Conversions;

namespace Enigma.Test.Frontend.InputSupport.Conversions;


[TestFixture]
public class TestValueRangeConverter
{
    readonly IValueRangeConverter Converter = new ValueRangeConverter();


    [Test]
    public void TestHappyFlow()
    {
        string text = "12-14-18";
        char separator = '-';
        (int[] numbers, bool success) = Converter.ConvertStringRangeToIntRange(text, separator);
        Assert.IsTrue(success);
        Assert.That(numbers.Length, Is.EqualTo(3));
        Assert.That(12, Is.EqualTo(numbers[0]));
        Assert.That(14, Is.EqualTo(numbers[1]));
        Assert.That(18, Is.EqualTo(numbers[2]));
    }

    [Test]
    public void TestSingleItem()
    {
        string text = "12";
        char separator = '-';
        (int[] numbers, bool success) = Converter.ConvertStringRangeToIntRange(text, separator);
        Assert.IsTrue(success);
        Assert.That(numbers.Length, Is.EqualTo(1));
        Assert.That(12, Is.EqualTo(numbers[0]));
    }

    [Test]
    public void TestEmptyString()
    {
        string text = "";
        char separator = '-';
        (int[] numbers, bool success) = Converter.ConvertStringRangeToIntRange(text, separator);
        Assert.IsFalse(success);
    }

    [Test]
    public void TestNullString()
    {
        string? text = null;
        char separator = '-';
        Assert.Throws<ArgumentNullException>(() => Converter.ConvertStringRangeToIntRange(text, separator));
    }

    [Test]
    public void TestNonNumeric()
    {
        string text = "aa-12-22";
        char separator = '-';
        (int[] numbers, bool success) = Converter.ConvertStringRangeToIntRange(text, separator);
        Assert.IsFalse(success);
    }

    [Test]
    public void TestWrongSeparator()
    {
        string text = "3-12-22";
        char separator = '|';
        (int[] numbers, bool success) = Converter.ConvertStringRangeToIntRange(text, separator);
        Assert.IsFalse(success);
    }

}