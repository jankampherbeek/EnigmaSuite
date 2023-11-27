// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using CommunityToolkit.Mvvm.Messaging;
using Enigma.Frontend.Ui.Messaging;
using Enigma.Frontend.Ui.State;
using Enigma.Frontend.Ui.Views;

namespace Enigma.Frontend.Ui.WindowsFlow;

public class GeneralWindowsFlow:     
    IRecipient<CancelMessage>, 
    IRecipient<CloseMessage>,
    IRecipient<OpenMessage>, 
    IRecipient<OkMessage>, 
    IRecipient<ContinueMessage>,
    IRecipient<HelpMessage>
{
    // Constants for the names of general views. The names are without the parts 'Window', 'ViewModel' and 'Model'. 
    private const string HELP = "Help";
    
    private HelpWindow? _helpWindow;
    
    
    public GeneralWindowsFlow()
    {
        WeakReferenceMessenger.Default.Register<CancelMessage>(this);
        WeakReferenceMessenger.Default.Register<CloseMessage>(this);
        WeakReferenceMessenger.Default.Register<ContinueMessage>(this);
        WeakReferenceMessenger.Default.Register<HelpMessage>(this);
        WeakReferenceMessenger.Default.Register<OkMessage>(this);   
        WeakReferenceMessenger.Default.Register<OpenMessage>(this);
    }
    
    
    public void Receive(CancelMessage message)
    {
        if (message.Value == "xxx")
        {
            // todo complete this
        }
    }

    public void Receive(OpenMessage message)
    {
        if (message.ViewToOpen == "ResearchMain")
        {
            new ResearchMainWindow().ShowDialog();
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
            case HELP:
                _helpWindow?.Close();
                break;
        }
    }

    public void Receive(HelpMessage message)
    {
        DataVaultGeneral dataVault = DataVaultGeneral.Instance;
        dataVault.CurrentViewBase = message.Value;
        _helpWindow = new();
        _helpWindow.ShowDialog();
    }
}
