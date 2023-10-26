// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Enigma.Domain.References;
using Enigma.Domain.Research;
using Enigma.Domain.Responses;
using Enigma.Frontend.Ui.Models;
using Enigma.Frontend.Ui.State;
using Enigma.Frontend.Ui.Views;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Enigma.Frontend.Ui.ViewModels;

/// <summary>ViewModel for input new project</summary>
public partial class ProjectInputViewModel: ObservableObject
{
    [NotifyCanExecuteChangedFor(nameof(SaveProjectCommand))]
    [ObservableProperty] private string _projectName = string.Empty;
    [NotifyCanExecuteChangedFor(nameof(SaveProjectCommand))]
    [ObservableProperty] private string _description = string.Empty;
    [NotifyPropertyChangedFor(nameof(MultiplicationValid))]
    [NotifyCanExecuteChangedFor(nameof(SaveProjectCommand))]    
    [ObservableProperty] private string _multiplication = "1";
    private int _multiplicationValue = 1;
    [ObservableProperty] private int _controlGroupIndex;
    [ObservableProperty] private int _datafileIndex;
    [ObservableProperty] private ObservableCollection<string> _availableControlGroupTypes;
    [ObservableProperty] private ObservableCollection<string> _availableDatafileNames;
    private readonly ProjectInputModel _model = App.ServiceProvider.GetRequiredService<ProjectInputModel>();
    
    public SolidColorBrush MultiplicationValid => IsMultiplicationValid() ? Brushes.White : Brushes.Yellow;
    
    public ProjectInputViewModel()
    {
        AvailableControlGroupTypes = new ObservableCollection<string>(ProjectInputModel.GetControlGroupTypeNames());
        AvailableDatafileNames = new ObservableCollection<string>(_model.GetDataNames());
    }
    
    private bool IsProjectNameValid()
    {
        ProjectName = ProjectName.Trim();
        return ProjectName != string.Empty;
    }

    private bool IsDescriptionValid()
    {
        Description = Description.Trim();
        return Description != string.Empty;
    }

    private bool IsMultiplicationValid()
    {
        bool isNumeric = int.TryParse(Multiplication, out int multiplValue);
        if (!isNumeric || multiplValue is <= 0 or > 10) return false;
        _multiplicationValue = multiplValue;
        return true;
    }
    
    
    [RelayCommand(CanExecute = nameof(IsInputOk))]
    private void SaveProject()
    {
        ControlGroupTypes cgType = ControlGroupTypesExtensions.ControlGroupTypeForIndex(ControlGroupIndex);
        ResearchProject project = new(ProjectName, Description, AvailableDatafileNames[DatafileIndex], cgType, _multiplicationValue);
        
        ResultMessage resultMessage =  _model.SaveProject(project);
        if (resultMessage.ErrorCode != 0)
        {
            Log.Error("Error while creating project, Enigma errorcode: {ErrorCode}, project: {@Project}", resultMessage.ErrorCode, project);
            MessageBox.Show("Projectname is already in use.");
        }
        else
        {
            Log.Information("Created project {ProjectName}", ProjectName);
            MessageBox.Show("Project has been saved.");
        }
    }
    
    [RelayCommand]
    private static void Help()
    {
        DataVaultGeneral.Instance.CurrentViewBase = "ProjectInput";
        new HelpWindow().ShowDialog();
    }

    private bool IsInputOk()
    {
        return (IsProjectNameValid() && IsDescriptionValid() && IsMultiplicationValid());
    }
}