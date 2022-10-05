// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Persistency.Handlers;

public interface IDataNameHandler
{
    List<string> GetExistingDataNames(string path);
}

public class DataNameHandler : IDataNameHandler
{
    public List<string> GetExistingDataNames(string path)
    {
        string[] dirs = Directory.GetDirectories(path, "*", SearchOption.TopDirectoryOnly);
        return dirs.ToList();
    }
}