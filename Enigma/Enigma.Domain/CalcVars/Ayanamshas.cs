// Jan Kampherbeek, (c) 2022.
// Enigma Research is open source.
// Please check the file copyright.txt in the root of the source for further details.


namespace Enigma.Domain.CalcVars;


/// <summary>Supported ayanamshas.</summary>
public enum Ayanamshas
{
    None, Fagan, Lahiri, DeLuce, Raman, UshaShashi, Krishnamurti, DjwhalKhul, Yukteshwar, Bhasin, Kugler1, Kugler2, Kugler3, Huber, EtaPiscium, Aldebaran15Tau, Hipparchus, Sassanian,
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
    public List<AyanamshaDetails> AllAyanamshaDetails();

}

/// <inheritdoc/>
public class AyanamshaSpecifications : IAyanamshaSpecifications
{
    /// <inheritdoc/>
    /// <exception cref="ArgumentException">Is thrown if the ayanamsha was not recognized.</exception>
    public AyanamshaDetails DetailsForAyanamsha(Ayanamshas ayanamsha)
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

    public List<AyanamshaDetails> AllAyanamshaDetails()
    {
        var allDetails = new List<AyanamshaDetails>();
        foreach (Ayanamshas ayanamsha in Enum.GetValues(typeof(Ayanamshas)))
        {
            allDetails.Add(DetailsForAyanamsha(ayanamsha));
        }
        return allDetails;
    }


}
