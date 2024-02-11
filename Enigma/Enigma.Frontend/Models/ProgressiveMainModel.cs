// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.Generic;
using System.Linq;
using Enigma.Api;
using Enigma.Domain.Dtos;
using Enigma.Domain.Persistables;
using Enigma.Domain.Presentables;
using Enigma.Frontend.Ui.Interfaces;
using Enigma.Frontend.Ui.State;
using Enigma.Frontend.Ui.Views;
using Serilog;

namespace Enigma.Frontend.Ui.Models;

public class ProgressiveMainModel
{
    private ProgEventWindow? _progEventWindow;
    private readonly IEventDataConverter _eventDataConverter;
    private readonly IEventDataPersistencyApi _eventDataPersistencyApi;
    private readonly IProgDatesForPresentationFactory _progDatesForPresentationFactory;
    public readonly List<ProgDates> AvailableEventsPeriods = new();

    public List<PresentableProgresData> PresentableEventsPeriods { get; } = new();
    private readonly DataVaultCharts _dataVaultCharts = DataVaultCharts.Instance;
    private readonly DataVaultProg _dataVaultProg = DataVaultProg.Instance;
    private ProgEvent _currentEvent;
 //   private readonly ChartsStateMachine _stateMachine = App.ServiceProvider.GetRequiredService<ChartsStateMachine>();
    public ProgressiveMainModel(IEventDataConverter eventDataConverter, 
        IEventDataPersistencyApi eventDataPersistencyApi,
        IProgDatesForPresentationFactory progDatesForPresentationFactory)
    {
        _eventDataConverter = eventDataConverter;
        _eventDataPersistencyApi = eventDataPersistencyApi;
        _progDatesForPresentationFactory = progDatesForPresentationFactory;
        ReadCurrentEvents();
        PresentableEventsPeriods = GetPresentableEventsPeriods();
    }

    public List<PresentableProgresData> GetPresentableEventsPeriods()
    {
        ReadCurrentEvents();
        return _progDatesForPresentationFactory.CreatePresentableProgresData(AvailableEventsPeriods);
    }

    
    /// <summary>Delete event from database.</summary>
    /// <param name="eventId">Id for the event.</param>
    /// <returns>True if the deletion was successful, otherwise false.</returns>
    public bool DeleteCurrentEvent(long eventId)
    {
        return _eventDataPersistencyApi.DeleteEventData(eventId);
    }

    public bool CreateNewEvent()
    {
        _progEventWindow = new();
        _progEventWindow.ShowDialog();
        
        if (DataVaultProg.Instance.CurrentProgEvent is null) return false;
        SaveCurrentEvent();
        return true;
    }

    public void CloseProgEventWindow()
    {
        _progEventWindow?.Close();
    }
    
    
    public long SaveCurrentEvent()
    {
        long newIndex = -1;
        _currentEvent = _dataVaultProg.CurrentProgEvent;
        if (_currentEvent == null) return newIndex;
        var currentChart = _dataVaultCharts.GetCurrentChart();
        if (currentChart == null) return newIndex;
        long chartId = currentChart.InputtedChartData.Id;
        Log.Information("ProgressiveMainModel.SaveCurrentEvent(): Requesting PersitableEventData to save event");
        PersistableEventData persEvent = _eventDataConverter.ToPersistableEventData(_currentEvent);
        newIndex = _eventDataPersistencyApi.AddEventData(persEvent, chartId);
        return newIndex;
    }

    private void ReadCurrentEvents()
    {
        AvailableEventsPeriods.Clear();
        var currentChart = _dataVaultCharts.GetCurrentChart();
        if (currentChart == null) return;
        long chartId = currentChart.InputtedChartData.Id;
        var persistableEventData = _eventDataPersistencyApi.SearchEventData(chartId);
        foreach (ProgEvent? progEventData in persistableEventData.Select(item 
                     => _eventDataConverter.FromPersistableEventData(item)))
        {
            AvailableEventsPeriods.Add(progEventData);
        }
        
    }

}