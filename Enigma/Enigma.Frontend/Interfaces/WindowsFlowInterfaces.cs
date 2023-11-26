// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using CommunityToolkit.Mvvm.Messaging;
using Enigma.Frontend.Ui.Messaging;

namespace Enigma.Frontend.Ui.Interfaces;

public interface IWindowsFlow: 
    IRecipient<CancelMessage>, 
    IRecipient<CloseMessage>,
    IRecipient<OpenMessage>, 
    IRecipient<OkMessage>, 
    IRecipient<ContinueMessage>
{
}


public interface IResearchWindowsFlow : IWindowsFlow, 
    IRecipient<CompletedMessage>
{
}