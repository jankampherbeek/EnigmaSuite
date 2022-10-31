// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Engima.Domain.Research;
using Enigma.Domain.AstronCalculations;
using Enigma.Domain.Enums;
using Enigma.Domain.Persistency;
using Enigma.Domain.Research;


namespace Enigma.Research.Interfaces;



public interface IResearchResult
{
    public bool NoErrors { get; }
    public string Comments { get; }
}

public interface IMethodProcessor
{
    public IResearchResult ProcessData(string dataName);
}


public interface IMethodDefinitions
{
    public Ayanamshas Ayanamsha { get; }
    public ObserverPositions ObserverPosition { get; }
    public HouseSystems HouseSystem { get; }
    public ProjectionTypes ProjectionType { get; }
}


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


public interface IProjectCreationHandler
{
    public bool CreateProject(ResearchProject project, out int errorCode);
}

public interface IResearchProjectParser
{
    public string Marshall(ResearchProject project);
    public ResearchProject UnMarshall(string jsonString);
}




