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
using Enigma.Api.Configuration;
using Enigma.Domain.Dtos;
using Enigma.Domain.Presentables;
using Enigma.Domain.References;
using Enigma.Frontend.Ui.Messaging;
using Enigma.Frontend.Ui.Models;
using Enigma.Frontend.Ui.PresentationFactories;
using Enigma.Frontend.Ui.State;
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
    
    [ObservableProperty] private string _chartName = "Chart Name";
    [ObservableProperty] private List<PresentableBlaPosition> _blaPositions = new();
    [ObservableProperty] private List<PresentableCrossElementsCount> _elementsCrosses = new();
    
    // ScottPlot histogram data
    public double[] HistogramValues { get; private set; } = new double[0];
    public string[] HistogramLabels { get; private set; } = new string[0];


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
        ElementsCrosses = _blaElementsCrossesFactory.CreateBlaItemsForElementsCrosses(chartDetails, _schemaModel.GetCalculatedChart());
        
        // Update histogram data
        UpdateHistogramData();
        
        // populate parts that depend only on positions for chartpoints or houses
        
        // call api for each set of data using the calculated chart as a parameter
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
        if (ElementsCrosses == null || ElementsCrosses.Count == 0)
        {
            HistogramValues = new double[0];
            HistogramLabels = new string[0];
            return;
        }
        
        // Extract totals and labels from ElementsCrosses
        HistogramValues = ElementsCrosses.Select(item => (double)item.Total).ToArray();
        HistogramLabels = ElementsCrosses.Select(item => item.Name).ToArray();
    }
}
