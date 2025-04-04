﻿// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Serilog;

namespace Enigma.Domain.References;


/// <summary>Supported ayanamshas.</summary>
public enum Ayanamshas
{
    None = 0, Fagan = 1, Lahiri = 2, DeLuce = 3, Raman = 4, UshaShashi = 5, Krishnamurti = 6, DjwhalKhul = 7, 
    Yukteshwar = 8, Bhasin = 9, Kugler1 = 10, Kugler2 = 11, Kugler3 = 12, Huber = 13, EtaPiscium = 14, 
    Aldebaran15Tau = 15, Hipparchus = 16, Sassanian = 17, GalactCtr0Sag = 18, J2000 = 19, J1900 = 20, B1950 = 21, 
    SuryaSiddhanta = 22, SuryaSiddhantaMeanSun = 23, Aryabhata = 24, AryabhataMeanSun = 25, SsRevati = 26, SsCitra = 27, 
    TrueCitra = 28, TrueRevati = 29, TruePushya = 30, GalacticCtrBrand = 31, GalacticEqIau1958 = 32, GalacticEq = 33, 
    GalacticEqMidMula = 34, Skydram = 35, TrueMula = 36, Dhruva = 37, Aryabhata522 = 38, Britton = 39, 
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
            Ayanamshas.None => new AyanamshaDetails(ayanamsha, -1, "ref.ayanamsha.none"),
            Ayanamshas.Fagan => new AyanamshaDetails(ayanamsha, 0, "ref.ayanamsha.fagan"),
            Ayanamshas.Lahiri => new AyanamshaDetails(ayanamsha, 1, "ref.ayanamsha.lahiri"),
            Ayanamshas.DeLuce => new AyanamshaDetails(ayanamsha, 2, "ref.ayanamsha.deluce"),
            Ayanamshas.Raman => new AyanamshaDetails(ayanamsha, 3, "ref.ayanamsha.raman"),
            Ayanamshas.UshaShashi => new AyanamshaDetails(ayanamsha, 4, "ref.ayanamsha.ushashashi"),
            Ayanamshas.Krishnamurti => new AyanamshaDetails(ayanamsha, 5, "ref.ayanamsha.krishnamurti"),
            Ayanamshas.DjwhalKhul => new AyanamshaDetails(ayanamsha, 6, "ref.ayanamsha.djwhalkhul"),
            Ayanamshas.Yukteshwar => new AyanamshaDetails(ayanamsha, 7, "ref.ayanamsha.yukteshwar"),
            Ayanamshas.Bhasin => new AyanamshaDetails(ayanamsha, 8, "ref.ayanamsha.bhasin"),
            Ayanamshas.Kugler1 => new AyanamshaDetails(ayanamsha, 9, "ref.ayanamsha.kugler1"),
            Ayanamshas.Kugler2 => new AyanamshaDetails(ayanamsha, 10, "ref.ayanamsha.kugler2"),
            Ayanamshas.Kugler3 => new AyanamshaDetails(ayanamsha, 11, "ref.ayanamsha.kugler3"),
            Ayanamshas.Huber => new AyanamshaDetails(ayanamsha, 12, "ref.ayanamsha.huber"),
            Ayanamshas.EtaPiscium => new AyanamshaDetails(ayanamsha, 13, "ref.ayanamsha.etapiscium"),
            Ayanamshas.Aldebaran15Tau => new AyanamshaDetails(ayanamsha, 14, "ref.ayanamsha.aldebaran15tau"),
            Ayanamshas.Hipparchus => new AyanamshaDetails(ayanamsha, 15, "ref.ayanamsha.hipparchus"),
            Ayanamshas.Sassanian => new AyanamshaDetails(ayanamsha, 16, "ref.ayanamsha.sassanian"),
            Ayanamshas.GalactCtr0Sag => new AyanamshaDetails(ayanamsha, 17, "ref.ayanamsha.galcent0sag"),
            Ayanamshas.J2000 => new AyanamshaDetails(ayanamsha, 18, "ref.ayanamsha.j2000"),
            Ayanamshas.J1900 => new AyanamshaDetails(ayanamsha, 19, "ref.ayanamsha.j1900"),
            Ayanamshas.B1950 => new AyanamshaDetails(ayanamsha, 20, "ref.ayanamsha.b1950"),
            Ayanamshas.SuryaSiddhanta => new AyanamshaDetails(ayanamsha, 21, "ref.ayanamsha.suryasiddhanta"),
            Ayanamshas.SuryaSiddhantaMeanSun => new AyanamshaDetails(ayanamsha, 22, "ref.ayanamsha.suryasiddhantameansun"),
            Ayanamshas.Aryabhata => new AyanamshaDetails(ayanamsha, 23, "ref.ayanamsha.aryabhata"),
            Ayanamshas.AryabhataMeanSun => new AyanamshaDetails(ayanamsha, 24, "ref.ayanamsha.aryabhatameansun"),
            Ayanamshas.SsRevati => new AyanamshaDetails(ayanamsha, 25, "ref.ayanamsha.ssrevati"),
            Ayanamshas.SsCitra => new AyanamshaDetails(ayanamsha, 26, "ref.ayanamsha.sscitra"),
            Ayanamshas.TrueCitra => new AyanamshaDetails(ayanamsha, 27, "ref.ayanamsha.truecitrapaksha"),
            Ayanamshas.TrueRevati => new AyanamshaDetails(ayanamsha, 28, "ref.ayanamsha.truerevati"),
            Ayanamshas.TruePushya => new AyanamshaDetails(ayanamsha, 29, "ref.ayanamsha.truepushya"),
            Ayanamshas.GalacticCtrBrand => new AyanamshaDetails(ayanamsha, 30, "ref.ayanamsha.galcentbrand"),
            Ayanamshas.GalacticEqIau1958 => new AyanamshaDetails(ayanamsha, 31, "ref.ayanamsha.galcentiau1958"),
            Ayanamshas.GalacticEq => new AyanamshaDetails(ayanamsha, 32, "ref.ayanamsha.galequator"),
            Ayanamshas.GalacticEqMidMula => new AyanamshaDetails(ayanamsha, 33, "ref.ayanamsha.galequatormidmula"),
            Ayanamshas.Skydram => new AyanamshaDetails(ayanamsha, 34, "ref.ayanamsha.skydram"),
            Ayanamshas.TrueMula => new AyanamshaDetails(ayanamsha, 35, "ref.ayanamsha.truemula"),
            Ayanamshas.Dhruva => new AyanamshaDetails(ayanamsha, 36, "ref.ayanamsha.dhruva"),
            Ayanamshas.Aryabhata522 => new AyanamshaDetails(ayanamsha, 37, "ref.ayanamsha.aryabhata522"),
            Ayanamshas.Britton => new AyanamshaDetails(ayanamsha, 38, "ref.ayanamsha.britton"),
            Ayanamshas.GalacticCtrOCap => new AyanamshaDetails(ayanamsha, 39, "ref.ayanamsha.galcent0cap"),
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
