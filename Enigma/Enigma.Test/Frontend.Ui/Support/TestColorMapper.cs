// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Drawing;
using Enigma.Frontend.Ui.Support;

namespace Enigma.Test.Frontend.Ui.Support;

[TestFixture]
public class TestColorMapper
{
    private ColorMapper _colorMapper = new ColorMapper();

    [Test]
    public void TestNameFromColorHappyFlow()
    {
        Color color = Color.Purple;
        string expectedName = "Purple";
        string resultName = _colorMapper.NameFromColor(color);
        Assert.That(expectedName, Is.EqualTo(resultName));
    }

    [Test]
    public void TestColorFromNameHappyFLow()
    {
        string name = "CornflowerBlue";
        Color expectedColor = Color.CornflowerBlue;
        Color resultColor = _colorMapper.ColorFromName(name);
        Assert.That(expectedColor, Is.EqualTo(resultColor));
    }
    
    [Test]
    public void TestNameFromColorHappyNotFound()
    {
        Color color = Color.Chartreuse;
        string expectedName = "Unknown color";
        string resultName = _colorMapper.NameFromColor(color);
        Assert.That(expectedName, Is.EqualTo(resultName));
    }
    
    [Test]
    public void TestColorFromNameNotFound()
    {
        string name = "Firebrick";
        Color expectedColor = Color.White;
        Color resultColor = _colorMapper.ColorFromName(name);
        Assert.That(expectedColor, Is.EqualTo(resultColor));
    }
    
}