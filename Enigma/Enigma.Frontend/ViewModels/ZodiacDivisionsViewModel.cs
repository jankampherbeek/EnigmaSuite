// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Messaging;
using Enigma.Domain.Presentables;
using Enigma.Frontend.Ui.Messaging;
using Enigma.Frontend.Ui.Models;
using Enigma.Frontend.Ui.State;
using Microsoft.Extensions.DependencyInjection;
using Enigma.Frontend.Ui.PresentationFactories;
using Enigma.Frontend.Ui.WindowsFlow;
using Serilog;

namespace Enigma.Frontend.Ui.ViewModels;

public partial class ZodiacDivisionsViewModel: ObservableObject
{
    private const string VM_IDENTIFICATION = ChartsWindowsFlow.ZODIAC_DIVISIONS;
    private readonly int _windowId = DataVaultCharts.Instance.LastWindowId;
    
    private readonly ZodiacDivisionsModel _model;
    private readonly DataVaultCharts _dataVaultCharts = DataVaultCharts.Instance;
    private readonly IZodiacDivisionForDataGridFactory _zodiacDivisionForDataGridFactory;
    private bool _isCalculating;
    
    public ZodiacDivisionsViewModel(IZodiacDivisionForDataGridFactory zodiacDivisionForDataGridFactory)
    {
        _zodiacDivisionForDataGridFactory = zodiacDivisionForDataGridFactory;
        _model = App.ServiceProvider.GetRequiredService<ZodiacDivisionsModel>();
    }
    
    // Radio button selection properties - these will sync with the Model
    [ObservableProperty]
    private bool _decansSignsSelected = true;
    
    [ObservableProperty]
    private bool _decansPlanetsSelected;
    
    [ObservableProperty]
    private bool _dodecatsOriginalSelected = true;
    
    [ObservableProperty]
    private bool _dodecatsPaulusSelected;
    
    [ObservableProperty]
    private bool _boundsEgyptianSelected = true;
    
    [ObservableProperty]
    private bool _boundsPtolemySelected;
    
    // Results collection for the DataGrid
    [ObservableProperty]
    private ObservableCollection<PresentableZodiacDivisions> _zodiacDivisionsResults = new();
    
    // Metadata property for binding
    [ObservableProperty]
    private string _metadataText = "No chart loaded";
    
    // Commands
    [RelayCommand]
    private void Help()
    {
        // TODO: Implement help functionality
    }
    
    [RelayCommand]
    private void Ok()
    {
        CalculateZodiacDivisions();
    }
    
    // Single property change handler for all radio button changes
    partial void OnDecansSignsSelectedChanged(bool value)
    {
        if (value && !_isCalculating) ScheduleCalculation();
    }
    
    partial void OnDecansPlanetsSelectedChanged(bool value)
    {
        if (value && !_isCalculating) ScheduleCalculation();
    }
    
    partial void OnDodecatsOriginalSelectedChanged(bool value)
    {
        if (value && !_isCalculating) ScheduleCalculation();
    }
    
    partial void OnDodecatsPaulusSelectedChanged(bool value)
    {
        if (value && !_isCalculating) ScheduleCalculation();
    }
    
    partial void OnBoundsEgyptianSelectedChanged(bool value)
    {
        if (value && !_isCalculating) ScheduleCalculation();
    }
    
    partial void OnBoundsPtolemySelectedChanged(bool value)
    {
        if (value && !_isCalculating) ScheduleCalculation();
    }
    
    private void ScheduleCalculation()
    {
        // Use Dispatcher to ensure calculation happens after all UI updates are complete
        System.Windows.Application.Current.Dispatcher.BeginInvoke(new Action(() =>
        {
            if (!_isCalculating)
            {
                CalculateZodiacDivisions();
            }
        }), System.Windows.Threading.DispatcherPriority.Background);
    }
    
    private void CalculateZodiacDivisions()
    {
        if (_isCalculating) return;
        
        _isCalculating = true;
        
        try
        {
            // Update the model's boolean properties based on radio button selections
            _model.UseDecansPlanet = DecansPlanetsSelected;
            _model.UseDodecatsOrignal = DodecatsOriginalSelected;
            _model.UseBoundsEgyptian = BoundsEgyptianSelected;
            
            // Get the current chart to access chart point names
            var currentChart = _dataVaultCharts.GetCurrentChart();
            if (currentChart == null)
            {
                // Handle case where no chart is loaded
                ZodiacDivisionsResults.Clear();
                MetadataText = "No chart loaded";
                return;
            }
            
            // Update metadata text
            MetadataText = _model.GetMetadataText();
            
            // Calculate all indexes using the model
            var dataResults = _model.DefineAllTextsForGrid();
            
            // Clear previous results
            ZodiacDivisionsResults.Clear();
            
            // Convert the dataResults to observable collection
            var presentableResults = _zodiacDivisionForDataGridFactory.CreateZodiacDivisionForDataGrid(dataResults);
            ZodiacDivisionsResults = new ObservableCollection<PresentableZodiacDivisions>(presentableResults);
        }
        catch (Exception ex)
        {
            // Handle any errors during calculation
            Log.Error($"ZodiacDivisionsViewModel.CalculateZodiacDivisions encountered an error: {ex.Message}");
            ZodiacDivisionsResults.Clear();
            MetadataText = "Error loading chart data";
        }
        finally
        {
            _isCalculating = false;
        }
    }
   
    [RelayCommand]
    private static void Close()
    {
        Log.Information("ZodiacDivisionsViewModel.Close(): send CloseMessage");         
        WeakReferenceMessenger.Default.Send(new CloseMessage(VM_IDENTIFICATION));
    }
    
} 