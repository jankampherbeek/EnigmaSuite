// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Dtos;
using Enigma.Facades.Se;

namespace Enigma.Core.Calc;

/// <summary>Calculations for Julian Day.</summary>
public interface IDateTimeValidator
{
    public bool ValidateDateTime(SimpleDateTime dateTime);
}


/// <inheritdoc/>
public sealed class DateTimeValidator : IDateTimeValidator
{
    private readonly IDateConversionFacade _dateConversionFacade;

    public DateTimeValidator(IDateConversionFacade dateConversionFacade) => _dateConversionFacade = dateConversionFacade;

    public bool ValidateDateTime(SimpleDateTime dateTime)
    {
        return _dateConversionFacade.DateTimeIsValid(dateTime);
    }
}