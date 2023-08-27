// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.Generic;
using System.Linq;
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
    private readonly IProgEventForPresentationFactory _progEventForPresentationFactory;
    private readonly IProgPeriodForPresentationFactory _progPeriodForPresentationFactory;
    private IPeriodDataPersistencyApi _periodDataPersistencyApi;
    public List<ProgEvent> AvailableEvents = new();
    public List<ProgPeriod> AvailablePeriods = new();
    public List<PresentableProgresData> PresentableEvents { get; set; } = new();
    public List<PresentableProgresData> PresentablePeriods { get; set; } = new();
    private readonly DataVault _dataVault = DataVault.Instance;

    public ProgressiveMainModel(IEventDataConverter eventDataConverter, 
        IPeriodDataConverter periodDataConverter,
        IEventDataPersistencyApi eventDataPersistencyApi,
        IPeriodDataPersistencyApi periodDataPersistencyApi,
        IProgEventForPresentationFactory progEventForPresentationFactory,
        IProgPeriodForPresentationFactory progPeriodForPresentationFactory)
    {
        _eventDataConverter = eventDataConverter;
        _periodDataConverter = periodDataConverter;
        _eventDataPersistencyApi = eventDataPersistencyApi;
        _periodDataPersistencyApi = periodDataPersistencyApi;
        _progEventForPresentationFactory = progEventForPresentationFactory;
        _progPeriodForPresentationFactory = progPeriodForPresentationFactory;
        ReadCurrentEvents();
        ReadCurrentPeriods();
    }
    
        
    public int SaveCurrentEvent()
    {
        int newIndex = -1;
        var currentEvent = _dataVault.CurrentProgEvent;
        if (currentEvent == null) return newIndex;
        var currentChart = _dataVault.GetCurrentChart();
        if (currentChart == null) return newIndex;
        int chartId = currentChart.InputtedChartData.Id;
        PersistableEventData persEvent = _eventDataConverter.ToPersistableEventData(currentEvent);
        newIndex = _eventDataPersistencyApi.AddEventData(persEvent, chartId);
        return newIndex;
    }

    public int SaveCurrentPeriod()
    {
        int newIndex = -1;
        var currentPeriod = _dataVault.CurrentProgPeriod;
        if (currentPeriod == null) return newIndex;
        var currentChart = _dataVault.GetCurrentChart();
        if (currentChart == null) return newIndex;
        int chartId = currentChart.InputtedChartData.Id;
        PersistablePeriodData persPeriod = _periodDataConverter.ToPersistablePeriodData(currentPeriod);
        newIndex = _periodDataPersistencyApi.AddPeriodData(persPeriod, chartId);
        return newIndex;
    }

    private void ReadCurrentEvents()
    {
        var currentChart = _dataVault.GetCurrentChart();
        if (currentChart == null) return;
        int chartId = currentChart.InputtedChartData.Id;
        var persistableEventData = _eventDataPersistencyApi.SearchEventData(chartId);
        foreach (ProgEvent? progEventData in persistableEventData.Select(item 
                     => _eventDataConverter.FromPersistableEventData(item)))
        {
            AvailableEvents.Add(progEventData);
        }
        PresentableEvents = _progEventForPresentationFactory.CreatePresentableProgresData(AvailableEvents);
    }

    private void ReadCurrentPeriods()
    {
        var currentChart = _dataVault.GetCurrentChart();
        if (currentChart == null) return;
        int chartId = currentChart.InputtedChartData.Id;
        var persistablePeriodData = _periodDataPersistencyApi.SearchPeriodData(chartId);
        foreach (ProgPeriod? progPeriodData in persistablePeriodData.Select(item 
                     => _periodDataConverter.FromPersistablePeriodData(item)))
        {
            AvailablePeriods.Add(progPeriodData);
        }
        PresentablePeriods = _progPeriodForPresentationFactory.CreatePresentableProgresData(AvailablePeriods);
    }
    
}