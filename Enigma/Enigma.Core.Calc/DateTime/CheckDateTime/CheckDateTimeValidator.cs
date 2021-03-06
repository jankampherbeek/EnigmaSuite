// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.



using Enigma.Core.Calc.SeFacades;
using Enigma.Domain.DateTime;

namespace Enigma.Core.Calc.DateTime.CheckDateTime;

/// <summary>Calculations for Julian Day.</summary>
public interface ICheckDateTimeValidator
{
    public bool ValidateDateTime(SimpleDateTime dateTime);
}

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