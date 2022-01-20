// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using E4C.be.domain;
using E4C.be.persistency;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

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
            mock.Setup(p => p.readSeparatedLines(It.IsAny<string>())).Returns(allLines);
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
            mock.Setup(p => p.readSeparatedLines(It.IsAny<string>())).Returns(allLines);
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
            mock.Setup(p => p.readSeparatedLines(It.IsAny<string>())).Returns(allLines);
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
            mock.Setup(p => p.readSeparatedLines(It.IsAny<string>())).Returns(allLines);
            IRosetta rosetta = new Rosetta(mock.Object);
            string key = "third.subject";
            string expectedValue = "MyThirdSubject";
            string retrievedValue = rosetta.TextForId(key);
            Assert.AreEqual(expectedValue, retrievedValue);
        }


    }
}
