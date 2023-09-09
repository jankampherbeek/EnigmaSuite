// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Core.Interfaces;
using Enigma.Core.Research.Interfaces;
using Enigma.Domain.Configuration;
using Enigma.Domain.Research;

namespace Enigma.Core.Research.Helpers;

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