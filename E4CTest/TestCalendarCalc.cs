using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using E4C.be.sefacade;
using E4C.be.astron;
using E4C.be.model;

namespace E4CTest
{
    [TestClass]
    public class TestCalendarCalc
    {
        readonly double delta = 0.00000001;

        [TestMethod]
        public void TestCalculateJd()
        {
            double expectedJd = 12345.678;
            var mock = new Mock<ISeDateTimeFacade>();
            SimpleDateTime dateTime = new(2000, 1, 1, 12.0, true);
            mock.Setup(p => p.JdFromSe(dateTime)).Returns(expectedJd);
            CalendarCalc calc = new (mock.Object);
            ResultForDouble result = calc.CalculateJd(dateTime);
            Assert.AreEqual(expectedJd, result.returnValue, delta);

        }
    }
}