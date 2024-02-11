// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Serilog;

namespace Enigma.Core.Persistency;


/// <summary>Copy a file in the file system.</summary>
public interface IFileCopier
{
    /// <summary>Performs a copy.</summary>
    /// <param name="source">Full path for the original file.</param>
    /// <param name="destination">Full path for the new location of the file.</param>
    /// <returns>True if the copy was successful, otherwise false.</returns>
    public bool CopyFile(string source, string destination);
}

/// <inheritdoc/>
public sealed class FileCopier : IFileCopier
{
    /// <inheritdoc/>
    public bool CopyFile(string source, string destination)
    {
        bool success = true;
        try
        {
            File.Copy(source, destination);
        }
        catch (Exception e)
        {
            Log.Error("An exception was thrown in FileCopier when copying {Source} to {Destination}. The exception: {E}", source, destination, e.Message);
            success = false;
        }
        return success;
    }
}