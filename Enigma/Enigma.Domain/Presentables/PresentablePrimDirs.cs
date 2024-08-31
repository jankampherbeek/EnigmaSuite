// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Domain.Presentables;

/// <summary>Results of primary directions to be shown in a datagrid.</summary>
/// <param name="Date">Date of exactness.</param>
/// <param name="Promissor">Glyph for the promissor.</param>
/// <param name="Significator">Glyph for the significator.</param>
/// <param name="Aspect">Glyph for the aspect.</param>
/// <param name="DirectConverse">Indication for direct/converse.</param>
public record PresentablePrimDirs(string Date, char Promissor, char Aspect, char Significator,  string DirectConverse);