// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.



using Enigma.Core.Calc.Interfaces;
using Enigma.Domain.AstronCalculations;

namespace Enigma.Core.Calc.DateTime.CheckDateTime;


/// <inheritdoc/>
public class CheckDateTimeValidator : ICheckDateTimeValidator
{
    private readonly IDateConversionFacade _dateConversionFacade;

    public CheckDateTimeValidator(IDateConversionFacade dateConversionFacade) => _dateConversionFacade = dateConversionFacade;

    public bool ValidateDateTime(SimpleDateTime dateTime)
    {
        return _dateConversionFacade.DateTimeIsValid(dateTime);
    }
}