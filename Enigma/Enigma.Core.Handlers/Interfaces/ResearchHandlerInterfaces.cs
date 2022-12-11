// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Domain.Persistency;
using Enigma.Domain.Research;

namespace Enigma.Core.Handlers.Interfaces;

public interface ITestMethodHandler
{
    public MethodResponse HandleTestMethod(TestMethodRequest request);
}

/// <summary>Handler for the creation of a research project.</summary>
public interface IProjectCreationHandler
{
    /// <summary>Handles the creation of a research project and the accompanying controlgroup.</summary>
    /// <param name="project">Definition of the project.</param>
    /// <param name="errorCode">Resulting errorcode.</param>
    /// <returns>True if no error occurred.</returns>
    public bool CreateProject(ResearchProject project, out int errorCode);
}

/// <summary>Handle retrieving overviews of projects.</summary>
public interface IProjectsOverviewHandler
{

    /// <summary>Read the details of all projects.</summary>
    /// <returns>Details for all projects.</returns>
    public List<ResearchProject> ReadAllProjectDetails();
}

/// <summary>Handle research data.</summary>
public interface IResearchDataHandler
{
    /// <summary>Convert Json text into StandardInput object.</summary>
    /// <param name="json">The Json text.</param>
    /// <returns>The resulting StandardInput object.</returns>
    public StandardInput GetStandardInputFromJson(string json);
}