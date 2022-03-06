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

    [TestClass]
    public class TestSolSysPointPosSpeeds
    {
        [TestMethod]
        public void TestConstructionWithArray()
        {
            double delta = 0.00000001;
            double[] values = { 1.0, 2.0, 3.0, 4.0, 5.0, 6.0 };
            SolSysPointPosSpeeds posSpeeds = new(values);
            Assert.AreEqual(1.0, posSpeeds.MainPosSpeed.Position, delta);
            Assert.AreEqual(2.0, posSpeeds.MainPosSpeed.Speed, delta);
            Assert.AreEqual(3.0, posSpeeds.DeviationPosSpeed.Position, delta);
            Assert.AreEqual(4.0, posSpeeds.DeviationPosSpeed.Speed, delta);
            Assert.AreEqual(5.0, posSpeeds.DistancePosSpeed.Position, delta);
            Assert.AreEqual(6.0, posSpeeds.DistancePosSpeed.Speed, delta);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestConstructionWrongArray()
        {
            double[] values = { 1.0, 2.0, 3.0, 4.0, 5.0 };
            SolSysPointPosSpeeds _ = new(values);
        }

        [TestMethod]
        public void TestConstructionWithPosSpeeds()
        {
            double delta = 0.00000001;
            PosSpeed mainPosSpeed = new(1.0, 2.0);
            PosSpeed deviationPosSpeed = new(3.0, 4.0);
            PosSpeed distancePosSpeed = new(5.0, 6.0);
            SolSysPointPosSpeeds posSpeeds = new(mainPosSpeed, deviationPosSpeed, distancePosSpeed);
            Assert.AreEqual(1.0, posSpeeds.MainPosSpeed.Position, delta);
            Assert.AreEqual(2.0, posSpeeds.MainPosSpeed.Speed, delta);
            Assert.AreEqual(3.0, posSpeeds.DeviationPosSpeed.Position, delta);
            Assert.AreEqual(4.0, posSpeeds.DeviationPosSpeed.Speed, delta);
            Assert.AreEqual(5.0, posSpeeds.DistancePosSpeed.Position, delta);
            Assert.AreEqual(6.0, posSpeeds.DistancePosSpeed.Speed, delta);
        }


    }


}