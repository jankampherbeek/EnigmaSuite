// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.Generic;
using Enigma.Domain.Dtos;
using Enigma.Domain.References;

namespace Enigma.Frontend.Ui.State;

public sealed class DataVaultProg
{
    
    private static readonly DataVaultProg instance = new();
    private readonly List<ProgEvent> _allEvents = new();
    private bool _newEventAdded;
    private int _indexCurrentEvent;

    
    
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

    public string PrimDirStarDate { get; set; }
    public string PrimDirEndDate { get; set; }

    public void AddNewEvent(ProgEvent newEvent)
    {
        _allEvents.Add(newEvent);
        CurrentProgEvent = newEvent;
    }
}