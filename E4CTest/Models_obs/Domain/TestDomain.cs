// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using E4C.Models.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace E4CTest.be.domain
{
    [TestClass]
    public class TestSeFlags
    {
        [TestMethod]
        public void TestFlagsWithoutSpecifics()
        {
            // should only contain 2 (using SE) and 256 (using speed) = 258.
            int flags = SeFlags.DefineFlags(CoordinateSystems.Ecliptical, ObserverPositions.GeoCentric, ZodiacTypes.Tropical);
            Assert.AreEqual(258, flags);
        }

        [TestMethod]
        public void TestFlagsForEquatorial()
        {
            // 2 (SE), 256 (speed) and 2048 (equatorial) = 2306.
            int flags = SeFlags.DefineFlags(CoordinateSystems.Equatorial, ObserverPositions.GeoCentric, ZodiacTypes.Tropical);
            Assert.AreEqual(2306, flags);
        }

        [TestMethod]
        public void TestFlagsForHelioCentric()
        {
            // 2 (SE), 256 (speed) and 8 (heliocentric) = 266.
            int flags = SeFlags.DefineFlags(CoordinateSystems.Ecliptical, ObserverPositions.HelioCentric, ZodiacTypes.Tropical);
            Assert.AreEqual(266, flags);
        }

        [TestMethod]
        public void TestFlagsForTopoCentric()
        {
            // 2 (SE), 256 (speed) and 32*1024 (topocentric) = 33026.
            int flags = SeFlags.DefineFlags(CoordinateSystems.Ecliptical, ObserverPositions.TopoCentric, ZodiacTypes.Tropical);
            Assert.AreEqual(33026, flags);
        }

        [TestMethod]
        public void TestFlagsForSidereal()
        {
            // 2 (SE), 256 (speed) and 64*1024 (sidereal) = 65794.
            int flags = SeFlags.DefineFlags(CoordinateSystems.Ecliptical, ObserverPositions.GeoCentric, ZodiacTypes.Sidereal);
            Assert.AreEqual(65794, flags);
        }

        [TestMethod]
        public void TestFlagsForHelioCentricAndSidereal()
        {
            // 2 (SE), 256 (speed), 8 (heliocentric) and 64*1024 (sidereal) = 65802.
            int flags = SeFlags.DefineFlags(CoordinateSystems.Ecliptical, ObserverPositions.HelioCentric, ZodiacTypes.Sidereal);
            Assert.AreEqual(65802, flags);
        }

        [TestMethod]
        public void TestFlagsForTopoCentricAndSidereal()
        {
            // 2 (SE), 256 (speed), 32*1024 (topocentric) and 64*1024 (sidereal) = 98562.
            int flags = SeFlags.DefineFlags(CoordinateSystems.Ecliptical, ObserverPositions.TopoCentric, ZodiacTypes.Sidereal);
            Assert.AreEqual(98562, flags);
        }


    }

 


}