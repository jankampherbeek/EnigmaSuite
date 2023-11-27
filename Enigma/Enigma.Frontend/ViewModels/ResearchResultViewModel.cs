// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Enigma.Frontend.Ui.Messaging;
using Enigma.Frontend.Ui.Models;
using Enigma.Frontend.Ui.State;
using Enigma.Frontend.Ui.Views;
using Enigma.Frontend.Ui.WindowsFlow;
using Microsoft.Extensions.DependencyInjection;

namespace Enigma.Frontend.Ui.ViewModels;

/// <summary>ViewModel for research result</summary>
public partial class ResearchResultViewModel: ObservableObject
{
    private static string VM_IDENTIFICATION = ResearchWindowsFlow.RESEARCH_RESULT;
    
    [ObservableProperty] private string _projectName;
    [ObservableProperty] private string _methodName;
    [ObservableProperty] private string _testResult;
    [ObservableProperty] private string _controlResult;
    private readonly ResearchResultModel _model = App.ServiceProvider.GetRequiredService<ResearchResultModel>();

    public ResearchResultViewModel()
    {
        ProjectName = _model.ProjectName;
        MethodName = _model.MethodName;
        TestResult = _model.TestResult;
        ControlResult = _model.ControlResult;
    }

    [RelayCommand]
    private static void Close()
    {
        WeakReferenceMessenger.Default.Send(new CloseMessage(VM_IDENTIFICATION));
    }
    
    
    [RelayCommand]
    private static void Help()
    {
        WeakReferenceMessenger.Default.Send(new HelpMessage(VM_IDENTIFICATION));
    }
}