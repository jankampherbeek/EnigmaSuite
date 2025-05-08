// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Globalization;
using Enigma.Core.Persistency;
using Enigma.Domain.Research;
using Serilog;

namespace Enigma.Core.Research;

/// <summary>Handle retrieving overviews of projects.</summary>
public interface IProjectsOverviewHandler
{
    /// <summary>Read the details of all projects.</summary>
    /// <returns>Details for all projects.</returns>
    public List<ResearchProject> ReadAllProjectDetails();
}

/// <inheritdoc/>
public sealed class ProjectsOverviewHandler(IProjectDao projectDao) : IProjectsOverviewHandler
{
    /// <inheritdoc/>
    public List<ResearchProject> ReadAllProjectDetails()
    {
        try
        {
            var projects = projectDao.GetAllProjectsWithDataFiles();
            var researchProjects = new List<ResearchProject>();

            foreach (var project in projects)
            {
                researchProjects.Add(new ResearchProject(
                    project.Id,
                    project.Name,
                    project.Description,
                    project.DataFile,
                    project.Created.ToString(CultureInfo.InvariantCulture),
                    project.ControlGroupType,
                    project.MultiFactor));
            }

            Log.Information("Converted {Count} projects to ResearchProject objects", researchProjects.Count);
            return researchProjects;
        }
        catch (Exception e)
        {
            Log.Error("Error converting projects to ResearchProject objects: {Message}", e.Message);
            return [];
        }
    }
}