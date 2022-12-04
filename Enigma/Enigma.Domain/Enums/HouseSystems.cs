// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Domain.Enums;


/// <summary>
/// Supported house systems.
/// </summary>
public enum HouseSystems
{
    NoHouses = 0, Placidus = 1, Koch = 2, Porphyri = 3, Regiomontanus = 4, Campanus = 5, Alcabitius = 6, TopoCentric = 7, Krusinski = 8, Apc = 9, Morin = 10,
    WholeSign = 11, EqualAsc = 12, EqualMc = 13, EqualAries = 14, Vehlow = 15, Axial = 16, Horizon = 17, Carter = 18, Gauquelin = 19, SunShine = 20, SunShineTreindl = 21
}

public static class HouseSystemsExtensions
{
    /// <summary>Retrieve details for house systems.</summary>
    /// <param name="system">The house system, is automatically filled.</param>
    /// <returns>Details for the house system.</returns>
    public static HouseSystemDetails GetDetails(this HouseSystems system)
    {
        return system switch
        {
            HouseSystems.NoHouses => new HouseSystemDetails(system, true, 'W', 0, false, false, "ref.enum.housesystemnohouses"),
            HouseSystems.Placidus => new HouseSystemDetails(system, true, 'P', 12, true, true, "ref.enum.housesystemplacidus"),
            HouseSystems.Koch => new HouseSystemDetails(system, true, 'K', 12, true, true, "ref.enum.housesystemkoch"),
            HouseSystems.Porphyri => new HouseSystemDetails(system, true, 'O', 12, true, true, "ref.enum.housesystemporphyri"),
            HouseSystems.Regiomontanus => new HouseSystemDetails(system, true, 'R', 12, true, true, "ref.enum.housesystemregiomontanus"),
            HouseSystems.Campanus => new HouseSystemDetails(system, true, 'C', 12, true, true, "ref.enum.housesystemcampanus"),
            HouseSystems.Alcabitius => new HouseSystemDetails(system, true, 'B', 12, true, true, "ref.enum.housesystemalcabitius"),
            HouseSystems.TopoCentric => new HouseSystemDetails(system, true, 'T', 12, true, true, "ref.enum.housesystemtopocentric"),
            HouseSystems.Krusinski => new HouseSystemDetails(system, true, 'U', 12, true, true, "ref.enum.housesystemkrusinski"),
            HouseSystems.Apc => new HouseSystemDetails(system, true, 'Y', 12, true, true, "ref.enum.housesystemapc"),
            HouseSystems.Morin => new HouseSystemDetails(system, true, 'M', 12, true, false, "ref.enum.housesystemmorin"),
            HouseSystems.WholeSign => new HouseSystemDetails(system, true, 'W', 12, true, false, "ref.enum.housesystemwholesign"),
            HouseSystems.EqualAsc => new HouseSystemDetails(system, true, 'A', 12, true, false, "ref.enum.housesystemequalasc"),
            HouseSystems.EqualMc => new HouseSystemDetails(system, true, 'D', 12, true, false, "ref.enum.housesystemequalmc"),
            HouseSystems.EqualAries => new HouseSystemDetails(system, true, 'N', 12, true, false, "ref.enum.housesystemequalaries"),
            HouseSystems.Vehlow => new HouseSystemDetails(system, true, 'V', 12, true, false, "ref.enum.housesystemvehlow"),
            HouseSystems.Axial => new HouseSystemDetails(system, true, 'X', 12, true, false, "ref.enum.housesystemaxial"),
            HouseSystems.Horizon => new HouseSystemDetails(system, true, 'H', 12, true, false, "ref.enum.housesystemhorizon"),
            HouseSystems.Carter => new HouseSystemDetails(system, true, 'F', 12, true, false, "ref.enum.housesystemcarter"),
            HouseSystems.Gauquelin => new HouseSystemDetails(system, true, 'G', 36, true, false, "ref.enum.housesystemgauquelin"),
            HouseSystems.SunShine => new HouseSystemDetails(system, true, 'i', 12, true, false, "ref.enum.housesystemsunshine"),
            HouseSystems.SunShineTreindl => new HouseSystemDetails(system, true, 'I', 12, true, false, "ref.enum.housesystemsunshinetreindl"),
            _ => new HouseSystemDetails(system, true, 'W', 0, false, false, "ref.enum.housesystemnohouses")
        };
    }

    public static HouseSystems HouseSystemForIndex(this HouseSystems system, int index)
    {
        foreach (HouseSystems houseSystem in Enum.GetValues(typeof(HouseSystems)))
        {
            if ((int)houseSystem == index) return houseSystem;
        }
        throw new ArgumentException("Could not find HouseSystem for index : " + index);
    }

    public static List<HouseSystemDetails> AllDetails(this HouseSystems system)
    {
        var allDetails = new List<HouseSystemDetails>();
        foreach (HouseSystems houseSystem in Enum.GetValues(typeof(HouseSystems)))
        {
            allDetails.Add(houseSystem.GetDetails());
        }
        return allDetails;
    }


}



/// <summary>Details for a house system.</summary>
/// <param name="HouseSystem">The house system.</param>
/// <param name="SeSupported">True if the house system is supported by the Swiss Ephyemeris.</param>
/// <param name="SeId">A character that identifies the house system for the Swiss Ephemeris. If SeSuported = false, SeId will have the value 0 and is ignored.</param>
/// <param name="NrOfCusps">Number of cusps for this house system.</param>
/// <param name="CounterClockWise">True if the cusps are counterclockwise, otherwise false.</param>
/// <param name="QuadrantSystem">True if the system is a quadrant system (Asc. = cusp 1, MC = cusp 10).</param>
/// <param name="TextId">Id to find a descriptive text in a resource bundle.</param>
public record HouseSystemDetails(HouseSystems HouseSystem, bool SeSupported, char SeId, int NrOfCusps, bool CounterClockWise, bool QuadrantSystem, string TextId);

