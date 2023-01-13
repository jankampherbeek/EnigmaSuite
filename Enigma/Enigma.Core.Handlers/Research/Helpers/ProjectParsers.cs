// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Handlers.Research.Interfaces;
using Enigma.Domain.Research;
using Newtonsoft.Json;

namespace Enigma.Core.Handlers.Research.Helpers;



/// <inheritdoc/>
public sealed class ResearchProjectParser : IResearchProjectParser
{
    /// <inheritdoc/>
    public string Marshall(ResearchProject project)
    {
        return JsonConvert.SerializeObject(project, Formatting.Indented);
    }

    /// <inheritdoc/>
    public ResearchProject UnMarshall(string jsonString)
    {
        return JsonConvert.DeserializeObject<ResearchProject>(jsonString)!;
    }

}