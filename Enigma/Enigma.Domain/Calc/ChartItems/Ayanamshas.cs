// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Serilog;

namespace Enigma.Domain.Calc.ChartItems;


/// <summary>Supported ayanamshas.</summary>
public enum Ayanamshas
{
    None = 0, Fagan = 1, Lahiri = 2, DeLuce = 3, Raman = 4, UshaShashi = 5, Krishnamurti = 6, DjwhalKhul = 7, Yukteshwar = 8, Bhasin = 9,
    Kugler1 = 10, Kugler2 = 11, Kugler3 = 12, Huber = 13, EtaPiscium = 14, Aldebaran15Tau = 15, Hipparchus = 16, Sassanian = 17, GalactCtr0Sag = 18, J2000 = 19,
    J1900 = 20, B1950 = 21, SuryaSiddhanta = 22, SuryaSiddhantaMeanSun = 23, Aryabhata = 24, AryabhataMeanSun = 25, SsRevati = 26, SsCitra = 27, TrueCitra = 28, TrueRevati = 29,
    TruePushya = 30, GalacticCtrBrand = 31, GalacticEqIau1958 = 32, GalacticEq = 33, GalacticEqMidMula = 34, Skydram = 35, TrueMula = 36, Dhruva = 37, Aryabhata522 = 38, Britton = 39,
    GalacticCtrOCap = 40
}

/// <summary>Details for an ayanamsha</summary>
/// <param name="Ayanamsha"/>
/// <param name="SeId">Id that identifies the ayanamsha for the Swiss Ephemeris</param>
/// <param name="Text">Descriptive text</param>
public record AyanamshaDetails(Ayanamshas Ayanamsha, int SeId, string Text);

