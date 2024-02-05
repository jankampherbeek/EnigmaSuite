// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Enigma.Frontend.Ui.Messaging;
using Enigma.Frontend.Ui.Models;
using Enigma.Frontend.Ui.WindowsFlow;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Enigma.Frontend.Ui.ViewModels;

/// <summary>ViewModel for research result. Shows the results of a test for both test data and controlgroup data.
/// Sends messages: CloseMessage and HelpMessage.</summary>
public partial class ResearchResultViewModel: ObservableObject
{
    private const string VM_IDENTIFICATION = ResearchWindowsFlow.RESEARCH_RESULT;
    
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
        Log.Information("ResearchResultViewModel.Close(): send CloseMessage");         
        WeakReferenceMessenger.Default.Send(new CloseMessage(VM_IDENTIFICATION));
    }
    
    
    [RelayCommand]
    private static void Help()
    {
        Log.Information("ResearchResultViewModel.Help(): send HelpMessage");   
        WeakReferenceMessenger.Default.Send(new HelpMessage(VM_IDENTIFICATION));
    }
}