// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Dtos;
using Enigma.Domain.References;

namespace Enigma.Frontend.Ui.State;

public sealed class DataVaultProg
{
    
    private static readonly DataVaultProg instance = new();
    
    // Explicit static constructor to tell C# compiler not to mark type as beforefieldinit
    static DataVaultProg()
    {
    }

    private DataVaultProg()
    {
    }

    // ReSharper disable once ConvertToAutoProperty
    public static DataVaultProg Instance => instance;   // instance is a singleton
    
    
    public ProgresMethods CurrentProgresMethod { get; set; } = ProgresMethods.Undefined;
    public ProgEvent? CurrentProgEvent { get; set; }

    
}