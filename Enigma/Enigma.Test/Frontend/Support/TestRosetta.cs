// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Frontend.Support;
using Moq;

namespace Enigma.Test.Frontend.Support;

[TestFixture]
public class TestRosetta
{

    [Test]
    public void TestHappyFlow()
    {
        IEnumerable<string> allLines = new string[] { "first.subject = MyFirstSubject", "second.subject = MySecondSubject", "third.subject = MyThirdSubject" };
        var mock = new Mock<ITextFileReader>();
        mock.Setup(p => p.ReadSeparatedLines(It.IsAny<string>())).Returns(allLines);
        IRosetta rosetta = new Rosetta(mock.Object);
        string key = "second.subject";
        string expectedValue = "MySecondSubject";
        string retrievedValue = rosetta.TextForId(key);
        Assert.That(retrievedValue, Is.EqualTo(expectedValue));
    }

    [Test]
    public void TestKeyNotFound()
    {
        IEnumerable<string> allLines = new string[] { "first.subject = MyFirstSubject", "second.subject = MySecondSubject", "third.subject = MyThirdSubject" };
        var mock = new Mock<ITextFileReader>();
        mock.Setup(p => p.ReadSeparatedLines(It.IsAny<string>())).Returns(allLines);
        IRosetta rosetta = new Rosetta(mock.Object);
        string key = "wrong.key";
        string expectedValue = "-NOT FOUND-";
        string retrievedValue = rosetta.TextForId(key);
        Assert.That(retrievedValue, Is.EqualTo(expectedValue));
    }

    [Test]
    public void TestWithEmptyLines()
    {
        IEnumerable<string> allLines = new string[] { "first.subject = MyFirstSubject", "", "third.subject = MyThirdSubject" };
        var mock = new Mock<ITextFileReader>();
        mock.Setup(p => p.ReadSeparatedLines(It.IsAny<string>())).Returns(allLines);
        IRosetta rosetta = new Rosetta(mock.Object);
        string key = "third.subject";
        string expectedValue = "MyThirdSubject";
        string retrievedValue = rosetta.TextForId(key);
        Assert.That(retrievedValue, Is.EqualTo(expectedValue));
    }

    [Test]
    public void TestWithCommentLines()
    {
        IEnumerable<string> allLines = new string[] { "first.subject = MyFirstSubject", "#Comment line", "third.subject = MyThirdSubject" };
        var mock = new Mock<ITextFileReader>();
        mock.Setup(p => p.ReadSeparatedLines(It.IsAny<string>())).Returns(allLines);
        IRosetta rosetta = new Rosetta(mock.Object);
        string key = "third.subject";
        string expectedValue = "MyThirdSubject";
        string retrievedValue = rosetta.TextForId(key);
        Assert.That(retrievedValue, Is.EqualTo(expectedValue));
    }


    [Test]
    public void TestWithSexagesimals()
    {
        IEnumerable<string> allLines = new string[] { "septile = Septile (51\u00B025\u203243\u2033)" };
        var mock = new Mock<ITextFileReader>();
        mock.Setup(p => p.ReadSeparatedLines(It.IsAny<string>())).Returns(allLines);
        IRosetta rosetta = new Rosetta(mock.Object);
        string key = "septile";
        string expectedValue = "Septile (51" + '\u00B0' + "25" + '\u2032' + "43" + '\u2033' + ")";
        string retrievedValue = rosetta.TextForId(key);
        Assert.That(retrievedValue, Is.EqualTo(expectedValue));
    }

}