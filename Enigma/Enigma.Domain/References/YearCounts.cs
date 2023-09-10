// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Serilog;

namespace Enigma.Domain.References;

/// <summary>Enum for Yearcounts, the way years are defined.</summary>
public enum YearCounts
{
    CE = 0, BCE = 1, Astronomical = 2
}

/// <summary>Details for YearCounts</summary>
/// <param name="YearCount">The YearCount</param>
/// <param name="Text">Descriptive text</param>
public record YearCountDetails(YearCounts YearCount, string Text);

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
            YearCounts.CE => new YearCountDetails(yearCount, "CE"),
            YearCounts.BCE => new YearCountDetails(yearCount, "BCE"),
            YearCounts.Astronomical => new YearCountDetails(yearCount, "Astronomical"),
            _ => throw new ArgumentException("YearCount unknown : " + yearCount)
        };
    }


    /// <summary>Retrieve details for items in the enum YearCounts.</summary>
    /// <returns>All details.</returns>
    public static List<YearCountDetails> AllDetails()
    {
        return (from YearCounts currentYc in Enum.GetValues(typeof(YearCounts)) select currentYc.GetDetails()).ToList();
    }


    /// <summary>Find year count for an index.</summary>
    /// <param name="index">Index to look for.</param>
    /// <returns>The year count for the index.</returns>
    /// <exception cref="ArgumentException">Is thrown if a non existing index is given.</exception>
    public static YearCounts YearCountForIndex(int index)
    {
        foreach (YearCounts currentYc in Enum.GetValues(typeof(YearCounts)))
        {
            if ((int)currentYc == index) return currentYc;
        }
        Log.Error("YearCounts.YearCountForIndex(): Could not find YearCount for index : {Index}", index);
        throw new ArgumentException("Wrong YearCount");
    }

}




