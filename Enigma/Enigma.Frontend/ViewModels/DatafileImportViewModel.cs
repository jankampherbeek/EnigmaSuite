// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
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

/// <summary>ViewModel for the import of data files.
/// Uses standard Windows file dialog to import datafiles. Saves imported files.
/// Sends messages: CloseMessage and HelpMessage.</summary>
public partial class DatafileImportViewModel: ObservableObject
{
    private const string VM_IDENTIFICATION = ResearchWindowsFlow.DATAFILE_IMPORT;
    private const string ERROR_MISSING_FILE_DEFS = "Define both the file to import and a name for the data";
    private const string ERROR_FILE_IO = "An error occurred while reading or writing the files ";
    private const string ERROR_FILENAME_IN_USE = "The name for the data is already in use";
    private const string ERROR = "Error";
    private const string EMPTY_STRING = "";
    [ObservableProperty] private string _datafileName = string.Empty;
    [ObservableProperty] private string _datasetName = string.Empty;
    [ObservableProperty] private string _resultText = string.Empty;
    [ObservableProperty] private string _errorText = string.Empty;
    

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
        ResultText = "";
        if (string.IsNullOrWhiteSpace(DatasetName) || string.IsNullOrWhiteSpace(DatafileName))
        {
            MessageBox.Show(ERROR_MISSING_FILE_DEFS);
        }
        else
        {
            DatafileImportModel model = App.ServiceProvider.GetRequiredService<DatafileImportModel>();
            if (model.CheckIfNameCanBeUsed(DatasetName))
            {
                ResultMessage resultMsg = model.PerformImport(DatafileName, DatasetName);
                if (resultMsg.ErrorCode > ResultCodes.OK)
                {
                    ErrorText = ERROR;
                    ResultText = ERROR_FILE_IO + resultMsg.Message;
                }
                else
                {
                    ErrorText = EMPTY_STRING;
                    ResultText = resultMsg.Message;
                }
            }
            else
            {
                ErrorText = ERROR;
                ResultText = ERROR_FILENAME_IN_USE;
            }
        }
    }

    [RelayCommand]
    private static void Close()
    {
        Log.Information("DatafileImportViewModel.Close(): send CloseMessage");        
        WeakReferenceMessenger.Default.Send(new CloseMessage(VM_IDENTIFICATION)); 
    }
    
    [RelayCommand]
    private static void Help()
    {
        Log.Information("DatafileImportViewModel.Help(): send HelpMessage");   
        WeakReferenceMessenger.Default.Send(new HelpMessage(VM_IDENTIFICATION));
    }

}