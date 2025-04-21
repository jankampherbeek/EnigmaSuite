// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Persistency;
using Enigma.Domain.Dtos;
using Enigma.Domain.Research;
using Enigma.Domain.References;
using Serilog;

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
    private readonly IProjectDao _projectDao;

    public ProjectsOverviewHandler(IProjectDao projectDao)
    {
        _projectDao = projectDao;
    }

    /// <inheritdoc/>
    public List<ResearchProject> ReadAllProjectDetails()
    {
        try
        {
            var projects = _projectDao.GetAllProjectsWithDataFiles();
            var researchProjects = new List<ResearchProject>();

            foreach (var project in projects)
            {
                researchProjects.Add(new ResearchProject(
                    project.Name,
                    project.Description,
                    project.DataFileName,
                    project.Created,
                    ControlGroupTypes.StandardShift, // Default value, as this is not stored in the database
                    project.MultiFactor));
            }

            Log.Information("Converted {Count} projects to ResearchProject objects", researchProjects.Count);
            return researchProjects;
        }
        catch (Exception e)
        {
            Log.Error("Error converting projects to ResearchProject objects: {Message}", e.Message);
            return new List<ResearchProject>();
        }
    }
}