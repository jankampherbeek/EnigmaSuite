// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2025.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.Generic;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Enigma.Domain.Dtos;
using Enigma.Domain.References;
using Enigma.Frontend.Ui.Messaging;
using Enigma.Frontend.Ui.Models;
using Enigma.Frontend.Ui.PresentationFactories;
using Enigma.Frontend.Ui.State;
using Enigma.Frontend.Ui.WindowsFlow;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Enigma.Frontend.Ui.ViewModels;

/// <summary>ViewModel for Prenatal window</summary>
public partial class PrenatalViewModel : ObservableObject
{
    private const string VM_IDENTIFICATION = ChartsWindowsFlow.PRENATAL;

    private PreNatalModel _preNatalModel = App.ServiceProvider.GetRequiredService<PreNatalModel>();

    [ObservableProperty] private string _chartName = "Chart Name";
    [ObservableProperty] private bool _useAspects;
    [ObservableProperty] private bool _useEclipses;
    [ObservableProperty] private bool _useIngresses;
    [ObservableProperty] private bool _useRetrogradeDirect;
    [ObservableProperty] private List<PresentablePreNatalMoment> _preNatalMoments = new();
    [ObservableProperty] private List<PresentablePreNatalEvent> _preNatalEvents = new();
    [ObservableProperty] private string _selectedFactors;
    [ObservableProperty] private string _selectedAspects;
    [ObservableProperty] private string _standardConception;
    [ObservableProperty] private string _actualConception;
    [ObservableProperty] private PresentablePreNatalMoment? _selectedMoment;
    [ObservableProperty] private PresentablePreNatalEvent? _selectedEvent;
    
    [RelayCommand]
    private void Help()
    {
        Log.Information("PrenatalViewModel.Help(): send HelpMessage");
        WeakReferenceMessenger.Default.Send(new HelpMessage(VM_IDENTIFICATION));
    }

    [RelayCommand]
    private void Close()
    {
        Log.Information("PrenatalViewModel.Close(): send CloseMessage");
        WeakReferenceMessenger.Default.Send(new CloseMessage(VM_IDENTIFICATION));
    }

    [RelayCommand]
    private void CorrectConception()
    {
        if (SelectedMoment == null || SelectedEvent == null)
        {
            MessageBox.Show("Please select both a moment and an event to combine.");
            return;
        }
        
        Log.Information($"PrenatalViewModel.CorrectConception(): Combining moment {SelectedMoment.RealDateTime} with event {SelectedEvent.Description}");
        
        // TODO: implement actual conception correction logic
        // For now, we have the selected moment and event available
        MessageBox.Show($"Combining:\nMoment: {SelectedMoment.RealDateTime}\nEvent: {SelectedEvent.Description} ({SelectedEvent.DateTime})");
    }
    
    
    public PrenatalViewModel()
    {
        UseAspects = true;
        UseEclipses = true;
        UseIngresses = false;
        UseRetrogradeDirect = false;
        var selectedFactors = new List<ChartPoints>
        {
            ChartPoints.Sun,
            ChartPoints.Mercury,
            ChartPoints.Venus,
            ChartPoints.Mars,
            ChartPoints.Jupiter,
            ChartPoints.Saturn,
            ChartPoints.Uranus,
            ChartPoints.Neptune,
            ChartPoints.Pluto
        };
        var selectedAspects = new List<AspectTypes>
        {
            AspectTypes.Conjunction
        };
        _preNatalModel.selectedFactors = selectedFactors;
        DataVaultCharts.Instance.CurrentPointsSelection = selectedFactors;
        _preNatalModel.selectedAspects = selectedAspects;
        DataVaultCharts.Instance.CurrentAspectsSelection = selectedAspects;
    }

    private void DefineFactors()
    {
        WeakReferenceMessenger.Default.Send(new OpenMessage(VM_IDENTIFICATION,
            ChartsWindowsFlow.PRENATAL_FACTOR_SELECTION));
        var selection = DataVaultCharts.Instance.CurrentPointsSelection;
        if (selection == null) return;
        if (selection.Count < 1)
        {
            MessageBox.Show("Please select at least one point." );
        }
        else
        {
            _preNatalModel.selectedFactors = selection;
            DataVaultCharts.Instance.CurrentPointsSelection = selection;
            SelectedFactors = _preNatalModel.GetSelectedFactorsGlyphs();
            Populate();       
        }
    }
    
    private void DefineAspects()
    {
        WeakReferenceMessenger.Default.Send(new OpenMessage(VM_IDENTIFICATION,
            ChartsWindowsFlow.PRENATAL_ASPECT_SELECTION));
        var selection = DataVaultCharts.Instance.CurrentAspectsSelection;
        if (selection == null) return;
        if (selection.Count < 1)
        {
            MessageBox.Show("Please select at least one aspect." );
        }
        else
        {
            _preNatalModel.selectedAspects = selection;
            DataVaultCharts.Instance.CurrentAspectsSelection = selection;
            SelectedAspects = _preNatalModel.GetSelectedAspectsGlyphs();
            Populate();       
        }
    }
    
    
    public void Populate()
    {
        _preNatalModel.useAspects = UseAspects;
        _preNatalModel.useEclipses = UseEclipses;
        _preNatalModel.useIngresses = UseIngresses;
        _preNatalModel.useRetrogradeDirect = UseRetrogradeDirect;
        PreNatalMoments = _preNatalModel.GetPrenatalMoments();
        PreNatalEvents = _preNatalModel.GetPrenatalEvents();  
        SelectedFactors = _preNatalModel.GetSelectedFactorsGlyphs();
        SelectedAspects = _preNatalModel.GetSelectedAspectsGlyphs();
        StandardConception = _preNatalModel.standardConception;
    }
    
    public void UpdateAspects(bool useIt)
    {
        UseAspects = useIt;
        Populate();
    }
    
    public void UpdateEclipses(bool useIt)
    {
        UseEclipses = useIt;
        Populate();
    }
    
    public void UpdateRetrogradeDirect(bool useIt)
    {
        UseRetrogradeDirect = useIt;
        Populate();
    }
    
    public void UpdateIngresses(bool useIt)
    {
        UseIngresses = useIt;
        Populate();
    }

    [RelayCommand]
    public void ChangePoints()
    {
        DefineFactors();       
    }
    
    [RelayCommand]
    public void ChangeAspects()
    {
        DefineAspects();       
    }
}
