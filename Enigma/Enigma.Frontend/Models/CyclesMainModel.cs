// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.Generic;
using Enigma.Domain.Research;

namespace Enigma.Frontend.Ui.Models;

public class CyclesMainModel
{
    
    
    
    public List<CycleItem> GetAllCycleItems()
    {
        List<CycleItem> cycleItems = new();
        
        
        // todo retrieve cycleitems
        return cycleItems;
    }
    
}


/// <summary>DTO for a single project item.</summary>
public class CycleItem
{ public string? CycleName { get; init; }
    public string? CycleDescription { get; init; }
}