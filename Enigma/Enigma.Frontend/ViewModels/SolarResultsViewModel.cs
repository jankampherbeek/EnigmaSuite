// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2025.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Enigma.Domain.Constants;
using Enigma.Domain.Presentables;
using Enigma.Frontend.Ui.Messaging;
using Enigma.Frontend.Ui.Models;
using Enigma.Frontend.Ui.State;
using Enigma.Frontend.Ui.WindowsFlow;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Enigma.Frontend.Ui.ViewModels;

public partial class SolarResultsViewModel : ObservableObject
{
    private const string VM_IDENTIFICATION = ChartsWindowsFlow.SOLAR_RESULTS;
    private readonly int _windowId = DataVaultCharts.Instance.LastWindowId;

    [ObservableProperty] private string _chartName;
    [ObservableProperty] private bool _showSignBackgroundColors = true;
    [ObservableProperty] private List<PresentableProgPosition> _solarPositions;
    [ObservableProperty] private List<PresentableProgAspect> _solarAspects;
    [ObservableProperty] private string _orbSolarText;
    
    private double _orbSolarValue;
    
    public SolidColorBrush OrbSolarValid => CheckOrbSolar() ? Brushes.Gray : Brushes.Red;
    public SolidColorBrush OrbSolarUnderline => CheckOrbSolar() ? Brushes.Transparent : Brushes.Red;
    
    public SolarResultsViewModel()
    {
        var _model = App.ServiceProvider.GetRequiredService<SolarResultsModel>();
     //   _model.HandleAspects(_orbSolarValue);
        ChartName = _model.GetChartName();
        SolarPositions = _model.GetSolarPositions();
        _orbSolarValue = _model.SolarOrb;        
        OrbSolarText = _orbSolarValue.ToString((CultureInfo.InvariantCulture));
        
        // Calculate aspects immediately so they show when the aspects tab is opened
        Calculate();
    }

    /// <summary>
    /// Initialize with calculated solar data
    /// </summary>
    /// <param name="model">The solar results model with calculated data</param>
    public void InitializeWithSolarData(SolarResultsModel model)
    {
        
        ChartName = model.GetChartName();
        SolarPositions = model.GetSolarPositions();
    }

    private bool CheckOrbSolar()
    {
        return double.TryParse(OrbSolarText.Replace(',', '.'), NumberStyles.Any, 
            CultureInfo.InvariantCulture, out _orbSolarValue);
    }

    private string FindErrors()
    {
        StringBuilder errorsText = new();
        if (!CheckOrbSolar())
        {
            errorsText.Append(StandardTexts.ERROR_ORB_SOLAR + EnigmaConstants.NEW_LINE);
        }
        return errorsText.ToString();
    }

   
    [RelayCommand]
    private void ReCalc()
    {
        var errorTxt = FindErrors();
        if (!string.IsNullOrEmpty(errorTxt))
        {
            MessageBox.Show(errorTxt, StandardTexts.TITLE_ERROR);
            return;
        }
        Calculate();
    }

    private void Calculate()
    {   var _model = App.ServiceProvider.GetRequiredService<SolarResultsModel>();
        _model.HandleAspects(_orbSolarValue);
        SolarAspects = _model.SolarAspects;
    }
    
    [RelayCommand]
    private void Close()
    {
        WeakReferenceMessenger.Default.Send(new CloseNonDlgMessage(VM_IDENTIFICATION, _windowId));
    }

    [RelayCommand]
    private static void Help()
    {
        Log.Information("SolarResultsViewModel.Help(): send HelpMessage");
        WeakReferenceMessenger.Default.Send(new HelpMessage(VM_IDENTIFICATION));
    }
} 