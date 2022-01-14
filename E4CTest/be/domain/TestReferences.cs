// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using E4C.be.domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace E4CTest.be.domain
{
    [TestClass]
    public class TestSolSysPointCatSpecifications
    {

        [TestMethod]
        public void TestRetrievingDetails()
        {
            SolSysPointCats solSysPointCat = SolSysPointCats.Modern;
            ISolSysPointCatSpecifications specifications = new SolSysPointCatSpecifications();
            SolSysPointCatDetails details = specifications.DetailsForCategory(solSysPointCat);
            Assert.IsNotNull(details);
            Assert.AreEqual(solSysPointCat, details.category);
            Assert.AreEqual("enumSolSysPointCatModern", details.textId);
        }

        [TestMethod]
        public void TestAvailabilityOffDetailsForAllEnums()
        {
            ISolSysPointCatSpecifications specifications = new SolSysPointCatSpecifications();
            foreach (SolSysPointCats category in Enum.GetValues(typeof(SolSysPointCats)))
            {
                SolSysPointCatDetails details = specifications.DetailsForCategory(category);
                Assert.IsNotNull(details);
                Assert.IsTrue(details.textId.Length > 0);
            }
        }
    }

    
}