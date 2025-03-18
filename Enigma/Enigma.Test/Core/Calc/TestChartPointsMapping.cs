// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Calc;
using Enigma.Domain.Constants;
using Enigma.Domain.Exceptions;
using Enigma.Domain.References;

namespace Enigma.Test.Core.Calc;

[TestFixture]
public class TestChartPointMappings
{
    private readonly IChartPointsMapping _mapping = new ChartPointsMapping();


    [Test]
    public void TestCalculationTypeForPointCelestialSe()
    {
        const ChartPoints point = ChartPoints.Jupiter;
        const CalculationCats expectedType = CalculationCats.CommonSe;
        CalculationCats actualType = _mapping.CalculationTypeForPoint(point);
        Assert.That(actualType, Is.EqualTo(expectedType));
    }

    [Test]
    public void TestCalculationTypeForPointCelestialElements()
    {
        const ChartPoints point = ChartPoints.PersephoneRam;
        const CalculationCats expectedType = CalculationCats.CommonElements;
        CalculationCats actualType = _mapping.CalculationTypeForPoint(point);
        Assert.That(actualType, Is.EqualTo(expectedType));
    }

    [Test]
    public void TestCalculationTypeForPointCelestialFormula()
    {
        const ChartPoints point = ChartPoints.PersephoneCarteret;
        const CalculationCats expectedType = CalculationCats.CommonFormulaLongitude;
        CalculationCats actualType = _mapping.CalculationTypeForPoint(point);
        Assert.That(actualType, Is.EqualTo(expectedType));
    }

    [Test]
    public void TestCalculationTypeForPointMundane()
    {
        const ChartPoints point = ChartPoints.Ascendant;
        const CalculationCats expectedType = CalculationCats.Mundane;
        CalculationCats actualType = _mapping.CalculationTypeForPoint(point);
        Assert.That(actualType, Is.EqualTo(expectedType));
    }

    [Test]
    public void TestCalculationTypeForPointCusp()
    {
        const ChartPoints point = ChartPoints.Cusp17;
        const CalculationCats expectedType = CalculationCats.Mundane;
        CalculationCats actualType = _mapping.CalculationTypeForPoint(point);
        Assert.That(actualType, Is.EqualTo(expectedType));
    }

    [Test]
    public void TestCalculationTypeForPointZodiac()
    {
        const ChartPoints point = ChartPoints.ZeroAries;
        const CalculationCats expectedType = CalculationCats.ZodiacFixed;
        CalculationCats actualType = _mapping.CalculationTypeForPoint(point);
        Assert.That(actualType, Is.EqualTo(expectedType));
    }

    [Test]
    public void TestCalculationTypeForPointArabic()
    {
        const ChartPoints point = ChartPoints.FortunaNoSect;
        const CalculationCats expectedType = CalculationCats.Lots;
        CalculationCats actualType = _mapping.CalculationTypeForPoint(point);
        Assert.That(actualType, Is.EqualTo(expectedType));
    }

    [Test]
    public void TestSeIdForPointHappyFlow()
    {
        const ChartPoints point = ChartPoints.Juno;
        int expectedId = ChartPoints.Juno.GetDetails().CalcId;
        int actualId = _mapping.SeIdForCelestialPoint(point);
        Assert.That(actualId, Is.EqualTo(expectedId));
    }



}