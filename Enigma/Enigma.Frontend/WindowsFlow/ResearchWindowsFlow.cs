// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using CommunityToolkit.Mvvm.Messaging;
using Enigma.Frontend.Ui.Messaging;
using Enigma.Frontend.Ui.Views;

namespace Enigma.Frontend.Ui.WindowsFlow;

public class ResearchWindowsFlow: 
    IRecipient<CancelMessage>, 
    IRecipient<CloseMessage>,
    IRecipient<OpenMessage>, 
    IRecipient<OkMessage>, 
    IRecipient<ContinueMessage>,
    IRecipient<CompletedMessage>
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
    public const string CONFIG_PROG = "ConfigProg";
    
    private ProjectInputWindow? _projectInputWindow;
    private ProjectUsageWindow? _projectUsageWindow;
    private DatafileOverviewWindow? _datafileOverviewWindow;
    private DatafileImportWindow? _datafileImportWindow;
    private ResearchHarmonicDetailsWindow? _researchHarmonicDetailsWindow;
    private ResearchMidpointDetailsWindow? _researchMidpointDetailsWindow;
    private ResearchResultWindow? _researchResultWindow;
    private ResearchPointSelectionWindow? _researchPointSelectionWindow;
    private ConfigProgWindow? _configProgWindow;
    
    
    public ResearchWindowsFlow()
    {
        WeakReferenceMessenger.Default.Register<OpenMessage>(this);
        WeakReferenceMessenger.Default.Register<CompletedMessage>(this);
        WeakReferenceMessenger.Default.Register<CancelMessage>(this);
        WeakReferenceMessenger.Default.Register<CloseMessage>(this);
        WeakReferenceMessenger.Default.Register<OkMessage>(this);
        WeakReferenceMessenger.Default.Register<ContinueMessage>(this);
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
            case RESEARCH_POINT_SELECTION:
                _researchPointSelectionWindow?.Close();
                break;
        }
    }

    public void Receive(OpenMessage message)
    {
        switch (message.ViewToOpen)
        {
            case CONFIG_PROG:
                _configProgWindow = new ConfigProgWindow();
                _configProgWindow.ShowDialog();
                break;
            case PROJECT_INPUT:
                _projectInputWindow = new ProjectInputWindow();
                _projectInputWindow.ShowDialog();
                break;
            case PROJECT_USAGE:
                _projectUsageWindow = new ProjectUsageWindow();
                _projectUsageWindow.ShowDialog();
                break;
            case DATAFILE_OVERVIEW:
                _datafileOverviewWindow = new DatafileOverviewWindow();
                _datafileOverviewWindow.ShowDialog();
                break;
            case DATAFILE_IMPORT:
                _datafileImportWindow = new DatafileImportWindow();
                _datafileImportWindow.ShowDialog();
                break;
            case RESEARCH_RESULT:
                _researchResultWindow = new ResearchResultWindow();
                _researchResultWindow.ShowDialog();
                break;
            case RESEARCH_MIDPOINT_DETAILS:
                _researchMidpointDetailsWindow = new ResearchMidpointDetailsWindow();
                _researchMidpointDetailsWindow.ShowDialog();
                break;
            case RESEARCH_HARMONIC_DETAILS:
                _researchHarmonicDetailsWindow = new ResearchHarmonicDetailsWindow();
                _researchHarmonicDetailsWindow.ShowDialog();
                break;
            case RESEARCH_POINT_SELECTION:
                _researchPointSelectionWindow = new ResearchPointSelectionWindow();
                _researchPointSelectionWindow.ShowDialog();
                break;
        }
    }

    public void Receive(CompletedMessage message)
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
            case RESEARCH_POINT_SELECTION:
                _researchPointSelectionWindow?.Close();
                break;
        }
    }
    

    public void Receive(OkMessage message)
    {
        switch (message.Value)
        {
            case CONFIG_PROG:
                _configProgWindow?.Close();
                break;
        }
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
            case RESEARCH_POINT_SELECTION:
                _researchPointSelectionWindow?.Close();
                break;
            case RESEARCH_RESULT:
                _researchResultWindow?.Close();
                break;
            case PROJECT_USAGE:
                _projectUsageWindow?.Close();
                break;            
        }
    }
    
}