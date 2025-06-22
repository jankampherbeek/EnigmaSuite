// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Slices.ZodiacDivisions;

namespace Enigma.Test.Core.Slices.ZodiacDivisions;

[TestFixture]
public class TestBounds
{
    [Test]
    public void TestInvalidLongitudes()
    {
        Assert.Multiple(() =>
        {
            Assert.That(Bounds.IndexBound(-0.1, true), Is.EqualTo(-1), "Negative longitude should return -1 for Egyptian bounds");
            Assert.That(Bounds.IndexBound(-1.0, false), Is.EqualTo(-1), "Negative longitude should return -1 for Ptolemy bounds");
            Assert.That(Bounds.IndexBound(-360.0, true), Is.EqualTo(-1), "Large negative longitude should return -1");
            Assert.That(Bounds.IndexBound(360.0, false), Is.EqualTo(-1), "360° should return -1");
            Assert.That(Bounds.IndexBound(360.1, true), Is.EqualTo(-1), "Longitude > 360° should return -1");
            Assert.That(Bounds.IndexBound(720.0, false), Is.EqualTo(-1), "Very large longitude should return -1");
        });
    }

    [Test]
    public void TestEgyptianBoundsAries()
    {
        Assert.Multiple(() =>
        {
            // Aries terms: 0-6° Jupiter(5), 6-12° Venus(3), 12-20° Mercury(2), 20-25° Mars(4), 25-30° Saturn(6)
            Assert.That(Bounds.IndexBound(0.0, true), Is.EqualTo(5), "0° should return Jupiter (5) in Aries");
            Assert.That(Bounds.IndexBound(3.0, true), Is.EqualTo(5), "3° should return Jupiter (5) in Aries");
            Assert.That(Bounds.IndexBound(6.0, true), Is.EqualTo(3), "6° should return Venus (3) in Aries");
            Assert.That(Bounds.IndexBound(9.0, true), Is.EqualTo(3), "9° should return Venus (3) in Aries");
            Assert.That(Bounds.IndexBound(12.0, true), Is.EqualTo(2), "12° should return Mercury (2) in Aries");
            Assert.That(Bounds.IndexBound(16.0, true), Is.EqualTo(2), "16° should return Mercury (2) in Aries");
            Assert.That(Bounds.IndexBound(20.0, true), Is.EqualTo(4), "20° should return Mars (4) in Aries");
            Assert.That(Bounds.IndexBound(22.5, true), Is.EqualTo(4), "22.5° should return Mars (4) in Aries");
            Assert.That(Bounds.IndexBound(25.0, true), Is.EqualTo(6), "25° should return Saturn (6) in Aries");
            Assert.That(Bounds.IndexBound(29.999, true), Is.EqualTo(6), "29.999° should return Saturn (6) in Aries");
        });
    }

    [Test]
    public void TestEgyptianBoundsTaurus()
    {
        Assert.Multiple(() =>
        {
            // Taurus terms: 30-38° Venus(3), 38-44° Mercury(2), 44-52° Jupiter(5), 52-57° Saturn(6), 57-60° Mars(4)
            Assert.That(Bounds.IndexBound(30.0, true), Is.EqualTo(3), "30° should return Venus (3) in Taurus");
            Assert.That(Bounds.IndexBound(35.0, true), Is.EqualTo(3), "35° should return Venus (3) in Taurus");
            Assert.That(Bounds.IndexBound(38.0, true), Is.EqualTo(2), "38° should return Mercury (2) in Taurus");
            Assert.That(Bounds.IndexBound(41.0, true), Is.EqualTo(2), "41° should return Mercury (2) in Taurus");
            Assert.That(Bounds.IndexBound(44.0, true), Is.EqualTo(5), "44° should return Jupiter (5) in Taurus");
            Assert.That(Bounds.IndexBound(48.0, true), Is.EqualTo(5), "48° should return Jupiter (5) in Taurus");
            Assert.That(Bounds.IndexBound(52.0, true), Is.EqualTo(6), "52° should return Saturn (6) in Taurus");
            Assert.That(Bounds.IndexBound(54.5, true), Is.EqualTo(6), "54.5° should return Saturn (6) in Taurus");
            Assert.That(Bounds.IndexBound(57.0, true), Is.EqualTo(4), "57° should return Mars (4) in Taurus");
            Assert.That(Bounds.IndexBound(59.999, true), Is.EqualTo(4), "59.999° should return Mars (4) in Taurus");
        });
    }

