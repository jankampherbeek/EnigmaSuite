// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Dtos;
using Enigma.Domain.References;
using Enigma.Domain.Research;
using Enigma.Domain.Responses;

namespace Enigma.Frontend.Ui.State;

public sealed class DataVaultResearch
{
    
        
    private static readonly DataVaultResearch instance = new();
    
    // Explicit static constructor to tell C# compiler not to mark type as beforefieldinit
    static DataVaultResearch()
    {
    }

    private DataVaultResearch()
    {
    }

    // ReSharper disable once ConvertToAutoProperty
    public static DataVaultResearch Instance => instance;   // instance is a singleton
    
    
    public ResearchProject? CurrentProject { get; set; }
    public ResearchMethods ResearchMethod { get; set; } 
    public ResearchPointsSelection? CurrentPointsSelection { get; set; }
    public bool ResearchCanceled { get; set; } 
    public bool ResearchIncludeCusps { get; set; }
    public MethodResponse? ResponseTest { get; set; }
    public MethodResponse? ResponseCg { get; set; }
    
    public double ResearchHarmonicValue { get; set; }
    public double ResearchHarmonicOrb { get; set; }

    public int ResearchMidpointDialDivision { get; set; }
    public double ResearchMidpointOrb { get; set; }
}