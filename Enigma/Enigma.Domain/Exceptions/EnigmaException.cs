// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Serilog;

namespace Enigma.Domain.Exceptions;

/// <summary>
/// Exception that is thrown if the program cannot continue. 
/// </summary>
[Serializable]
public sealed class EnigmaException : Exception
{
    public readonly string message;

    /// <summary>
    /// Empty constructor.
    /// </summary>
    public EnigmaException()
    {
        message = string.Empty;
    }


    /// <summary>Initialize the exception.</summary>
    /// <param name="text">The text with the error. This text is logged.</param>
    public EnigmaException(string text) : base(text)
    {
        message = text;
        string logText = "EnigmaException was thrown with the message: " + text;
        Log.Error(logText);
    }


}