// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.ObjectModel;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Enigma.Domain.Dtos;
using Enigma.Domain.References;
using Enigma.Domain.Research;
using Enigma.Frontend.Ui.Messaging;
using Enigma.Frontend.Ui.Models;
using Enigma.Frontend.Ui.State;
using Enigma.Frontend.Ui.Views;
using Enigma.Frontend.Ui.WindowsFlow;
using Microsoft.Extensions.DependencyInjection;

namespace Enigma.Frontend.Ui.ViewModels;

/// <summary>ViewModel for project usage</summary>
public partial class ProjectUsageViewModel: ObservableObject, 
    IRecipient<HarmonicDetailsMessage>,
    IRecipient<MidpointDetailsMessage>,
    IRecipient<ResearchPointSelectionMessage>,
    IRecipient<CompletedMessage>,
    IRecipient<CancelMessage>
{
    private const string VM_IDENTIFICATION = ResearchWindowsFlow.PROJECT_USAGE;

    [ObservableProperty] private string _projectName = string.Empty;
    [ObservableProperty] private string _description = string.Empty;
    [ObservableProperty] private string _startDate = string.Empty;
    [ObservableProperty] private string _dataSetName = string.Empty;
    [ObservableProperty] private string _controlGroupType = string.Empty;
    [ObservableProperty] private string _multiplFactor = string.Empty;
    [NotifyCanExecuteChangedFor(nameof(PrepareTestCommand))]
    [ObservableProperty] private int _methodIndex = -1;
    [ObservableProperty] private ObservableCollection<PresentableMethodDetails> _testMethods = new();
    
    private readonly ProjectUsageModel _model = App.ServiceProvider.GetRequiredService<ProjectUsageModel>();
    private bool _sufficientSelections;
    private bool _testCanceled;
    
    public ProjectUsageViewModel()
    {
        WeakReferenceMessenger.Default.Register<CompletedMessage>(this);
        WeakReferenceMessenger.Default.Register<CancelMessage>(this);
        WeakReferenceMessenger.Default.Register<ResearchPointSelectionMessage>(this);
        WeakReferenceMessenger.Default.Register<HarmonicDetailsMessage>(this);
        WeakReferenceMessenger.Default.Register<MidpointDetailsMessage>(this);
        
        ResearchProject? currentProject = DataVaultResearch.Instance.CurrentProject;
        if (currentProject == null) return;
        TestMethods = new ObservableCollection<PresentableMethodDetails>(ProjectUsageModel.GetAllMethodDetails());
        ProjectName = currentProject.Name;
        Description = currentProject.Description;
        StartDate = currentProject.CreationDate;
        DataSetName = currentProject.DataName;
        MultiplFactor = currentProject.ControlGroupMultiplication.ToString();
        ControlGroupType = currentProject.ControlGroupType.GetDetails().Text;
    }

    
    [RelayCommand(CanExecute = nameof(IsMethodSelected))]
    private void PrepareTest()
    {
        _testCanceled = false;
        _sufficientSelections = false;
        ResearchMethods method = ResearchMethodsExtensions.ResearchMethodForIndex(MethodIndex);
        DataVaultResearch.Instance.ResearchMethod = method;
        
        int minNumber = method.GetDetails().MinNumberOfPoints;
        while (!_sufficientSelections && !_testCanceled)
        {
            WeakReferenceMessenger.Default.Send(new OpenMessage(VM_IDENTIFICATION,
                ResearchWindowsFlow.RESEARCH_POINT_SELECTION));
            ResearchPointSelection selection = _model.ResearchPointSelection;
            int selectedNumber = selection != null ? selection.SelectedPoints.Count : 0; 
            _sufficientSelections = selectedNumber >= minNumber;
            if (_sufficientSelections || _testCanceled)
            {
                continue;
            }
            MessageBox.Show("Please select at least " + minNumber + " points." );
        }
        if (method == ResearchMethods.CountHarmonicConjunctions)
        {
            WeakReferenceMessenger.Default.Send(new OpenMessage(VM_IDENTIFICATION, ResearchWindowsFlow.RESEARCH_HARMONIC_DETAILS)); 
        }
        if (method == ResearchMethods.CountOccupiedMidpoints)
        {
            WeakReferenceMessenger.Default.Send(new OpenMessage(VM_IDENTIFICATION, ResearchWindowsFlow.RESEARCH_MIDPOINT_DETAILS)); 
        }

        if (method is ResearchMethods.CountPosInSigns or ResearchMethods.CountPosInHouses or ResearchMethods.CountAspects 
            or ResearchMethods.CountUnaspected)    
        {
            _model.PerformRequest(method);
            WeakReferenceMessenger.Default.Send(new OpenMessage(VM_IDENTIFICATION,
                ResearchWindowsFlow.RESEARCH_RESULT));
        }

    }

    private void PerformTest(ResearchMethods method)
    {
        _model.PerformRequest(method);
        WeakReferenceMessenger.Default.Send(new OpenMessage(VM_IDENTIFICATION, ResearchWindowsFlow.RESEARCH_RESULT));         
    }
    
    
    private void CompleteRequest()
    {
        _model.PerformRequest(ResearchMethodsExtensions.ResearchMethodForIndex(MethodIndex));
    }
    
    
    [RelayCommand]
    private static void Config()
    {
        new ConfigurationWindow().ShowDialog();
    }

    [RelayCommand]
    private static void Help()
    {
        DataVaultGeneral.Instance.CurrentViewBase = "ProjectUsage";
        new HelpWindow().ShowDialog();
    }


    private bool IsMethodSelected()
    {
        return MethodIndex >= 0;
    }


    public void Receive(HarmonicDetailsMessage message)
    {
        _model.HarmonicDetailsSelection = message.Value;
        PerformTest(ResearchMethods.CountHarmonicConjunctions);
    }

    public void Receive(MidpointDetailsMessage message)
    {
        _model.MidpointDetailsSelection = message.Value;
        PerformTest(ResearchMethods.CountOccupiedMidpoints);
    }
    
    public void Receive(CompletedMessage message)
    { 
        CompleteRequest();
    }

    public void Receive(ResearchPointSelectionMessage message)
    {
        _model.ResearchPointSelection = message.Value;
    }

    public void Receive(CancelMessage message)
    {
        if (message.Value == ResearchWindowsFlow.RESEARCH_POINT_SELECTION)
        {
            _testCanceled = true;
        }
    }
}

