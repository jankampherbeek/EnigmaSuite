// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Serilog;

namespace Enigma.Domain.Exceptions;

/// <summary>Exception related to writing files or writing to a database</summary>
[Serializable]
public sealed class PersistencyException: Exception
{
    public readonly string Msg;
    /// <summary>
    /// Empty constructor.
    /// </summary>
    public PersistencyException()
    {
        Msg = string.Empty;
    }


    /// <summary>Initialize the exception.</summary>
    /// <param name="text">The text with the error. This text is logged.</param>
    public PersistencyException(string text) : base(text)
    {
        Msg = text;
        Log.Error("PersistencyException was thrown with the message: {Text}", text);
    }

}
