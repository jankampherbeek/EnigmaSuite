// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using CommunityToolkit.Mvvm.Messaging;
using Enigma.Frontend.Ui.Interfaces;
using Enigma.Frontend.Ui.Views;

namespace Enigma.Frontend.Ui.Messaging;

public class ResearchMsgAgent: IResearchMsgAgent
{
    private ProjectInputWindow _projectInputWindow;
    
    
    
    public ResearchMsgAgent()
    {
        WeakReferenceMessenger.Default.Register<OpenMessage>(this);
        WeakReferenceMessenger.Default.Register<CompletedMessage>(this);
        WeakReferenceMessenger.Default.Register<CancelMessage>(this);
    }
    
    
    public void Receive(CancelMessage message)
    {
        if (message.Value == "ProjectInput")
        {
            _projectInputWindow.Close();
        }
    }

    public void Receive(OpenMessage message)
    {
        if (message.ViewToOpen == "ProjectInput")
        {
            _projectInputWindow = new();
            _projectInputWindow.ShowDialog();
        }
    }

    public void Receive(CompletedMessage message)
    {
        if (message.Value == "ProjectInput")
        {
            _projectInputWindow.Close();
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

    public void Receive(ResearchResultMessage message)
    {
        throw new System.NotImplementedException();
    }
    
}