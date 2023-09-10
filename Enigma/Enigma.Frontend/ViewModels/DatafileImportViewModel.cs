// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Enigma.Domain.Constants;
using Enigma.Domain.RequestResponse;
using Enigma.Frontend.Ui.Models;
using Enigma.Frontend.Ui.State;
using Enigma.Frontend.Ui.Views;
using Microsoft.Extensions.DependencyInjection;

namespace Enigma.Frontend.Ui.ViewModels;

/// <summary>ViewModel for the import of data files</summary>
public partial class DatafileImportViewModel: ObservableObject
{
    [ObservableProperty] private string _datafileName = string.Empty;
    [ObservableProperty] private string _datasetName = string.Empty;
    [ObservableProperty] private string _resultText = string.Empty;
    [ObservableProperty] private string _errorText = string.Empty;
    
    [RelayCommand]
    private static void Help()
    {
        ShowHelp();
    }

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
            MessageBox.Show("Define both the file to import and a name for the data");
        }
        else
        {
            DatafileImportModel model = App.ServiceProvider.GetRequiredService<DatafileImportModel>();
            if (model.CheckIfNameCanBeUsed(DatasetName))
            {
                ResultMessage resultMsg = model.PerformImport(DatafileName, DatasetName);
                if (resultMsg.ErrorCode > ResultCodes.OK)
                {
                    ErrorText = "Error";
                    ResultText = "An error occurred while reading or writing the files " + resultMsg.Message;
                }
                else
                {
                    ErrorText = "";
                    ResultText = resultMsg.Message;
                }
            }
            else
            {
                ErrorText = "Error";
                ResultText = "The name for the data is already in use";
            }
        }
    }

    private static void ShowHelp()
    {
        DataVault.Instance.CurrentViewBase = "DatafileImport";
        new HelpWindow().ShowDialog();
    }

}