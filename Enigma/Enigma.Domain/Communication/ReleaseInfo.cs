// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Domain.Communication;

/// <summary>INfo about the latest available release for Enigma.</summary>
/// <remarks>Should be compatible with JSON in the following format:
/// <pre>
/// {"Version":"0.1.0",
/// "ReleaseDate":"2023/06/01",
/// "DescriptionEn":"Initial release",
/// "DescroptionNl":"Eerste release",
/// "Download":"https://radixpro.com/enigma/install.exe"} 
/// </pre>
/// </remarks>
public record ReleaseInfo(string Version, string ReleaseDate, string DescriptionEn, string DescriptionNl, string Download);
/*{
    /// <summary>Version number in format major.minor.fix</summary>
    public string Version { get; set; }
    
    /// <summary>Date of release in the format yyyy/mm/dd</summary>
    public string ReleaseDate { get; set; }

    /// <summary>Short description of release in English.</summary>
    public string DescriptionEn { get; set; }

    /// <summary>Short description of release in Dutch.</summary>
    public string DescriptionNl { get; set; }   

    /// <summary>Full download link for this release.</summary>
    public string Download { get; set; }    

}
*/
