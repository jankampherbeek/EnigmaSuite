// Jan Kampherbeek, (c) 2022.
// Enigma Research is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Domain.Exceptions;

/// <summary>
/// Exception that handles errors when starting Enigma.
/// </summary>
[Serializable]
public class StartupException : Exception
{
    /// <summary>Empty constructor.</summary>
    public StartupException()
    { }


    public StartupException(string text) : base(text)
    { }


}