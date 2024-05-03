// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Dtos;
using Enigma.Domain.References;

namespace Enigma.Domain.Requests;


/// <summary>Data for a request to calculate secondary directions.</summary>
/// <param name="JdRadix">Julian day for the radix.</param>
/// <param name="JdEvent">Julian day for the event.</param>
/// <param name="Location">Location (only latitude and longitude are used).</param>
/// <param name="ConfigSecDir">User preferences for the calculation of secondary directions.</param>
/// <param name="Ayanamsha">The Ayanamsha as defined in the user preferences, 'None' for tropical positions.</param>
/// <param name="ObserverPos">The observer position as defined in the user preferences.</param>
public record SecDirEventRequest(double JdRadix,  double JdEvent, Location? Location, ConfigProgSecDir ConfigSecDir,
    Ayanamshas Ayanamsha, ObserverPositions ObserverPos);

