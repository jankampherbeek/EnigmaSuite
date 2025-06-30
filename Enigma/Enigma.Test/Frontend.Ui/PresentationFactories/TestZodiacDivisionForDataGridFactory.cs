// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.Generic;
using System.Linq;
using Enigma.Domain.Presentables;
using Enigma.Domain.References;
using Enigma.Frontend.Ui.PresentationFactories;

namespace Enigma.Test.Frontend.Ui.PresentationFactories;

[TestFixture]
public class TestZodiacDivisionForDataGridFactory
{
    private IZodiacDivisionForDataGridFactory _factory;

    [SetUp]
    public void Setup()
    {
        _factory = new ZodiacDivisionForDataGridFactory();
    }

    [Test]
    public void TestCreateZodiacDivisionForDataGrid_HappyPath()
    {
        // Arrange
        var dataList = CreateValidDataList();

        // Act
        var result = _factory.CreateZodiacDivisionForDataGrid(dataList);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count(), Is.EqualTo(2));
            
            var resultList = result.ToList();
            
            // Test first item
            Assert.That(resultList[0].Planet, Is.EqualTo("a"));
            Assert.That(resultList[0].Longitude, Is.EqualTo("15°30′45″"));
            Assert.That(resultList[0].Signs, Is.EqualTo("1"));
            Assert.That(resultList[0].Decans, Is.EqualTo("2"));
            Assert.That(resultList[0].Dodecatemoria, Is.EqualTo("3"));
            Assert.That(resultList[0].Bounds, Is.EqualTo("4"));
            
