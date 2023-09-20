// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.Generic;
using Enigma.Frontend.Helpers.Interfaces;

namespace Enigma.Frontend.Ui.Support.Conversions;



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