// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Persistency;
using Enigma.Domain.Research;

namespace Enigma.Core.Helpers.Research;

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

