// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Domain.CalcVars;

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

/// <summary>
/// Details for the Category of a chart.
/// </summary>
public record RoddenRatingDetails
{
    readonly public RoddenRatings Rating;
    readonly public string TextId;

    /// <summary>
    /// Construct details for a Rodden rating.
    /// </summary>
    /// <param name="rating">The standard acronym for the Rodden rating.</param>
    /// <param name="textId">Id to find a descriptive text in a resource bundle.</param>
    public RoddenRatingDetails(RoddenRatings rating, string textId)
    {
        Rating = rating;
        TextId = textId;
    }
}

/// <summary>
/// Specifications for a Rodden rating.
/// </summary>
public interface IRoddenRatingSpecifications
{
    /// <summary>
    /// Returns the details for a Rodden rating.
    /// </summary>
    /// <param name="rating">The Rodden rating, from the enum RoddenRatings.</param>
    /// <returns>A record RoddenRatingDetails with the specifications.</returns>
    public RoddenRatingDetails DetailsForRating(RoddenRatings rating);

    /// <summary>
    /// Returns a value from the enum RoddenRatings that corresponds with an index.
    /// </summary>
    /// <param name="roddenRatingIndex">The index for the requested item from RoddenRatings. 
    /// Throws an exception if no RoddenRatings for the given index does exist.</param>
    /// <returns>Instance from enum RoddenRatings that corresponds with the given index.</returns>
    public RoddenRatings RoddenRatingForIndex(int roddenRatingIndex);
}


public class RoddenRatingSpecifications : IRoddenRatingSpecifications
{
    /// <exception cref="ArgumentException">Is thrown if the rating was not recognized.</exception>
    public RoddenRatingDetails DetailsForRating(RoddenRatings rating)
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

    public RoddenRatings RoddenRatingForIndex(int roddenRatingIndex)
    {
        foreach (RoddenRatings rating in Enum.GetValues(typeof(RoddenRatings)))
        {
            if ((int)rating == roddenRatingIndex) return rating;
        }
        throw new ArgumentException("Could not find RoddenRatings for index : " + roddenRatingIndex);
    }
}
