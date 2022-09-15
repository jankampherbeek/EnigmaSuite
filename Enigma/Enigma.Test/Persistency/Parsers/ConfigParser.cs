// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Settings;
using System.Text.Json;

namespace Enigma.Persistency.Parsers;




public class AstroConfigPersister
{
    public string Marshall(AstroConfig astroConfig)
    {
        var options = new JsonSerializerOptions()
        {
            WriteIndented = true
        };
        string jsonString = JsonSerializer.Serialize(astroConfig, options);

        return jsonString;
    }

    public AstroConfig UnMarshall(string jsonString)
    {
        AstroConfig? astroConfig = JsonSerializer.Deserialize<AstroConfig>(jsonString);
        return astroConfig;
    }

}