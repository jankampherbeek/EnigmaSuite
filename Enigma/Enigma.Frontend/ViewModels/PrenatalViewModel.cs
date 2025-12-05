// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2025.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.Generic;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Enigma.Api.Calc;
using Enigma.Core.Slices.PreNatal;
using Enigma.Domain.Dtos;
using Enigma.Domain.References;
using Enigma.Frontend.Ui.Messaging;
using Enigma.Frontend.Ui.Models;
using Enigma.Frontend.Ui.PresentationFactories;
using Enigma.Frontend.Ui.State;
using Enigma.Frontend.Ui.Support.Parsers;
using Enigma.Frontend.Ui.WindowsFlow;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Enigma.Frontend.Ui.ViewModels;

/// <summary>ViewModel for Prenatal window</summary>
public partial class PrenatalViewModel : ObservableObject
{
    private const string VM_IDENTIFICATION = ChartsWindowsFlow.PRENATAL;

    private PreNatalModel _preNatalModel = App.ServiceProvider.GetRequiredService<PreNatalModel>();
    private IDateInputParser _dateInputParser = App.ServiceProvider.GetRequiredService<IDateInputParser>();
    private IJulianDayApi _julianDayApi = App.ServiceProvider.GetRequiredService<IJulianDayApi>();
    private double _originalConceptionJd;
    private bool _hasRecalculation;

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
    [ObservableProperty] private string _combineDate = string.Empty;
    [ObservableProperty] private bool _isResetEnabled;
    [ObservableProperty] private bool _isCombineEnabled = true;
    
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
        _preNatalModel.SetInitialConception();
        _originalConceptionJd = DataVaultCharts.Instance.GetCurrentChart().InputtedChartData.FullDateTime.JulianDayForEt - 273.217;
        _hasRecalculation = false;
        IsResetEnabled = false;
        IsCombineEnabled = true;
    }
    
    
    
    
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

    partial void OnSelectedEventChanged(PresentablePreNatalEvent? value)
    {
        if (value != null && !string.IsNullOrEmpty(value.DateTime))
        {
            // Extract date part (yyyy/mm/dd) from DateTime string (format: "yyyy/mm/dd hh:mi:ss")
            var dateTimeParts = value.DateTime.Split(' ');
            if (dateTimeParts.Length > 0)
            {
                CombineDate = dateTimeParts[0];
            }
        }
    }

    [RelayCommand]
    private void CorrectConception()
    {
        if (SelectedMoment == null)
        {
            MessageBox.Show("Please select a moment from the moments datagrid.");
            return;
        }

        if (string.IsNullOrWhiteSpace(CombineDate))
        {
            MessageBox.Show("Please enter a valid date in the CombineDate field.");
            return;
        }

        // Get calendar from current chart
        var currentChart = DataVaultCharts.Instance.GetCurrentChart();
        var calendar = currentChart.InputtedChartData.FullDateTime.DateText.Contains("[g]")
            ? Calendars.Gregorian
            : Calendars.Julian;

        // Validate the date
        if (!_dateInputParser.HandleDate(CombineDate, calendar, YearCounts.CE, out FullDate? fullDate))
        {
            MessageBox.Show("The date in CombineDate is not valid. Please enter a valid date in the format yyyy/mm/dd.");
            return;
        }

        if (fullDate == null)
        {
            MessageBox.Show("The date in CombineDate is not valid. Please enter a valid date in the format yyyy/mm/dd.");
            return;
        }

        // Convert date to Julian Day
        var simpleDateTime = new SimpleDateTime(fullDate.YearMonthDay[0], fullDate.YearMonthDay[1], fullDate.YearMonthDay[2], 0.0, calendar);
        var julianDayResponse = _julianDayApi.GetJulianDay(simpleDateTime);
        var eventJd = julianDayResponse.JulDayEt;

        var momentJd = SelectedMoment.Jd;
        _preNatalModel.DefineActualConceptionJd(eventJd, momentJd);
        Log.Information($"PrenatalViewModel.CorrectConception(): Combining moment {SelectedMoment.RealDateTime} with date {CombineDate}");
        
        _hasRecalculation = true;
        IsResetEnabled = true;
        IsCombineEnabled = false;
        Populate();       
    }

    [RelayCommand]
    private void Reset()
    {
        if (!_hasRecalculation) return;

        // Restore original conception by calling DefineActualConceptionJd with baseConceptionJd for both parameters
        // This will result in: baseConceptionJd + (baseConceptionJd - baseConceptionJd) / conversionFactor = baseConceptionJd
        var currentChart = DataVaultCharts.Instance.GetCurrentChart();
        var radixJd = currentChart.InputtedChartData.FullDateTime.JulianDayForEt;
        var baseConceptionJd = radixJd - 273.217;
        _preNatalModel.DefineActualConceptionJd(baseConceptionJd, baseConceptionJd);
        
        _hasRecalculation = false;
        IsResetEnabled = false;
        IsCombineEnabled = true;
        Populate();
    }

    [RelayCommand]
    private void NewEvent()
    {
        if (_preNatalModel.CreateNewEvent()) Populate();
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
        ChartName = DataVaultCharts.Instance.GetCurrentChart().InputtedChartData.MetaData.Name;
        _preNatalModel.useAspects = UseAspects;
        _preNatalModel.useEclipses = UseEclipses;
        _preNatalModel.useIngresses = UseIngresses;
        _preNatalModel.useRetrogradeDirect = UseRetrogradeDirect;
        PreNatalMoments = _preNatalModel.GetPrenatalMoments();
        PreNatalEvents = _preNatalModel.GetPrenatalEvents();  
        SelectedFactors = _preNatalModel.GetSelectedFactorsGlyphs();
        SelectedAspects = _preNatalModel.GetSelectedAspectsGlyphs();
        StandardConception = _preNatalModel.standardConception;
        ActualConception = _preNatalModel.actualConception;
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
