// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Constants;
using Enigma.Domain.Dtos;
using Enigma.Domain.References;

namespace Enigma.Frontend.Ui.Support.Conversions;

/// <summary>Converter from text to date.</summary>
public interface ITextToDateConverter
{
    /// <summary>Perform conversion from text to date.</summary>
    /// <param name="dateText">Date in the format yyyy/mm/dd.</param>
    /// <param name="cal">The calendar: Gregorian or Julian.</param>
    /// <param name="simpleDate">Out parameter: contains the constructed instance of SimpleDate.</param>
    /// <returns>True if the conversion was successful.</returns>
    public bool ConvertText(string dateText, Calendars cal, out SimpleDate simpleDate);
}

// ================== Implementation ================================================

public class TextToDateConverter(IValueRangeConverter valueRangeConverter): ITextToDateConverter
{
    private IValueRangeConverter _valueRangeConverter = valueRangeConverter;
    
    public bool ConvertText(string dateText, Calendars cal, out SimpleDate? simpleDate)
    {
        (int[] dateNumbers, bool dateSuccess) = _valueRangeConverter.ConvertStringRangeToIntRange(dateText, EnigmaConstants.SEPARATOR_DATE);
        simpleDate = dateSuccess ? new SimpleDate(dateNumbers[0], dateNumbers[1], dateNumbers[2], cal) : null;
        return dateSuccess;
    }
}