// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


using CommunityToolkit.Mvvm.Messaging.Messages;
using Enigma.Domain.Dtos;

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

/// <summary>Message indicating that the Close-button has been clicked.</summary>
public class CloseMessage : ValueChangedMessage<string>
{
    public CloseMessage(string value) : base(value)
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

/// <summary>Message to show a help page. The value contains the name of the help file without the extension.</summary>
public class HelpMessage : ValueChangedMessage<string>
{
    public HelpMessage(string value) : base(value)
    {
        
    }
}

public class HarmonicDetailsMessage : ValueChangedMessage<HarmonicDetailsSelection>
{
    public HarmonicDetailsMessage(HarmonicDetailsSelection selection): base(selection)
    {
        
    }
}

public class MidpointDetailsMessage : ValueChangedMessage<MidpointDetailsSelection>
{
    public MidpointDetailsMessage(MidpointDetailsSelection selection): base(selection)
    {
        
    }
    
}
