// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Linq;
using Enigma.Frontend.Ui.Models;
using Enigma.Frontend.Ui.State;
using Enigma.Domain.References;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using Enigma.Domain.Dtos;
using Enigma.Frontend.Ui.PresentationFactories;
using Enigma.Domain.Presentables;
using Serilog;

namespace Enigma.Frontend.Ui.ViewModels;

public partial class ZodiacDivisionsViewModel: ObservableObject
{
    private readonly ZodiacDivisionsModel _model;
    private readonly DataVaultCharts _dataVaultCharts = DataVaultCharts.Instance;
    private readonly IZodiacDivisionForDataGridFactory _zodiacDivisionForDataGridFactory;
    
    public ZodiacDivisionsViewModel(IZodiacDivisionForDataGridFactory zodiacDivisionForDataGridFactory)
    {
        _zodiacDivisionForDataGridFactory = zodiacDivisionForDataGridFactory;
        _model = App.ServiceProvider.GetRequiredService<ZodiacDivisionsModel>();
    }
    
    // Radio button selection properties - these will sync with the Model
    [ObservableProperty]
    private bool _decansSignsSelected = true;
    
    [ObservableProperty]
    private bool _decansPlanetsSelected = false;
    
    [ObservableProperty]
    private bool _dodecatsOriginalSelected = true;
    
    [ObservableProperty]
    private bool _dodecatsPaulusSelected = false;
    
    [ObservableProperty]
    private bool _termsEgyptianSelected = true;
    
    [ObservableProperty]
    private bool _termsPtolemySelected = false;
    
    // Results collection for the DataGrid
    [ObservableProperty]
    private ObservableCollection<Enigma.Domain.Presentables.PresentableZodiacDivisions> _zodiacDivisionsResults = new();
    
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
    
    private void CalculateZodiacDivisions()
    {
        try
        {
            // Update the model's boolean properties based on radio button selections
            _model.UseDecansPlanet = DecansPlanetsSelected;
            _model.UseDodecatsOrignal = DodecatsOriginalSelected;
            _model.UseBoundsEgyptian = TermsEgyptianSelected;
            
            // Get the current chart to access chart point names
            var currentChart = _dataVaultCharts.GetCurrentChart();
            if (currentChart == null)
            {
                // Handle case where no chart is loaded
                ZodiacDivisionsResults.Clear();
                return;
            }
            
            // Calculate all indexes using the model
            var dataResults = _model.DefineAllTextsForGrid();
            
            // Clear previous results
            ZodiacDivisionsResults.Clear();
            
            // Convert the dataResults to observable collection
            var presentableResults = _zodiacDivisionForDataGridFactory.CreateZodiacDivisionForDataGrid(dataResults);
            ZodiacDivisionsResults = new ObservableCollection<Enigma.Domain.Presentables.PresentableZodiacDivisions>(presentableResults);
        }
        catch (Exception ex)
        {
            // Handle any errors during calculation
            Log.Error($"ZodiacDivisionsViewModel.CalculateZodiacDivisions encountered an error: {ex.Message}");
            ZodiacDivisionsResults.Clear();
            // You might want to show an error message to the user here
        }
    }
   
} 