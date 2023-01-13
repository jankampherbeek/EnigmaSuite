// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Handlers.Calc;
using Enigma.Core.Handlers.Interfaces;
using Enigma.Domain.Calc;
using Enigma.Domain.Constants;
using Enigma.Domain.Exceptions;
using Enigma.Domain.Points;

namespace Enigma.Test.Core.Handlers.Calc;

[TestFixture]
public class TestChartPointMappings
{
    private readonly IChartPointsMapping _mapping = new ChartPointsMapping();


    [Test]
    public void TestCalculationTypeForPointCelestialSe()
    {
        ChartPoints point = ChartPoints.Jupiter;
        CalculationTypes expectedType = CalculationTypes.CelPointSE;
        CalculationTypes actualType = _mapping.CalculationTypeForPoint(point);
        Assert.That(expectedType, Is.EqualTo(actualType));
    }

    [Test]
    public void TestCalculationTypeForPointCelestialElements()
    {
        ChartPoints point = ChartPoints.PersephoneRam;
        CalculationTypes expectedType = CalculationTypes.CelPointElements;
        CalculationTypes actualType = _mapping.CalculationTypeForPoint(point);
        Assert.That(expectedType, Is.EqualTo(actualType));
    }

    [Test]
    public void TestCalculationTypeForPointCelestialFormula()
    {
        ChartPoints point = ChartPoints.PersephoneCarteret;
        CalculationTypes expectedType = CalculationTypes.CelPointFormula;
        CalculationTypes actualType = _mapping.CalculationTypeForPoint(point);
        Assert.That(expectedType, Is.EqualTo(actualType));
    }

    [Test]
    public void TestCalculationTypeForPointMundane()
    {
        ChartPoints point = ChartPoints.Ascendant;
        CalculationTypes expectedType = CalculationTypes.Mundane;
        CalculationTypes actualType = _mapping.CalculationTypeForPoint(point);
        Assert.That(expectedType, Is.EqualTo(actualType));
    }

    [Test]
    public void TestCalculationTypeForPointCusp()
    {
        ChartPoints point = ChartPoints.Cusp17;
        CalculationTypes expectedType = CalculationTypes.Mundane;
        CalculationTypes actualType = _mapping.CalculationTypeForPoint(point);
        Assert.That(expectedType, Is.EqualTo(actualType));
    }

    [Test]
    public void TestCalculationTypeForPointZodiac()
    {
        ChartPoints point = ChartPoints.ZeroAries;
        CalculationTypes expectedType = CalculationTypes.Specific;
        CalculationTypes actualType = _mapping.CalculationTypeForPoint(point);
        Assert.That(expectedType, Is.EqualTo(actualType));
    }

    [Test]
    public void TestCalculationTypeForPointArabic()
    {
        ChartPoints point = ChartPoints.FortunaNoSect;
        CalculationTypes expectedType = CalculationTypes.Specific;
        CalculationTypes actualType = _mapping.CalculationTypeForPoint(point);
        Assert.That(expectedType, Is.EqualTo(actualType));
    }

    [Test]
    public void TestSeIdForPointHappyFlow()
    {
        ChartPoints point = ChartPoints.Juno;
        int expectedId = EnigmaConstants.SE_JUNO;
        int actualId = _mapping.SeIdForCelestialPoint(point);
        Assert.That(expectedId, Is.EqualTo(actualId));
    }

    [Test]
    public void TestSeIdForPointError()
    {
        ChartPoints point = ChartPoints.Mc;
        var _ = Assert.Throws<EnigmaException>(() => _mapping.SeIdForCelestialPoint(point));
    }

}