/// <summary>Extension class for enum Ayanamshas.</summary>
public static class AyanamshaExtensions
{
    /// <summary>Retrieve details for ayanamsha.</summary>
    /// <param name="ayanamsha">The ayanamsha.</param>
    /// <returns>Details for the ayanamsha.</returns>
    public static AyanamshaDetails GetDetails(this Ayanamshas ayanamsha)
    {
        return ayanamsha switch
        {
            Ayanamshas.None => new AyanamshaDetails(ayanamsha, -1, "None (Tropical zodiac)"),
            Ayanamshas.Fagan => new AyanamshaDetails(ayanamsha, 0, "Fagan"),
            Ayanamshas.Lahiri => new AyanamshaDetails(ayanamsha, 1, "Lahiri"),
            Ayanamshas.DeLuce => new AyanamshaDetails(ayanamsha, 2, "DeLuce"),
            Ayanamshas.Raman => new AyanamshaDetails(ayanamsha, 3, "Raman"),
            Ayanamshas.UshaShashi => new AyanamshaDetails(ayanamsha, 4, "UshaShashi"),
            Ayanamshas.Krishnamurti => new AyanamshaDetails(ayanamsha, 5, "Krishnamurti"),
            Ayanamshas.DjwhalKhul => new AyanamshaDetails(ayanamsha, 6, "Djwhal Khul"),
            Ayanamshas.Yukteshwar => new AyanamshaDetails(ayanamsha, 7, "Yukteshwar"),
            Ayanamshas.Bhasin => new AyanamshaDetails(ayanamsha, 8, "Bhasin"),
            Ayanamshas.Kugler1 => new AyanamshaDetails(ayanamsha, 9, "Kugler (version 1)"),
            Ayanamshas.Kugler2 => new AyanamshaDetails(ayanamsha, 10, "Kugler (version 2)"),
            Ayanamshas.Kugler3 => new AyanamshaDetails(ayanamsha, 11, "Kugler (version 3)"),
            Ayanamshas.Huber => new AyanamshaDetails(ayanamsha, 12, "Huber"),
            Ayanamshas.EtaPiscium => new AyanamshaDetails(ayanamsha, 13, "Eta Piscium"),
            Ayanamshas.Aldebaran15Tau => new AyanamshaDetails(ayanamsha, 14, "Aldebaran 15 Taurus"),
            Ayanamshas.Hipparchus => new AyanamshaDetails(ayanamsha, 15, "Hipparchus"),
            Ayanamshas.Sassanian => new AyanamshaDetails(ayanamsha, 16, "Sassanian"),
            Ayanamshas.GalactCtr0Sag => new AyanamshaDetails(ayanamsha, 17, "Galactic Center 0 Sag."),
            Ayanamshas.J2000 => new AyanamshaDetails(ayanamsha, 18, "J2000"),
            Ayanamshas.J1900 => new AyanamshaDetails(ayanamsha, 19, "J1900"),
            Ayanamshas.B1950 => new AyanamshaDetails(ayanamsha, 20, "B1950"),
            Ayanamshas.SuryaSiddhanta => new AyanamshaDetails(ayanamsha, 21, "SuryaSiddhanta"),
            Ayanamshas.SuryaSiddhantaMeanSun => new AyanamshaDetails(ayanamsha, 22, "SuryaSiddhantavMean Sun"),
            Ayanamshas.Aryabhata => new AyanamshaDetails(ayanamsha, 23, "Aryabhata"),
            Ayanamshas.AryabhataMeanSun => new AyanamshaDetails(ayanamsha, 24, "Aryabhata Mean Sun"),
            Ayanamshas.SsRevati => new AyanamshaDetails(ayanamsha, 25, "Ss Revati"),
            Ayanamshas.SsCitra => new AyanamshaDetails(ayanamsha, 26, "Ss Citra"),
            Ayanamshas.TrueCitra => new AyanamshaDetails(ayanamsha, 27, "Trye Citrapaksha"),
            Ayanamshas.TrueRevati => new AyanamshaDetails(ayanamsha, 28, "True Revati"),
            Ayanamshas.TruePushya => new AyanamshaDetails(ayanamsha, 29, "True Pushya"),
            Ayanamshas.GalacticCtrBrand => new AyanamshaDetails(ayanamsha, 30, "Galactic Center (Brand)"),
            Ayanamshas.GalacticEqIau1958 => new AyanamshaDetails(ayanamsha, 31, "Galactic Center IAU 1958"),
            Ayanamshas.GalacticEq => new AyanamshaDetails(ayanamsha, 32, "Galactic Equator"),
            Ayanamshas.GalacticEqMidMula => new AyanamshaDetails(ayanamsha, 33, "Galactic Equator Mid Mula"),
            Ayanamshas.Skydram => new AyanamshaDetails(ayanamsha, 34, "Skydram"),
            Ayanamshas.TrueMula => new AyanamshaDetails(ayanamsha, 35, "True Mula"),
            Ayanamshas.Dhruva => new AyanamshaDetails(ayanamsha, 36, "Dhruva"),
            Ayanamshas.Aryabhata522 => new AyanamshaDetails(ayanamsha, 37, "Aryabhata 522"),
            Ayanamshas.Britton => new AyanamshaDetails(ayanamsha, 38, "Britton"),
            Ayanamshas.GalacticCtrOCap => new AyanamshaDetails(ayanamsha, 39, "Galactic Center 0 Capricornus"),
            _ => throw new ArgumentException("Ayanamsha unknown : " + ayanamsha)
        };
    }

    /// <summary>Retrieve details for items in the enum Ayanamshas</summary>
    /// <returns>All details</returns>
    public static List<AyanamshaDetails> AllDetails(this Ayanamshas _)
    {
        return (from Ayanamshas ayanamshaCurrent in Enum.GetValues(typeof(Ayanamshas)) 
            select ayanamshaCurrent.GetDetails()).ToList();
    }

    /// <summary>Find ayanamsha for a given index</summary>
    /// <param name="_">Any Ayanamsha to access the enum </param>
    /// <param name="index">The index</param>
    /// <returns>The ayanamsha</returns>
    /// <exception cref="ArgumentException">Thrown if the ayanamsha could not be found</exception>
    public static Ayanamshas AyanamshaForIndex(this Ayanamshas _, int index)
    {
        foreach (Ayanamshas ayanamshaCurrent in Enum.GetValues(typeof(Ayanamshas)))
        {
            if ((int)ayanamshaCurrent == index) return ayanamshaCurrent;
        }
        Log.Error("Ayanamshas.AyanamshaForIndex(): Could not find Ayanamsha for index : {Index}", index );
        throw new ArgumentException("Enum for Ayanamsha not found");
    }

}
