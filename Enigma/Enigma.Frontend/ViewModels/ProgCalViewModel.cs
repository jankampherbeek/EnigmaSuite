// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2025.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
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
    private readonly SemaphoreSlim _recalculationSemaphore = new SemaphoreSlim(1, 1);
    private volatile bool _recalculationPending;
    private CancellationTokenSource? _dateChangeCancellation;
    
    [ObservableProperty] private string _chartName = "Chart Name";
    [ObservableProperty] private List<PresentableProgCalItem> _progCalItems;
    [ObservableProperty] private List<PresentableProgCalPeriod> _progCalPeriods;
    [ObservableProperty] private bool _useSecundary;
    [ObservableProperty] private bool _useParallels;
    [ObservableProperty] private bool _useExtPeriod;
    [ObservableProperty] private string _date;
    [ObservableProperty] private bool _isLoading;
    
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
        _ = PopulateAsync();
    }

    private async Task PopulateAsync()
    {
        // Mark that a recalculation is needed
        _recalculationPending = true;
        
        // Try to acquire the semaphore. If already in progress, wait for it.
        // This ensures only one recalculation runs at a time.
        await _recalculationSemaphore.WaitAsync();
        
        try
        {
            // Process recalculations in a loop to handle multiple pending requests
            // This ensures that if multiple changes occurred while waiting,
            // we process them all with the latest values
            while (_recalculationPending)
            {
                // Clear the pending flag before starting this recalculation
                _recalculationPending = false;
                
                IsLoading = true;
                try
                {
                    await Task.Run(() =>
                    {
                        _model.useExtPeriod = UseExtPeriod;
                        _model.useSecundary = UseSecundary;
                        _model.useParallels = UseParallels;
                        _model.DefineProgCal();
                    });

                    ProgCalItems = _model.allItems;
                    ProgCalPeriods = _model.allPeriods;
                }
                finally
                {
                    IsLoading = false;
                }
                
                // Check again if another recalculation was requested during execution
                // If so, process it with the latest values
            }
        }
        finally
        {
            _recalculationSemaphore.Release();
        }
    }
    
    
    public void UpdateParallels(bool useIt)
    {
        UseParallels = useIt;
        // If a recalculation is in progress, it will pick up the new value
        // and trigger another recalculation if needed
        _ = PopulateAsync();
    }

    public void UpdateSecundary(bool useIt)
    {
        UseSecundary = useIt;
        // If a recalculation is in progress, it will pick up the new value
        // and trigger another recalculation if needed
        _ = PopulateAsync();       
    }

    public void UpdateExtPeriod(bool useIt)
    {
        UseExtPeriod = useIt;
        // If a recalculation is in progress, it will pick up the new value
        // and trigger another recalculation if needed
        _ = PopulateAsync();       
    }

    partial void OnDateChanged(string value)
    {
        // Cancel any pending date change recalculation
        _dateChangeCancellation?.Cancel();
        _dateChangeCancellation?.Dispose();
        _dateChangeCancellation = new CancellationTokenSource();
        
        var cancellationToken = _dateChangeCancellation.Token;
        
        // Debounce: wait a bit before processing the date change
        // This prevents recalculations on every keystroke
        _ = Task.Run(async () =>
        {
            try
            {
                await Task.Delay(500, cancellationToken); // Wait 500ms after last keystroke
                
                if (cancellationToken.IsCancellationRequested)
                    return;
                
                // Check if the date is valid on the UI thread
                await Application.Current.Dispatcher.InvokeAsync(() =>
                {
                    if (_model.CheckDate(value))
                    {
                        // Date property is already updated by the binding, just trigger recalculation
                        _ = PopulateAsync();
                    }
                    else if (!string.IsNullOrWhiteSpace(value) && value.Length >= 8)
                    {
                        // Only show error if the input is substantial (at least 8 chars like "2025/01/")
                        // This prevents error messages while user is still typing
                        MessageBox.Show("Please enter a valid date in the format yyyy/mm/dd", "Error in date");
                    }
                });
            }
            catch (OperationCanceledException)
            {
                // Expected when a new date change occurs
            }
        }, cancellationToken);
    }
}
