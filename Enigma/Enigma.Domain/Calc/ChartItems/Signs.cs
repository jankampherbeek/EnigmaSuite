// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Serilog;

namespace Enigma.Domain.Calc.ChartItems;

/// <summary>Supported mundane.</summary>
public enum Signs
{
    None = 0, Aries = 1, Taurus = 2, Gemini = 3, Cancer = 4, Leo = 5, Virgo = 6, Libra = 7, Scorpio = 8, Sagittarius = 9, Capricorn = 10, Aquarius = 11, Pisces = 12
}


/// <summary>Details for a Sign.</summary>
/// <param name="Sign">The sign.</param>
/// <param name="TextId">Id to find a descriptive text in a resource bundle.</param>
/// <param name="TextIdAbbreviated">Abbreviated version for TextId.</param>
public record SignDetails(Signs Sign, string TextId, string TextIdAbbreviated);


/// <summary>Extension class for the enum Signs.</summary>
public static class SignsExtensions
{
    /// <summary>Retrieve details for Signs.</summary>
    /// <param name="sign">The Sign.</param>
    /// <returns>Details for the Sign.</returns>
    public static SignDetails GetDetails(this Signs sign)
    {
        return sign switch
        {
            Signs.Aries => new SignDetails(Signs.Aries, "ref.enum.sign.aries.text", "ref.enum.sign.aries.abbr"),
            Signs.Taurus => new SignDetails(Signs.Taurus, "ref.enum.sign.taurus.text", "ref.enum.sign.taurus.abbr"),
            Signs.Gemini => new SignDetails(Signs.Gemini, "ref.enum.sign.gemini.text", "ref.enum.sign.gemini.abbr"),
            Signs.Cancer => new SignDetails(Signs.Cancer, "ref.enum.sign.cancer.text", "ref.enum.sign.cancer.abbr"),
            Signs.Leo => new SignDetails(Signs.Leo, "ref.enum.sign.leo.text", "ref.enum.sign.leo.abbr"),
            Signs.Virgo => new SignDetails(Signs.Virgo, "ref.enum.sign.virgo.text", "ref.enum.sign.virgo.abbr"),
            Signs.Libra => new SignDetails(Signs.Libra, "ref.enum.sign.libra.text", "ref.enum.sign.libra.abbr"),
            Signs.Scorpio => new SignDetails(Signs.Scorpio, "ref.enum.sign.scorpio.text", "ref.enum.sign.scorpio.abbr"),
            Signs.Sagittarius => new SignDetails(Signs.Sagittarius, "ref.enum.sign.sagittarius.text", "ref.enum.sign.sagittarius.abbr"),
            Signs.Capricorn => new SignDetails(Signs.Capricorn, "ref.enum.sign.capricorn.text", "ref.enum.sign.capricorn.abbr"),
            Signs.Aquarius => new SignDetails(Signs.Aquarius, "ref.enum.sign.aquarius.text", "ref.enum.sign.aquarius.abbr"),
            Signs.Pisces => new SignDetails(Signs.Pisces, "ref.enum.sign.pisces.text", "ref.enum.sign.pisces.abbr"),
            _ => throw new ArgumentException("Sign unknown : " + sign)
        };
    }

    /// <summary>Retrieve details for items in the enum Signs.</summary>
    /// <returns>All details.</returns>
    public static List<SignDetails> AllDetails(this Signs _)
    {
        return (from Signs currentSign in Enum.GetValues(typeof(Signs)) select currentSign.GetDetails()).ToList();
    }


    /// <summary>Find Sign for an index.</summary>
    /// <param name="_">Any sign.</param>
    /// <param name="index">Index to look for.</param>
    /// <returns>The Sign.</returns>
    /// <exception cref="ArgumentException">Is thrown if a non existing index is given.</exception>
    public static Signs SignForIndex(this Signs _, int index)
    {
        foreach (Signs currentSign in Enum.GetValues(typeof(Signs)))
        {
            if ((int)currentSign == index) return currentSign;
        }
        Log.Error("Signs.SignForIndex(): Could not find Sign for index : {Index}", index);
        throw new ArgumentException("Sign for index not found");
    }

}


