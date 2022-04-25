// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System;

namespace E4C.Ui.Shared;

public enum YearCounts
{
    CE = 0, BCE = 1, Astronomical = 2
}

public record YearCountDetails
{
    readonly public YearCounts YearCount;
    readonly public string TextId;

    /// <param name="yearCount">The YearCount.</param>
    /// <param name="textId">Id to find a descriptive text in a resource bundle.</param>
    public YearCountDetails(YearCounts yearCount, string textId)
    {
        YearCount = yearCount;
        TextId = textId;
    }
}


public interface IYearCountSpecifications
{
    /// <summary>Returns the details for a YearCount.</summary>
    /// <param name="yearCount">The YearCount, from the enum YearCounts.</param>
    /// <returns>A record YearCountDetails with the specifications.</returns>
    public YearCountDetails DetailsForYearCount(YearCounts yearCount);

    /// <summary>Returns a value from the enum YearCounts that corresponds with an index.</summary>
    /// <param name="yearCountIndex">The index for the requested item from YearCounts. 
    /// Throws an exception if no YearCount for the given index does exist.</param>
    /// <returns>Instance from enum YearCounts that corresponds with the given index.</returns>
    public YearCounts YearCountForIndex(int yearCountIndex);
}


public class YearCountSpecifications : IYearCountSpecifications
{
    /// <inheritdoc/>
    /// <exception cref="ArgumentException">Is thrown if the calendar was not recognized.</exception>
    YearCountDetails IYearCountSpecifications.DetailsForYearCount(YearCounts yearCount)
    {
        return yearCount switch
        {
            YearCounts.CE => new YearCountDetails(yearCount, "ref.enum.yearcount.ce"),
            YearCounts.BCE => new YearCountDetails(yearCount, "ref.enum.yearcount.bce"),
            YearCounts.Astronomical => new YearCountDetails(yearCount, "ref.enum.yearcount.astronomical"),
            _ => throw new ArgumentException("YearCount unknown : " + yearCount.ToString())
        };
    }

    /// <inheritdoc/>
    public YearCounts YearCountForIndex(int yearCountIndex)
    {
        foreach (YearCounts yearCount in Enum.GetValues(typeof(YearCounts)))
        {
            if ((int)yearCount == yearCountIndex) return yearCount;
        }
        throw new ArgumentException("Could not find YearCount for index : " + yearCountIndex);
    }
}
