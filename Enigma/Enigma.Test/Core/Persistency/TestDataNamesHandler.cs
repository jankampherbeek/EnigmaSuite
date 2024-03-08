// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Handlers;
using Enigma.Core.Persistency;
using FakeItEasy;

namespace Enigma.Test.Core.Persistency;

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
        var foldersInfoFake = A.Fake<IFoldersInfo>();
        A.CallTo(() => foldersInfoFake.GetExistingFolderNames(A<string>._, useSubFolders))
            .Returns(expectedDataNames);
        IDataNamesHandler handler = new DataNamesHandler(foldersInfoFake);
        List<string> resultDataNames = handler.GetExistingDataNames();
        Assert.Multiple(() =>
        {
            Assert.That(resultDataNames, Has.Count.EqualTo(2));
            Assert.That(expectedDataNames[0], Is.EqualTo(resultDataNames[0]));
            Assert.That(expectedDataNames[1], Is.EqualTo(resultDataNames[1]));
        });
    }


}