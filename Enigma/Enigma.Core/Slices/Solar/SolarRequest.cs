// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Dtos;

namespace Enigma.Core.Slices.Solar;

/// <summary>
/// Request for the calculation of a solar
/// </summary>
/// <param name="JdRadix">Julian day in the radix</param>
/// <param name="Age">Age in years</param>
/// <param name="CalculationPreferences">Details for the calculation</param>
/// <param name="SiderealReturn">True if a sidereal return should be used, false for a tropical return</param> 
/// <param name="RadixLocation">Location for radix</param>
/// <param name="RelocateLocation">Optional location for relocation</param>
public record SolarRequest(double JdRadix, int Age, bool SiderealReturn, CalculationPreferences CalculationPreferences, 
    Location RadixLocation, Location? RelocateLocation);