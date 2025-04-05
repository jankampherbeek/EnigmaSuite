// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Domain.Exceptions;

using Serilog;


/// <summary>Exception for errors related to timezones</summary>
[Serializable]
public sealed class TimeZoneException : Exception
{
    public readonly string message;

    /// <summary>Empty constructor</summary>
    public TimeZoneException()
    {
        message = "";
    }


    /// <summary>Initialize the exception.</summary>
    /// <param name="text">The text with the error. This text is logged.</param>
    public TimeZoneException(string text) : base(text)
    {
        message = text;
        Log.Error("TimeZoneException was thrown with the message: {Text}", text);
    }
    
}