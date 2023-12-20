// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Frontend.Ui.State;

public class DataVaultGeneral
{
    private static readonly DataVaultGeneral instance = new();
    
    // Explicit static constructor to tell C# compiler not to mark type as beforefieldinit
    static DataVaultGeneral()
    {
        
    }

    private DataVaultGeneral()
    {
    }

    // ReSharper disable once ConvertToAutoProperty
    public static DataVaultGeneral Instance => instance;   // instance is a singleton

    
    /// <summary>Base name for the current view (without the 'View' part)</summary>
    public string CurrentViewBase { get; set; } = string.Empty;

}