﻿// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Research;
using Enigma.Research.Interfaces;
using Newtonsoft.Json;
using Formatting = Newtonsoft.Json.Formatting;

namespace Enigma.Research.Parsers;




public class ResearchProjectParser : IResearchProjectParser
{
    public string Marshall(ResearchProject project)
    {
        return JsonConvert.SerializeObject(project, Formatting.Indented);
    }

    public ResearchProject UnMarshall(string jsonString)
    {
        return JsonConvert.DeserializeObject<ResearchProject>(jsonString);
    }

}