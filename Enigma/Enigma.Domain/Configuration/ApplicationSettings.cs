// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Domain.Configuration;

/// <summary>Settings for application.</summary>
/// <remarks>Implemented as singleton, based on code by Jon Skeet: https://csharpindepth.com/articles/singleton .</remarks>
public class ApplicationSettings
{
    public string LocationEnigmaRoot { get; } = @"c:\enigma_ar";
    public string LocationDataFiles { get; } = @"c:\enigma_ar\data";
    public string LocationProjectFiles { get; set; } = @"c:\enigma_ar\project";
    public string LocationExportFiles { get; } = @"c:\enigma_ar\export";
    public string LocationLogFiles { get; } = @"c:\enigma_ar\logs";
    public string LocationDatabase { get; } = @"c:\enigma_ar\database";


    // Explicit static constructor to tell C# compiler not to mark type as beforefieldinit
    static ApplicationSettings()
    {

    }

    private ApplicationSettings()
    {
    }

    public static ApplicationSettings Instance { get; } = new();
}