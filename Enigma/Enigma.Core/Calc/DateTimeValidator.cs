// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Core.Interfaces;
using Enigma.Domain.Dtos;
using Enigma.Facades.Interfaces;

namespace Enigma.Core.Calc;


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