    [Test]
    public void TestPtolemyBoundsAries()
    {
        Assert.Multiple(() =>
        {
            // Ptolemy Aries terms: 0-6° Jupiter(5), 6-14° Venus(3), 14-21° Mercury(2), 21-26° Mars(4), 26-30° Saturn(6)
            Assert.That(Bounds.IndexBound(0.0, false), Is.EqualTo(5), "0° should return Jupiter (5) in Aries (Ptolemy)");
            Assert.That(Bounds.IndexBound(3.0, false), Is.EqualTo(5), "3° should return Jupiter (5) in Aries (Ptolemy)");
            Assert.That(Bounds.IndexBound(6.0, false), Is.EqualTo(3), "6° should return Venus (3) in Aries (Ptolemy)");
            Assert.That(Bounds.IndexBound(10.0, false), Is.EqualTo(3), "10° should return Venus (3) in Aries (Ptolemy)");
            Assert.That(Bounds.IndexBound(14.0, false), Is.EqualTo(2), "14° should return Mercury (2) in Aries (Ptolemy)");
            Assert.That(Bounds.IndexBound(17.5, false), Is.EqualTo(2), "17.5° should return Mercury (2) in Aries (Ptolemy)");
            Assert.That(Bounds.IndexBound(21.0, false), Is.EqualTo(4), "21° should return Mars (4) in Aries (Ptolemy)");
            Assert.That(Bounds.IndexBound(23.5, false), Is.EqualTo(4), "23.5° should return Mars (4) in Aries (Ptolemy)");
            Assert.That(Bounds.IndexBound(26.0, false), Is.EqualTo(6), "26° should return Saturn (6) in Aries (Ptolemy)");
            Assert.That(Bounds.IndexBound(29.999, false), Is.EqualTo(6), "29.999° should return Saturn (6) in Aries (Ptolemy)");
        });
    }

    [Test]
    public void TestPtolemyBoundsTaurus()
    {
        Assert.Multiple(() =>
        {
            // Ptolemy Taurus terms: 30-38° Venus(3), 38-45° Mercury(2), 45-52° Jupiter(5), 52-54° Saturn(6), 54-60° Mars(4)
            Assert.That(Bounds.IndexBound(30.0, false), Is.EqualTo(3), "30° should return Venus (3) in Taurus (Ptolemy)");
            Assert.That(Bounds.IndexBound(35.0, false), Is.EqualTo(3), "35° should return Venus (3) in Taurus (Ptolemy)");
            Assert.That(Bounds.IndexBound(38.0, false), Is.EqualTo(2), "38° should return Mercury (2) in Taurus (Ptolemy)");
            Assert.That(Bounds.IndexBound(41.5, false), Is.EqualTo(2), "41.5° should return Mercury (2) in Taurus (Ptolemy)");
            Assert.That(Bounds.IndexBound(45.0, false), Is.EqualTo(5), "45° should return Jupiter (5) in Taurus (Ptolemy)");
            Assert.That(Bounds.IndexBound(48.5, false), Is.EqualTo(5), "48.5° should return Jupiter (5) in Taurus (Ptolemy)");
            Assert.That(Bounds.IndexBound(52.0, false), Is.EqualTo(6), "52° should return Saturn (6) in Taurus (Ptolemy)");
            Assert.That(Bounds.IndexBound(53.0, false), Is.EqualTo(6), "53° should return Saturn (6) in Taurus (Ptolemy)");
            Assert.That(Bounds.IndexBound(54.0, false), Is.EqualTo(4), "54° should return Mars (4) in Taurus (Ptolemy)");
            Assert.That(Bounds.IndexBound(59.999, false), Is.EqualTo(4), "59.999° should return Mars (4) in Taurus (Ptolemy)");
        });
    }

    [Test]
    public void TestBoundaryValuesEgyptian()
    {
        Assert.Multiple(() =>
        {
            // Test exact boundary values for Egyptian bounds
            Assert.That(Bounds.IndexBound(6.0, true), Is.EqualTo(3), "Exact 6° should return Venus (3) in Aries (Egyptian)");
            Assert.That(Bounds.IndexBound(12.0, true), Is.EqualTo(2), "Exact 12° should return Mercury (2) in Aries (Egyptian)");
            Assert.That(Bounds.IndexBound(20.0, true), Is.EqualTo(4), "Exact 20° should return Mars (4) in Aries (Egyptian)");
            Assert.That(Bounds.IndexBound(25.0, true), Is.EqualTo(6), "Exact 25° should return Saturn (6) in Aries (Egyptian)");
            Assert.That(Bounds.IndexBound(30.0, true), Is.EqualTo(3), "Exact 30° should return Venus (3) in Taurus (Egyptian)");
            Assert.That(Bounds.IndexBound(38.0, true), Is.EqualTo(2), "Exact 38° should return Mercury (2) in Taurus (Egyptian)");
            Assert.That(Bounds.IndexBound(44.0, true), Is.EqualTo(5), "Exact 44° should return Jupiter (5) in Taurus (Egyptian)");
            Assert.That(Bounds.IndexBound(52.0, true), Is.EqualTo(6), "Exact 52° should return Saturn (6) in Taurus (Egyptian)");
            Assert.That(Bounds.IndexBound(57.0, true), Is.EqualTo(4), "Exact 57° should return Mars (4) in Taurus (Egyptian)");
        });
    }

