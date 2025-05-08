// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.ObjectModel;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Enigma.Core.Persistency;
using Enigma.Frontend.Ui.Messaging;
using Enigma.Frontend.Ui.Models;
using Enigma.Frontend.Ui.WindowsFlow;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Enigma.Frontend.Ui.ViewModels;

/// <summary>ViewModel for an overview of data files.
/// Shows names of datafiles, projects using them, and allows deletion if no projects are using the datafile.</summary>
public partial class DatafileOverviewViewModel: ObservableObject
{
    private const string VM_IDENTIFICATION = ResearchWindowsFlow.DATAFILE_OVERVIEW;
    private readonly IDataFileDao _dataFileDao;
    private readonly IDataFilePersistencyHandler _dataFilePersistencyHandler;
    
    [ObservableProperty] private ObservableCollection<string> _dataNames;
    [ObservableProperty] private ObservableCollection<string> _projectsForSelectedDataFile;
    [ObservableProperty] private string? _selectedDataFile;
    [ObservableProperty] private bool _canDelete;

    public DatafileOverviewViewModel()
    {
        var model = App.ServiceProvider.GetRequiredService<DatafileOverviewModel>();
        _dataFileDao = App.ServiceProvider.GetRequiredService<IDataFileDao>();
        _dataFilePersistencyHandler = App.ServiceProvider.GetRequiredService<IDataFilePersistencyHandler>();
        
        _dataNames = new ObservableCollection<string>(model.GetDataNames());
        _projectsForSelectedDataFile = new ObservableCollection<string>();
    }

    partial void OnSelectedDataFileChanged(string? value)
    {
        if (value == null)
        {
            ProjectsForSelectedDataFile.Clear();
            CanDelete = false;
            return;
        }

        var projects = _dataFileDao.ReadProjectsForDataFile(value);
        ProjectsForSelectedDataFile = new ObservableCollection<string>(projects);
        CanDelete = projects.Count == 0;
    }

    [RelayCommand]
    private void Delete()
    {
        if (SelectedDataFile == null) return;

        var dataFile = _dataFileDao.ReadDataFile(SelectedDataFile);
        if (dataFile == null) return;

        var result = MessageBox.Show(
            $"Are you sure you want to delete data file '{SelectedDataFile}'? This action cannot be undone.",
            "Confirm Deletion",
            MessageBoxButton.YesNo,
            MessageBoxImage.Warning
        );

        if (result == MessageBoxResult.Yes)
        {
            if (_dataFilePersistencyHandler.DeleteDataFileAndRemoveFiles(dataFile.Id))
            {
                DataNames.Remove(SelectedDataFile);
                SelectedDataFile = null;
                MessageBox.Show(
                    "Data file was successfully deleted.",
                    "Success",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information
                );
            }
            else
            {
                MessageBox.Show(
                    "Could not delete the data file. Please check the log for details.",
                    "Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                );
            }
        }
    }

    [RelayCommand]
    private static void Close()
    {
        Log.Information("DatafileOverviewViewModel.Close(): send CloseMessage");   
        WeakReferenceMessenger.Default.Send(new CloseMessage(VM_IDENTIFICATION)); 
    }
    
    [RelayCommand]
    private static void Help()
    {
        Log.Information("DatafileOverviewViewModel.Help(): send HelpMessage"); 
        WeakReferenceMessenger.Default.Send(new HelpMessage(VM_IDENTIFICATION));
    }
}
