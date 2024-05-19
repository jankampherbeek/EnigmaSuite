// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Frontend.Ui.Support;

namespace Enigma.Test.Frontend.Ui.Support;

[TestFixture]
public class TestRosetta
{
    private Rosetta _rosetta = Rosetta.Instance;
    
    
    [Test]
    public void TestSetAndGetLanguage()
    {
        _rosetta.SetLanguage("en");
        Assert.That(_rosetta.GetLanguage(), Is.EqualTo("en"));
        _rosetta.SetLanguage("nl");
        Assert.That(_rosetta.GetLanguage(), Is.EqualTo("nl"));
    }

    [Test]
    public void TestSettingWrongLanguage()
    {
        _rosetta.SetLanguage("nl");
        _rosetta.SetLanguage("xx");
        Assert.That(_rosetta.GetLanguage(), Is.EqualTo("nl"));
    }

    [Test]
    public void TestGetText()
    {
        const string rbKey = "test-line-1";
        const string expectedEn = "First line for testing";
        const string expectedNl = "Eerste test regel";
        _rosetta.SetLanguage("en");
        Assert.That(_rosetta.GetText(rbKey), Is.EqualTo(expectedEn));
        _rosetta.SetLanguage("nl");
        Assert.That(_rosetta.GetText(rbKey), Is.EqualTo(expectedNl));
    }
    
}