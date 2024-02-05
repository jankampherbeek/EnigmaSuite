// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.ObjectModel;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Enigma.Domain.Presentables;
using Enigma.Frontend.Ui.Messaging;
using Enigma.Frontend.Ui.Models;
using Enigma.Frontend.Ui.State;
using Enigma.Frontend.Ui.Support;
using Enigma.Frontend.Ui.WindowsFlow;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Enigma.Frontend.Ui.ViewModels;

/// <summary>ViewModel for main charts screen</summary>
public partial class ChartsMainViewModel: ObservableObject, 
    IRecipient<NewChartMessage>, 
    IRecipient<FoundChartMessage>,
    IRecipient<ConfigUpdatedMessage>,
    IRecipient<CloseRadixDataInputViewMessage>
{
    private const string VM_IDENTIFICATION = GeneralWindowsFlow.CHARTS_MAIN;
    private const string ABOUT_CHARTS = "AboutCharts";
    private readonly ChartsMainModel _model;
    // ReSharper disable once NotAccessedField.Local  An instance of ChartsWindowsFlow must be instantiated so it can
    // handle incoming messages.
    private readonly ChartsWindowsFlow _chartsWindowsFlow;    
    private readonly DataVaultCharts _dataVaultCharts = DataVaultCharts.Instance;
    [NotifyCanExecuteChangedFor(nameof(DeleteChartCommand))]
    [NotifyCanExecuteChangedFor(nameof(ShowWheelCommand))]
    [NotifyCanExecuteChangedFor(nameof(ShowPositionsCommand))]
    [NotifyCanExecuteChangedFor(nameof(AspectsCommand))]
    [NotifyCanExecuteChangedFor(nameof(MidpointsCommand))]
    [NotifyCanExecuteChangedFor(nameof(HarmonicsCommand))]
    [NotifyCanExecuteChangedFor(nameof(ProgressionsCommand))]
    [NotifyPropertyChangedFor(nameof(SelectedChart))]
    [ObservableProperty] private int _chartIndex = -1;
    [ObservableProperty] private string _nrOfChartsInDatabase = string.Empty;
    [ObservableProperty] private string _lastAddedChart = string.Empty;
    [ObservableProperty] private string _currentlySelectedChart = string.Empty;
    [ObservableProperty] private ObservableCollection<PresentableChartData> _availableCharts = new();
    [NotifyPropertyChangedFor(nameof(ChartIndex))]
    [ObservableProperty] private PresentableChartData? _selectedChart;
    
    public ChartsMainViewModel()
    {
        WeakReferenceMessenger.Default.Register<NewChartMessage>(this);
        WeakReferenceMessenger.Default.Register<FoundChartMessage>(this);
        WeakReferenceMessenger.Default.Register<ConfigUpdatedMessage>(this);
        WeakReferenceMessenger.Default.Register<CloseRadixDataInputViewMessage>(this);
        _chartsWindowsFlow = App.ServiceProvider.GetRequiredService<ChartsWindowsFlow>();
        _model = App.ServiceProvider.GetRequiredService<ChartsMainModel>();
        Log.Information("ChartsMainviewModel constructor: calling Populate()");
        Populate();
    }

    [RelayCommand]
    private void ItemChanged()
    {
        if (ChartIndex >= 0)
        {
            SelectedChart = AvailableCharts[ChartIndex];
            _model.SetCurrentChartId(ChartIndex);
        }
        Log.Information("ChartsMainviewModel.ItemChanged(): calling Populate()");
        Populate();
    }

    [RelayCommand]
    private void Populate()
    {
        Log.Information("ChartsMainViewModel.Populate()");
        NrOfChartsInDatabase = "Nr. of charts in database : " + _model.CountPersistedCharts();
        LastAddedChart = "Last added to database : " + _model.MostRecentChart();
        CurrentlySelectedChart = "Currently selected : " + _model.CurrentChartName();
        SelectedChart = _model.GetCurrentChart();
        
        AvailableCharts = new ObservableCollection<PresentableChartData>(_model.AvailableCharts());
        ChartIndex = _model.IndexCurrentChart();
    }

    
    
    [RelayCommand]
    private static void AppSettings()
    {
        Log.Information("ChartsMainViewModel.AppSettings(): send OpenMessage");
        WeakReferenceMessenger.Default.Send(new OpenMessage(VM_IDENTIFICATION, GeneralWindowsFlow.APP_SETTINGS));
    }
        
    [RelayCommand]
    private static void Configuration()
    {
        Log.Information("ChartsMainViewModel.Configuration(): send OpenMessage");
        WeakReferenceMessenger.Default.Send(new OpenMessage(VM_IDENTIFICATION, GeneralWindowsFlow.CONFIGURATION));
    }
    
    [RelayCommand]
    private static void ConfigProg()
    {
        Log.Information("ChartsMainViewModel.ConfigProg(): send OpenMessage");
        WeakReferenceMessenger.Default.Send(new OpenMessage(VM_IDENTIFICATION, GeneralWindowsFlow.CONFIG_PROG));
    }

    [RelayCommand]
    private void NewChart()
    {
        if (_model.CreateNewChart()) Populate();
    }

    [RelayCommand(CanExecute = nameof(IsChartSelected))]
    private void Progressions()
    {
        Log.Information("ChartsMainViewModel.Progressions(): send OpenMessage");
        WeakReferenceMessenger.Default.Send(new OpenMessage(VM_IDENTIFICATION, ChartsWindowsFlow.PROGRESSIVE_MAIN));
    }
    
    [RelayCommand]
    private static void SearchChart()
    {
        Log.Information("ChartsMainViewModel.SearchChart(): send OpenMessage");
        WeakReferenceMessenger.Default.Send(new OpenMessage(VM_IDENTIFICATION, ChartsWindowsFlow.RADIX_SEARCH));
    }

    [RelayCommand(CanExecute = nameof(IsChartSelected))]
    private void DeleteChart()
    {
        Log.Information("ChartsMainViewModel.DeleteChart(): send OpenMessage");
        var currentChart = _dataVaultCharts.GetCurrentChart();
        string name = currentChart != null ? currentChart.InputtedChartData.MetaData.Name : "";
        if (MessageBox.Show("Do you want to delete the chart for  "+ name + " from the database?",
                "Delete chart",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question) == MessageBoxResult.Yes)
            MessageBox.Show(
                _model.DeleteCurrentChart()
                    ? "The chart for "+ name + " was succesfully deleted."
                    : "The chart for  "+ name + " was not found and could not be deleted.",
                "Result of delete");
        Log.Information("ChartsMainviewModel.DeleteChart(): calling Populate()");        
        Populate();
    }

    [RelayCommand(CanExecute = nameof(IsChartSelected))]
    private void ShowWheel()
    {
        Log.Information("ChartsMainViewModel.ShowWheel(): send OpenMessage");
        WeakReferenceMessenger.Default.Send(new OpenMessage(VM_IDENTIFICATION, ChartsWindowsFlow.CHARTS_WHEEL));
    }
    
    [RelayCommand(CanExecute = nameof(IsChartSelected))]
    private void ShowPositions()
    {
        Log.Information("ChartsMainViewModel.ShowPositions(): send OpenMessage");
        WeakReferenceMessenger.Default.Send(new OpenMessage(VM_IDENTIFICATION, ChartsWindowsFlow.RADIX_POSITIONS));
    }

    [RelayCommand(CanExecute = nameof(IsChartSelected))]
    private void Aspects()
    {
        Log.Information("ChartsMainViewModel.Aspects(): send OpenMessage");
        WeakReferenceMessenger.Default.Send(new OpenMessage(VM_IDENTIFICATION, ChartsWindowsFlow.RADIX_ASPECTS));
    }

    [RelayCommand(CanExecute = nameof(IsChartSelected))]
    private void Harmonics()
    {
        Log.Information("ChartsMainViewModel.Harmonics(): send OpenMessage");
        WeakReferenceMessenger.Default.Send(new OpenMessage(VM_IDENTIFICATION, ChartsWindowsFlow.RADIX_HARMONICS));
    }
    
    [RelayCommand(CanExecute = nameof(IsChartSelected))]
    private void Midpoints()
    {
        Log.Information("ChartsMainViewModel.Midpoints(): send OpenMessage");
        WeakReferenceMessenger.Default.Send(new OpenMessage(VM_IDENTIFICATION, ChartsWindowsFlow.RADIX_MIDPOINTS));
    }

    
    [RelayCommand]
    private static void About()
    {
        Log.Information("ChartsMainViewModel.About(): send HelpMessage");
        WeakReferenceMessenger.Default.Send(new HelpMessage(ABOUT_CHARTS));
    }
    
    [RelayCommand]
    private static void Help()
    {
        Log.Information("ChartsMainViewModel.Help(): send HelpMessage");
        WeakReferenceMessenger.Default.Send(new HelpMessage(VM_IDENTIFICATION));
    }


    [RelayCommand]
    private static void UserManual()
    {
       UserManual userManual = new();
       userManual.ShowUserManual();
    }
    
    /// <summary>Closes all child windows of main chart window. Clears all charts in DataVault.</summary>
    [RelayCommand]
    private void Close()
    {
        _dataVaultCharts.ClearExistingCharts();
        Log.Information("ChartsMainViewModel.Close(): send CloseChildWindowsMessage amd CloseMessage");
        WeakReferenceMessenger.Default.Send(new CloseChildWindowsMessage(VM_IDENTIFICATION));
        WeakReferenceMessenger.Default.Send(new CloseMessage(VM_IDENTIFICATION));
    }
    

    private bool IsChartSelected()
    {
        return ChartIndex >= 0;
    }
    

    public void Receive(NewChartMessage message)
    {
        Log.Information("ChartsMainViewModel.Receive(NewChartMessage) with value {Value}", message.Value);
        
        if (!_dataVaultCharts.GetNewChartAdded()) return;
        Log.Information("ChartsMainViewModel.Receive(NewChartMessage): calls model.SaveCurrentChart");
        long newIndex = _model.SaveCurrentChart();
        if (_dataVaultCharts.GetCurrentChart() == null) return;
        _dataVaultCharts.GetCurrentChart()!.InputtedChartData.Id = newIndex;
        Log.Information("ChartsMainviewModel.Receive(NewChartMessage): calling Populate()");        
        Populate();
    }

    public void Receive(FoundChartMessage message)
    {
        Log.Information("ChartsMainviewModel.Receive(FoundChartMessage): calling Populate()");            
        Populate();
        DataVaultCharts.Instance.SetCurrentChart(message.ChartId);
    }

    public void Receive(ConfigUpdatedMessage message)
    {
        Log.Information("ChartsMainviewModel.Receive(ConfigUpdatedMessage): calling Populate()");    
        Populate();
    }

    public void Receive(CloseRadixDataInputViewMessage message)
    {
        _model.CloseRadixDataInputWindow();
    }
}