// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.ObjectModel;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Enigma.Domain.Research;
using Enigma.Frontend.Ui.Models;
using Enigma.Frontend.Ui.Research;
using Enigma.Frontend.Ui.State;
using Enigma.Frontend.Ui.Views;
using Microsoft.Extensions.DependencyInjection;

namespace Enigma.Frontend.Ui.ViewModels;

/// <summary>ViewModel for project usage</summary>
public partial class ProjectUsageViewModel: ObservableObject
{
    [ObservableProperty] private string _projectName = string.Empty;
    [ObservableProperty] private string _description = string.Empty;
    [ObservableProperty] private string _startDate = string.Empty;
    [ObservableProperty] private string _dataSetName = string.Empty;
    [ObservableProperty] private string _controlGroupType = string.Empty;
    [ObservableProperty] private string _multiplFactor = string.Empty;
    [NotifyCanExecuteChangedFor(nameof(PerformTestCommand))]
    [ObservableProperty] private int _methodIndex = -1;
    [ObservableProperty] private ObservableCollection<PresentableMethodDetails> _testMethods = new();
    
    
    private readonly ProjectUsageModel _model = App.ServiceProvider.GetRequiredService<ProjectUsageModel>();

    public ProjectUsageViewModel()
    {
        ResearchProject? currentProject = DataVault.Instance.CurrentProject;
        if (currentProject == null) return;
        TestMethods = new ObservableCollection<PresentableMethodDetails>(_model.GetAllMethodDetails());
        ProjectName = currentProject.Name;
        Description = currentProject.Description;
        StartDate = currentProject.CreationDate;
        DataSetName = currentProject.DataName;
        MultiplFactor = currentProject.ControlGroupMultiplication.ToString();
        ControlGroupType = currentProject.ControlGroupType.GetDetails().Text;
    }

    
    [RelayCommand(CanExecute = nameof(IsMethodSelected))]
    private void PerformTest()
    {
        ResearchMethods method = ResearchMethods.None.ResearchMethodForIndex(MethodIndex);
        DataVault.Instance.ResearchMethod = method;
        bool sufficientSelections = false;
        int minNumber = method.GetDetails().MinNumberOfPoints;
        while (!sufficientSelections)
        {
            ResearchPointSelectionWindow selectionWindow = new();
            selectionWindow.ShowDialog();
            ResearchPointsSelection? selection = DataVault.Instance.CurrentPointsSelection;
            int selectedNumber = selection != null ? selection.SelectedPoints.Count : 0; 
            sufficientSelections = selectedNumber >= minNumber;
            if (sufficientSelections)
            {
                selectionWindow.Close();
                continue;
            }
            MessageBox.Show("Please select at least " + minNumber + " points." );
        }
        _model.PerformRequest(ResearchMethods.None.ResearchMethodForIndex(MethodIndex));
        new ResearchResultWindow().ShowDialog();
    }

    
    [RelayCommand]
    private static void Config()
    {
        new ConfigurationWindow().ShowDialog();
    }

    [RelayCommand]
    private static void Help()
    {
        DataVault.Instance.CurrentViewBase = "ProjectUsage";
        new HelpWindow().ShowDialog();
    }


    private bool IsMethodSelected()
    {
        return MethodIndex >= 0;
    }
    
    
}

