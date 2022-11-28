// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Work.Configuration.Interfaces;
using Enigma.Domain.Configuration;
using Newtonsoft.Json;

namespace Enigma.Core.Work.Configuration;


/// <inheritdoc/>
public class AstroConfigParser : IAstroConfigParser
{
    /// <inheritdoc/>
    public string Marshall(AstroConfig astroConfig)
    {
        return JsonConvert.SerializeObject(astroConfig, Formatting.Indented);
    }

    /// <inheritdoc/>
    public AstroConfig UnMarshall(string jsonString)
    {
        return JsonConvert.DeserializeObject<AstroConfig>(jsonString);
    }

}