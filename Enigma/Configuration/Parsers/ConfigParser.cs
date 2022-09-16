// Jan Kampherbeek, (c) 2022.
// Enigma Research is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Configuration.Domain;
using System.Text.Json;

namespace Enigma.Persistency.Parsers;


public interface IAstroConfigParser
{
    public string Marshall(AstroConfig astroConfig);
    public AstroConfig UnMarshall(string jsonString);
}

public class AstroConfigParser: IAstroConfigParser
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