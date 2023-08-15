// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Serilog;

namespace Enigma.Domain.Calc.ChartItems;

/// <summary>Supported mundane.</summary>
public enum Signs
{
    Aries = 1, Taurus = 2, Gemini = 3, Cancer = 4, Leo = 5, Virgo = 6, Libra = 7, Scorpio = 8, Sagittarius = 9, Capricorn = 10, Aquarius = 11, Pisces = 12
}


/// <summary>Details for a Sign.</summary>
/// <param name="Sign">The sign.</param>
/// <param name="Text">Descriptive text.</param>
/// <param name="TextAbbreviated">Abbreviated description.</param>
public record SignDetails(Signs Sign, string Text, string TextAbbreviated);


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
            Signs.Aries => new SignDetails(Signs.Aries, "Aries", "ARI"),
            Signs.Taurus => new SignDetails(Signs.Taurus, "Taurus", "TAU"),
            Signs.Gemini => new SignDetails(Signs.Gemini, "Gemini", "GEM"),
            Signs.Cancer => new SignDetails(Signs.Cancer, "Cancer", "CAN"),
            Signs.Leo => new SignDetails(Signs.Leo, "Leo", "LEO"),
            Signs.Virgo => new SignDetails(Signs.Virgo, "Virgo", "VIR"),
            Signs.Libra => new SignDetails(Signs.Libra, "Libra", "LIB"),
            Signs.Scorpio => new SignDetails(Signs.Scorpio, "Scorpio", "SCO"),
            Signs.Sagittarius => new SignDetails(Signs.Sagittarius, "Sagittarius", "SAG"),
            Signs.Capricorn => new SignDetails(Signs.Capricorn, "Capricorn", "CAP"),
            Signs.Aquarius => new SignDetails(Signs.Aquarius, "Aquarius", "AQU"),
            Signs.Pisces => new SignDetails(Signs.Pisces, "Pisces", "PIS"),
            _ => throw new ArgumentException("Sign unknown : " + sign)
        };
    }

    /// <summary>Retrieve details for items in the enum Signs.</summary>
    /// <returns>All details.</returns>
    public static List<SignDetails> AllDetails()
    {
        return (from Signs currentSign in Enum.GetValues(typeof(Signs)) select currentSign.GetDetails()).ToList();
    }


    /// <summary>Find Sign for an index.</summary>
    /// <param name="index">Index to look for.</param>
    /// <returns>The Sign.</returns>
    /// <exception cref="ArgumentException">Is thrown if a non existing index is given.</exception>
    public static Signs SignForIndex(int index)
    {
        foreach (Signs currentSign in Enum.GetValues(typeof(Signs)))
        {
            if ((int)currentSign == index) return currentSign;
        }
        Log.Error("Signs.SignForIndex(): Could not find Sign for index : {Index}", index);
        throw new ArgumentException("Sign for index not found");
    }

}


