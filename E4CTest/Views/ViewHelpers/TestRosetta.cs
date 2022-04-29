// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using E4C.be.persistency;
using E4C.Ui.UiHelpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace E4CTest.be.domain
{
    [TestClass]
    public class TestRosetta
    {

        [TestMethod]
        public void TestHappyFlow()
        {
            IEnumerable<string> allLines = new string[] { "first.subject = MyFirstSubject", "second.subject = MySecondSubject", "third.subject = MyThirdSubject" };
            var mock = new Mock<ITextFromFileReader>();
            mock.Setup(p => p.ReadSeparatedLines(It.IsAny<string>())).Returns(allLines);
            IRosetta rosetta = new Rosetta(mock.Object);
            string key = "second.subject";
            string expectedValue = "MySecondSubject";
            string retrievedValue = rosetta.TextForId(key);
            Assert.AreEqual(expectedValue, retrievedValue);
        }

        [TestMethod]
        public void TestKeyNotFound()
        {
            IEnumerable<string> allLines = new string[] { "first.subject = MyFirstSubject", "second.subject = MySecondSubject", "third.subject = MyThirdSubject" };
            var mock = new Mock<ITextFromFileReader>();
            mock.Setup(p => p.ReadSeparatedLines(It.IsAny<string>())).Returns(allLines);
            IRosetta rosetta = new Rosetta(mock.Object);
            string key = "wrong.key";
            string expectedValue = "-NOT FOUND-";
            string retrievedValue = rosetta.TextForId(key);
            Assert.AreEqual(expectedValue, retrievedValue);
        }

        [TestMethod]
        public void TestWithEmptyLines()
        {
            IEnumerable<string> allLines = new string[] { "first.subject = MyFirstSubject", "", "third.subject = MyThirdSubject" };
            var mock = new Mock<ITextFromFileReader>();
            mock.Setup(p => p.ReadSeparatedLines(It.IsAny<string>())).Returns(allLines);
            IRosetta rosetta = new Rosetta(mock.Object);
            string key = "third.subject";
            string expectedValue = "MyThirdSubject";
            string retrievedValue = rosetta.TextForId(key);
            Assert.AreEqual(expectedValue, retrievedValue);
        }

        [TestMethod]
        public void TestWithCommentLines()
        {
            IEnumerable<string> allLines = new string[] { "first.subject = MyFirstSubject", "#Comment line", "third.subject = MyThirdSubject" };
            var mock = new Mock<ITextFromFileReader>();
            mock.Setup(p => p.ReadSeparatedLines(It.IsAny<string>())).Returns(allLines);
            IRosetta rosetta = new Rosetta(mock.Object);
            string key = "third.subject";
            string expectedValue = "MyThirdSubject";
            string retrievedValue = rosetta.TextForId(key);
            Assert.AreEqual(expectedValue, retrievedValue);
        }


    }
}
