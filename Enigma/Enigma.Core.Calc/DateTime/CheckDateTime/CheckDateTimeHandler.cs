// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Core.Calc.Interfaces;
using Enigma.Core.Calc.ReqResp;
using Enigma.Domain.Exceptions;

namespace Enigma.Core.Calc.DateTime.CheckDateTime;


public class CheckDateTimeHandler : ICheckDateTimeHandler
{
    private readonly ICheckDateTimeValidator _checkDateTimeValidator;

    public CheckDateTimeHandler(ICheckDateTimeValidator checkDateTimeValidator) => _checkDateTimeValidator = checkDateTimeValidator;

    public CheckDateTimeResponse CheckDateTime(CheckDateTimeRequest request)
    {
        bool dateIsValid = true;
        string errorText = "";
        bool success = true;
        try
        {
            dateIsValid = _checkDateTimeValidator.ValidateDateTime(request.DateTime);
        }
        catch (SwissEphException see)
        {
            errorText = see.Message;
            success = false;
        }
        return new CheckDateTimeResponse(dateIsValid, success, errorText);
    }

}