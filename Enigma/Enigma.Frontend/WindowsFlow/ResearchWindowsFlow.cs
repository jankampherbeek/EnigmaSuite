// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using CommunityToolkit.Mvvm.Messaging;
using Enigma.Frontend.Ui.Interfaces;
using Enigma.Frontend.Ui.Messaging;
using Enigma.Frontend.Ui.Views;

namespace Enigma.Frontend.Ui.WindowsFlow;

public class ResearchWindowsFlow: IResearchWindowsFlow
{
    // Constants for the names of views in the research module. The names are without the parts 'Window', 'ViewModel' and 'Model'. 
    public const string DATAFILE_IMPORT = "DatafileImport";
    public const string DATAFILE_OVERVIEW = "DatafileOverview";
    public const string PROJECT_INPUT = "ProjectInput";
    public const string PROJECT_USAGE = "ProjectUsage";
    public const string RESEARCH_HARMONIC_DETAILS = "ResearchHarmonicDetails";
    public const string RESEARCH_MAIN = "ResearchMain";
    public const string RESEARCH_MIDPOINT_DETAILS = "ResearchMidpointDetails";
    public const string RESEARCH_POINT_SELECTION = "ResearchPointSelection";
    public const string RESEARCH_RESULT = "ResearchResult";
    
    
    
    
    
    
    
    
    private ProjectInputWindow? _projectInputWindow;
    private DatafileOverviewWindow? _datafileOverviewWindow;
    private DatafileImportWindow? _datafileImportWindow;
    private ResearchHarmonicDetailsWindow? _researchHarmonicDetailsWindow;
    private ResearchMidpointDetailsWindow? _researchMidpointDetailsWindow;
    
    
    public ResearchWindowsFlow()
    {
        WeakReferenceMessenger.Default.Register<OpenMessage>(this);
        WeakReferenceMessenger.Default.Register<CompletedMessage>(this);
        WeakReferenceMessenger.Default.Register<CancelMessage>(this);
        WeakReferenceMessenger.Default.Register<CloseMessage>(this);
        WeakReferenceMessenger.Default.Register<OkMessage>(this);
        WeakReferenceMessenger.Default.Register<ContinueMessage>(this);
//        WeakReferenceMessenger.Default.Register<ResearchResultMessage>(this);
    }
    
    
    public void Receive(CancelMessage message)
    {
        switch (message.Value)
        {
            case PROJECT_INPUT:
                _projectInputWindow?.Close();
                break;
            case RESEARCH_HARMONIC_DETAILS:
                _researchHarmonicDetailsWindow?.Close();
                break;
            case RESEARCH_MIDPOINT_DETAILS:
                _researchMidpointDetailsWindow?.Close();
                break;
        }
    }

    public void Receive(OpenMessage message)
    {
        switch (message.ViewToOpen)
        {
            case PROJECT_INPUT:
                _projectInputWindow = new ProjectInputWindow();
                _projectInputWindow.ShowDialog();
                break;
            case DATAFILE_OVERVIEW:
                _datafileOverviewWindow = new DatafileOverviewWindow();
                _datafileOverviewWindow.ShowDialog();
                break;
            case DATAFILE_IMPORT:
                _datafileImportWindow = new DatafileImportWindow();
                _datafileImportWindow.ShowDialog();
                break;
            case RESEARCH_MIDPOINT_DETAILS:
                _researchMidpointDetailsWindow = new ResearchMidpointDetailsWindow();
                _researchMidpointDetailsWindow.ShowDialog();
                break;
            case RESEARCH_HARMONIC_DETAILS:
                _researchHarmonicDetailsWindow = new ResearchHarmonicDetailsWindow();
                _researchHarmonicDetailsWindow.ShowDialog();
                break;
        }
    }

    public void Receive(CompletedMessage message)
    {
        if (message.Value == PROJECT_INPUT)
        {
            _projectInputWindow?.Close();
        }
    }
    

    public void Receive(OkMessage message)
    {
        throw new System.NotImplementedException();
    }

    public void Receive(ContinueMessage message)
    {
        throw new System.NotImplementedException();
    }
    

    public void Receive(CloseMessage message)
    {
        switch (message.Value)
        {
            case DATAFILE_OVERVIEW:
                _datafileOverviewWindow?.Close();
                break;
            case DATAFILE_IMPORT:
                _datafileImportWindow?.Close();
                break;
        }
    }
    
}