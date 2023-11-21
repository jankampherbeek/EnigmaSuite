// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


using CommunityToolkit.Mvvm.Messaging.Messages;

namespace Enigma.Frontend.Ui.Messaging;

// Messages that are specific for the Research module..

/// <summary>Message indicating that a research result is available.</summary>
public class ResearchResultMessage : ValueChangedMessage<string>
{
    public ResearchResultMessage(string sender) : base(sender)
    {
        
    }
}