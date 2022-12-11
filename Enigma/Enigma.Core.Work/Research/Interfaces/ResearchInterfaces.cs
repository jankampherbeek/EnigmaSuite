// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Persistency;
using Enigma.Domain.Research;

namespace Enigma.Core.Work.Research.Interfaces;



public interface IControlGroupCreator
{
    public List<StandardInputItem> CreateMultipleControlData(List<StandardInputItem> inputItems,
        ControlGroupTypes controlGroupType,
        int multiplicity);

}

public interface IControlDataCalendar
{
    public bool DayFitsInMonth(int day, int month, int year);
}



/// <summary>Create true random numbers to be used in the construction of a control group.</summary>
public interface IControlGroupRng
{
    /// <summary>Create a list of random integers.</summary>
    /// <param name="minInclusive">Minimum value of range (inclusive).</param>
    /// <param name="maxExclusive">Maximum value of range (exclusive).</param>
    /// <param name="count">Number of random values that should be returned.</param>
    /// <returns>The generated values.If the sequence of minInclusive/maxExclusive is wrong, or if count is invalid, an empty list is returned.</returns>
    List<int> GetIntegers(int minInclusive, int maxExclusive, int count);

    /// <summary>Create a list of random integers.</summary>
    /// <remarks>Uses 0 as the minimum inclusive value.</remarks>
    /// <param name="maxExclusive">Maximum value of range (exclusive).</param>
    /// <param name="count">Number of random values that should be returned.</param>
    /// <returns>The generated values. If not maxExclusive > 0, or if count is invalid, an empty list is returned.</returns>
    List<int> GetIntegers(int maxExclusive, int count);

    List<int> ShuffleList(List<int> data);
    List<double> ShuffleList(List<double> data);
}

/// <summary>Retrieve details for a project.</summary>
public interface IProjectDetails
{
    /// <summary>Read the details for a specific project.</summary>
    /// <param name="projectName">The project for which the details are required.</param>
    /// <returns>Instance of ResearchProject with the details.</returns>
    public ResearchProject FindProjectDetails(string projectName);

}

/// <summary>Converts a ResearchProject to Json and vice versa.</summary>
public interface IResearchProjectParser
{
    /// <summary>Create Json from a ResearchProject.</summary>
    /// <param name="project">The project to convert to Json.</param>
    /// <returns>The Json result.</returns>
    public string Marshall(ResearchProject project);

    /// <summary>Create a ResearchProject from Json.</summary>
    /// <param name="jsonString">The Json with the project data.</param>
    /// <returns>The rtesulting Research Project.</returns>
    public ResearchProject UnMarshall(string jsonString);
}



/// <summary>Converts a StandardInputItem to Json and vice versa.</summary>
public interface IInputDataConverter
{
    /// <summary>Create Json from a StandardInputItem.</summary>
    /// <param name="inputItem">The StandardInputItem to convert.</param>
    /// <returns>The Json result.</returns>
    public string MarshallInputItem(StandardInputItem inputItem);

    /// <summary>Create a StandardInputItem from Json.</summary>
    /// <param name="jsonString">The Json with the standard input item data.</param>
    /// <returns>The resulting StandardInputItem.</returns>
    public StandardInputItem UnMarshallInputItem(string jsonString);


    /// <summary>Create Json from StandardInput.</summary>
    /// <param name="inputItem">The StandardInput to convert.</param>
    /// <returns>The Json result.</returns>
    public string MarshallStandardInput(StandardInput standardInput);


    /// <summary>Create StandardInput from Json.</summary>
    /// <param name="jsonString">The Json with the standard input data.</param>
    /// <returns>The resulting StandardInput.</returns>
    public StandardInput UnMarshallStandardInput(string jsonString);
}

/// <summary>File paths for research.</summary>
public interface IResearchPaths
{
    /// <summary>Path to data files.</summary>
    /// <param name="projName">Name for project.</param>
    /// <param name="useControlGroup">True if data contains a controlgroup, false if data contains testcases.</param>
    /// <returns>String with full path to the required data, including the filename.</returns>
    public string DataPath(String projName, bool useControlGroup);

    /// <summary>Path to result files.</summary>
    /// <param name="projName">Name for project.</param>
    /// <param name="methodName">Name for method.</param>
    /// <param name="useControlGroup">True if result is based on a controlgroup, false if result is based on testcases.</param>
    /// <returns>String with full path for the results, including the filename.</returns>
    public string ResultPath(string projName, string methodName, bool useControlGroup);
}

