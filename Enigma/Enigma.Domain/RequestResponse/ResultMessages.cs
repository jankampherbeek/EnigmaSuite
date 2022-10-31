// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Domain.RequestResponse;

/// <summary>Result for an action that is returned to the object that started the action</summary>
public record ResultMessage
{

    /// <summary>Errorcode, a constant retrieved from the class Enigma.Domain.Constants.ErrorCodes. Zero (ERR_NONE) if no error occurred.</summary>
    public int ErrorCode { get; }
    /// <summary>Textual description of the result. Can contain error message but also other texts (if no error occurs).</summary>
    public string Message { get; }

    public ResultMessage(int errorCode, string message)
    {
        ErrorCode = errorCode;
        Message = message;
    }
}


