// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Work.Research.Interfaces;
using Enigma.Domain.Research;
using Newtonsoft.Json;

namespace Enigma.Core.Work.Research;



/// <inheritdoc/>
public class ResearchProjectParser : IResearchProjectParser
{
    /// <inheritdoc/>
    public string Marshall(ResearchProject project)
    {
        return JsonConvert.SerializeObject(project, Formatting.Indented);
    }

    /// <inheritdoc/>
    public ResearchProject UnMarshall(string jsonString)
    {
        return JsonConvert.DeserializeObject<ResearchProject>(jsonString);
    }

}