// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System;

namespace E4C.domain.shared.references;


/// <summary>
/// Supported house systems.
/// </summary>
public enum HouseSystems
{
    NoHouses, Placidus, Koch, Porphyri, Regiomontanus, Campanus, Alcabitius, TopoCentric, Krusinski, Apc, Morin,
    WholeSign, EqualAsc, EqualMc, EqualAries, Vehlow, Axial, Horizon, Carter, Gauquelin, SunShine, SunShineTreindl
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
    /// Identification of a dscriptive test in a resource bundle.</summary>
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

/// <summary>
/// Specifications for the different housesystems.
/// </summary>
public interface IHouseSystemSpecifications
{
    /// <summary>
    /// Returns the specification for a house system.
    /// </summary>
    /// <param name="houseSystem">The house system, from the enum HouseSystems.</param>
    /// <returns>A record HouseSystemDetails with the specification of the house system.</returns>
    public HouseSystemDetails DetailsForHouseSystem(HouseSystems houseSystem);
}

/// <inheritdoc/>
public class HouseSystemSpecifications : IHouseSystemSpecifications
{
    /// <exception cref="ArgumentException">Is thrown if the house system was not recognized.</exception>
    HouseSystemDetails IHouseSystemSpecifications.DetailsForHouseSystem(HouseSystems houseSystem)
    {
        return houseSystem switch
        {
            HouseSystems.NoHouses => new HouseSystemDetails(houseSystem, true, 'W', 0, false, false, "houseSystemNoHouses"),
            HouseSystems.Placidus => new HouseSystemDetails(houseSystem, true, 'P', 12, true, true, "houseSystemPlacidus"),
            HouseSystems.Koch => new HouseSystemDetails(houseSystem, true, 'K', 12, true, true, "houseSystemKoch"),
            HouseSystems.Porphyri => new HouseSystemDetails(houseSystem, true, 'O', 12, true, true, "houseSystemPorphyri"),
            HouseSystems.Regiomontanus => new HouseSystemDetails(houseSystem, true, 'R', 12, true, true, "houseSystemRegiomontanus"),
            HouseSystems.Campanus => new HouseSystemDetails(houseSystem, true, 'C', 12, true, true, "houseSystemCampanus"),
            HouseSystems.Alcabitius => new HouseSystemDetails(houseSystem, true, 'B', 12, true, true, "houseSystemAlcabitius"),
            HouseSystems.TopoCentric => new HouseSystemDetails(houseSystem, true, 'T', 12, true, true, "houseSystemTopoCentric"),
            HouseSystems.Krusinski => new HouseSystemDetails(houseSystem, true, 'U', 12, true, true, "houseSystemKrusinski"),
            HouseSystems.Apc => new HouseSystemDetails(houseSystem, true, 'Y', 12, true, true, "houseSystemApc"),
            HouseSystems.Morin => new HouseSystemDetails(houseSystem, true, 'M', 12, true, false, "houseSystemMorin"),
            HouseSystems.WholeSign => new HouseSystemDetails(houseSystem, true, 'W', 12, true, false, "houseSystemWholeSign"),
            HouseSystems.EqualAsc => new HouseSystemDetails(houseSystem, true, 'A', 12, true, false, "houseSystemEqualAsc"),
            HouseSystems.EqualMc => new HouseSystemDetails(houseSystem, true, 'D', 12, true, false, "houseSystemEqualMc"),
            HouseSystems.EqualAries => new HouseSystemDetails(houseSystem, true, 'N', 12, true, false, "houseSystemEqualAries"),
            HouseSystems.Vehlow => new HouseSystemDetails(houseSystem, true, 'V', 12, true, false, "houseSystemVehlow"),
            HouseSystems.Axial => new HouseSystemDetails(houseSystem, true, 'X', 12, true, false, "houseSystemAxial"),
            HouseSystems.Horizon => new HouseSystemDetails(houseSystem, true, 'H', 12, true, false, "houseSystemHorizon"),
            HouseSystems.Carter => new HouseSystemDetails(houseSystem, true, 'F', 12, true, false, "houseSystemCarter"),
            HouseSystems.Gauquelin => new HouseSystemDetails(houseSystem, true, 'G', 36, true, false, "houseSystemGauquelin"),
            HouseSystems.SunShine => new HouseSystemDetails(houseSystem, true, 'i', 12, true, false, "houseSystemSunShine"),
            HouseSystems.SunShineTreindl => new HouseSystemDetails(houseSystem, true, 'I', 12, true, false, "houseSystemSunShineTreindl"),
            _ => throw new ArgumentException("House system type unknown : " + houseSystem.ToString())
        };
    }
}
