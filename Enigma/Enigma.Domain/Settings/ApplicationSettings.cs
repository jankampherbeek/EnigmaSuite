// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain;

namespace Enigma.Domain.Settings;

/// <summary>Settings for application.</summary>
/// <remarks>Implemented as singleton, based on code by Jon Skeet: https://csharpindepth.com/articles/singleton .</remarks>
public class ApplicationSettings
{

    private static readonly ApplicationSettings instance = new();

    public string LocationDataFiles { get; set; } = @"c:\enigma\data";
    public string LocationProjectFiles { get; set; } = @"c:\enigma\project";
    public string LocationExportFiles { get; set; } = @"c:\enigma\export";
    public string LocationSeFiles { get; set; } = @"c:\enigma\se";



    // Explicit static constructor to tell C# compiler not to mark type as beforefieldinit
    static ApplicationSettings()
    {
        
    }

    private ApplicationSettings()
    {
    }

    public static ApplicationSettings Instance
    {
        get
        {
            return instance;
        }
    }

}