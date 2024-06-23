// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.References;

namespace Enigma.Domain.Dtos;

/// <summary>Specification for a primary direction 'hit', an exactly formed direction.</summary>
/// <param name="Jd">Julian day number.</param>
/// <param name="DateTxt">Textual presetnationof date, format yyyy/mm/dd</param>
/// <param name="Significator">The significator.</param>
/// <param name="Promissor">The promissor.</param>
/// <param name="Aspect">Aspect, if no aspects are used always conjunction.</param>
public record PrimDirHit(double Jd, string DateTxt, ChartPoints Significator, ChartPoints Promissor, AspectTypes Aspect);
