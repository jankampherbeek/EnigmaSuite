// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.Generic;
using Enigma.Api.Interfaces;
using Enigma.Domain.Persistency;
using Enigma.Domain.Progressive;
using Enigma.Frontend.Ui.Interfaces;
using Enigma.Frontend.Ui.State;

namespace Enigma.Frontend.Ui.Models;

public class ProgressiveMainModel
{
    private readonly IEventDataConverter _eventDataConverter;
    private readonly IPeriodDataConverter _periodDataConverter;
    private readonly IEventDataPersistencyApi _eventDataPersistencyApi;
    private IPeriodDataPersistencyApi _periodDataPersistencyApi;
    public List<PresentableProgresData> AvailableEvents { get; set; } = new();
    public List<PresentableProgresData> AvailablePeriods { get; set; } = new();
    private readonly DataVault _dataVault = DataVault.Instance;

    public ProgressiveMainModel(IEventDataConverter eventDataConverter, 
        IPeriodDataConverter periodDataConverter,
        IEventDataPersistencyApi eventDataPersistencyApi,
        IPeriodDataPersistencyApi periodDataPersistencyApi)
    {
        _eventDataConverter = eventDataConverter;
        _periodDataConverter = periodDataConverter;
        _eventDataPersistencyApi = eventDataPersistencyApi;
        _periodDataPersistencyApi = periodDataPersistencyApi;
    }
    
        
    public int SaveCurrentEvent()
    {
        int newIndex = -1;
        var currentEvent = _dataVault.CurrentProgEvent;
        if (currentEvent == null) return newIndex;
        PersistableEventData persEvent = _eventDataConverter.ToPersistableEventData(currentEvent);
        newIndex = _eventDataPersistencyApi.AddEventData(persEvent);
        return newIndex;
    }

    public int SaveCurrentPeriod()
    {
        int newIndex = -1;
        var currentPeriod = _dataVault.CurrentProgPeriod;
        if (currentPeriod == null) return newIndex;
        PersistablePeriodData persPeriod = _periodDataConverter.ToPersistablePeriodData(currentPeriod);
        newIndex = _periodDataPersistencyApi.AddPeriodData(persPeriod);
        return newIndex;
    }
    
    
    
}