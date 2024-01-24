// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Api.Interfaces;





/// <summary>API for simple read and write access to files.</summary>
public interface IFileAccessApi
{
    /// <summary>Write a file to disk.</summary>
    /// <param name="path">Full path for the file.</param>
    /// <param name="text">Content of the file.</param>
    /// <returns>True if no error occured, otherwise false.</returns>
    public bool WriteFile(string path, string text);

    /// <summary>Read a file from disk.</summary>
    /// <param name="path">Full path for the file.</param>
    /// <returns>Content of the file.</returns>
    public string ReadFile(string path);
}







