// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Ardalis.GuardClauses;

namespace Enigma.Frontend.InputSupport.Conversions;

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
        Guard.Against.Null(text);
        string[] strings = text.Split(separator);
        return CreateNumerics(strings);
    }

    private (int[] numbers, bool success) CreateNumerics(string[] strings)
    {
        bool success = true;
        int[] numbers = new int[strings.Length];
        for (int i = 0; i < strings.Length; i++)
        {
            bool isValid = int.TryParse(strings[i], out numbers[i]);
            if (!isValid) success = false;
        }
        return (numbers, success);
    }
}