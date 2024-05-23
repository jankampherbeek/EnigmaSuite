// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Serilog;

namespace Enigma.Domain.References;


/// <summary>
/// Supported house systems.
/// </summary>
public enum HouseSystems
{
    NoHouses = 0, Placidus = 1, Koch = 2, Porphyri = 3, Regiomontanus = 4, Campanus = 5, Alcabitius = 6, 
    TopoCentric = 7, Krusinski = 8, Apc = 9, Morin = 10, WholeSign = 11, EqualAsc = 12, EqualMc = 13, EqualAries = 14, 
    Vehlow = 15, Axial = 16, Horizon = 17, Carter = 18, Gauquelin = 19, SunShine = 20, SunShineTreindl = 21,
    PullenSd = 22, PullenSr = 23, Sripati = 24

}

/// <summary>Details for a house system.</summary>
/// <param name="HouseSystem">The house system.</param>
/// <param name="SeSupported">True if the house system is supported by the Swiss Ephemeris</param>
/// <param name="SeId">A character that identifies the house system for the Swiss Ephemeris.
/// If SeSuported = false, SeId will have the value 0 and is ignored.</param>
/// <param name="NrOfCusps">Number of cusps for this house system.</param>
/// <param name="CounterClockWise">True if the cusps are counterclockwise, otherwise false.</param>
/// <param name="QuadrantSystem">True if the system is a quadrant system (Asc. = cusp 1, MC = cusp 10).</param>
/// <param name="RbKey">Key for name of this house system in resource bundle.</param>
public record HouseSystemDetails(HouseSystems HouseSystem, bool SeSupported, char SeId, int NrOfCusps, 
    bool CounterClockWise, bool QuadrantSystem, string RbKey);


/// <summary>Extension class for the enum HouseSystems.</summary>
public static class HouseSystemsExtensions
{
    /// <summary>Retrieve details for house systems.</summary>
    /// <param name="system">The house system, is automatically filled.</param>
    /// <returns>Details for the house system.</returns>
    public static HouseSystemDetails GetDetails(this HouseSystems system)
    {
        return system switch
        {
            HouseSystems.NoHouses => new HouseSystemDetails(system, true, 'W', 0, false, false, "ref_housesys_nohouses"),
            HouseSystems.Placidus => new HouseSystemDetails(system, true, 'P', 12, true, true, "ref_housesys_placidus"),
            HouseSystems.Koch => new HouseSystemDetails(system, true, 'K', 12, true, true, "ref_housesys_koch"),
            HouseSystems.Porphyri => new HouseSystemDetails(system, true, 'O', 12, true, true, "ref_housesys_porphyri"),
            HouseSystems.Regiomontanus => new HouseSystemDetails(system, true, 'R', 12, true, true, "ref_housesys_regiomontanus"),
            HouseSystems.Campanus => new HouseSystemDetails(system, true, 'C', 12, true, true, "ref_housesys_campanus"),
            HouseSystems.Alcabitius => new HouseSystemDetails(system, true, 'B', 12, true, true, "ref_housesys_alcabitius"),
            HouseSystems.TopoCentric => new HouseSystemDetails(system, true, 'T', 12, true, true, "ref_housesys_topocentric"),
            HouseSystems.Krusinski => new HouseSystemDetails(system, true, 'U', 12, true, true, "ref_housesys_krusinski"),
            HouseSystems.Apc => new HouseSystemDetails(system, true, 'Y', 12, true, true, "ref_housesys_apc"),
            HouseSystems.Morin => new HouseSystemDetails(system, true, 'M', 12, true, false, "ref_housesys_morin"),
            HouseSystems.WholeSign => new HouseSystemDetails(system, true, 'W', 12, true, false, "ref_housesys_wholesign"),
            HouseSystems.EqualAsc => new HouseSystemDetails(system, true, 'A', 12, true, false, "ref_housesys_equalasc"),
            HouseSystems.EqualMc => new HouseSystemDetails(system, true, 'D', 12, true, false, "ref_housesys_equalmc"),
            HouseSystems.EqualAries => new HouseSystemDetails(system, true, 'N', 12, true, false, "ref_housesys_equalaries"),
            HouseSystems.Vehlow => new HouseSystemDetails(system, true, 'V', 12, true, false, "ref_housesys_vehlow"),
            HouseSystems.Axial => new HouseSystemDetails(system, true, 'X', 12, true, false, "ref_housesys_axial"),
            HouseSystems.Horizon => new HouseSystemDetails(system, true, 'H', 12, true, false, "ref_housesys_horizon"),
            HouseSystems.Carter => new HouseSystemDetails(system, true, 'F', 12, true, false, "ref_housesys_carter"),
            HouseSystems.Gauquelin => new HouseSystemDetails(system, true, 'G', 36, true, false, "ref_housesys_gauquelin"),
            HouseSystems.SunShine => new HouseSystemDetails(system, true, 'i', 12, true, false, "ref_housesys_sunshine"),
            HouseSystems.SunShineTreindl => new HouseSystemDetails(system, true, 'I', 12, true, false, "ref_housesys_sunshinetreindl"),
            HouseSystems.PullenSd => new HouseSystemDetails(system, true, 'L', 12, true, true, "ref_housesys_pullensd"),
            HouseSystems.PullenSr => new HouseSystemDetails(system, true, 'Q', 12, true, true, "ref_housesys_pullensr"),
            HouseSystems.Sripati => new HouseSystemDetails(system, true, 'S', 12, true, false, "ref_housesys_sripati"),
            _ => new HouseSystemDetails(system, true, 'W', 0, false, false, "ref_housesys_nohouses")
        };
    }
   
    
    /// <summary>Find house system for a given index.</summary>
    /// <param name="index">The index.</param>
    /// <returns>The house system</returns>
    /// <exception cref="ArgumentException">Thrown if the house system could not be found.</exception>
    public static HouseSystems HouseSystemForIndex(int index)
    {
        foreach (HouseSystems houseSystem in Enum.GetValues(typeof(HouseSystems)))
        {
            if ((int)houseSystem == index) return houseSystem;
        }
        Log.Error("HouseSystems.HouseSystemForIndex(): Could not find HouseSystem for index : {Index}", index);
        throw new ArgumentException("No house system found for given index");
    }

    /// <summary>Creates list with all details for house systems.</summary>
    /// <returns>All details.</returns>
    public static List<HouseSystemDetails> AllDetails()
    {
        return (from HouseSystems houseSystem in Enum.GetValues(typeof(HouseSystems)) 
            select houseSystem.GetDetails()).ToList();
    }


}


