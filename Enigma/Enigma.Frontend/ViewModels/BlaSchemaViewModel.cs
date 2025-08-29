// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2025.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.Generic;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
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
    private IBlaPositionForDataGridFactory _blaPositionFactory = App.ServiceProvider.GetRequiredService<IBlaPositionForDataGridFactory>();
    private BlaElementsCrossesForDataGridFactory _blaElementsCrossesFactory = App.ServiceProvider.GetRequiredService<BlaElementsCrossesForDataGridFactory>();
    private BlaPresQuadrantCountFactory _blaPresQuadrantCountFactory = App.ServiceProvider.GetRequiredService<BlaPresQuadrantCountFactory>();
    
    [ObservableProperty] private string _chartName = "Chart Name";
    [ObservableProperty] private List<PresentableBlaPosition> _blaPositions = new();
    [ObservableProperty] private List<PresentableCrossElementsCount> _crossesCounts = new();
    [ObservableProperty] private List<PresentableCrossElementsCount> _elementsCounts = new();
    [ObservableProperty] private List<PresentableQuadrantCount> _quadrantCounts = new();
    
    // ScottPlot histogram data
    public double[] HistogramCrossesValues { get; private set; } = new double[0];
    public double[] HistogramElementsValues { get; private set; } = new double[0];
    public double[] HistogramQuadrantValues { get; private set; } = new double[0];
    public string[] HistogramCrossesLabels { get; private set; } = new string[0];
    public string[] HistogramElementsLabels { get; private set; } = new string[0];
    public string[] HistogramQuadrantLabels { get; private set; } = new string[4];

    public void Populate()
    {
        // TODO find correct values for Housesystem, Chiron and Eris 
        var houseSystem = HouseSystems.Placidus;
        var useChiron = true;
        var useEris = true;
        
        _schemaModel.CreateDataForBla(houseSystem, useChiron, useEris);

        // Get chart points and populate the DataGrid
        var chartDetails = _schemaModel.GetChartDetails();
        BlaPositions = _blaPositionFactory.CreateBlaPositionsForDataGrid(chartDetails);
        
        // Populate Elements/Crosses DataGrid
        CrossesCounts = _blaElementsCrossesFactory.CreatePresCrossesCounts(chartDetails, _schemaModel.GetCalculatedChart());
        ElementsCounts = _blaElementsCrossesFactory.CreatePresElementsCounts(chartDetails, _schemaModel.GetCalculatedChart());
        QuadrantCounts = _blaPresQuadrantCountFactory.CreatePresQuadrants(chartDetails);
        
        // Update histogram data
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
        if (CrossesCounts.Count == 0)
        {
            HistogramCrossesValues = [];
            HistogramCrossesLabels = [];
            return;
        }
        if (ElementsCounts.Count == 0)
        {
            HistogramElementsValues = [];
            HistogramElementsLabels = [];
            return;
        }

        if (QuadrantCounts.Count == 0)
        {
            HistogramQuadrantValues = [];
            HistogramQuadrantLabels = new string[4];
            return;
        }
        
        // Extract totals and labels from ElementsCrosses
        HistogramCrossesValues = CrossesCounts.Select(item => (double)item.Total).ToArray();
        HistogramElementsValues = ElementsCounts.Select(item => (double)item.Total).ToArray();
        HistogramQuadrantValues = QuadrantCounts.Select(item => (double)item.Count).ToArray();
        HistogramCrossesLabels = CrossesCounts.Select(item => item.Name).ToArray();
        HistogramElementsLabels = ElementsCounts.Select(item => item.Name).ToArray();
        for (int i = 0; i < 4; i++)
        {
            HistogramQuadrantLabels[i] = $"Quadrant {i + 1}";
        }
    }
}
