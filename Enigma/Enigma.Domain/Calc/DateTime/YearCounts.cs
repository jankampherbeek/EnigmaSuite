// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Serilog;

namespace Enigma.Domain.Calc.DateTime;

/// <summary>Enum for Yearcounts, the way years are defined.</summary>
public enum YearCounts
{
    CE = 0, BCE = 1, Astronomical = 2
}

/// <summary>Details for YearCounts.</summary>
/// <param name="YearCount">The YearCount.</param>
/// <param name="TextId">Id to find a descriptive text in a resource bundle.</param>
public record YearCountDetails(YearCounts YearCount, string TextId);

/// <summary>Extension class for enum YearCounts.</summary>
public static class YearCountsExtensions
{
    /// <summary>Retrieve details for year counts.</summary>
    /// <param name="yearCount">The year count, is automatically filled.</param>
    /// <returns>Details for the year count.</returns>
    public static YearCountDetails GetDetails(this YearCounts yearCount)
    {
        return yearCount switch
        {
            YearCounts.CE => new YearCountDetails(yearCount, "ref.enum.yearcount.ce"),
            YearCounts.BCE => new YearCountDetails(yearCount, "ref.enum.yearcount.bce"),
            YearCounts.Astronomical => new YearCountDetails(yearCount, "ref.enum.yearcount.astronomical"),
            _ => throw new ArgumentException("YearCount unknown : " + yearCount.ToString())
        };
    }


    /// <summary>Retrieve details for items in the enum YearCounts.</summary>
    /// <returns>All details.</returns>
    public static List<YearCountDetails> AllDetails(this YearCounts _)
    {
        var allDetails = new List<YearCountDetails>();
        foreach (YearCounts currentYc in Enum.GetValues(typeof(YearCounts)))
        {
            allDetails.Add(currentYc.GetDetails());
        }
        return allDetails;
    }


    /// <summary>Find year count for an index.</summary>
    /// <param name="index">Index to look for.</param>
    /// <returns>The year count for the index.</returns>
    /// <exception cref="ArgumentException">Is thrown if a non existing index is given.</exception>
    public static YearCounts YearCountForIndex(this YearCounts _, int index)
    {
        foreach (YearCounts currentYc in Enum.GetValues(typeof(YearCounts)))
        {
            if ((int)currentYc == index) return currentYc;
        }
        string errorText = "YearCounts.YearCountForIndex(): Could not find YearCount for index : " + index;
        Log.Error(errorText);
        throw new ArgumentException(errorText);
    }

}




