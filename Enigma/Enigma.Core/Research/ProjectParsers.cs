// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Text.Json;
using Enigma.Domain.Research;

namespace Enigma.Core.Research;


/// <summary>Converts a ResearchProject to Json and vice versa.</summary>
public interface IResearchProjectParser
{
    /// <summary>Create Json from a ResearchProject.</summary>
    /// <param name="project">The project to convert to Json.</param>
    /// <returns>The Json result.</returns>
    public string Marshall(ResearchProject project);

    /// <summary>Create a ResearchProject from Json.</summary>
    /// <param name="jsonString">The Json with the project data.</param>
    /// <returns>The rtesulting Research Project.</returns>
    public ResearchProject UnMarshall(string jsonString);
}

/// <inheritdoc/>
public sealed class ResearchProjectParser : IResearchProjectParser
{
    /// <inheritdoc/>
    public string Marshall(ResearchProject project)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        return JsonSerializer.Serialize(project, options);
    }

    /// <inheritdoc/>
    public ResearchProject UnMarshall(string jsonString)
    {
        return JsonSerializer.Deserialize<ResearchProject>(jsonString)!;
    }

}