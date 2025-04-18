﻿// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Calc;
using Enigma.Facades.Se;
using FakeItEasy;

namespace Enigma.Test.Core.Calc;

[TestFixture]
public class TestObliquityCalc
{
    private const double DELTA = 0.00000001;

    [Test]
    public void TestCalculateTrueObliquity()
    {
        const bool trueObliquity = true;
        const double expectedObliquity = 23.447;
        const double jd = 12345.678;
        IObliquityCalc calc = CreateObliquityCalc();
        double result = calc.CalculateObliquity(jd, trueObliquity);
        Assert.That(result, Is.EqualTo(expectedObliquity).Within(DELTA));
    }

    [Test]
    public void TestCalculateMeanObliquity()
    {
        const bool trueObliquity = false;
        const double expectedObliquity = 23.448;
        const double jd = 12345.678;
        IObliquityCalc calc = CreateObliquityCalc();
        double result = calc.CalculateObliquity(jd, trueObliquity);
        Assert.That(result, Is.EqualTo(expectedObliquity).Within(DELTA));
    }

    private static IObliquityCalc CreateObliquityCalc()
    {
        const int celpointId = -1;
        const int flags = 0;
        const double jd = 12345.678;
        double[] positions = { 23.447, 23.448, 0.0, 0.0, 0.0, 0.0 };
        var facadeFake = A.Fake<ICalcUtFacade>();
        A.CallTo(() => facadeFake.PositionFromSe(jd, celpointId, flags)).Returns(positions);
        ObliquityCalc calc = new(facadeFake);
        return calc;
    }
}

