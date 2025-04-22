// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Domain.References;

namespace Enigma.Domain.Research;

/// <summary>Representation of ResearchProject. Also used for persistency with csv</summary>
public record ResearchProject(
    string Name,
    string Description,
    int IndexDataFile,
    string CreationDate,
    ControlGroupTypes ControlGroupType,
    int ControlGroupMultiplication);
