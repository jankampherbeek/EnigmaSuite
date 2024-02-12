// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.Generic;

namespace Enigma.Frontend.Ui.Support.Conversions;

/// <summary>
/// Convert array with range of values from one type to another type.
/// </summary>
public interface IValueRangeConverter
{
    /// <summary>
    /// Convert a string with a separator character to a range of integers.
    /// </summary>
    /// <param name="text">The string containing the substrings with the values, separated with the separator char.
    /// An empty or null string is considered an error.</param>
    /// <param name="separator">The character that separates the parts in the string 'text'.</param>
    /// <returns>A tuple with the converted numbers and an indication if the conversions was successful. 
    /// If the conversion was not successful the value of the numbers can be undefined.</returns>
    public (int[] numbers, bool success) ConvertStringRangeToIntRange(string text, char separator);
}

/// <inheritdoc/>
public class ValueRangeConverter : IValueRangeConverter
{
    /// <inheritdoc/>
    public (int[] numbers, bool success) ConvertStringRangeToIntRange(string text, char separator)
    {
        string[] strings = text.Split(separator);
        return CreateNumerics(strings);
    }

    private static (int[] numbers, bool success) CreateNumerics(IReadOnlyList<string> strings)
    {
        bool success = true;
        int[] numbers = new int[strings.Count];
        for (int i = 0; i < strings.Count; i++)
        {
            bool isValid = int.TryParse(strings[i], out numbers[i]);
            if (!isValid) success = false;
        }
        return (numbers, success);
    }
}