// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2025.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Enigma.Core.Slices.BlaSchema;
using Enigma.Domain.Presentables;
using Enigma.Domain.References;
using Enigma.Frontend.Ui.Messaging;
using Enigma.Frontend.Ui.Models;
using Enigma.Frontend.Ui.PresentationFactories;
using Enigma.Frontend.Ui.WindowsFlow;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Enigma.Frontend.Ui.ViewModels;

/// <summary>ViewModel for BLA Schema window</summary>
public partial class BlaSchemaViewModel : ObservableObject
{
    private const string VM_IDENTIFICATION = ChartsWindowsFlow.BLA_SCHEMA;
    private BlaSchemaModel _schemaModel = App.ServiceProvider.GetRequiredService<BlaSchemaModel>();
    
    [ObservableProperty] private string _chartName = "Chart Name";
    [ObservableProperty] private List<PresentableBlaPosition> _blaPositions = new();
    [ObservableProperty] private List<PresentableCrossElementsCount> _crossesCounts = new();
    [ObservableProperty] private List<PresentableCrossElementsCount> _elementsCounts = new();
    [ObservableProperty] private List<PresentableQuadrantCount> _quadrantCounts = new();
    [ObservableProperty] private List<PresentableDispositorCounts> _dispositorCounts = new();
    [ObservableProperty] private List<PresentableBlaDetails> _blaDetails = new();
    [ObservableProperty] private List<PresentableBlaCycle> _blaCycles = new();
    [ObservableProperty] private List<PresentableBlaCycle> _shortenedCycles = new();
    [ObservableProperty] private List<PresenTableReinforcementFactor> _factorsInOwnSigns = new();
    [ObservableProperty] private List<PresenTableReinforcementFactor> _factorsInOwnHouses = new();
    [ObservableProperty] private List<PresenTableReinforcementFactor> _factorsInOwnMundaneHouses = new();
    [ObservableProperty] private List<PresenTableReinforcementFactor> _houseLordsInAnalogSigns = new();
    [ObservableProperty] private List<PresentablePairAnalogHouseSign> _pairAnalogHouseSigns = new();
    [ObservableProperty] private List<PresentableReception> _receptionsInSigns = new();
    [ObservableProperty] private List<PresentableReception> _receptionsInHouses = new();
    [ObservableProperty] private List<PresentableReception> _receptionsInMundaneHouses = new();
    // ScottPlot histogram data
    public double[] HistogramDispositorValues { get; private set; } = new double[0];
    public string[] HistogramDispositorLabels { get; private set; } = new string[7];

    public void Populate()
    {
        // TODO find correct values for Housesystem, Chiron and Eris 
        // TODO check for true or mean node
        var houseSystem = HouseSystems.Placidus;
        var useChiron = false;
        var useEris = false;
        
        _schemaModel.CreateDataForBla(houseSystem, useChiron, useEris);
        CrossesCounts = _schemaModel.GetCrossesCounts();
        ElementsCounts = _schemaModel.GetElementsCounts();
        QuadrantCounts = _schemaModel.GetQuadrantCounts();
        DispositorCounts = _schemaModel.GetDispositors();
        BlaDetails = _schemaModel.GetBlaDetails();
        BlaCycles = _schemaModel.GetBlaCycles();
        ShortenedCycles = _schemaModel.GetBlaShortenedCycles();
        FactorsInOwnSigns = _schemaModel.GetBlaFactorsInOwnSigns();
        FactorsInOwnHouses = _schemaModel.GetBlaFactorsInOwnHouses();
        FactorsInOwnMundaneHouses = _schemaModel.GetBlaFactorsInOwnMundaneHouses();
        HouseLordsInAnalogSigns = _schemaModel.GetBlaHouseLordsInAnalogSigns();
        PairAnalogHouseSigns = _schemaModel.GetBlaPairsAnalogHouseSigns();
        ReceptionsInSigns = _schemaModel.GetReceptionsInSigns();
        ReceptionsInHouses = _schemaModel.GetReceptionsInHouses();
        ReceptionsInMundaneHouses = _schemaModel.GetReceptionsInMundaneHouses();
        
        UpdateHistogramData();

    }
    
    
    
    
    
    [RelayCommand]
    private void Help()
    {
        Log.Information("BlaSchemaViewModel.Help(): send HelpMessage");
        WeakReferenceMessenger.Default.Send(new HelpMessage(VM_IDENTIFICATION));
    }

    [RelayCommand]
    private void Close()
    {
        Log.Information("BlaSchemaViewModel.Close(): send CloseMessage");
        WeakReferenceMessenger.Default.Send(new CloseMessage(VM_IDENTIFICATION));
    }

    public BlaSchemaViewModel()
    {
        // Initialize data here when needed
    }
    
    private void UpdateHistogramData()
    {
        if (DispositorCounts.Count == 0)
        {
            HistogramDispositorValues = [];
            HistogramDispositorLabels = new string[7];
            return;
        }

        HistogramDispositorValues = DispositorCounts.Select(item => (double)item.Total).ToArray();
        HistogramDispositorLabels = DispositorCounts.Select(item => item.Rulers).ToArray();
    }

}
