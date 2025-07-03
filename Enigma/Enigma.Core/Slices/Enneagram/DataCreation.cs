// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Globalization;

namespace Enigma.Core.Slices.Enneagram;

/// <summary>
/// Create data for Enneagram calculations.
/// </summary>
public class DataCreation
{

    /// <summary>
    /// Reads Enneagram factors for signs
    /// </summary>
    /// <remarks>
    /// Prompt: read data from the file enneagram-signs.csv in the res/enneagram folder in the Enigma.Frontend.UI project
    /// Ignore commented lines that start with a '#'. The values are comma-separated.
    /// Each line starts with an int for a chartpoint, an int for a sign, and is followed by 9 doubles.
    /// The values should be added to a list that contains EnneagramData and the list should be returned.
    /// If one of the lines does not contain 2 ints and 9 doubles, an exception should be thrown. 
    /// </remarks>
    /// <returns>Data for signs</returns>
    public List<EnneagramData> ReadDataForSigns(IEnumerable<string>? searchPaths = null)
    {
        return ReadEnneagramData("enneagram-signs.csv", searchPaths);
    }


    /// <summary>
    /// Reads Enneagram factors for houses
    /// </summary>
    /// <remarks>
    /// Prompt: read data from the file enneagram-houses.csv in the res/enneagram folder in the Enigma.Frontend.UI project
    /// Ignore commented lines that start with a '#'. The values are comma-separated.
    /// Each line starts with an int for a chartpoint, an int for a sign, and is followed by 9 doubles.
    /// The values should be added to a list that contains EnneagramData and the list should be returned.
    /// If one of the lines does not contain 2 ints and 9 doubles, an exception should be thrown. 
    /// </remarks>
    /// <returns>Data for houses</returns>
    public List<EnneagramData> ReadDataForHouses(IEnumerable<string>? searchPaths = null)
    {
        return ReadEnneagramData("enneagram-houses.csv", searchPaths);
    }

    /// <summary>
    /// Reads Enneagram data from a CSV file
    /// </summary>
    /// <param name="fileName">The name of the CSV file to read</param>
    /// <param name="searchPaths">Optional: override the default search paths</param>
    /// <returns>List of EnneagramData objects</returns>
    private List<EnneagramData> ReadEnneagramData(string fileName, IEnumerable<string>? searchPaths = null)
    {
        var data = new List<EnneagramData>();
        
        // Try multiple possible paths for the data files
        var possiblePaths = searchPaths?.ToArray() ?? new[]
        {
            Path.Combine("res", "enneagram", fileName), // Current directory (test files) - highest priority
            Path.Combine("Enigma.Frontend", "res", "enneagram", fileName),
            Path.Combine("..", "Enigma.Frontend", "res", "enneagram", fileName),
            Path.Combine("..", "..", "Enigma.Frontend", "res", "enneagram", fileName),
            Path.Combine("..", "..", "..", "Enigma.Frontend", "res", "enneagram", fileName),
            // Test output directory paths
            Path.Combine("..", "..", "..", "..", "Enigma.Frontend", "res", "enneagram", fileName),
            Path.Combine("..", "..", "..", "..", "..", "Enigma.Frontend", "res", "enneagram", fileName)
        };
        
        string? filePath = null;
        foreach (var path in possiblePaths)
        {
            if (File.Exists(path))
            {
                filePath = path;
                break;
            }
        }
        
        if (filePath == null)
        {
            throw new FileNotFoundException($"Enneagram data file not found: {fileName}. Searched in: {string.Join(", ", possiblePaths)}");
        }
        
        var lines = File.ReadAllLines(filePath);
        
        foreach (var line in lines)
        {
            var trimmedLine = line.Trim();
            
            // Skip empty lines and commented lines
            if (string.IsNullOrEmpty(trimmedLine) || trimmedLine.StartsWith("#"))
            {
                continue;
            }
            
            var values = trimmedLine.Split(',');
            
            // Check if we have exactly 11 values (2 ints + 9 doubles)
            if (values.Length != 11)
            {
                throw new FormatException($"Invalid line format. Expected 11 comma-separated values, but found {values.Length}: {trimmedLine}");
            }
            
            // Parse the first two values as integers
            if (!int.TryParse(values[0], NumberStyles.Integer, CultureInfo.InvariantCulture, out int point))
            {
                throw new FormatException($"Invalid chartpoint value: {values[0]}");
            }
            
            if (!int.TryParse(values[1], NumberStyles.Integer, CultureInfo.InvariantCulture, out int index))
            {
                throw new FormatException($"Invalid index value: {values[1]}");
            }
            
            // Parse the remaining 9 values as doubles
            var factors = new double[9];
            for (var i = 0; i < 9; i++)
            {
                if (!double.TryParse(values[i + 2], NumberStyles.Float, CultureInfo.InvariantCulture, out factors[i]))
                {
                    throw new FormatException($"Invalid factor value at position {i + 2}: {values[i + 2]}");
                }
            }
            
            data.Add(new EnneagramData(point, index, factors));
        }
        
        return data;
    }
    
    
}