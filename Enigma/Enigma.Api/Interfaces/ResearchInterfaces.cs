// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

// Interfaces for API's that support persistency.


using Enigma.Domain.RequestResponse;
using Enigma.Domain.Research;

namespace Enigma.Api.Interfaces;

/// <summary>Api for creation of a research project.</summary>
public interface IProjectCreationApi
{
    /// <summary>Create a research project and a controlgroup.</summary>
    /// <param name="project">Definition of the project to create.</param>
    /// <returns>Resultmessage with info about this action.</returns>
    public ResultMessage CreateProject(ResearchProject project);
}

/// <summary>Overview of available projects.</summary>
public interface IProjectsOverviewApi
{
    /// <summary>Get details for all available projects.</summary>
    /// <returns>List with projects.</returns>
    public List<ResearchProject> GetDetailsForAllProjects();
}

/// <summary>Api for handling tests.</summary>
public interface IResearchPerformApi
{
    /// <summary>Perform a test.</summary>
    /// <param name="request">Research request.</param>
    /// <returns>The results as a ResearchResponse.</returns>
    public ResearchResponse PerformTest(ResearchRequest request);
}