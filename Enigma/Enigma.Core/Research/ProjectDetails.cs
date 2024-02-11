// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Core.Persistency;
using Enigma.Domain.Dtos;
using Enigma.Domain.Research;

namespace Enigma.Core.Research;


/// <summary>Retrieve details for a project.</summary>
public interface IProjectDetails
{
    /// <summary>Read the details for a specific project.</summary>
    /// <param name="projectName">The project for which the details are required.</param>
    /// <returns>Instance of ResearchProject with the details.</returns>
    public ResearchProject FindProjectDetails(string projectName);

}

/// <inheritdoc/>
public sealed class ProjectDetails : IProjectDetails
{

    private readonly ITextFileReader _textFileReader;
    private readonly IResearchProjectParser _parser;

    public ProjectDetails(ITextFileReader textFileReader, IResearchProjectParser parser)
    {
        _textFileReader = textFileReader;
        _parser = parser;
    }

    /// <inheritdoc/>
    public ResearchProject FindProjectDetails(string projectName)
    {
        string projectPath = ApplicationSettings.Instance.LocationProjectFiles + Path.DirectorySeparatorChar + projectName
            + Path.DirectorySeparatorChar + "project.json";
        string jsonText = _textFileReader.ReadFile(projectPath);
        return _parser.UnMarshall(jsonText);
    }

}