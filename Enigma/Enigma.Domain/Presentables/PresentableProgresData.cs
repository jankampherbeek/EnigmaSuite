// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Domain.Presentables;

/// <summary>Event data to be shown in a data grid.</summary>
/// <param name="DateType">Type of date, e.g. 'p' for period or 'e' for event.</param>
/// <param name="Id">Unique id for the event.</param>
/// <param name="Description">Descriptive text.</param>
public record PresentableProgresData(string DateType, string Id, string Description);





