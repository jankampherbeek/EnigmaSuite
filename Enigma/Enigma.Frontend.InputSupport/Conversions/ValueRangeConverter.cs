// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Ardalis.GuardClauses;
using Enigma.Frontend.Helpers.Interfaces;

namespace Enigma.Frontend.Helpers.Conversions;



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