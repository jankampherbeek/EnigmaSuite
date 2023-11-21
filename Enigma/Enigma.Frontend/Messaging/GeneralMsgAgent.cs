// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using CommunityToolkit.Mvvm.Messaging;
using Enigma.Frontend.Ui.Interfaces;
using Enigma.Frontend.Ui.Views;

namespace Enigma.Frontend.Ui.Messaging;

public class GeneralMsgAgent: IMsgAgent
{
    public GeneralMsgAgent()
    {
        WeakReferenceMessenger.Default.Register<OpenMessage>(this);
    }
    
    
    public void Receive(CancelMessage message)
    {
        throw new System.NotImplementedException();
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
}
