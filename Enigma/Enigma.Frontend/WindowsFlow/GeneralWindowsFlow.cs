// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using CommunityToolkit.Mvvm.Messaging;
using Enigma.Frontend.Ui.Messaging;
using Enigma.Frontend.Ui.State;
using Enigma.Frontend.Ui.Views;
using Serilog;

namespace Enigma.Frontend.Ui.WindowsFlow;

public class GeneralWindowsFlow:     
    IRecipient<CloseMessage>,
    IRecipient<OpenMessage>, 
    IRecipient<HelpMessage>
{
    // Constants for the names of general views. The names are without the parts 'Window', 'ViewModel' and 'Model'. 
    public const string APP_SETTINGS = "AppSettings";
    public const string CHARTS_MAIN = "ChartsMain";
    public const string CONFIGURATION = "Configuration";
    public const string CONFIG_PROG = "ConfigProg";
    public const string MAIN = "Main";
    public const string RESEARCH_MAIN = "ResearchMain";
    public const string CYCLES_MAIN = "CyclesMain";
    
    private const string HELP = "Help";
    
    private HelpWindow? _helpWindow;
    private ConfigurationWindow? _configurationWindow;
    private ConfigProgWindow? _configProgWindow;
    private AppSettingsWindow? _appSettingsWindow;
    private ChartsMainWindow? _chartsMainWindow;
    private ResearchMainWindow? _researchMainWindow;
    private CyclesMainWindow? _cyclesMainWindow;
    private MainWindow? _mainWindow;
    public GeneralWindowsFlow()
    {
        WeakReferenceMessenger.Default.Register<CloseMessage>(this);
        WeakReferenceMessenger.Default.Register<HelpMessage>(this);
        WeakReferenceMessenger.Default.Register<OpenMessage>(this);
    }
    
    public void Receive(OpenMessage message)
    {
        switch (message.ViewToOpen)
        {
            case MAIN:
                _mainWindow = new MainWindow();
                _mainWindow.ShowDialog();
                break;
            case RESEARCH_MAIN:
                _researchMainWindow = new ResearchMainWindow();
                _researchMainWindow.ShowDialog();
                break;
            case CYCLES_MAIN:
                _cyclesMainWindow = new CyclesMainWindow();
                _cyclesMainWindow.ShowDialog();
                break;
            case CHARTS_MAIN:
                _chartsMainWindow = new ChartsMainWindow();
                _chartsMainWindow.ShowDialog();
                break;
            case CONFIGURATION:
                _configurationWindow = new ConfigurationWindow();
                _configurationWindow.ShowDialog();
                break;
            case CONFIG_PROG:
                _configProgWindow = new ConfigProgWindow();
                _configProgWindow.ShowDialog();
                break;
            case APP_SETTINGS:
                _appSettingsWindow = new AppSettingsWindow();
                _appSettingsWindow.ShowDialog();
                break;
        }
    }

    public void Receive(CloseMessage message)
    {
        Log.Information("GeneralWindowsFlow.Receive(CloseMessage) with value {Value}", message.Value);
        switch (message.Value)
        {
            case RESEARCH_MAIN:
                _researchMainWindow?.Close();
                break;
            case CHARTS_MAIN:
                _chartsMainWindow?.Close();
                break;
            case CYCLES_MAIN:
                _cyclesMainWindow?.Close();
                break;
            case APP_SETTINGS:
                _appSettingsWindow?.Close();
                break;
            case CONFIGURATION:
                _configurationWindow?.Close();
                break;
            case CONFIG_PROG:
                _configProgWindow?.Close();
                break;
            case HELP:
                _helpWindow?.Close();
                break;
        }
    }

    public void Receive(HelpMessage message)
    {
        Log.Information("GeneralWindowsFlow.Receive(HelpMessage) with value {Value}", message.Value);        
        DataVaultGeneral dataVault = DataVaultGeneral.Instance;
        dataVault.CurrentViewBase = message.Value;
        _helpWindow = new HelpWindow();
        _helpWindow.ShowDialog();
    }
}
