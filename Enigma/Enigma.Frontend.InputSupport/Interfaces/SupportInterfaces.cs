// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Frontend.Helpers.Interfaces;

public interface IRangeCheck
{
    public double InRange360(double angle);
}


/// <summary>Manage texts that are stored in an external dictionary file.</summary>
/*public interface IRosetta
{
    /// <summary>Retrieve text from a resource bundle.</summary>
    /// <param name="id">The id to search.</param>
    /// <returns>The text for the Id. Returns the string '-NOT FOUND-' if the text could not be found.</returns>
    public string TextForId(string id);
}
*/

public interface ITextFileReaderFE   // todo, check if this has the same functionality as textfilereader in persistency
{
    public IEnumerable<string> ReadSeparatedLines(string fileName);
    public string ReadAllText(string fileName);
}