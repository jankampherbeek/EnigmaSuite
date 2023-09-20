// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Domain.Dtos;

/// <summary>Info about the latest available release for Enigma.</summary>
/// <remarks>Should be compatible with JSON in the following format:
/// <pre>
/// {"Version":"0.1.0",
/// "ReleaseDate":"2023/04/25",
/// "DescriptionEn":"Initial release",
/// "DescriptionNl":"Eerste release",
/// "Download":"https://radixpro.com/enigma/download/Enigma AR installation.exe"} 
/// </pre>
/// </remarks>
public record ReleaseInfo(string Version, string ReleaseDate, string DescriptionEn, string DescriptionNl, string Download);

