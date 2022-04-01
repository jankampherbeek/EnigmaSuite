// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace E4C.domain.shared.reqresp;

/// <summary>
/// Parent for responses that have a validation part: an indication if the calculation was successfull and a text that explains any error(s).
/// </summary>
public abstract record ValidatedResponse
{
    public readonly bool Success;
    public readonly string ErrorText;

    /// <summary>
    /// Constructor for abstract ValidResponse, needs to be overridden.
    /// </summary>
    /// <param name="success">True if the calculation was successfull, otherwise false.</param>
    /// <param name="errorText">The text of any error(s).</param>
    public ValidatedResponse(bool success, string errorText)
    {
        Success = success;
        ErrorText = errorText;
    }
}