    [Test]
    public void TestBoundaryValuesPtolemy()
    {
        Assert.Multiple(() =>
        {
            // Test exact boundary values for Ptolemy bounds
            Assert.That(Bounds.IndexBound(6.0, false), Is.EqualTo(3), "Exact 6° should return Venus (3) in Aries (Ptolemy)");
            Assert.That(Bounds.IndexBound(14.0, false), Is.EqualTo(2), "Exact 14° should return Mercury (2) in Aries (Ptolemy)");
            Assert.That(Bounds.IndexBound(21.0, false), Is.EqualTo(4), "Exact 21° should return Mars (4) in Aries (Ptolemy)");
            Assert.That(Bounds.IndexBound(26.0, false), Is.EqualTo(6), "Exact 26° should return Saturn (6) in Aries (Ptolemy)");
            Assert.That(Bounds.IndexBound(30.0, false), Is.EqualTo(3), "Exact 30° should return Venus (3) in Taurus (Ptolemy)");
            Assert.That(Bounds.IndexBound(38.0, false), Is.EqualTo(2), "Exact 38° should return Mercury (2) in Taurus (Ptolemy)");
            Assert.That(Bounds.IndexBound(45.0, false), Is.EqualTo(5), "Exact 45° should return Jupiter (5) in Taurus (Ptolemy)");
            Assert.That(Bounds.IndexBound(52.0, false), Is.EqualTo(6), "Exact 52° should return Saturn (6) in Taurus (Ptolemy)");
            Assert.That(Bounds.IndexBound(54.0, false), Is.EqualTo(4), "Exact 54° should return Mars (4) in Taurus (Ptolemy)");
        });
    }

    [Test]
    public void TestEndOfZodiacEgyptian()
    {
        Assert.Multiple(() =>
        {
            // Test the end of the zodiac (Pisces) for Egyptian bounds
            // Pisces terms: 330-342° Venus(3), 342-346° Jupiter(5), 346-349° Mercury(2), 349-358° Mars(4), 358-360° Saturn(6)
            Assert.That(Bounds.IndexBound(330.0, true), Is.EqualTo(3), "330° should return Venus (3) in Pisces (Egyptian)");
            Assert.That(Bounds.IndexBound(335.0, true), Is.EqualTo(3), "335° should return Venus (3) in Pisces (Egyptian)");
            Assert.That(Bounds.IndexBound(342.0, true), Is.EqualTo(5), "342° should return Jupiter (5) in Pisces (Egyptian)");
            Assert.That(Bounds.IndexBound(344.0, true), Is.EqualTo(5), "344° should return Jupiter (5) in Pisces (Egyptian)");
            Assert.That(Bounds.IndexBound(346.0, true), Is.EqualTo(2), "346° should return Mercury (2) in Pisces (Egyptian)");
            Assert.That(Bounds.IndexBound(347.5, true), Is.EqualTo(2), "347.5° should return Mercury (2) in Pisces (Egyptian)");
            Assert.That(Bounds.IndexBound(349.0, true), Is.EqualTo(4), "349° should return Mars (4) in Pisces (Egyptian)");
            Assert.That(Bounds.IndexBound(355.0, true), Is.EqualTo(4), "355° should return Mars (4) in Pisces (Egyptian)");
            Assert.That(Bounds.IndexBound(358.0, true), Is.EqualTo(6), "358° should return Saturn (6) in Pisces (Egyptian)");
            Assert.That(Bounds.IndexBound(359.999, true), Is.EqualTo(6), "359.999° should return Saturn (6) in Pisces (Egyptian)");
        });
    }

