// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Helpers.Interfaces;
using Serilog;

namespace Enigma.Core.Helpers.Persistency;


/// <inheritdoc/>
public class FileCopier : IFileCopier
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
            Log.Error("An exception was thrown in FileCopier when copying {source} to {destination}. The exception: {e}", source, destination, e.Message);
            success = false;
		}
        return success;
    }
} 