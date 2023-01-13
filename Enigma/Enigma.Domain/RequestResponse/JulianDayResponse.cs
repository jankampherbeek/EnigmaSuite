// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Domain.RequestResponse;

/// <summary>Response for the calculation of a Julian day number.</summary>
/// <param name="JulDayUt">Julian day for Universal time</param>
/// <param name="JulDayEt">Julian day for Ephemeris time.</param>
/// <param name="DeltaT">Value for delta T.</param>
public record JulianDayResponse(double JulDayUt, double JulDayEt, double DeltaT);
