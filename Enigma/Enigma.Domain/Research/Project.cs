// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Domain.Research;

/// <summary>Representation of ResearchProject. Also used for persistency with csv</summary>
public record ResearchProject(
    int id,
    string Name,
    string Description,
    int IndexDataFile,
    string CreationDate,
    int IndexControlGroupType,
    int ControlGroupMultiplication);
