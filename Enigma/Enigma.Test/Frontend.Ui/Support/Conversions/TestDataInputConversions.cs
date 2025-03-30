// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.


using System.Diagnostics;
using System.Globalization;
using Enigma.Frontend.Ui.Support.Conversions;

namespace Enigma.Test.Frontend.Ui.Support.Conversions;

[TestFixture]
public class TestDataInputConverter
{
    private readonly IDataInputConverter _converter = new DataInputConverter();
    
    [Test]
    [TestCase("52.9", "52:54:00")]
    [TestCase("52,9", "52:54:00")]  // Test comma decimal separator
    [TestCase("-33.5", "33:30:00")]
    [TestCase("44.1", "44:06:00")]
    [TestCase("0.0", "0:00:00")]
    [TestCase("0.000277778", "0:00:01")]
    public void TestValueTxtForFormattedCoordinate_ReturnsExpectedFormat(string input, string expected)
    {
        Assert.That(_converter.ValueTxtToFormattedCoordinate(input), Is.EqualTo(expected));
    }
    
    [Test]
    [TestCase("")]
    [TestCase(" ")]
    [TestCase(null)]
    public void ValueTxtToFormattedCoordinate_InvalidInput_ReturnsEmptyString(string input)
    {
        Assert.That(_converter.ValueTxtToFormattedCoordinate(input), Is.Empty);
    }

    [Test]
    [TestCase("abc")]
    [TestCase("12.34.56")]
    [TestCase("12,34,56")]
    public void ValueTxtToFormattedCoordinate_NonNumericInput_ReturnsEmptyString(string input)
    {
        Assert.That(_converter.ValueTxtToFormattedCoordinate(input), Is.Empty);
    }

    [Test]
    public void ValueTxtToFormattedCoordinate_DifferentCultures_HandlesCorrectly()
    {
        var originalCulture = Thread.CurrentThread.CurrentCulture;
        try
        {
            // Test with different cultures
            foreach (var cultureName in new[] { "en-US", "fr-FR", "de-DE" })
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo(cultureName);
                Assert.That(_converter.ValueTxtToFormattedCoordinate("12.5"), Is.EqualTo("12:30:00"));
                Assert.That(_converter.ValueTxtToFormattedCoordinate("12,5"), Is.EqualTo("12:30:00"));
            }
        }
        finally
        {
            // Restore original culture
            Thread.CurrentThread.CurrentCulture = originalCulture;
        }
    }
    
    [Test]
    [Category("Performance")]
    public void ValueTxtToFormattedCoordinate_Performance_CompletesWithinTimeLimit()
    {
        const int iterations = 1000;
        var sw = Stopwatch.StartNew();
        
        for (var i = 0; i < iterations; i++)
        {
            _converter.ValueTxtToFormattedCoordinate("12.345");
        }
        
        sw.Stop();
        Assert.That(sw.ElapsedMilliseconds / iterations, Is.LessThan(1), 
            "Average conversion time should be less than 1ms");
    }
    
}