// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Enigma.Domain.Constants;
using Enigma.Domain.Responses;
using Enigma.Frontend.Ui.Messaging;
using Enigma.Frontend.Ui.Models;
using Enigma.Frontend.Ui.WindowsFlow;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Enigma.Frontend.Ui.ViewModels;

public partial class ChartsImportViewModel: ObservableObject
{
    private const string VM_IDENTIFICATION = ChartsWindowsFlow.CHARTS_IMPORT;
    [ObservableProperty] private string _datafileName = string.Empty;
    [ObservableProperty] private string _resultText = string.Empty;
   
    
    [RelayCommand]
    private void Import()
    {
        TryImport();
    }
    
    [RelayCommand]
    private void Browse()
    {
        PerformBrowse();
    }
    
    private void PerformBrowse()
    {
        Microsoft.Win32.OpenFileDialog openFileDlg = new();
        bool? result = openFileDlg.ShowDialog();
        if (result == true)
        {
            DatafileName = openFileDlg.FileName;
        }
    }

    private void TryImport()
    {
        ChartsImportModel model = App.ServiceProvider.GetRequiredService<ChartsImportModel>();
        ResultText = model.PerformImport(DatafileName);
    }

    
    [RelayCommand]
    private static void Help()
    {
        Log.Information("DatafileImportViewModel.Help(): send HelpMessage");   
        WeakReferenceMessenger.Default.Send(new HelpMessage(VM_IDENTIFICATION));
    }
}