// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Core.Analysis.Helpers;
using Enigma.Core.Interfaces;

namespace Enigma.Test.Core.Analysis;

[TestFixture]
public class TestHarmonicCalculator
{
    private const double DELTA = 0.00000001;
    private readonly IHarmonicsCalculator _harmonicCalculator = new HarmonicsCalculator();

    [Test]
    public void TestCalculateHarmonicsHarmonic1()
    {
        List<double> harmonics = _harmonicCalculator.CalculateHarmonics(CreateOriginalPositions(), 1);
        Assert.Multiple(() =>
        {
            Assert.That(harmonics[0], Is.EqualTo(1.0).Within(DELTA));
            Assert.That(harmonics[1], Is.EqualTo(22.234567).Within(DELTA));
            Assert.That(harmonics[2], Is.EqualTo(345.67).Within(DELTA));
            Assert.That(harmonics[3], Is.EqualTo(152.0).Within(DELTA));
        });
    }

    [Test]
    public void TestCalculateHarmonicsHarmonic2()
    {
        List<double> harmonics = _harmonicCalculator.CalculateHarmonics(CreateOriginalPositions(), 2);
        Assert.Multiple(() =>
        {
            Assert.That(harmonics[0], Is.EqualTo(2.0).Within(DELTA));
            Assert.That(harmonics[1], Is.EqualTo(44.469134).Within(DELTA));
            Assert.That(harmonics[2], Is.EqualTo(331.34).Within(DELTA));
            Assert.That(harmonics[3], Is.EqualTo(304.0).Within(DELTA));
        });
    }

    [Test]
    public void TestCalculateHarmonicsHarmonic9()
    {
        List<double> harmonics = _harmonicCalculator.CalculateHarmonics(CreateOriginalPositions(), 9);
        Assert.Multiple(() =>
        {
            Assert.That(harmonics[0], Is.EqualTo(9.0).Within(DELTA));
            Assert.That(harmonics[1], Is.EqualTo(200.111103).Within(DELTA));
            Assert.That(harmonics[2], Is.EqualTo(231.03).Within(DELTA));
            Assert.That(harmonics[3], Is.EqualTo(288.0).Within(DELTA));
        });
    }

    [Test]
    public void TestCalculateHarmonicsHarmonic2048()
    {
        List<double> harmonics = _harmonicCalculator.CalculateHarmonics(CreateOriginalPositions(), 2048);
        Assert.Multiple(() =>
        {
            Assert.That(harmonics[0], Is.EqualTo(248.0).Within(DELTA));
            Assert.That(harmonics[1], Is.EqualTo(176.393216).Within(DELTA));
            Assert.That(harmonics[2], Is.EqualTo(172.16).Within(DELTA));
            Assert.That(harmonics[3], Is.EqualTo(256).Within(DELTA));
        });
    }

    [Test]
    public void TestCalculateHarmonicsHarmonicFractional()
    {
        List<double> harmonics = _harmonicCalculator.CalculateHarmonics(CreateOriginalPositions(), 3.45);
        Assert.Multiple(() =>
        {
            Assert.That(harmonics[0], Is.EqualTo(3.45).Within(DELTA));
            Assert.That(harmonics[1], Is.EqualTo(76.70925615).Within(DELTA));
            Assert.That(harmonics[2], Is.EqualTo(112.5615).Within(DELTA));
            Assert.That(harmonics[3], Is.EqualTo(326.4).Within(DELTA));
        });
    }
    [Test]
    public void TestSingleHarmonicHappyFlow()
    {
        const double position = 100.0;
        const double harmonic = 5;
        const double expected = 140.0;
        double result = _harmonicCalculator.CalculateHarmonic(position, harmonic);
        Assert.That(result, Is.EqualTo(expected).Within(DELTA));
    }



    private static List<double> CreateOriginalPositions()
    {
        return new List<double> { 1.0, 22.234567, 345.67, 512.0 };

    }
}