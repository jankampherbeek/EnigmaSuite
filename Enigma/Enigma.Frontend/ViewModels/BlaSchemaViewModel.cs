// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2025.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Enigma.Api.Configuration;
using Enigma.Domain.Dtos;
using Enigma.Domain.References;
using Enigma.Frontend.Ui.Messaging;
using Enigma.Frontend.Ui.Models;
using Enigma.Frontend.Ui.State;
using Enigma.Frontend.Ui.WindowsFlow;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Enigma.Frontend.Ui.ViewModels;

/// <summary>ViewModel for BLA Schema window</summary>
public partial class BlaSchemaViewModel : ObservableObject
{
    private const string VM_IDENTIFICATION = ChartsWindowsFlow.BLA_SCHEMA;
    private BlaModel _model = App.ServiceProvider.GetRequiredService<BlaModel>();
    
    [ObservableProperty] private string _chartName = "Chart Name";


    public void Populate()
    {
        // TODO find correct values for Housesystem, Chiron and Eris 
        var houseSystem = HouseSystems.Placidus;
        var useChiron = true;
        var useEris = true;
        
        _model.CreateDataForBla(houseSystem, useChiron, useEris);

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
}
