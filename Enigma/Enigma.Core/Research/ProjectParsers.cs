// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Text.Json;
using Enigma.Domain.Research;

namespace Enigma.Core.Research;


/// <summary>Converts a ResearchProject to csv and vice versa.</summary>
public interface IResearchProjectParser
{
    /// <summary>Create csv from a ResearchProject.</summary>
    /// <param name="project">The project to convert to csv.</param>
    /// <returns>The csv result.</returns>
    public string Marshall(ResearchProject project);

    /// <summary>Create a ResearchProject from csv.</summary>
    /// <param name="csv">The csv with the project data.</param>
    /// <returns>The resulting Research Project.</returns>
    public ResearchProject UnMarshall(string csv);
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