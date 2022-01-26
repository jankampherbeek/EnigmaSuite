// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using E4C.Models.Astron;
using E4C.Models.Domain;
using E4C.Models.SeFacade;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;


namespace E4CTest
{
    [TestClass]
    public class TestObliquityNutationCalc
    {
        readonly double delta = 0.00000001;

        [TestMethod]
        public void TestCalculateTrueObliquity()
        {
            bool trueObliquity = true;
            double expectedObliquity = 23.447;
            double jd = 12345.678;
            ObliquityNutationCalc calc = CreateObliquityNutationCalc();
            ResultForDouble result = calc.CalculateObliquity(jd, trueObliquity);
            Assert.AreEqual(expectedObliquity, result.returnValue, delta);
        }

        [TestMethod]
        public void TestCalculateMeanObliquity()
        {
            bool trueObliquity = false;
            double expectedObliquity = 23.448;
            double jd = 12345.678;
            ObliquityNutationCalc calc = CreateObliquityNutationCalc();
            ResultForDouble result = calc.CalculateObliquity(jd, trueObliquity);
            Assert.AreEqual(expectedObliquity, result.returnValue, delta);
        }

        private static ObliquityNutationCalc CreateObliquityNutationCalc()
        {
            int celpointId = -1;
            int flags = 0;
            double jd = 12345.678;
            double[] positions = { 23.448, 23.447, 0.0, 0.0, 0.0, 0.0 };
            var mock = new Mock<ISePosCelPointFacade>();
            mock.Setup(p => p.PosCelPointFromSe(jd, celpointId, flags)).Returns(positions);
            ObliquityNutationCalc calc = new(mock.Object);
            return calc;
        }
    }

}
