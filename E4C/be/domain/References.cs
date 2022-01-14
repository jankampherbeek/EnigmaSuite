// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System;

namespace E4C.be.domain
{

    /// <summary>
    /// Types of points in the solar system.
    /// </summary>
    /// <remarks>
    /// CLASSIC: Sun, Moon and visible planets, 
    /// MODERN: Uranus, Neptune, Pluto, 
    /// MATHPOINT: mathematical points like the lunare node, 
    /// MINOR: Plutoids (except Pluto), planetoids, centaurs, 
    /// HYPOTHETICAL: hypothetical bodies and points.
    /// </remarks>
    public enum SolSysPointCats
    {
        Classic, Modern, MathPoint, Minor, Hypothetical
    }

    /// <summary>
    /// Details for a Solar System Point.
    /// </summary>
    public record SolSysPointCatDetails
    {
        public SolSysPointCats category;
        public string textId;

        public SolSysPointCatDetails(SolSysPointCats category, string textId)
        {
            this.category = category;
            this.textId = textId;
        }
    }

    /// <summary>
    /// Specifications for a Solar Systempoint Category.
    /// </summary>
    public interface ISolSysPointCatSpecifications
    {
        /// <summary>
        /// Returns the details for a Solar System Point Category.
        /// </summary>
        /// <param name="category">The category, from the enum SolSysPointCats.</param>
        /// <returns>A record SolSysPointCatDetails with the specifications.</returns>
        public SolSysPointCatDetails DetailsForCategory(SolSysPointCats category);
    }


    public class SolSysPointCatSpecifications: ISolSysPointCatSpecifications { 

        /// <exception cref="ArgumentException">Is thrown if the category was not recognized.</exception>
        public SolSysPointCatDetails DetailsForCategory(SolSysPointCats category)
        {
            return category switch
            {
                SolSysPointCats.Classic => new SolSysPointCatDetails(category, "enumSolSysPointCatClassic"),
                SolSysPointCats.Modern => new SolSysPointCatDetails(category, "enumSolSysPointCatModern"),
                SolSysPointCats.MathPoint => new SolSysPointCatDetails(category, "enumSolSysPointCatMathPoint"),
                SolSysPointCats.Minor => new SolSysPointCatDetails(category, "enumSolSysPointCatMinor"),
                SolSysPointCats.Hypothetical => new SolSysPointCatDetails(category, "enumSolSysPointCatHypothetical"),
                _ => throw new ArgumentException("SolSysPointCats unknown : " + category.ToString()),
            };
        }
    }

    /// <summary>
    /// Enum for Gregorian and Julian Calendar.
    /// </summary>
    public enum Calendars
    {
        Gregorian, Julian
    }


}