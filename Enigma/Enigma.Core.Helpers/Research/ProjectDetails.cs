// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Helpers.Interfaces;
using Enigma.Domain.Configuration;
using Enigma.Domain.Research;

namespace Enigma.Core.Helpers.Research;

/// <inheritdoc/>
public class ProjectDetails: IProjectDetails
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