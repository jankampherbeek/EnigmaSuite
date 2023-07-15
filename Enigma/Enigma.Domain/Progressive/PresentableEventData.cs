// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Domain.Progressive;

/// <summary>Event data to be shown in a data grid.</summary>
/// <param name="Id">Unique id for the chart.</param>
/// <param name="Description">Descriptive text, can be empty.</param>
public record PresentableEventData(string Id, string Description);