// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.ReqResp;

namespace Enigma.Domain.RequestResponse;

public record CheckDateTimeResponse : ValidatedResponse
{
    public bool Validated { get; }

    /// <summary>Check the validity of a date.</summary>
    /// <param name="validated">True if the data was valid, otherwise false.</param>
    /// <param name="success">True if the validation could be performed, regardless of the result of the validation. Otherwise false.</param>
    /// <param name="errorText">Description of any error if 'success' is false.</param>
    public CheckDateTimeResponse(bool validated, bool success, string errorText) : base(success, errorText)
    {
        Validated = validated;
    }

}