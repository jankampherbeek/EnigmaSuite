// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System;

namespace E4C.Shared.References;


/// <summary>Supported ayanamshas.</summary>
public enum Ayanamshas
{
    Fagan, Lahiri, DeLuce, Raman, UshaShashi, Krishnamurti, DjwhalKhul, Yukteshwar, Bhasin, Kugler1, Kugler2, Kugler3, Huber, EtaPiscium, Aldebaran15Tau, Hipparchus, Sassanian,
    GalactCtr0Sag, J2000, J1900, B1950, SuryaSiddhanta, SuryaSiddhantaMeanSun, Aryabhata, AryabhataMeanSun, SsRevati, SsCitra, TrueCitra, TrueRevati, TruePushya, GalacticCtrBrand,
    GalacticEqIau1958, GalacticEq, GalacticEqMidMula, Skydram, TrueMula, Dhruva, Aryabhata522, Britton, GalacticCtrOCap
}

/// <summary>Specifications for an ayanamsha.</summary>
public record AyanamshaDetails
{
    readonly public Ayanamshas Ayanamsha;
    readonly public int SeId;
    readonly public string TextId;

    /// <param name="ayanamsha"/>
    /// <param name="seId">Id that identifies the ayanamsha for the Swiss Ephemeris.</param>
    /// <param name="textId">Id to find a descriptive text in a resource bundle.</param>
    public AyanamshaDetails(Ayanamshas ayanamsha, int seId, string textId)
    {
        Ayanamsha = ayanamsha;
        SeId = seId;
        TextId = textId;
    }
}

/// <summary>Specifications for an ayanamsha.</summary>
public interface IAyanamshaSpecifications
{
    /// <summary>Returns the specification for an ayanamsha.</summary>
    /// <param name="ayanamsha">The ayanamsha, from the enum Ayanamshas.</param>
    /// <returns>A record AyanamshaDetails with the specification of the ayanamsha.</returns>
    public AyanamshaDetails DetailsForAyanamsha(Ayanamshas ayanamsha);
}

