// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Core.Handlers.Interfaces;
using Enigma.Core.Helpers.Interfaces;
using Enigma.Core.Helpers.Research;
using Enigma.Domain.Configuration;
using Enigma.Domain.Research;

namespace Enigma.Core.Handlers.Research;

/// <inheritdoc/>
public class ProjectsOverviewHandler: IProjectsOverviewHandler
{
    private readonly IProjectDetails _projectDetails;
    private readonly IFoldersInfo _foldersInfo;
    public ProjectsOverviewHandler(IProjectDetails projectDetails, IFoldersInfo foldersInfo)
    {
        _projectDetails = projectDetails;
        _foldersInfo = foldersInfo;
    }

    /// <inheritdoc/>
    public List<ResearchProject> ReadAllProjectDetails()
    {
        List<ResearchProject> projects = new();
        string path = ApplicationSettings.Instance.LocationProjectFiles;
        List<string> projectNames = _foldersInfo.GetExistingFolderNames(path, false);
        foreach (string proj in projectNames)
        {
            projects.Add(_projectDetails.FindProjectDetails(proj));
        }
        return projects;
    }
}