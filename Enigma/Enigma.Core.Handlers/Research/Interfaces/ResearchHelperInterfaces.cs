// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Analysis.Aspects;
using Enigma.Domain.Configuration;
using Enigma.Domain.Persistency;
using Enigma.Domain.Points;
using Enigma.Domain.Research;

namespace Enigma.Core.Handlers.Research.Interfaces;

/// <summary>Calculates the positions for multiple charts to be used in research methods.</summary>
public interface ICalculatedResearchPositions
{
    /// <summary>Calculate the positions.</summary>
    /// <param name="standardInput">Contains the positions.</param>
    /// <returns>The calculated charts.</returns>
    public List<CalculatedResearchChart> CalculatePositions(StandardInput standardInput);
}


/// <summary>Counting for points in parts of the zodiac (e.g. signs, decanates etc.</summary>
public interface IPointsInPartsCounting
{
    /// <summary>Perform a count for points in parts of the zodiac or in the housesystem.</summary>
    /// <param name="charts">The calculatred charts to check.</param>
    /// <param name="request">The original request.</param>
    /// <returns>The calculated counts.</returns>
    public CountOfPartsResponse CountPointsInParts(List<CalculatedResearchChart> charts, GeneralResearchRequest request);
}

/// <summary>Counting for aspects.</summary>
public interface IAspectsCounting
{
    /// <summary>Perform a count of aspects.</summary>
    /// <param name="charts">The calculated charts to check.</param>
    /// <param name="request">The original request.</param>
    /// <returns>The calculated counts.</returns>
    public CountOfAspectsResponse CountAspects(List<CalculatedResearchChart> charts, GeneralResearchRequest request);
}


/// <summary>Counting for unaspected points.</summary>
public interface IUnaspectedCounting
{
    /// <summary>Perform a count of unaspected points.</summary>
    /// <param name="charts">The calculated charts to check.</param>
    /// <param name="request">The original request.</param>
    /// <returns>The calculated counts.</returns>
    public CountOfUnaspectedResponse CountUnaspected(List<CalculatedResearchChart> charts, GeneralResearchRequest request);
}

/// <summary>Counting for occupied midpoints.</summary>
public interface IOccupiedMidpointsCounting
{
    /// <summary>Perform a count for occupied midpoints.</summary>
    /// <param name="charts">The calculated charts to check.</param>
    /// <param name="request">The original request.</param>
    /// <returns>The calculated counts.</returns>
    public CountOfOccupiedMidpointsResponse CountMidpoints(List<CalculatedResearchChart> charts, CountOccupiedMidpointsRequest request);
}

public interface IHarmonicConjunctionsCounting
{
    /// <summary>Perform a count for harmonic conunctions.</summary>
    /// <param name="charts">The calculated charts to check.</param>
    /// <param name="request">The original request.</param>
    /// <returns>The calculated counts.</returns>
    public CountHarmonicConjunctionsResponse CountHarmonicConjunctions(List<CalculatedResearchChart> charts, CountHarmonicConjunctionsRequest request);
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

    // TODO 0.2 check if SHuffleList is required. Currently, it is not used.
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
    /// <param name="standardInput">The StandardInput to convert.</param>
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

/// <summary>Utilities for research methods.</summary>
public interface IResearchMethodUtils
{
    /// <summary>Create list of aspects as defined in configuration.</summary>
    /// <param name="config">The configuration to check.</param>
    /// <returns>The aspects that are found.</returns>
    public Dictionary<AspectTypes, AspectConfigSpecs> DefineConfigSelectedAspects(AstroConfig config);

    /// <summary>Create list of chart points as defined in configuration.</summary>
    /// <param name="config">The configuration to check.</param>
    /// <returns>The chart points that are found.</returns>
    public Dictionary<ChartPoints, ChartPointConfigSpecs> DefineConfigSelectedChartPoints(AstroConfig config);

    /// <summary>Create list of positioned points according to a given selection of chart points.</summary>
    /// <param name="calcResearchChart">Calculated chart.</param>
    /// <param name="pointsSelection">Selection of all points, including mundane points.</param>
    /// <returns>Positioned points that match wityh the selection.</returns>
    public Dictionary<ChartPoints, FullPointPos> DefineSelectedPointPositions(CalculatedResearchChart calcResearchChart, ResearchPointsSelection pointsSelection);

    /// <summary>Find the index for a chart point in a list with psotioned points.</summary>
    /// <param name="point">The chart point for which to find the index.</param>
    /// <param name="allPoints">Positioned points.</param>
    /// <returns>If found: the index. Otherwise: throws EnigmaException.</returns>
    public int FindIndexForPoint(ChartPoints point, List<PositionedPoint> allPoints);

    /// <summary>Find the index for an aspect type in list with aspect config specs.</summary>
    /// <param name="aspectType">The aspect type for which to find the index.</param>
    /// <param name="allAspects">Aspect config specs.</param>
    /// <returns>If found: the index.  Otherwise: throws EnigmaException.</returns>
    public int FindIndexForAspectType(AspectTypes aspectType, Dictionary<AspectTypes, AspectConfigSpecs> allAspects);
}
