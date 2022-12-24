// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Domain.AstronCalculations;

/// <summary>
/// Ratings according to Rodden and an added 'Undefined' rating.
/// </summary>
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

public static class RoddenRatingsExtensions
{
    /// <summary>Retrieve details for Rodden rating.</summary>
    /// <param name="rating">The Rodden rating, is automatically filled.</param>
    /// <returns>Details for the Rodden rating.</returns>
    public static RoddenRatingDetails GetDetails(this RoddenRatings rating)
    {
        return rating switch
        {
            RoddenRatings.Unknown => new RoddenRatingDetails(rating, "ref.enum.roddenrating.unknown"),
            RoddenRatings.AA => new RoddenRatingDetails(rating, "ref.enum.roddenrating.aa"),
            RoddenRatings.A => new RoddenRatingDetails(rating, "ref.enum.roddenrating.a"),
            RoddenRatings.B => new RoddenRatingDetails(rating, "ref.enum.roddenrating.b"),
            RoddenRatings.C => new RoddenRatingDetails(rating, "ref.enum.roddenrating.c"),
            RoddenRatings.DD => new RoddenRatingDetails(rating, "ref.enum.roddenrating.dd"),
            RoddenRatings.X => new RoddenRatingDetails(rating, "ref.enum.roddenrating.x"),
            RoddenRatings.XX => new RoddenRatingDetails(rating, "ref.enum.roddenrating.xx"),
            _ => throw new ArgumentException("RoddenRatings unknown : " + rating.ToString())
        };
    }

    /// <summary>Retrieve details for items in the enum RoddenRatings.</summary>
    /// <param name="...">The ..., is automatically filled.</param>
    /// <returns>All details.</returns>
    public static List<RoddenRatingDetails> AllDetails(this RoddenRatings rating)
    {
        var allDetails = new List<RoddenRatingDetails>();
        foreach (RoddenRatings currentRating in Enum.GetValues(typeof(RoddenRatings)))
        {
            allDetails.Add(currentRating.GetDetails());
        }
        return allDetails;
    }


    /// <summary>Find Rodden rating for an index.</summary>
    /// <param name="rating">Any Rodden rating, is automatically filled.</param>
    /// <param name="index">Index to look for.</param>
    /// <returns>The Rodden rating for the index.</returns>
    /// <exception cref="ArgumentException">Is thrown if a non existing index is given.</exception>
    public static RoddenRatings RoddenRatingForIndex(this RoddenRatings rating, int index)
    {
        foreach (RoddenRatings currentRating in Enum.GetValues(typeof(RoddenRatings)))
        {
            if ((int)currentRating == index) return currentRating;
        }
        throw new ArgumentException("Could not find Rodden rating for index : " + index);
    }

}

/// <summary>Details for the Category of a chart.</summary>
/// <param name="rating">The standard acronym for the Rodden rating.</param>
/// <param name="textId">Id to find a descriptive text in a resource bundle.</param>
public record RoddenRatingDetails(RoddenRatings Rating, string TextId);