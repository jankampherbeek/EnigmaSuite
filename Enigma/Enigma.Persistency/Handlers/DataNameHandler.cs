// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Persistency.Interfaces;

namespace Enigma.Persistency.Handlers;

public class DataNameHandler : IDataNameHandler
{
    public List<string> GetExistingDataNames(string path)
    {
        string[] dirs = Directory.GetDirectories(path, "*", SearchOption.TopDirectoryOnly);
        return dirs.ToList();
    }
}