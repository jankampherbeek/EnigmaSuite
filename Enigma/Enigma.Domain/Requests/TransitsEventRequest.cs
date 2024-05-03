// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Dtos;
using Enigma.Domain.References;

namespace Enigma.Domain.Requests;

/// <summary>Data for a request to calculate transits.</summary>
/// <param name="JulianDayUt">Julian day for universal time.</param>
/// <param name="Location">Location (only latitude and longitude are used).</param>
/// <param name="ConfigTransits">User preferences for the calculation of transits.</param>
/// <param name="Ayanamsha">The Ayanamsha as defined in the user preferences, 'None' for tropical positions.</param>
/// <param name="ObserverPos">The observer position as defined in the user preferences.</param>
public record TransitsEventRequest(double JulianDayUt, Location? Location, ConfigProgTransits ConfigTransits,
    Ayanamshas Ayanamsha, ObserverPositions ObserverPos);

