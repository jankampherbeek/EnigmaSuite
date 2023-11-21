// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


using CommunityToolkit.Mvvm.Messaging.Messages;

namespace Enigma.Frontend.Ui.Messaging;

// Standard messages that can be used by all modules.

/// <summary>Message indicating that a new view should be opened.</summary>
public class OpenMessage : ValueChangedMessage<string>
{
    public string ViewToOpen { get; set; }
    public OpenMessage(string value, string viewToOpen) : base(value)
    {
        ViewToOpen = viewToOpen;
    }
}

/// <summary>Message indicating that the Cancel-button has been clicked.</summary>
public class CancelMessage : ValueChangedMessage<string>
{
    public CancelMessage(string value) : base(value)
    {
        
    }
}

/// <summary>Message indicating that the OK-button has been clicked.</summary>
public class OkMessage : ValueChangedMessage<string>
{
    public OkMessage(string value) : base(value)
    {
        
    }
}


/// <summary>Message indicating that the Continue-button has been clicked.</summary>
public class ContinueMessage : ValueChangedMessage<string>
{
    public ContinueMessage(string value) : base(value)
    {
        
    }
}

/// <summary>Message that indicates the successful completion of some action.</summary>
public class CompletedMessage : ValueChangedMessage<string>
{
    public CompletedMessage(string value) : base(value)
    {
        
    }
}

