// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Frontend.Helpers.Support;

namespace Enigma.Test.Frontend.Ui.Support;


/// <summary>Integration test for Rosetta</summary>
/// <remarks>Expects an existing resource file 'Texts.txt'.</remarks>
[TestFixture]
public class TestRosetta
{

    [Test]
    public void TestHappyFlow()
    {
        string key = "mainwindow.charts";
        string expectedValue = "Charts";
        string retrievedValue = Rosetta.TextForId(key);
        Assert.That(retrievedValue, Is.EqualTo(expectedValue));
    }

    [Test]
    public void TestKeyNotFound()
    {
        string key = "wrong.key";
        string expectedValue = "-NOT FOUND-";
        string retrievedValue = Rosetta.TextForId(key);
        Assert.That(retrievedValue, Is.EqualTo(expectedValue));
    }

    [Test]
    public void TestWithSexagesimals()
    {
        string key = "ref.enum.aspect.septile";
        string expectedValue = "Septile (51" + '\u00B0' + "25" + '\u2032' + "43" + '\u2033' + ")";
        string retrievedValue = Rosetta.TextForId(key);
        Assert.That(retrievedValue, Is.EqualTo(expectedValue));
    }

}