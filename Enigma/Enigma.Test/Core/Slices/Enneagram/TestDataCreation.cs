// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Slices.Enneagram;

namespace Enigma.Test.Core.Slices.Enneagram;

[TestFixture]
public class TestDataCreation
{
    private DataCreation _dataCreation = null!;
    private const double DELTA = 0.00000001;

    [SetUp]
    public void SetUp()
    {
        _dataCreation = new DataCreation();
    }

    [Test]
    public void TestReadDataForSigns_ValidData()
    {
        // Arrange
        var testPath = "res/enneagram/enneagram-signs.csv";
        CreateTestFile(testPath, CreateValidSignsData());

        // Act
        var result = _dataCreation.ReadDataForSigns(new[] { testPath });

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Has.Count.EqualTo(2));
        
        // Check first record
        var firstRecord = result[0];
        Assert.Multiple(() =>
        {
            Assert.That(firstRecord.Point, Is.EqualTo(0));
            Assert.That(firstRecord.Index, Is.EqualTo(1));
            Assert.That(firstRecord.Factors, Has.Length.EqualTo(9));
            Assert.That(firstRecord.Factors[0], Is.EqualTo(1.21).Within(DELTA));
            Assert.That(firstRecord.Factors[1], Is.EqualTo(0.80).Within(DELTA));
            Assert.That(firstRecord.Factors[8], Is.EqualTo(0.84).Within(DELTA));
        });

