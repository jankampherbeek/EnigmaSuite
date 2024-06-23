// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Domain.Dtos;

namespace Enigma.Domain.Charts.Prog.PrimDir;

/// <summary>Response for the calculation of primary directions.</summary>
/// <param name="Hits">All formed directions.</param>
/// <param name="Errors">True if errors occurred, otherwise false.</param>
/// <param name="ResultText">Description of the result, typically an error text or "OK".</param>
public record PrimDirResponse(List<PrimDirHit> Hits, bool Errors, string ResultText);