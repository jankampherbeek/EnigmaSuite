// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Interfaces;

namespace Enigma.Domain.Enums;


/// <summary>
/// Supported house systems.
/// </summary>
public enum HouseSystems
{
    NoHouses = 0, Placidus = 1, Koch = 2, Porphyri = 3, Regiomontanus = 4, Campanus = 5, Alcabitius = 6, TopoCentric = 7, Krusinski = 8, Apc = 9, Morin = 10,
    WholeSign = 11, EqualAsc = 12, EqualMc = 13, EqualAries = 14, Vehlow = 15, Axial = 16, Horizon = 17, Carter = 18, Gauquelin = 19, SunShine = 20, SunShineTreindl = 21


}

/// <summary>
/// Specifications for a house system.
/// </summary>
public record HouseSystemDetails
{
    readonly public HouseSystems HouseSystem;
    readonly public bool SeSupported;
    readonly public char SeId;
    readonly public int NrOfCusps;
    readonly public bool CounterClockWise;
    readonly public bool QuadrantSystem;
    /// <summary>Identification of a descriptive text in a resource bundle.</summary>
    readonly public string TextId;

    /// <summary>
    /// Constructor for the details of a house system.
    /// </summary>
    /// <param name="houseSystem">The house system.</param>
    /// <param name="seSupported">True if the house system is supported by the Swiss Ephyemeris.</param>
    /// <param name="seId">A character that identifies the house system for the Swiss Ephemeris. If SeSuported = false, SeId will have the value 0 and is ignored.</param>
    /// <param name="nrOfCusps">Number of cusps for this house system.</param>
    /// <param name="counterClockWise">True if the cusps are counterclockwise, otherwise false.</param>
    /// <param name="quadrantSystem">True if the system is a quadrant system (Asc. = cusp 1, MC = cusp 10).</param>
    /// <param name="textId">Id to find a descriptive text in a resource bundle.</param>
    public HouseSystemDetails(HouseSystems houseSystem, bool seSupported, char seId, int nrOfCusps, bool counterClockWise, bool quadrantSystem, string textId)
    {
        HouseSystem = houseSystem;
        SeSupported = seSupported;
        SeId = seId;
        NrOfCusps = nrOfCusps;
        CounterClockWise = counterClockWise;
        QuadrantSystem = quadrantSystem;
        TextId = textId;
    }
}


/// <inheritdoc/>
public class HouseSystemSpecifications : IHouseSystemSpecifications
{

    /// <exception cref="ArgumentException">Is thrown if the house system was not recognized.</exception>
    public HouseSystemDetails DetailsForHouseSystem(HouseSystems houseSystem)
    {
        return houseSystem switch
        {
            HouseSystems.NoHouses => new HouseSystemDetails(houseSystem, true, 'W', 0, false, false, "ref.enum.housesystemnohouses"),
            HouseSystems.Placidus => new HouseSystemDetails(houseSystem, true, 'P', 12, true, true, "ref.enum.housesystemplacidus"),
            HouseSystems.Koch => new HouseSystemDetails(houseSystem, true, 'K', 12, true, true, "ref.enum.housesystemkoch"),
            HouseSystems.Porphyri => new HouseSystemDetails(houseSystem, true, 'O', 12, true, true, "ref.enum.housesystemporphyri"),
            HouseSystems.Regiomontanus => new HouseSystemDetails(houseSystem, true, 'R', 12, true, true, "ref.enum.housesystemregiomontanus"),
            HouseSystems.Campanus => new HouseSystemDetails(houseSystem, true, 'C', 12, true, true, "ref.enum.housesystemcampanus"),
            HouseSystems.Alcabitius => new HouseSystemDetails(houseSystem, true, 'B', 12, true, true, "ref.enum.housesystemalcabitius"),
            HouseSystems.TopoCentric => new HouseSystemDetails(houseSystem, true, 'T', 12, true, true, "ref.enum.housesystemtopocentric"),
            HouseSystems.Krusinski => new HouseSystemDetails(houseSystem, true, 'U', 12, true, true, "ref.enum.housesystemkrusinski"),
            HouseSystems.Apc => new HouseSystemDetails(houseSystem, true, 'Y', 12, true, true, "ref.enum.housesystemapc"),
            HouseSystems.Morin => new HouseSystemDetails(houseSystem, true, 'M', 12, true, false, "ref.enum.housesystemmorin"),
            HouseSystems.WholeSign => new HouseSystemDetails(houseSystem, true, 'W', 12, true, false, "ref.enum.housesystemwholesign"),
            HouseSystems.EqualAsc => new HouseSystemDetails(houseSystem, true, 'A', 12, true, false, "ref.enum.housesystemequalasc"),
            HouseSystems.EqualMc => new HouseSystemDetails(houseSystem, true, 'D', 12, true, false, "ref.enum.housesystemequalmc"),
            HouseSystems.EqualAries => new HouseSystemDetails(houseSystem, true, 'N', 12, true, false, "ref.enum.housesystemequalaries"),
            HouseSystems.Vehlow => new HouseSystemDetails(houseSystem, true, 'V', 12, true, false, "ref.enum.housesystemvehlow"),
            HouseSystems.Axial => new HouseSystemDetails(houseSystem, true, 'X', 12, true, false, "ref.enum.housesystemaxial"),
            HouseSystems.Horizon => new HouseSystemDetails(houseSystem, true, 'H', 12, true, false, "ref.enum.housesystemhorizon"),
            HouseSystems.Carter => new HouseSystemDetails(houseSystem, true, 'F', 12, true, false, "ref.enum.housesystemcarter"),
            HouseSystems.Gauquelin => new HouseSystemDetails(houseSystem, true, 'G', 36, true, false, "ref.enum.housesystemgauquelin"),
            HouseSystems.SunShine => new HouseSystemDetails(houseSystem, true, 'i', 12, true, false, "ref.enum.housesystemsunshine"),
            HouseSystems.SunShineTreindl => new HouseSystemDetails(houseSystem, true, 'I', 12, true, false, "ref.enum.housesystemsunshinetreindl"),
            _ => throw new ArgumentException("House system type unknown : " + houseSystem.ToString())
        };
    }

    public List<HouseSystemDetails> AllHouseSystemDetails()
    {
        var allDetails = new List<HouseSystemDetails>();
        foreach (HouseSystems houseSystem in Enum.GetValues(typeof(HouseSystems)))
        {
            allDetails.Add(DetailsForHouseSystem(houseSystem));
        }
        return allDetails;
    }

    public HouseSystems HouseSystemForIndex(int index)
    {
        foreach (HouseSystems houseSystem in Enum.GetValues(typeof(HouseSystems)))
        {
            if ((int)houseSystem == index) return houseSystem;
        }
        throw new ArgumentException("Could not find HouseSystem for index : " + index);
    }

}
