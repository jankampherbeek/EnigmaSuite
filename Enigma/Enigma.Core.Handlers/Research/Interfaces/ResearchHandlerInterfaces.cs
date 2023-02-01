// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Domain.Persistency;
using Enigma.Domain.Research;

namespace Enigma.Core.Handlers.Research.Interfaces;

/// <summary>Handlers for performing research methods.</summary>
public interface IResearchMethodHandler
{
    /// <summary>Start running a test.</summary>
    /// <param name="request">Instance of GeneralResearchRequest or one of its children.</param>
    /// <returns>Results of the test as instance of MethodResponse or one of its children.</returns>
    public MethodResponse HandleResearch(GeneralResearchRequest request);

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

/// <summary>Handler for paths for files that are used for research.</summary>
public interface IResearchPathHandler
{
    /// <summary>Path to data files.</summary>
    /// <param name="projName">Name for project.</param>
    /// <param name="useControlGroup">True if data contains a controlgroup, false if data contains testcases.</param>
    /// <returns>String with full path to the required data, including the filename.</returns>
    public string DataPath(string projName, bool useControlGroup);

    /// <summary>Path to result files with positions.</summary>
    /// <param name="projName">Name for project.</param>
    /// <param name="methodName">Name for method.</param>
    /// <param name="useControlGroup">True if result is based on a controlgroup, false if result is based on testcases.</param>
    /// <returns>String with full path for the results, including the filename.</returns>
    public string ResultPath(string projName, string methodName, bool useControlGroup);

    /// <summary>Path to result files with countings.</summary>
    /// <param name="projName">Name for project.</param>
    /// <param name="methodName">Name for method.</param>
    /// <param name="useControlGroup">True if result is based on a controlgroup, false if result is based on testcases.</param>
    /// <returns>String with full path for the results, including the filename.</returns>
    public string CountResultsPath(string projName, string methodName, bool useControlGroup);

    /// <summary>Path to result files with the sums of all countings.</summary>
    /// <param name="projName">Name for project.</param>
    /// <param name="methodName">Name for method.</param>
    /// <param name="useControlGroup">True if result is based on a controlgroup, false if result is based on testcases.</param>
    /// <returns>String with full path for the summed totals, including the filename.</returns>
    public string SummedResultsPath(string projName, string methodName, bool useControlGroup);

}