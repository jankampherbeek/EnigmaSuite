// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Constants;
using Enigma.Domain.Interfaces;

namespace Enigma.Domain.Enums;


/// <summary>Supported ayanamshas.</summary>
public enum Ayanamshas
{
    None = 0, Fagan = 1, Lahiri = 2, DeLuce = 3, Raman = 4, UshaShashi = 5, Krishnamurti = 6, DjwhalKhul = 7, Yukteshwar = 8, Bhasin = 9, 
    Kugler1 = 10, Kugler2 = 11, Kugler3 = 12, Huber = 13, EtaPiscium = 14, Aldebaran15Tau = 15, Hipparchus = 16, Sassanian = 17, GalactCtr0Sag = 18, J2000 = 19, 
    J1900 = 20, B1950 = 21, SuryaSiddhanta = 22, SuryaSiddhantaMeanSun = 23, Aryabhata = 24, AryabhataMeanSun = 25, SsRevati = 26, SsCitra = 27, TrueCitra = 28, TrueRevati = 29, 
    TruePushya = 30, GalacticCtrBrand = 31, GalacticEqIau1958 = 32, GalacticEq = 33, GalacticEqMidMula = 34, Skydram = 35, TrueMula = 36, Dhruva = 37, Aryabhata522 = 38, Britton = 39, 
    GalacticCtrOCap = 40
}

/// <summary>Specifications for an ayanamsha.</summary>
/// <param name="ayanamsha"/>
/// <param name="seId">Id that identifies the ayanamsha for the Swiss Ephemeris.</param>
/// <param name="textId">Id to find a descriptive text in a resource bundle.</param>
public record AyanamshaDetails(Ayanamshas Ayanamsha, int SeId, string TextId);


