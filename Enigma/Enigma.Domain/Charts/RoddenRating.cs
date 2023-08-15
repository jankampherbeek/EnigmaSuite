// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Serilog;

namespace Enigma.Domain.Charts;

/// <summary>Ratings according to Rodden and an added 'Undefined' rating.</summary>
/// <remarks>
/// Suports:
/// AA - Accurate.As recorded by family or state
/// A - Quoted by the person, kin, friend, or associate
/// B - Biography or autobiography
/// C - Caution, no source
/// DD - Dirty data.Conflicting quotes that are unqualified
/// X - No time of birth
/// XX - No date of birth
/// Also added 'Unknown' which is not an original Rodden Rating.
/// </remarks>
public enum RoddenRatings
{

    Unknown = 0, AA = 1, A = 2, B = 3, C = 4, DD = 5, X = 6, XX = 7
}


/// <summary>Details for the Category of a chart</summary>
/// <param name="Rating">The standard acronym for the Rodden rating</param>
/// <param name="Text">Descriptive text</param>
public record RoddenRatingDetails(RoddenRatings Rating, string Text);


/// <summary>Extension class for enum RoddenRatings.</summary>
public static class RoddenRatingsExtensions
{
    /// <summary>Retrieve details for Rodden rating.</summary>
    /// <param name="rating">The Rodden rating, is automatically filled.</param>
    /// <returns>Details for the Rodden rating.</returns>
    public static RoddenRatingDetails GetDetails(this RoddenRatings rating)
    {
        return rating switch
        {
            RoddenRatings.Unknown => new RoddenRatingDetails(rating, "Unknown"),
            RoddenRatings.AA => new RoddenRatingDetails(rating, "AA - Accurate"),
            RoddenRatings.A => new RoddenRatingDetails(rating, "A - Quoted"),
            RoddenRatings.B => new RoddenRatingDetails(rating, "B - (Auto)biography"),
            RoddenRatings.C => new RoddenRatingDetails(rating, "C - Caution, no source"),
            RoddenRatings.DD => new RoddenRatingDetails(rating, "DD - Dirty data"),
            RoddenRatings.X => new RoddenRatingDetails(rating, "X - No time of birth"),
            RoddenRatings.XX => new RoddenRatingDetails(rating, "XX - No data of birth"),
            _ => throw new ArgumentException("RoddenRatings unknown : " + rating)
        };
    }

    /// <summary>Retrieve details for items in the enum RoddenRatings.</summary>
    /// <returns>All details.</returns>
    public static List<RoddenRatingDetails> AllDetails()
    {
        return (from RoddenRatings currentRating in Enum.GetValues(typeof(RoddenRatings)) select currentRating.GetDetails()).ToList();
    }


    /// <summary>Find Rodden rating for an index.</summary>
    /// <param name="index">Index to look for.</param>
    /// <returns>The Rodden rating for the index.</returns>
    /// <exception cref="ArgumentException">Is thrown if a non existing index is given.</exception>
    public static RoddenRatings RoddenRatingForIndex(int index)
    {
        
        foreach (RoddenRatings currentRating in Enum.GetValues(typeof(RoddenRatings)))
        {
            if ((int)currentRating == index) return currentRating;
        }
        Log.Error("RoddenRating.RoddenRatingForIndex(): Could not find Rodden rating for index : {Index}", index);
        throw new ArgumentException("Wrong RoddenRating");
    }

}

