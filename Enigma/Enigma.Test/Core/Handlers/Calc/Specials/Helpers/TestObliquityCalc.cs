﻿// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Handlers.Calc.Specials.Helpers;
using Enigma.Core.Handlers.Interfaces;
using Enigma.Facades.Interfaces;
using Moq;

namespace Enigma.Test.Core.Handlers.Calc.Specials.Helpers;

[TestFixture]
public class TestObliquityCalc
{
    readonly double delta = 0.00000001;

    [Test]
    public void TestCalculateTrueObliquity()
    {
        bool trueObliquity = true;
        double expectedObliquity = 23.447;
        double jd = 12345.678;
        IObliquityCalc calc = CreateObliquityCalc();
        double result = calc.CalculateObliquity(jd, trueObliquity);
        Assert.That(result, Is.EqualTo(expectedObliquity).Within(delta));
    }

    [Test]
    public void TestCalculateMeanObliquity()
    {
        bool trueObliquity = false;
        double expectedObliquity = 23.448;
        double jd = 12345.678;
        IObliquityCalc calc = CreateObliquityCalc();
        double result = calc.CalculateObliquity(jd, trueObliquity);
        Assert.That(result, Is.EqualTo(expectedObliquity).Within(delta));
    }

    private static IObliquityCalc CreateObliquityCalc()
    {
        int celpointId = -1;
        int flags = 0;
        double jd = 12345.678;
        double[] positions = { 23.448, 23.447, 0.0, 0.0, 0.0, 0.0 };
        var mock = new Mock<ICalcUtFacade>();
        mock.Setup(p => p.PositionFromSe(jd, celpointId, flags)).Returns(positions);
        ObliquityCalc calc = new(mock.Object);
        return calc;
    }
}
