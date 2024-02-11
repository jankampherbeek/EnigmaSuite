// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Persistency;
using Enigma.Core.Research;
using Enigma.Domain.Dtos;
using Enigma.Domain.Research;

namespace Enigma.Core.Handlers;


/// <summary>Handle retrieving overviews of projects.</summary>
public interface IProjectsOverviewHandler
{

    /// <summary>Read the details of all projects.</summary>
    /// <returns>Details for all projects.</returns>
    public List<ResearchProject> ReadAllProjectDetails();
}

/// <inheritdoc/>
public sealed class ProjectsOverviewHandler : IProjectsOverviewHandler
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
        string path = ApplicationSettings.Instance.LocationProjectFiles;
        List<string> projectNames = _foldersInfo.GetExistingFolderNames(path, false);
        return (from proj in projectNames let startPos = proj.LastIndexOf(Path.DirectorySeparatorChar) + 1 
            select proj[startPos..] into projText select _projectDetails.FindProjectDetails(projText)).ToList();
    }
}