// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Serilog;

namespace Enigma.Domain.References;


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
/// <param name="RbKey">Key for descriptive text in resource bundle.</param>
public record AyanamshaDetails(Ayanamshas Ayanamsha, int SeId, string RbKey);

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
            Ayanamshas.None => new AyanamshaDetails(ayanamsha, -1, "ref_ayanamsha_none"),
            Ayanamshas.Fagan => new AyanamshaDetails(ayanamsha, 0, "ref_ayanamsha_fagan"),
            Ayanamshas.Lahiri => new AyanamshaDetails(ayanamsha, 1, "ref_ayanamsha_lahiri"),
            Ayanamshas.DeLuce => new AyanamshaDetails(ayanamsha, 2, "ref_ayanamsha_deluce"),
            Ayanamshas.Raman => new AyanamshaDetails(ayanamsha, 3, "ref_ayanamsha_raman"),
            Ayanamshas.UshaShashi => new AyanamshaDetails(ayanamsha, 4, "ref_ayanamsha_ushashashi"),
            Ayanamshas.Krishnamurti => new AyanamshaDetails(ayanamsha, 5, "ref_ayanamsha_krishnamurti"),
            Ayanamshas.DjwhalKhul => new AyanamshaDetails(ayanamsha, 6, "ref_ayanamsha_djwhalkhul"),
            Ayanamshas.Yukteshwar => new AyanamshaDetails(ayanamsha, 7, "ref_ayanamsha_yukteshwar"),
            Ayanamshas.Bhasin => new AyanamshaDetails(ayanamsha, 8, "ref_ayanamsha_bhasin"),
            Ayanamshas.Kugler1 => new AyanamshaDetails(ayanamsha, 9, "ref_ayanamsha_kugler1"),
            Ayanamshas.Kugler2 => new AyanamshaDetails(ayanamsha, 10, "ref_ayanamsha_kugler2"),
            Ayanamshas.Kugler3 => new AyanamshaDetails(ayanamsha, 11, "ref_ayanamsha_kugler3"),
            Ayanamshas.Huber => new AyanamshaDetails(ayanamsha, 12, "ref_ayanamsha_huber"),
            Ayanamshas.EtaPiscium => new AyanamshaDetails(ayanamsha, 13, "ref_ayanamsha_etapiscium"),
            Ayanamshas.Aldebaran15Tau => new AyanamshaDetails(ayanamsha, 14, "ref_ayanamsha_aldebaran15tau"),
            Ayanamshas.Hipparchus => new AyanamshaDetails(ayanamsha, 15, "ref_ayanamsha_hipparchus"),
            Ayanamshas.Sassanian => new AyanamshaDetails(ayanamsha, 16, "ref_ayanamsha_sassanian"),
            Ayanamshas.GalactCtr0Sag => new AyanamshaDetails(ayanamsha, 17, "ref_ayanamsha_galcent0sag"),
            Ayanamshas.J2000 => new AyanamshaDetails(ayanamsha, 18, "ref_ayanamsha_j2000"),
            Ayanamshas.J1900 => new AyanamshaDetails(ayanamsha, 19, "ref_ayanamsha_j1900"),
            Ayanamshas.B1950 => new AyanamshaDetails(ayanamsha, 20, "ref_ayanamsha_b1950"),
            Ayanamshas.SuryaSiddhanta => new AyanamshaDetails(ayanamsha, 21, "ref_ayanamsha_suryasiddhanta"),
            Ayanamshas.SuryaSiddhantaMeanSun => new AyanamshaDetails(ayanamsha, 22, "ref_ayanamsha_suryasiddhantameansun"),
            Ayanamshas.Aryabhata => new AyanamshaDetails(ayanamsha, 23, "ref_ayanamsha_aryabhata"),
            Ayanamshas.AryabhataMeanSun => new AyanamshaDetails(ayanamsha, 24, "ref_ayanamsha_aryabhatameansun"),
            Ayanamshas.SsRevati => new AyanamshaDetails(ayanamsha, 25, "ref_ayanamsha_ssrevati"),
            Ayanamshas.SsCitra => new AyanamshaDetails(ayanamsha, 26, "ref_ayanamsha_sscitra"),
            Ayanamshas.TrueCitra => new AyanamshaDetails(ayanamsha, 27, "ref_ayanamsha_truecitrapaksha"),
            Ayanamshas.TrueRevati => new AyanamshaDetails(ayanamsha, 28, "ref_ayanamsha_truerevati"),
            Ayanamshas.TruePushya => new AyanamshaDetails(ayanamsha, 29, "ref_ayanamsha_truepushya"),
            Ayanamshas.GalacticCtrBrand => new AyanamshaDetails(ayanamsha, 30, "ref_ayanamsha_galcentbrand"),
            Ayanamshas.GalacticEqIau1958 => new AyanamshaDetails(ayanamsha, 31, "ref_ayanamsha_galcentiau1958"),
            Ayanamshas.GalacticEq => new AyanamshaDetails(ayanamsha, 32, "ref_ayanamsha_galequator"),
            Ayanamshas.GalacticEqMidMula => new AyanamshaDetails(ayanamsha, 33, "ref_ayanamsha_galequatormidmula"),
            Ayanamshas.Skydram => new AyanamshaDetails(ayanamsha, 34, "ref_ayanamsha_skydram"),
            Ayanamshas.TrueMula => new AyanamshaDetails(ayanamsha, 35, "ref_ayanamsha_truemula"),
            Ayanamshas.Dhruva => new AyanamshaDetails(ayanamsha, 36, "ref_ayanamsha_dhruva"),
            Ayanamshas.Aryabhata522 => new AyanamshaDetails(ayanamsha, 37, "ref_ayanamsha_aryabhata522"),
            Ayanamshas.Britton => new AyanamshaDetails(ayanamsha, 38, "ref_ayanamsha_britton"),
            Ayanamshas.GalacticCtrOCap => new AyanamshaDetails(ayanamsha, 39, "ref_ayanamsha_galcent0cap"),
            _ => throw new ArgumentException("Ayanamsha unknown : " + ayanamsha)
        };
    }

    /// <summary>Retrieve details for items in the enum Ayanamshas</summary>
    /// <returns>All details</returns>
    public static List<AyanamshaDetails> AllDetails()
    {
        return (from Ayanamshas ayanamshaCurrent in Enum.GetValues(typeof(Ayanamshas)) 
            select ayanamshaCurrent.GetDetails()).ToList();
    }

    /// <summary>Find ayanamsha for a given index</summary>
    /// <param name="index">The index</param>
    /// <returns>The ayanamsha</returns>
    /// <exception cref="ArgumentException">Thrown if the ayanamsha could not be found</exception>
    public static Ayanamshas AyanamshaForIndex(int index)
    {
        foreach (Ayanamshas ayanamshaCurrent in Enum.GetValues(typeof(Ayanamshas)))
        {
            if ((int)ayanamshaCurrent == index) return ayanamshaCurrent;
        }
        Log.Error("Ayanamshas.AyanamshaForIndex(): Could not find Ayanamsha for index : {Index}", index );
        throw new ArgumentException("Enum for Ayanamsha not found");
    }

}
