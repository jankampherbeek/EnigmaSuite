// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2025.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System;
using System.Collections.Generic;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Enigma.Core.Slices.ProgCalendar;
using Enigma.Domain.Constants;
using Enigma.Frontend.Ui.Messaging;
using Enigma.Frontend.Ui.Models;
using Enigma.Frontend.Ui.Support.Parsers;
using Enigma.Frontend.Ui.WindowsFlow;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Enigma.Frontend.Ui.ViewModels;

/// <summary>ViewModel for Progressive Calendar window</summary>
public partial class ProgCalViewModel : ObservableObject
{
    private const string VM_IDENTIFICATION = ChartsWindowsFlow.PROG_CAL;
    
    private ProgCalModel _model = App.ServiceProvider.GetRequiredService<ProgCalModel>();
    
    [ObservableProperty] private string _chartName = "Chart Name";
    [ObservableProperty] private List<PresentableProgCalItem> _progCalItems;
    [ObservableProperty] private List<PresentableProgCalPeriod> _progCalPeriods;
    [ObservableProperty] private bool _useSecundary;
    [ObservableProperty] private bool _useParallels;
    [ObservableProperty] private bool _useExtPeriod;
    [ObservableProperty] private string _date;
    
    [RelayCommand]
    private void Help()
    {
        Log.Information("ProgCalViewModel.Help(): send HelpMessage");
        WeakReferenceMessenger.Default.Send(new HelpMessage(VM_IDENTIFICATION));
    }

    [RelayCommand]
    private void Close()
    {
        Log.Information("ProgCalViewModel.Close(): send CloseMessage");
        WeakReferenceMessenger.Default.Send(new CloseMessage(VM_IDENTIFICATION));
    }

    public ProgCalViewModel()
    {
        ChartName = _model.GetCurrentChartName();
        DateOnly currentDate = DateOnly.FromDateTime(DateTime.Now);
        _date = $"{currentDate.Year}/{currentDate.Month}/{currentDate.Day}";
        _model.CheckDate(_date);
        Populate();
    }

    private void Populate()
    {
        _model.useExtPeriod = UseExtPeriod;
        _model.useSecundary = UseSecundary;
        _model.useParallels = UseParallels;
        _model.DefineProgCal();
        ProgCalItems = _model.allItems;
        ProgCalPeriods = _model.allPeriods;
    }
    
    
    public void UpdateParallels(bool useIt)
    {
        UseParallels = useIt;
        Populate();
    }

    public void UpdateSecundary(bool useIt)
    {
        UseSecundary = useIt;
        Populate();       
    }

    public void UpdateExtPeriod(bool useIt)
    {
        UseExtPeriod = useIt;
        Populate();       
    }

    partial void OnDateChanged(string value)
    {
        if (_model.CheckDate(value))
        {
            Date = value;
            Populate();
        }
        else
        {
            MessageBox.Show("Please enter a valid date in the format yyyy/mm/dd", "Error in date");
        }
    }
}
