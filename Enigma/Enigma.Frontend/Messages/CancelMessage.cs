// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


using CommunityToolkit.Mvvm.Messaging.Messages;

namespace Enigma.Frontend.Ui.Messages;

public class CancelMessage : ValueChangedMessage<string>
{
    public CancelMessage(string value) : base(value)
    {
        
    }
}