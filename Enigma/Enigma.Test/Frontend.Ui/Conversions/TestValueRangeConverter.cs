﻿// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Frontend.Helpers.Conversions;
using Enigma.Frontend.Helpers.Interfaces;

namespace Enigma.Test.Frontend.Ui.Conversions;


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
        string text = "12";
        char separator = '-';
        (int[] numbers, bool success) = Converter.ConvertStringRangeToIntRange(text, separator);
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
        string text = "";
        char separator = '-';
        (_, bool success) = Converter.ConvertStringRangeToIntRange(text, separator);
        Assert.That(success, Is.False);
    }

    [Test]
    public void TestNullString()
    {
        string? text = null;
        char separator = '-';
        _ = Assert.Throws<NullReferenceException>(() => Converter.ConvertStringRangeToIntRange(text!, separator));
    }

    [Test]
    public void TestNonNumeric()
    {
        string text = "aa-12-22";
        char separator = '-';
        (_, bool success) = Converter.ConvertStringRangeToIntRange(text, separator);
        Assert.That(success, Is.False);
    }

    [Test]
    public void TestWrongSeparator()
    {
        string text = "3-12-22";
        char separator = '|';
        (_, bool success) = Converter.ConvertStringRangeToIntRange(text, separator);
        Assert.That(success, Is.False);
    }

}