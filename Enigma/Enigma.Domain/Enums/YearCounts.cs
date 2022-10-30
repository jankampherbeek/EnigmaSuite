// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Interfaces;

namespace Enigma.Domain.Enums;

/// <summary>Enum for Yearcounts, the way years are defined.</summary>
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



public class YearCountSpecifications : IYearCountSpecifications
{
    /// <inheritdoc/>
    /// <exception cref="ArgumentException">Is thrown if the calendar was not recognized.</exception>
    public YearCountDetails DetailsForYearCount(YearCounts yearCount)
    {
        return yearCount switch
        {
            YearCounts.CE => new YearCountDetails(yearCount, "ref.enum.yearcount.ce"),
            YearCounts.BCE => new YearCountDetails(yearCount, "ref.enum.yearcount.bce"),
            YearCounts.Astronomical => new YearCountDetails(yearCount, "ref.enum.yearcount.astronomical"),
            _ => throw new ArgumentException("YearCount unknown : " + yearCount.ToString())
        };
    }

    public List<YearCountDetails> AllDetailsForYearCounts()
    {
        var allDetails = new List<YearCountDetails>();
        foreach (YearCounts yearCount in Enum.GetValues(typeof(YearCounts)))
        {
            allDetails.Add(DetailsForYearCount(yearCount));
        }
        return allDetails;
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