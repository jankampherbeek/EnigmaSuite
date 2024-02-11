// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Analysis;
using Enigma.Domain.References;

namespace Enigma.Test.Core.Analysis;

[TestFixture]
public class TestCheckedProgAspects
{
    private const double Delta = 0.00000001;
    private readonly ICheckedProgAspects _checkedProgAspects = new CheckedProgAspects();
    
    [Test]
    public void TestCheckAspectsHappyFlow()
    {
        const double distance = 176.0;
        const double orb = 10.0;
        List<AspectTypes> aspectTypes = CreateAspectTypes();
        Dictionary<AspectTypes, double> actualAspects = _checkedProgAspects.CheckAspects(distance, orb, aspectTypes);

        Assert.Multiple(() =>
        {
            Assert.That(actualAspects, Is.Not.Null);
            Assert.That(actualAspects, Has.Count.EqualTo(1));
            Assert.That(actualAspects.Keys, Does.Contain(AspectTypes.Opposition));
            Assert.That(actualAspects[AspectTypes.Opposition], Is.EqualTo(4.0).Within(Delta));
        });
    }

    [Test]
    public void TestCheckAspectsNoMatch()
    {
        const double distance = 33.0;
        const double orb = 10.0;
        List<AspectTypes> aspectTypes = CreateAspectTypes();
        Dictionary<AspectTypes, double> actualAspects = _checkedProgAspects.CheckAspects(distance, orb, aspectTypes);

        Assert.Multiple(() =>
        {
            Assert.That(actualAspects, Is.Not.Null);
            Assert.That(actualAspects, Has.Count.EqualTo(0));
        });
    }
    
    [Test]
    public void TestCheckAspectsDistanceNegative()
    {
        const double distance = -33.0;
        const double orb = 10.0;
        List<AspectTypes> aspectTypes = CreateAspectTypes();
        _ = Assert.Throws<ArgumentException>(() => _checkedProgAspects.CheckAspects(distance, orb, aspectTypes));
    }
    
    [Test]
    public void TestCheckAspectsDistanceTooLarge()
    {
        const double distance = 181.0;
        const double orb = 10.0;
        List<AspectTypes> aspectTypes = CreateAspectTypes();
        _ = Assert.Throws<ArgumentException>(() => _checkedProgAspects.CheckAspects(distance, orb, aspectTypes));
    }
    
    [Test]
    public void TestCheckAspectsOrbZero()
    {
        const double distance = 176.0;
        const double orb = 0.0;
        List<AspectTypes> aspectTypes = CreateAspectTypes();
        _ = Assert.Throws<ArgumentException>(() => _checkedProgAspects.CheckAspects(distance, orb, aspectTypes));
    }
    
    [Test]
    public void TestCheckAspectsOrbTooLarge()
    {
        const double distance = 176.0;
        const double orb = 31.0;
        List<AspectTypes> aspectTypes = CreateAspectTypes();
        _ = Assert.Throws<ArgumentException>(() => _checkedProgAspects.CheckAspects(distance, orb, aspectTypes));
    }
    
    [Test]
    public void TestCheckAspectsAspectTypesEmpty()
    {
        const double distance = 176.0;
        const double orb = 10.0;
        List<AspectTypes> aspectTypes = new();
        _ = Assert.Throws<ArgumentException>(() => _checkedProgAspects.CheckAspects(distance, orb, aspectTypes));
    }
    
    
    private static List<AspectTypes> CreateAspectTypes()
    {
        List<AspectTypes> aspectTypes = new()
        {
            AspectTypes.Conjunction,
            AspectTypes.Opposition,
            AspectTypes.Quintile
        };
        return aspectTypes;
    }
    
    
}