            // Test second item
            Assert.That(resultList[1].Planet, Is.EqualTo("b"));
            Assert.That(resultList[1].Longitude, Is.EqualTo("45°15′30″"));
            Assert.That(resultList[1].Signs, Is.EqualTo("5"));
            Assert.That(resultList[1].Decans, Is.EqualTo("6"));
            Assert.That(resultList[1].Dodecatemoria, Is.EqualTo("7"));
            Assert.That(resultList[1].Bounds, Is.EqualTo("8"));
        });
    }

    [Test]
    public void TestCreateZodiacDivisionForDataGrid_EmptyList()
    {
        // Arrange
        var dataList = new List<KeyValuePair<ChartPoints, string[]>>();

        // Act
        var result = _factory.CreateZodiacDivisionForDataGrid(dataList);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count(), Is.EqualTo(0));
        });
    }


    [Test]
    public void TestCreateZodiacDivisionForDataGrid_InvalidArrayLength()
    {
        // Arrange
        var dataList = new List<KeyValuePair<ChartPoints, string[]>>
        {
            new(ChartPoints.Sun, new string[] { "a", "15°30′45″", "1", "2", "3" }), // Only 5 elements instead of 6
            new(ChartPoints.Moon, new string[] { "b", "45°15′30″", "5", "6", "7", "8" }) // Valid 6 elements
        };

        // Act
        var result = _factory.CreateZodiacDivisionForDataGrid(dataList);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count(), Is.EqualTo(1)); // Only the valid item should be included
            
            var resultList = result.ToList();
            Assert.That(resultList[0].Planet, Is.EqualTo("b")); // Should be the second item
        });
    }

    [Test]
    public void TestCreateZodiacDivisionForDataGrid_MixedValidAndInvalidData()
    {
        // Arrange
        var dataList = new List<KeyValuePair<ChartPoints, string[]>>
        {
            new(ChartPoints.Sun, new string[] { "a", "15°30′45″", "1", "2", "3", "4" }), // Valid
            new(ChartPoints.Moon, new string[] { "b", "45°15′30″", "5", "6" }), // Invalid - too short
            new(ChartPoints.Mercury, new string[] { "c", "75°45′20″", "9", "10", "11", "12" }), // Valid
            new(ChartPoints.Venus, new string[] { "d", "120°20′10″" }), // Invalid - too short
            new(ChartPoints.Mars, new string[] { "e", "180°00′00″", "13", "14", "15", "16" }) // Valid
        };

        // Act
        var result = _factory.CreateZodiacDivisionForDataGrid(dataList);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count(), Is.EqualTo(3)); // Only valid items should be included
            
            var resultList = result.ToList();
            Assert.That(resultList[0].Planet, Is.EqualTo("a")); // First valid item
            Assert.That(resultList[1].Planet, Is.EqualTo("c")); // Second valid item
            Assert.That(resultList[2].Planet, Is.EqualTo("e")); // Third valid item
        });
    }

    [Test]
    public void TestCreateZodiacDivisionForDataGrid_AllInvalidData()
    {
        // Arrange
        var dataList = new List<KeyValuePair<ChartPoints, string[]>>
        {
            new(ChartPoints.Sun, new string[] { "a", "15°30′45″", "1", "2", "3" }), // Too short
            new(ChartPoints.Moon, new string[] { "b", "45°15′30″" }), // Too short
            new(ChartPoints.Mercury, new string[] { "c" }) // Too short
        };

        // Act
        var result = _factory.CreateZodiacDivisionForDataGrid(dataList);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count(), Is.EqualTo(0)); // No valid items
        });
    }

    [Test]
    public void TestCreateZodiacDivisionForDataGrid_ExactSixElements()
    {
        // Arrange
        var dataList = new List<KeyValuePair<ChartPoints, string[]>>
        {
            new(ChartPoints.Sun, new string[] { "a", "15°30′45″", "1", "2", "3", "4" })
        };

        // Act
        var result = _factory.CreateZodiacDivisionForDataGrid(dataList);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count(), Is.EqualTo(1));
            
            var resultList = result.ToList();
            Assert.That(resultList[0].Planet, Is.EqualTo("a"));
            Assert.That(resultList[0].Longitude, Is.EqualTo("15°30′45″"));
            Assert.That(resultList[0].Signs, Is.EqualTo("1"));
            Assert.That(resultList[0].Decans, Is.EqualTo("2"));
            Assert.That(resultList[0].Dodecatemoria, Is.EqualTo("3"));
            Assert.That(resultList[0].Bounds, Is.EqualTo("4"));
        });
    }

    [Test]
    public void TestCreateZodiacDivisionForDataGrid_MoreThanSixElements()
    {
        // Arrange
        var dataList = new List<KeyValuePair<ChartPoints, string[]>>
        {
            new(ChartPoints.Sun, new string[] { "a", "15°30′45″", "1", "2", "3", "4", "extra1", "extra2" }) // 8 elements
        };

        // Act
        var result = _factory.CreateZodiacDivisionForDataGrid(dataList);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count(), Is.EqualTo(1)); // Should still be valid since it has at least 6 elements
            
            var resultList = result.ToList();
            Assert.That(resultList[0].Planet, Is.EqualTo("a"));
            Assert.That(resultList[0].Longitude, Is.EqualTo("15°30′45″"));
            Assert.That(resultList[0].Signs, Is.EqualTo("1"));
            Assert.That(resultList[0].Decans, Is.EqualTo("2"));
            Assert.That(resultList[0].Dodecatemoria, Is.EqualTo("3"));
            Assert.That(resultList[0].Bounds, Is.EqualTo("4"));
        });
    }

    [Test]
    public void TestCreateZodiacDivisionForDataGrid_EmptyStrings()
    {
        // Arrange
        var dataList = new List<KeyValuePair<ChartPoints, string[]>>
        {
            new(ChartPoints.Sun, new string[] { "", "", "", "", "", "" }) // All empty strings
        };

        // Act
        var result = _factory.CreateZodiacDivisionForDataGrid(dataList);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count(), Is.EqualTo(1));
            
            var resultList = result.ToList();
            Assert.That(resultList[0].Planet, Is.EqualTo(""));
            Assert.That(resultList[0].Longitude, Is.EqualTo(""));
            Assert.That(resultList[0].Signs, Is.EqualTo(""));
            Assert.That(resultList[0].Decans, Is.EqualTo(""));
            Assert.That(resultList[0].Dodecatemoria, Is.EqualTo(""));
            Assert.That(resultList[0].Bounds, Is.EqualTo(""));
        });
    }

    [Test]
    public void TestCreateZodiacDivisionForDataGrid_NullStrings()
    {
        // Arrange
        var dataList = new List<KeyValuePair<ChartPoints, string[]>>
        {
            new(ChartPoints.Sun, new string[] { null, null, null, null, null, null }) // All null strings
        };

        // Act
        var result = _factory.CreateZodiacDivisionForDataGrid(dataList);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count(), Is.EqualTo(1));
            
            var resultList = result.ToList();
            Assert.That(resultList[0].Planet, Is.Null);
            Assert.That(resultList[0].Longitude, Is.Null);
            Assert.That(resultList[0].Signs, Is.Null);
            Assert.That(resultList[0].Decans, Is.Null);
            Assert.That(resultList[0].Dodecatemoria, Is.Null);
            Assert.That(resultList[0].Bounds, Is.Null);
        });
    }

    private static List<KeyValuePair<ChartPoints, string[]>> CreateValidDataList()
    {
        return new List<KeyValuePair<ChartPoints, string[]>>
        {
            new(ChartPoints.Sun, new string[] { "a", "15°30′45″", "1", "2", "3", "4" }),
            new(ChartPoints.Moon, new string[] { "b", "45°15′30″", "5", "6", "7", "8" })
        };
    }
} 