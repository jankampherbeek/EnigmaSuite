// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using CommunityToolkit.Mvvm.Messaging;
using Enigma.Frontend.Ui.Messaging;
using Enigma.Frontend.Ui.State;
using Enigma.Frontend.Ui.Views;
using Serilog;

namespace Enigma.Frontend.Ui.WindowsFlow;

/// <summary>Manages navigation between windows.</summary>
/// <remarks>Should be created as a singleton.</remarks>
public class ChartsWindowsFlow:
    IRecipient<CloseMessage>,
    IRecipient<CloseNonDlgMessage>,
    IRecipient<CloseChildWindowsMessage>,
    IRecipient<OpenMessage>
{
    // Constants for the names of general views. The names are without the parts 'Window', 'ViewModel' and 'Model'. 
    public const string CHARTS_WHEEL = "ChartsWheel";
    public const string CONFIG_PROG = "ConfigProg";
    public const string PROG_EVENT = "ProgEvent";
    public const string PROG_EVENT_RESULTS = "ProgEventResults";
    public const string PROGRESSIVE_MAIN = "ProgressiveMain";
    public const string RADIX_POSITIONS = "RadixPositions";
    public const string RADIX_SEARCH = "RadixSearch";
    public const string RADIX_ASPECTS = "RadixAspects";
    public const string RADIX_DATA_INPUT = "RadixDataInput";
    public const string RADIX_MIDPOINTS = "RadixMidpoints";
    public const string RADIX_HARMONICS = "RadixHarmonics";


    
    private RadixDataInputWindow? _radixDataInputWindow;
    private RadixSearchWindow? _radixSearchWindow;
    private ProgEventWindow? _progEventWindow;
    private ProgEventResultsWindow? _progEventResultsWindow;
    private ProgressiveMainWindow? _progressiveMainWindow;
    private RadixPositionsWindow? _radixPositionsWindow;
    private ChartsWheelWindow? _chartsWheelWindow;
    private RadixAspectsWindow? _radixAspectsWindow;
    private RadixMidpointsWindow? _radixMidpointsWindow;
    private RadixHarmonicsWindow? _radixHarmonicsWindow;
    
    // The tuple contains the id for window, the current window and the textid for the parent window.
    private readonly List<Tuple<int, Window, string>> _openWindows = new();
    private int _windowCounter;
    
    public ChartsWindowsFlow()
    {
        WeakReferenceMessenger.Default.Register<CloseMessage>(this);
        WeakReferenceMessenger.Default.Register<CloseNonDlgMessage>(this);
        WeakReferenceMessenger.Default.Register<CloseChildWindowsMessage>(this);
        WeakReferenceMessenger.Default.Register<OpenMessage>(this);
    }
    
    public void Receive(CloseMessage message)
    {
        switch (message.Value)
        {
            case RADIX_DATA_INPUT:
                _radixDataInputWindow?.Close();
                break;
            case RADIX_SEARCH:
                _radixSearchWindow?.Close();
                break;
            case PROGRESSIVE_MAIN:
                _progressiveMainWindow?.Close();
                break;
            case PROG_EVENT:
                _progEventWindow?.Close();
                break;
        }
    }

    public void Receive(OpenMessage message)        // handles dialogs and delegates the handling of non-dialog views. 
    {
        switch (message.ViewToOpen)
        {
            case RADIX_DATA_INPUT:
                _radixDataInputWindow = new RadixDataInputWindow();
                _radixDataInputWindow.ShowDialog();
                break;
            case RADIX_SEARCH:
                _radixSearchWindow = new RadixSearchWindow();
                _radixSearchWindow.ShowDialog();
                break;
            case PROGRESSIVE_MAIN:
                _progressiveMainWindow = new ProgressiveMainWindow();
                _progressiveMainWindow.ShowDialog();
                break;
            case PROG_EVENT:
                _progEventWindow = new ProgEventWindow();
                _progEventWindow.ShowDialog();
                break;
            default:
                HandleNonDialogView(message.ViewToOpen, message.ParentView);
                break;
        }
    }

    private void HandleNonDialogView(string viewToOpen, string parentView)
    {
        _windowCounter++;
        DataVaultCharts.Instance.LastWindowId = _windowCounter;
        switch (viewToOpen)
        {
            case PROGRESSIVE_MAIN:
                _progressiveMainWindow = new ProgressiveMainWindow();
                _openWindows.Add(new Tuple<int, Window, string>(_windowCounter, _progressiveMainWindow, parentView));
                _progressiveMainWindow.Show();
                break;
            case PROG_EVENT_RESULTS:
                _progEventResultsWindow = new ProgEventResultsWindow();
                _openWindows.Add(new Tuple<int, Window, string>(_windowCounter, _progEventResultsWindow, parentView));
                _progEventResultsWindow.Show();
                break;
            case CHARTS_WHEEL:
                _chartsWheelWindow = new ChartsWheelWindow();
                _openWindows.Add(new Tuple<int, Window, string>(_windowCounter, _chartsWheelWindow, parentView));
                _chartsWheelWindow.Show();
                _chartsWheelWindow.Populate();
                break;
            case RADIX_POSITIONS:
                _radixPositionsWindow = new RadixPositionsWindow();
                _openWindows.Add(new Tuple<int, Window, string>(_windowCounter, _radixPositionsWindow, parentView));
                _radixPositionsWindow.Show();
                break;
            case RADIX_ASPECTS:
                _radixAspectsWindow = new RadixAspectsWindow();
                _openWindows.Add(new Tuple<int, Window, string>(_windowCounter, _radixAspectsWindow, parentView));
                _radixAspectsWindow.Show();
                break;
            case RADIX_HARMONICS:
                _radixHarmonicsWindow = new RadixHarmonicsWindow();
                _openWindows.Add(new Tuple<int, Window, string>(_windowCounter, _radixHarmonicsWindow, parentView));
                _radixHarmonicsWindow.Show();
                break;
            case RADIX_MIDPOINTS:
                _radixMidpointsWindow = new RadixMidpointsWindow();
                _openWindows.Add(new Tuple<int, Window, string>(_windowCounter, _radixMidpointsWindow, parentView));
                _radixMidpointsWindow.Show();
                break;
        }
    }
    
    
    public void Receive(CloseNonDlgMessage message)
    {
        Log.Information("ChartsWindowsFlow.Receive(CloseNonDlgMessage) with value {Value}", message.Value);
        int windowId = message.WindowId;
        Tuple<int, Window, string>? windowToRemove = null;
        foreach (Tuple<int, Window, string> openWindow in _openWindows.Where(openWindow => 
                     openWindow.Item1 == windowId))
        {
            openWindow.Item2.Close();
            windowToRemove = openWindow;   
           
        }
        if (windowToRemove is not null) _openWindows.Remove(windowToRemove);
    }

    public void Receive(CloseChildWindowsMessage message)
    {
        Log.Information("ChartsWindowsFlow.Receive(CloseChildWindowsMessage) with value {Value}", message.Value);        
        List<Tuple<int, Window, string>> windowsToRemove = new();
        foreach (Tuple<int, Window, string>? openWindow in _openWindows.Where(window => 
                     message.Value == window.Item3))
        {
            windowsToRemove.Add(openWindow);
            openWindow.Item2.Close(); 
        }
        foreach (var windowToRemove in windowsToRemove)
        {
            _openWindows.Remove(windowToRemove);
        }
    }

  
}