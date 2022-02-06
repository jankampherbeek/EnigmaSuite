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
    public class TestCalendarCalc
    {
        readonly double delta = 0.00000001;

        [TestMethod]
        public void TestCalculateJd()
        {
            double expectedJd = 12345.678;
            var mock = new Mock<ISeDateTimeFacade>();
            SimpleDateTime dateTime = new(2000, 1, 1, 12.0, Calendars.Gregorian);
            mock.Setup(p => p.JdFromSe(dateTime)).Returns(expectedJd);
            CalendarCalc calc = new(mock.Object);
            ResultForDouble result = calc.CalculateJd(dateTime);
            Assert.AreEqual(expectedJd, result.ReturnValue, delta);

        }
    }
}