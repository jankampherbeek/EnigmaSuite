// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Enigma.Domain.Constants;
using Enigma.Domain.References;
using Enigma.Domain.Research;
using Enigma.Domain.Responses;
using Enigma.Frontend.Ui.Messaging;
using Enigma.Frontend.Ui.Models;
using Enigma.Frontend.Ui.WindowsFlow;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Enigma.Frontend.Ui.ViewModels;

/// <summary>ViewModel for input new project. Collects user input and sends a request to save the project.
/// Sends messages: CompletedMessage, CancelMessage, HelpMessage.
/// </summary>
public partial class ProjectInputViewModel: ObservableObject
{
    private const string VM_IDENTIFICATION = ResearchWindowsFlow.PROJECT_INPUT;
    private const string ERROR_PROJECT_IN_USE = "Projectname is already in use.";
    private const string RESULTMSG_PROJECT_SAVED = "Project has been saved.";
    
    private readonly ProjectInputModel _model = App.ServiceProvider.GetRequiredService<ProjectInputModel>();
    private bool _saveClicked;
    
    [NotifyPropertyChangedFor(nameof(ProjectNameValid))]   
    [ObservableProperty] private string _projectName = string.Empty;
    [NotifyPropertyChangedFor(nameof(ProjectDescriptionValid))]   
    [ObservableProperty] private string _projectDescription = string.Empty;
    [ObservableProperty] private int _controlGroupIndex;
    [ObservableProperty] private string _selectedDatafileName = string.Empty;
    [ObservableProperty] private int _datafileIndex = -1;
    [ObservableProperty] private string _multiplicationValue = "1";
    [ObservableProperty] private ObservableCollection<string> _availableControlGroupTypes;
    [ObservableProperty] private ObservableCollection<string> _controlGroupMultiplications;
    [ObservableProperty] private ObservableCollection<string> _availableDatafileNames;
    
    public SolidColorBrush ProjectNameValid => IsProjectNameValid() ? Brushes.Gray : Brushes.Red;
    public SolidColorBrush ProjectDescriptionValid => IsProjectDescriptionValid() ? Brushes.Gray : Brushes.Red;
    public SolidColorBrush DatafileValid => IsDatafileValid() ? Brushes.Gray : Brushes.Red;
    public SolidColorBrush MultiplicationValid => IsMultiplicationValid() ? Brushes.Gray : Brushes.Red;
    
    public ProjectInputViewModel()
    {
        AvailableControlGroupTypes = new ObservableCollection<string>(_model.GetControlGroupTypeNames());
        AvailableDatafileNames = new ObservableCollection<string>(_model.GetDataNames());
        ControlGroupMultiplications = new ObservableCollection<string>(ProjectInputModel.GetCgMultiplicationFactors());
    }
    
    partial void OnDatafileIndexChanged(int value)
    {
        if (value >= 0 && value < AvailableDatafileNames.Count)
        {
            SelectedDatafileName = AvailableDatafileNames[value];
        }
    }
    
    private bool IsProjectNameValid()
    {
        if (string.IsNullOrEmpty(ProjectName) && !_saveClicked) return true;   
        ProjectName = ProjectName.Trim();
        return ProjectName != string.Empty;
    }

    private bool IsProjectDescriptionValid()
    {
        if (string.IsNullOrEmpty(ProjectDescription) && !_saveClicked) return true;           
        ProjectDescription = ProjectDescription.Trim();
        return ProjectDescription != string.Empty;
    }

    private bool IsDatafileValid()
    {
        if (!_saveClicked) return true;
        return !string.IsNullOrEmpty(SelectedDatafileName) && AvailableDatafileNames.Contains(SelectedDatafileName);
    }

    private bool IsMultiplicationValid()
    {
        if (string.IsNullOrEmpty(MultiplicationValue) && !_saveClicked) return true;
        if (!int.TryParse(MultiplicationValue, out int value)) return false;
        return value is >= 1 and <= 1000;
    }
    
    [RelayCommand]
    private void SaveProject()
    {
        _saveClicked = true;
        string errors = FindErrors();
        if (string.IsNullOrEmpty(errors))
        {
            if (!IsDatafileValid())
            {
                MessageBox.Show(StandardTexts.ERROR_DATAFILE_MISSING, StandardTexts.TITLE_ERROR);
                return;
            }

            if (!int.TryParse(MultiplicationValue, out int multiplicationValue) || multiplicationValue < 1 || multiplicationValue > 1000)
            {
                MessageBox.Show("Multiplication value must be between 1 and 1000", StandardTexts.TITLE_ERROR);
                return;
            }

            ControlGroupTypes cgType = ControlGroupTypesExtensions.ControlGroupTypeForIndex(ControlGroupIndex);
            DateTime now = DateTime.Now;
            var cgIndexinDb = _model.GetControlGroupTypeId(cgType.ToString());
            int datafileId = _model.GetDatafileId(SelectedDatafileName);
            if (datafileId < 0)
            {
                MessageBox.Show(StandardTexts.ERROR_DATAFILE_MISSING, StandardTexts.TITLE_ERROR);
                return;
            }
            
            // use -1 for id, the real id is defined by the RDBMS 
            ResearchProject project = new(-1, ProjectName, ProjectDescription, 
                datafileId,
                now.ToString(CultureInfo.InvariantCulture),
                cgIndexinDb, 
                multiplicationValue);
                
            ResultMessage resultMessage = _model.SaveProject(project);
            if (resultMessage.ErrorCode != 0)
            {
                Log.Error("Error while creating project, Enigma errorcode: {ErrorCode}, project: {@Project}",
                    resultMessage.ErrorCode, project);
                MessageBox.Show(ERROR_PROJECT_IN_USE);
            }
            else
            {
                Log.Information("Created project {ProjectName}", ProjectName);
                MessageBox.Show(RESULTMSG_PROJECT_SAVED);
                Log.Information("ProjectInputViewModel.SaveProject(): send CompletedMessage");                
                WeakReferenceMessenger.Default.Send(new CompletedMessage(VM_IDENTIFICATION)); 
            }
        }
        else
            MessageBox.Show(errors, StandardTexts.TITLE_ERROR);
    }
    
    private string FindErrors()
    {
        StringBuilder errorsText = new();
        if (!IsProjectNameValid())
            errorsText.Append(StandardTexts.ERROR_NAME + EnigmaConstants.NEW_LINE);
        if (!IsProjectDescriptionValid())
            errorsText.Append(StandardTexts.ERROR_DESCRIPTION + EnigmaConstants.NEW_LINE);
        if (!IsDatafileValid())
            errorsText.Append(StandardTexts.ERROR_DATAFILE_MISSING + EnigmaConstants.NEW_LINE);
        if (!IsMultiplicationValid())
            errorsText.Append("Multiplication value must be between 1 and 1000" + EnigmaConstants.NEW_LINE);
        return errorsText.ToString();
    }

    [RelayCommand]
    private static void Cancel()
    {
        Log.Information("ProjectInputViewModel.Cancel(): send CancelMessage");  
        WeakReferenceMessenger.Default.Send(new CancelMessage(VM_IDENTIFICATION));
    }
    
    [RelayCommand]
    private static void Help()
    {
        Log.Information("ProjectInputViewModel.Help(): send HelpMessage");  
        WeakReferenceMessenger.Default.Send(new HelpMessage(VM_IDENTIFICATION));
    }
}