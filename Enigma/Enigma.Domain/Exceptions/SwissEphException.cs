// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Domain.Exceptions;

/// <summary>
/// Exception that handles any error from accessing the dll from the Swiss Ephemeris.
/// </summary>
[Serializable]
public class SwissEphException : Exception
{
    string message;

    /// <summary>
    /// Empty constructor.
    /// </summary>
    public SwissEphException()
    { }


    public SwissEphException(string text) : base(text)
    {
        message = text;
    }


}