/// <inheritdoc/>
public class AyanamshaSpecifications : IAyanamshaSpecifications
{
    /// <inheritdoc/>
    /// <exception cref="ArgumentException">Is thrown if the ayanamsha was not recognized.</exception>
    AyanamshaDetails IAyanamshaSpecifications.DetailsForAyanamsha(Ayanamshas ayanamsha)
    {
        return ayanamsha switch
        {
            Ayanamshas.Fagan => new AyanamshaDetails(ayanamsha, 0, "ayanamshaFagan"),
            Ayanamshas.Lahiri => new AyanamshaDetails(ayanamsha, 1, "ayanamshaLahiri"),
            Ayanamshas.DeLuce => new AyanamshaDetails(ayanamsha, 2, "ayanamshaDeLuce"),
            Ayanamshas.Raman => new AyanamshaDetails(ayanamsha, 3, "ayanamshaRaman"),
            Ayanamshas.UshaShashi => new AyanamshaDetails(ayanamsha, 4, "ayanamshaUshaSashi"),
            Ayanamshas.Krishnamurti => new AyanamshaDetails(ayanamsha, 5, "ayanamshaKrishnamurti"),
            Ayanamshas.DjwhalKhul => new AyanamshaDetails(ayanamsha, 6, "ayanamshaDjwhalKhul"),
            Ayanamshas.Yukteshwar => new AyanamshaDetails(ayanamsha, 7, "ayanamshaYukteshwar"),
            Ayanamshas.Bhasin => new AyanamshaDetails(ayanamsha, 8, "ayanamshaBhasin"),
            Ayanamshas.Kugler1 => new AyanamshaDetails(ayanamsha, 9, "ayanamshaKugler1"),
            Ayanamshas.Kugler2 => new AyanamshaDetails(ayanamsha, 10, "ayanamshaKugler2"),
            Ayanamshas.Kugler3 => new AyanamshaDetails(ayanamsha, 11, "ayanamshaKugler3"),
            Ayanamshas.Huber => new AyanamshaDetails(ayanamsha, 12, "ayanamshaHuber"),
            Ayanamshas.EtaPiscium => new AyanamshaDetails(ayanamsha, 13, "ayanamshaEtaPiscium"),
            Ayanamshas.Aldebaran15Tau => new AyanamshaDetails(ayanamsha, 14, "ayanamshaAldebaran15Tau"),
            Ayanamshas.Hipparchus => new AyanamshaDetails(ayanamsha, 15, "ayanamshaHipparchus"),
            Ayanamshas.Sassanian => new AyanamshaDetails(ayanamsha, 16, "ayanamshaSassanian"),
            Ayanamshas.GalactCtr0Sag => new AyanamshaDetails(ayanamsha, 17, "ayanamshaGalactCtr0Sag"),
            Ayanamshas.J2000 => new AyanamshaDetails(ayanamsha, 18, "ayanamshaJ2000"),
            Ayanamshas.J1900 => new AyanamshaDetails(ayanamsha, 19, "ayanamshaJ1900"),
            Ayanamshas.B1950 => new AyanamshaDetails(ayanamsha, 20, "ayanamshaB1950"),
            Ayanamshas.SuryaSiddhanta => new AyanamshaDetails(ayanamsha, 21, "ayanamshaSuryaSiddhanta"),
            Ayanamshas.SuryaSiddhantaMeanSun => new AyanamshaDetails(ayanamsha, 22, "ayanamshaSuryaSiddhantaMeanSun"),
            Ayanamshas.Aryabhata => new AyanamshaDetails(ayanamsha, 23, "ayanamshaAryabhata"),
            Ayanamshas.AryabhataMeanSun => new AyanamshaDetails(ayanamsha, 24, "ayanamshaAryabhataMeanSun"),
            Ayanamshas.SsRevati => new AyanamshaDetails(ayanamsha, 25, "ayanamshaSsRevati"),
            Ayanamshas.SsCitra => new AyanamshaDetails(ayanamsha, 26, "ayanamshaSsCitra"),
            Ayanamshas.TrueCitra => new AyanamshaDetails(ayanamsha, 27, "ayanamshaTrueCitra"),
            Ayanamshas.TrueRevati => new AyanamshaDetails(ayanamsha, 28, "ayanamshaTrueRevati"),
            Ayanamshas.TruePushya => new AyanamshaDetails(ayanamsha, 29, "ayanamshaTruePushya"),
            Ayanamshas.GalacticCtrBrand => new AyanamshaDetails(ayanamsha, 30, "ayanamshaGalacticCtrBrand"),
            Ayanamshas.GalacticEqIau1958 => new AyanamshaDetails(ayanamsha, 31, "ayanamshaGalacticCtrEqIau1958"),
            Ayanamshas.GalacticEq => new AyanamshaDetails(ayanamsha, 32, "ayanamshaGalacticEq"),
            Ayanamshas.GalacticEqMidMula => new AyanamshaDetails(ayanamsha, 33, "ayanamshaGalacticEqMidMula"),
            Ayanamshas.Skydram => new AyanamshaDetails(ayanamsha, 34, "ayanamshaSkydram"),
            Ayanamshas.TrueMula => new AyanamshaDetails(ayanamsha, 35, "ayanamshaTrueMula"),
            Ayanamshas.Dhruva => new AyanamshaDetails(ayanamsha, 36, "ayanamshaDhruva"),
            Ayanamshas.Aryabhata522 => new AyanamshaDetails(ayanamsha, 37, "ayanamshaAryabhata522"),
            Ayanamshas.Britton => new AyanamshaDetails(ayanamsha, 38, "ayanamshaBritton"),
            Ayanamshas.GalacticCtrOCap => new AyanamshaDetails(ayanamsha, 39, "ayanamshaGalacticCtr0Cap"),
            _ => throw new ArgumentException("Ayanamsha unknown : " + ayanamsha.ToString())
        };
    }
}
