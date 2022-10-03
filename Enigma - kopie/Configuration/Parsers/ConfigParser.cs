// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Configuration;
using Newtonsoft.Json;
using System.Security.Principal;

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
        return JsonConvert.SerializeObject(astroConfig, Formatting.Indented);
    }

    public AstroConfig UnMarshall(string json)
    {
        return JsonConvert.DeserializeObject<AstroConfig>(json);
    }

}