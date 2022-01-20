// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using E4C.be.domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace E4CTest.be.domain
{
    [TestClass]
    public class TestRosetta
    {
        private IRosetta rosetta;

        [TestMethod]
        public void TestHappyFlow()
        {
            rosetta = new Rosetta();
            string key = "charts.datainputchart.titleform";
            string expectedValue = "Data input for new chart";
            string retrievedValue = rosetta.TextForId(key);
            Assert.AreEqual(expectedValue, retrievedValue);
        }
    }
}
