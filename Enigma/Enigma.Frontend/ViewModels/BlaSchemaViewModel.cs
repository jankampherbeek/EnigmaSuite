// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2025.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System;
using System.Collections.Generic;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Enigma.Domain.Presentables;
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

    [ObservableProperty] private List<string> _houseSystemNames;
    [ObservableProperty] private List<string> _apogeeTypeNames;
    [ObservableProperty] private int _houseSystemIndex = 4;
    [ObservableProperty] private int _apogeeIndex = 1;
    private bool _useChiron;
    private bool _useCeres;
    private bool _useTrueNode;
    private bool _useDecanates;
    [ObservableProperty] private string _chartName;
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

    public void SetUpBlaSchema()
    {
        _schemaModel.PrepareBlaSchema();
        Populate();
    }
    
    partial void OnHouseSystemIndexChanged(int value)
    {
        Populate();
    }
    
    partial void OnApogeeIndexChanged(int value)
    {
        Populate();
    }
    
    public void Populate()
    {
        _schemaModel.CreateBlaSchema(_useChiron, _useCeres, _useTrueNode, _useDecanates, _houseSystemIndex, _apogeeIndex);
        ChartName = _schemaModel.ChartName;
        HouseSystemNames = _schemaModel.GetHouseSystemNames();
        ApogeeTypeNames = _schemaModel.GetApogeeTypeNames();
        BlaPositions = _schemaModel.GetBlaPositions();
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


    public void UpdateChiron()
    {
        _useChiron = !_useChiron;
        Populate();
    }
    
    public void UpdateCeres()
    {
        _useCeres = !_useCeres;
        Populate();       
    }
    
    public void UpdateTrueNode()
    {
        _useTrueNode = !_useTrueNode;
        Populate();
    }

    public void UpdateDecanates()
    {
        _useDecanates = !_useDecanates;
        Populate();       
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