    [Test]
    public void TestEndOfZodiacPtolemy()
    {
        Assert.Multiple(() =>
        {
            // Test the end of the zodiac (Pisces) for Ptolemy bounds
            // The function returns the planet of the NEXT term boundary
            // Aquarius ends at 330° with Mars(4), then Pisces terms: 330-338° Venus(3), 338-344° Jupiter(5), 344-350° Mercury(2), 350-355° Mars(4), 355-360° Saturn(6)
            Assert.That(Bounds.IndexBound(330.0, false), Is.EqualTo(3), "330° should return Venus (3) in Pisces (Ptolemy)");
            Assert.That(Bounds.IndexBound(335.0, false), Is.EqualTo(3), "335° should return Venus (3) in Pisces (Ptolemy)");
            Assert.That(Bounds.IndexBound(338.0, false), Is.EqualTo(5), "338° should return Jupiter (5) in Pisces (Ptolemy)");
            Assert.That(Bounds.IndexBound(341.0, false), Is.EqualTo(5), "341° should return Jupiter (5) in Pisces (Ptolemy)");
            Assert.That(Bounds.IndexBound(344.0, false), Is.EqualTo(2), "344° should return Mercury (2) in Pisces (Ptolemy)");
            Assert.That(Bounds.IndexBound(347.0, false), Is.EqualTo(2), "347° should return Mercury (2) in Pisces (Ptolemy)");
            Assert.That(Bounds.IndexBound(350.0, false), Is.EqualTo(4), "350° should return Mars (4) in Pisces (Ptolemy)");
            Assert.That(Bounds.IndexBound(352.5, false), Is.EqualTo(4), "352.5° should return Mars (4) in Pisces (Ptolemy)");
            Assert.That(Bounds.IndexBound(355.0, false), Is.EqualTo(6), "355° should return Saturn (6) in Pisces (Ptolemy)");
            Assert.That(Bounds.IndexBound(359.999, false), Is.EqualTo(6), "359.999° should return Saturn (6) in Pisces (Ptolemy)");
        });
    }

    [Test]
    public void TestMidZodiacEgyptian()
    {
        Assert.Multiple(() =>
        {
            // Test middle of the zodiac for Egyptian bounds
            Assert.That(Bounds.IndexBound(180.0, true), Is.EqualTo(6), "180° should return Saturn (6) in Libra (Egyptian)");
            Assert.That(Bounds.IndexBound(186.0, true), Is.EqualTo(2), "186° should return Mercury (2) in Libra (Egyptian)");
            Assert.That(Bounds.IndexBound(194.0, true), Is.EqualTo(5), "194° should return Jupiter (5) in Libra (Egyptian)");
            Assert.That(Bounds.IndexBound(201.0, true), Is.EqualTo(3), "201° should return Venus (3) in Libra (Egyptian)");
            Assert.That(Bounds.IndexBound(208.0, true), Is.EqualTo(4), "208° should return Mars (4) in Libra (Egyptian)");
        });
    }

    [Test]
    public void TestMidZodiacPtolemy()
    {
        Assert.Multiple(() =>
        {
            // Test middle of the zodiac for Ptolemy bounds
            // The function returns the planet of the NEXT term boundary
            // Leo ends at 150° with Mars(4), then Virgo terms: 150-157° Mercury(2), 157-163° Venus(3), 163-168° Jupiter(5), 168-174° Saturn(6), 174-180° Mars(4)
            // Virgo ends at 180° with Mars(4), then Libra terms: 180-186° Saturn(6), 186-191° Venus(3), 191-196° Mercury(2), 196-204° Jupiter(5), 204-210° Mars(4)
            Assert.That(Bounds.IndexBound(150.0, false), Is.EqualTo(2), "150° should return Mercury (2) in Virgo (Ptolemy)");
            Assert.That(Bounds.IndexBound(157.0, false), Is.EqualTo(3), "157° should return Venus (3) in Virgo (Ptolemy)");
            Assert.That(Bounds.IndexBound(163.0, false), Is.EqualTo(5), "163° should return Jupiter (5) in Virgo (Ptolemy)");
            Assert.That(Bounds.IndexBound(168.0, false), Is.EqualTo(6), "168° should return Saturn (6) in Virgo (Ptolemy)");
            Assert.That(Bounds.IndexBound(174.0, false), Is.EqualTo(4), "174° should return Mars (4) in Virgo (Ptolemy)");
            Assert.That(Bounds.IndexBound(180.0, false), Is.EqualTo(6), "180° should return Saturn (6) in Libra (Ptolemy)");
            Assert.That(Bounds.IndexBound(186.0, false), Is.EqualTo(3), "186° should return Venus (3) in Libra (Ptolemy)");
            Assert.That(Bounds.IndexBound(191.0, false), Is.EqualTo(2), "191° should return Mercury (2) in Libra (Ptolemy)");
            Assert.That(Bounds.IndexBound(196.0, false), Is.EqualTo(5), "196° should return Jupiter (5) in Libra (Ptolemy)");
            Assert.That(Bounds.IndexBound(204.0, false), Is.EqualTo(4), "204° should return Mars (4) in Libra (Ptolemy)");
        });
    }
} 