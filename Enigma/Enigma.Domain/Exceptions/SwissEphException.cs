// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Domain.Exceptions;

/// <summary>
/// Exception that handles any error from accessing the dll from the Swiss Ephemeris.
/// </summary>
[Serializable]
public class SwissEphException : Exception
{
    public readonly string message;

    public SwissEphException()
    {
        message = string.Empty;
    }

    public SwissEphException(string text) : base(text)
    {
        message = text;
    }


}