        // Check second record
        var secondRecord = result[1];
        Assert.Multiple(() =>
        {
            Assert.That(secondRecord.Point, Is.EqualTo(1));
            Assert.That(secondRecord.Index, Is.EqualTo(2));
            Assert.That(secondRecord.Factors, Has.Length.EqualTo(9));
            Assert.That(secondRecord.Factors[0], Is.EqualTo(1.11).Within(DELTA));
            Assert.That(secondRecord.Factors[1], Is.EqualTo(1.45).Within(DELTA));
            Assert.That(secondRecord.Factors[8], Is.EqualTo(0.86).Within(DELTA));
        });
    }

    [Test]
    public void TestReadDataForHouses_ValidData()
    {
        // Arrange
        var testPath = "res/enneagram/ennegram-houses.csv";
        CreateTestFile(testPath, CreateValidHousesData());

        // Act
        var result = _dataCreation.ReadDataForHouses(new[] { testPath });

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Has.Count.EqualTo(2));
        
        // Check first record
        var firstRecord = result[0];
        Assert.Multiple(() =>
        {
            Assert.That(firstRecord.Point, Is.EqualTo(0));
            Assert.That(firstRecord.Index, Is.EqualTo(1));
            Assert.That(firstRecord.Factors, Has.Length.EqualTo(9));
            Assert.That(firstRecord.Factors[0], Is.EqualTo(0.92).Within(DELTA));
            Assert.That(firstRecord.Factors[1], Is.EqualTo(0.98).Within(DELTA));
            Assert.That(firstRecord.Factors[8], Is.EqualTo(0.69).Within(DELTA));
        });

        // Check second record
        var secondRecord = result[1];
        Assert.Multiple(() =>
        {
            Assert.That(secondRecord.Point, Is.EqualTo(1));
            Assert.That(secondRecord.Index, Is.EqualTo(2));
            Assert.That(secondRecord.Factors, Has.Length.EqualTo(9));
            Assert.That(secondRecord.Factors[0], Is.EqualTo(1.14).Within(DELTA));
            Assert.That(secondRecord.Factors[1], Is.EqualTo(0.89).Within(DELTA));
            Assert.That(secondRecord.Factors[8], Is.EqualTo(1.07).Within(DELTA));
        });
    }

    [Test]
    public void TestReadDataForSigns_IgnoresComments()
    {
        // Arrange
        var testPath = "res/enneagram/enneagram-signs.csv";
        var data = new[]
        {
            "# This is a comment",
            "# Another comment line",
            "0,1,1.21,0.80,0.80,1.00,0.97,1.31,1.11,0.96,0.84",
            "# Comment in middle",
            "1,2,1.11,1.45,1.13,0.94,0.87,0.92,1.02,0.71,0.86"
        };
        CreateTestFile(testPath, data);

        // Act
        var result = _dataCreation.ReadDataForSigns(new[] { testPath });

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Has.Count.EqualTo(2));
    }

    [Test]
    public void TestReadDataForHouses_IgnoresComments()
    {
        // Arrange
        var testPath = "res/enneagram/ennegram-houses.csv";
        var data = new[]
        {
            "# This is a comment",
            "# Another comment line",
            "0,1,0.92,0.98,0.61,1.11,1.44,0.66,1.30,1.28,0.69",
            "# Comment in middle",
            "1,2,1.14,0.89,0.71,0.62,1.40,1.13,0.94,1.09,1.07"
        };
        CreateTestFile(testPath, data);

        // Act
        var result = _dataCreation.ReadDataForHouses(new[] { testPath });

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Has.Count.EqualTo(2));
    }

    [Test]
    public void TestReadDataForSigns_IgnoresEmptyLines()
    {
        // Arrange
        var testPath = "res/enneagram/enneagram-signs.csv";
        var data = new[]
        {
            "",
            "0,1,1.21,0.80,0.80,1.00,0.97,1.31,1.11,0.96,0.84",
            "   ",
            "1,2,1.11,1.45,1.13,0.94,0.87,0.92,1.02,0.71,0.86"
        };
        CreateTestFile(testPath, data);

        // Act
        var result = _dataCreation.ReadDataForSigns(new[] { testPath });

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Has.Count.EqualTo(2));
    }

    [Test]
    public void TestReadDataForHouses_IgnoresEmptyLines()
    {
        // Arrange
        var testPath = "res/enneagram/ennegram-houses.csv";
        var data = new[]
        {
            "",
            "0,1,0.92,0.98,0.61,1.11,1.44,0.66,1.30,1.28,0.69",
            "   ",
            "1,2,1.14,0.89,0.71,0.62,1.40,1.13,0.94,1.09,1.07"
        };
        CreateTestFile(testPath, data);

        // Act
        var result = _dataCreation.ReadDataForHouses(new[] { testPath });

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Has.Count.EqualTo(2));
    }

    [Test]
    public void TestReadDataForSigns_FileNotFound()
    {
        // Arrange - Don't create the file
        var testPath = "res/enneagram/enneagram-signs.csv";

        // Act & Assert
        var exception = Assert.Throws<FileNotFoundException>(() => _dataCreation.ReadDataForSigns(new[] { testPath }));
        Assert.That(exception!.Message, Does.Contain("Enneagram data file not found"));
        Assert.That(exception.Message, Does.Contain("enneagram-signs.csv"));
    }

    [Test]
    public void TestReadDataForHouses_FileNotFound()
    {
        // Arrange - Don't create the file
        var testPath = "res/enneagram/ennegram-houses.csv";

        // Act & Assert
        var exception = Assert.Throws<FileNotFoundException>(() => _dataCreation.ReadDataForHouses(new[] { testPath }));
        Assert.That(exception!.Message, Does.Contain("Enneagram data file not found"));
        Assert.That(exception.Message, Does.Contain("enneagram-houses.csv"));
    }

    [Test]
    public void TestReadDataForSigns_InvalidLineFormat_TooFewValues()
    {
        // Arrange
        var testPath = "res/enneagram/enneagram-signs.csv";
        var data = new[]
        {
            "0,1,1.21,0.80,0.80,1.00,0.97,1.31,1.11,0.96", // Only 10 values
            "1,2,1.11,1.45,1.13,0.94,0.87,0.92,1.02,0.71,0.86"
        };
        CreateTestFile(testPath, data);

        // Act & Assert
        var exception = Assert.Throws<FormatException>(() => _dataCreation.ReadDataForSigns(new[] { testPath }));
        Assert.That(exception!.Message, Does.Contain("Invalid line format. Expected 11 comma-separated values"));
    }

    [Test]
    public void TestReadDataForHouses_InvalidLineFormat_TooManyValues()
    {
        // Arrange
        var testPath = "res/enneagram/ennegram-houses.csv";
        var data = new[]
        {
            "0,1,0.92,0.98,0.61,1.11,1.44,0.66,1.30,1.28,0.69,0.99", // 12 values
            "1,2,1.14,0.89,0.71,0.62,1.40,1.13,0.94,1.09,1.07"
        };
        CreateTestFile(testPath, data);

        // Act & Assert
        var exception = Assert.Throws<FormatException>(() => _dataCreation.ReadDataForHouses(new[] { testPath }));
        Assert.That(exception!.Message, Does.Contain("Invalid line format. Expected 11 comma-separated values"));
    }

    [Test]
    public void TestReadDataForSigns_InvalidChartpointValue()
    {
        // Arrange
        var testPath = "res/enneagram/enneagram-signs.csv";
        var data = new[]
        {
            "abc,1,1.21,0.80,0.80,1.00,0.97,1.31,1.11,0.96,0.84", // Invalid chartpoint
            "1,2,1.11,1.45,1.13,0.94,0.87,0.92,1.02,0.71,0.86"
        };
        CreateTestFile(testPath, data);

        // Act & Assert
        var exception = Assert.Throws<FormatException>(() => _dataCreation.ReadDataForSigns(new[] { testPath }));
        Assert.That(exception!.Message, Does.Contain("Invalid chartpoint value: abc"));
    }

    [Test]
    public void TestReadDataForHouses_InvalidIndexValue()
    {
        // Arrange
        var testPath = "res/enneagram/ennegram-houses.csv";
        var data = new[]
        {
            "0,xyz,0.92,0.98,0.61,1.11,1.44,0.66,1.30,1.28,0.69", // Invalid index
            "1,2,1.14,0.89,0.71,0.62,1.40,1.13,0.94,1.09,1.07"
        };
        CreateTestFile(testPath, data);

        // Act & Assert
        var exception = Assert.Throws<FormatException>(() => _dataCreation.ReadDataForHouses(new[] { testPath }));
        Assert.That(exception!.Message, Does.Contain("Invalid index value: xyz"));
    }

    [Test]
    public void TestReadDataForSigns_InvalidFactorValue()
    {
        // Arrange
        var testPath = "res/enneagram/enneagram-signs.csv";
        var data = new[]
        {
            "0,1,1.21,invalid,0.80,1.00,0.97,1.31,1.11,0.96,0.84", // Invalid factor
            "1,2,1.11,1.45,1.13,0.94,0.87,0.92,1.02,0.71,0.86"
        };
        CreateTestFile(testPath, data);

        // Act & Assert
        var exception = Assert.Throws<FormatException>(() => _dataCreation.ReadDataForSigns(new[] { testPath }));
        Assert.That(exception!.Message, Does.Contain("Invalid factor value at position 3: invalid"));
    }

    [Test]
    public void TestReadDataForHouses_InvalidFactorValue()
    {
        // Arrange
        var testPath = "res/enneagram/ennegram-houses.csv";
        var data = new[]
        {
            "0,1,0.92,0.98,0.61,1.11,1.44,0.66,1.30,1.28,invalid", // Invalid factor
            "1,2,1.14,0.89,0.71,0.62,1.40,1.13,0.94,1.09,1.07"
        };
        CreateTestFile(testPath, data);

        // Act & Assert
        var exception = Assert.Throws<FormatException>(() => _dataCreation.ReadDataForHouses(new[] { testPath }));
        Assert.That(exception!.Message, Does.Contain("Invalid factor value at position 10: invalid"));
    }

    [Test]
    public void TestReadDataForSigns_EmptyFile()
    {
        // Arrange
        var testPath = "res/enneagram/enneagram-signs.csv";
        CreateTestFile(testPath, []);

        // Act
        var result = _dataCreation.ReadDataForSigns(new[] { testPath });

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.Empty);
    }

    [Test]
    public void TestReadDataForHouses_EmptyFile()
    {
        // Arrange
        var testPath = "res/enneagram/ennegram-houses.csv";
        CreateTestFile(testPath, []);

        // Act
        var result = _dataCreation.ReadDataForHouses(new[] { testPath });

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.Empty);
    }

    [Test]
    public void TestReadDataForSigns_FileWithOnlyComments()
    {
        // Arrange
        var testPath = "res/enneagram/enneagram-signs.csv";
        var data = new[]
        {
            "# This is a comment",
            "# Another comment",
            "# No data lines"
        };
        CreateTestFile(testPath, data);

        // Act
        var result = _dataCreation.ReadDataForSigns(new[] { testPath });

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.Empty);
    }

    [Test]
    public void TestReadDataForHouses_FileWithOnlyComments()
    {
        // Arrange
        var testPath = "res/enneagram/ennegram-houses.csv";
        var data = new[]
        {
            "# This is a comment",
            "# Another comment",
            "# No data lines"
        };
        CreateTestFile(testPath, data);

        // Act
        var result = _dataCreation.ReadDataForHouses(new[] { testPath });

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.Empty);
    }

    [TearDown]
    public void TearDown()
    {
        // Clean up test files
        CleanupTestFiles();
    }

    private static string[] CreateValidSignsData()
    {
        return
        [
            "# Sun in signs",
            "0,1,1.21,0.80,0.80,1.00,0.97,1.31,1.11,0.96,0.84",
            "# Moon in signs",
            "1,2,1.11,1.45,1.13,0.94,0.87,0.92,1.02,0.71,0.86"
        ];
    }

    private static string[] CreateValidHousesData()
    {
        return
        [
            "# Sun in houses",
            "0,1,0.92,0.98,0.61,1.11,1.44,0.66,1.30,1.28,0.69",
            "# Moon in houses",
            "1,2,1.14,0.89,0.71,0.62,1.40,1.13,0.94,1.09,1.07"
        ];
    }

    private static void CreateTestFile(string filePath, string[] content)
    {
        var directory = Path.GetDirectoryName(filePath);
        if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }
        File.WriteAllLines(filePath, content);
    }

    private static void CleanupTestFiles()
    {
        var testFiles = new[]
        {
            "res/enneagram/enneagram-signs.csv",
            "res/enneagram/ennegram-houses.csv"
        };

        foreach (var file in testFiles)
        {
            if (File.Exists(file))
            {
                File.Delete(file);
            }
        }

        // Clean up empty directories
        var testDirectories = new[]
        {
            "res/enneagram",
            "res"
        };

        foreach (var dir in testDirectories)
        {
            if (Directory.Exists(dir) && !Directory.EnumerateFileSystemEntries(dir).Any())
            {
                Directory.Delete(dir);
            }
        }
    }
} 