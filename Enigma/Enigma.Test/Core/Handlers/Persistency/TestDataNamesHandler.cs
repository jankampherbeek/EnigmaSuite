// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Handlers.Interfaces;
using Enigma.Core.Handlers.Persistency;
using Moq;

namespace Enigma.Test.Core.Handlers.Persistency;

[TestFixture]
public class TestDataNamesHandler
{
    [Test]
    public void TestGetExistingDataNamesHappyFlow()
    {
        const bool useSubFolders = false;
        List<string> expectedDataNames = new()
        {
            "dataname-1",
            "dataname-2"
        };
        var mock = new Mock<IFoldersInfo>();
        mock.Setup(p => p.GetExistingFolderNames(It.IsAny<string>(), useSubFolders)).Returns(expectedDataNames);
        IDataNamesHandler handler = new DataNamesHandler(mock.Object);
        List<string> resultDataNames = handler.GetExistingDataNames();
        Assert.Multiple(() =>
        {
            Assert.That(resultDataNames, Has.Count.EqualTo(2));
            Assert.That(expectedDataNames[0], Is.EqualTo(resultDataNames[0]));
            Assert.That(expectedDataNames[1], Is.EqualTo(resultDataNames[1]));
        });
    }


}