public static class AyanamshaExtensions
{
    /// <summary>Retrieve dtails for ayanamsha.</summary>
    /// <param name="ayanamsha">The ayanamsha, is automatically filled.</param>
    /// <returns>Details for the ayanamsha.</returns>
    public static AyanamshaDetails GetDetails(this Ayanamshas ayanamsha)
    {
        return ayanamsha switch
        {
            Ayanamshas.None => new AyanamshaDetails(ayanamsha, -1, "ref.enum.ayanamsha.none"),
            Ayanamshas.Fagan => new AyanamshaDetails(ayanamsha, 0, "ref.enum.ayanamsha.fagan"),
            Ayanamshas.Lahiri => new AyanamshaDetails(ayanamsha, 1, "ref.enum.ayanamsha.lahiri"),
            Ayanamshas.DeLuce => new AyanamshaDetails(ayanamsha, 2, "ref.enum.ayanamsha.deluce"),
            Ayanamshas.Raman => new AyanamshaDetails(ayanamsha, 3, "ref.enum.ayanamsha.raman"),
            Ayanamshas.UshaShashi => new AyanamshaDetails(ayanamsha, 4, "ref.enum.ayanamsha.ushashashi"),
            Ayanamshas.Krishnamurti => new AyanamshaDetails(ayanamsha, 5, "ref.enum.ayanamsha.krishnamurti"),
            Ayanamshas.DjwhalKhul => new AyanamshaDetails(ayanamsha, 6, "ref.enum.ayanamsha.djwhalkhul"),
            Ayanamshas.Yukteshwar => new AyanamshaDetails(ayanamsha, 7, "ref.enum.ayanamsha.yukteshwar"),
            Ayanamshas.Bhasin => new AyanamshaDetails(ayanamsha, 8, "ref.enum.ayanamsha.bhasin"),
            Ayanamshas.Kugler1 => new AyanamshaDetails(ayanamsha, 9, "ref.enum.ayanamsha.kugler1"),
            Ayanamshas.Kugler2 => new AyanamshaDetails(ayanamsha, 10, "ref.enum.ayanamsha.kugler2"),
            Ayanamshas.Kugler3 => new AyanamshaDetails(ayanamsha, 11, "ref.enum.ayanamsha.kugler3"),
            Ayanamshas.Huber => new AyanamshaDetails(ayanamsha, 12, "ref.enum.ayanamsha.huber"),
            Ayanamshas.EtaPiscium => new AyanamshaDetails(ayanamsha, 13, "ref.enum.ayanamsha.etapiscium"),
            Ayanamshas.Aldebaran15Tau => new AyanamshaDetails(ayanamsha, 14, "ref.enum.ayanamsha.aldebaran15tau"),
            Ayanamshas.Hipparchus => new AyanamshaDetails(ayanamsha, 15, "ref.enum.ayanamsha.hipparchus"),
            Ayanamshas.Sassanian => new AyanamshaDetails(ayanamsha, 16, "ref.enum.ayanamsha.sassanian"),
            Ayanamshas.GalactCtr0Sag => new AyanamshaDetails(ayanamsha, 17, "ref.enum.ayanamsha.galactctr0sag"),
            Ayanamshas.J2000 => new AyanamshaDetails(ayanamsha, 18, "ref.enum.ayanamsha.j2000"),
            Ayanamshas.J1900 => new AyanamshaDetails(ayanamsha, 19, "ref.enum.ayanamsha.j1900"),
            Ayanamshas.B1950 => new AyanamshaDetails(ayanamsha, 20, "ref.enum.ayanamsha.b1950"),
            Ayanamshas.SuryaSiddhanta => new AyanamshaDetails(ayanamsha, 21, "ref.enum.ayanamsha.suryasiddhanta"),
            Ayanamshas.SuryaSiddhantaMeanSun => new AyanamshaDetails(ayanamsha, 22, "ref.enum.ayanamsha.suryasiddhantameansun"),
            Ayanamshas.Aryabhata => new AyanamshaDetails(ayanamsha, 23, "ref.enum.ayanamsha.aryabhata"),
            Ayanamshas.AryabhataMeanSun => new AyanamshaDetails(ayanamsha, 24, "ref.enum.ayanamsha.aryabhatameansun"),
            Ayanamshas.SsRevati => new AyanamshaDetails(ayanamsha, 25, "ref.enum.ayanamsha.ssrevati"),
            Ayanamshas.SsCitra => new AyanamshaDetails(ayanamsha, 26, "ref.enum.ayanamsha.sscitra"),
            Ayanamshas.TrueCitra => new AyanamshaDetails(ayanamsha, 27, "ref.enum.ayanamsha.truecitra"),
            Ayanamshas.TrueRevati => new AyanamshaDetails(ayanamsha, 28, "ref.enum.ayanamsha.truerevati"),
            Ayanamshas.TruePushya => new AyanamshaDetails(ayanamsha, 29, "ref.enum.ayanamsha.truepushya"),
            Ayanamshas.GalacticCtrBrand => new AyanamshaDetails(ayanamsha, 30, "ref.enum.ayanamsha.galacticctrbrand"),
            Ayanamshas.GalacticEqIau1958 => new AyanamshaDetails(ayanamsha, 31, "ref.enum.ayanamsha.galacticeqiau1958"),
            Ayanamshas.GalacticEq => new AyanamshaDetails(ayanamsha, 32, "ref.enum.ayanamsha.galacticeq"),
            Ayanamshas.GalacticEqMidMula => new AyanamshaDetails(ayanamsha, 33, "ref.enum.ayanamsha.galacticeqmidmula"),
            Ayanamshas.Skydram => new AyanamshaDetails(ayanamsha, 34, "ref.enum.ayanamsha.skydram"),
            Ayanamshas.TrueMula => new AyanamshaDetails(ayanamsha, 35, "ref.enum.ayanamsha.truemula"),
            Ayanamshas.Dhruva => new AyanamshaDetails(ayanamsha, 36, "ref.enum.ayanamsha.dhruva"),
            Ayanamshas.Aryabhata522 => new AyanamshaDetails(ayanamsha, 37, "ref.enum.ayanamsha.aryabhata522"),
            Ayanamshas.Britton => new AyanamshaDetails(ayanamsha, 38, "ref.enum.ayanamsha.britton"),
            Ayanamshas.GalacticCtrOCap => new AyanamshaDetails(ayanamsha, 39, "ref.enum.ayanamsha.galacticctrOcap"),
            _ => throw new ArgumentException("Ayanamsha unknown : " + ayanamsha.ToString())
        };
    }

    /// <summary>Retrieve details for items in the enum Ayanamshas.</summary>
    /// <param name="ayanamsha">Any instance of ayanamsha, is automatically filled.</param>
    /// <returns>All details.</returns>
    public static List<AyanamshaDetails> AllDetails(this Ayanamshas ayanamsha)
    {
        var allDetails = new List<AyanamshaDetails>();
        foreach (Ayanamshas ayanamshaCurrent in Enum.GetValues(typeof(Ayanamshas)))
        {
            allDetails.Add(ayanamshaCurrent.GetDetails());
        }
        return allDetails;
    }

    public static Ayanamshas AyanamshaForIndex(this Ayanamshas ayanamsha, int index)
    {
        foreach (Ayanamshas ayanamshaCurrent in Enum.GetValues(typeof(Ayanamshas)))
        {
            if ((int)ayanamshaCurrent == index) return ayanamshaCurrent;
        }
        throw new ArgumentException("Could not find Ayanamsha for index : " + index);
    }


}
