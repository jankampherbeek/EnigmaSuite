// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Domain.Dtos;

/// <summary>System defined settings for application.</summary>
/// <remarks>Implemented as singleton, based on code by Jon Skeet: https://csharpindepth.com/articles/singleton .</remarks>
public class ApplicationSettings
{

    private static readonly string BaseDirectory = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
        "enigma"
    );
    public static string LocationEnigmaRoot => BaseDirectory;
    public static string LocationExportFiles => Path.Combine(BaseDirectory, "export");
    public static string LocationDatabase => Path.Combine(BaseDirectory, "database");
    public static string LocationDocs => Path.Combine(BaseDirectory, "docs");

    public static string WorkFolder = "[Your workfolder]";

    /// <summary>Location of deltas for configuration file, contains path and filename.</summary>
    public static string ConfigDeltaLocation = LocationEnigmaRoot + Path.DirectorySeparatorChar + "enigmacfgdelta.json";
    /// <summary>Location of deltas for configuration file for rogressions, contains path and filename.</summary>
    public static string ConfigProgDeltaLocation =  LocationEnigmaRoot + Path.DirectorySeparatorChar + "enigmaprogcfgdelta.json";
    
    public static void SetWorkFolder(string wfName)
    {
        WorkFolder = wfName;
    }
    
    public static string LocationDataFiles => Path.Combine(WorkFolder!, "data");
    public static string LocationProjectFiles => Path.Combine(WorkFolder!, "project");
    public static string LocationLogFiles => Path.Combine(WorkFolder!, "logs");
    
    // Explicit static constructor to tell C# compiler not to mark type as beforefieldinit
    static ApplicationSettings()
    {

    }

    private ApplicationSettings()
    {
    }

    public static ApplicationSettings Instance { get; } = new();
}

