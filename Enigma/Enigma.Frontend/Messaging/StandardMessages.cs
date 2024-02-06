// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


using CommunityToolkit.Mvvm.Messaging.Messages;

namespace Enigma.Frontend.Ui.Messaging;

// Standard messages that can be used by all modules.

/// <summary>Message indicating that a new view should be opened.</summary>
public class OpenMessage : ValueChangedMessage<string>
{
    public string ParentView { get; }
    public string ViewToOpen { get; }
    
    public OpenMessage(string value, string viewToOpen) : base(value)
    {
        ParentView = value;
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

/// <summary>Message indicating that the Close-button has been clicked in a window that is non-dialog.</summary>
public class CloseNonDlgMessage : ValueChangedMessage<string>
{
    public int WindowId { get; }
    public string WindowToClose { get; }
    public CloseNonDlgMessage(string value, int windowId) : base(value)
    {
        WindowId = windowId;
        WindowToClose = value;
    }
}


/// <summary>Close all child windows for the parentwindow as defined with the windowid in 'value'.</summary>
public class CloseChildWindowsMessage : ValueChangedMessage<string>
{
    public CloseChildWindowsMessage(string value) : base(value)
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

/// <summary>Message that a chart has been found.</summary>
public class FoundChartMessage : ValueChangedMessage<string>
{
    public long ChartId;
    public FoundChartMessage(string value, long chartId) : base(value)
    {
        ChartId = chartId;
    }
}


public class EventCompletedMessage : ValueChangedMessage<string>
{
    public EventCompletedMessage(string value) : base(value)
    {
        
    }
}

public class ConfigUpdatedMessage : ValueChangedMessage<string>
{
    public ConfigUpdatedMessage(string value) : base(value)
    {
        
    }
}

public class ProgConfigUpdatedMessage : ValueChangedMessage<string>
{
    public ProgConfigUpdatedMessage(string value) : base(value)
    {
        
    }
}

public class CloseProgEventViewMessage : ValueChangedMessage<string>
{
    public CloseProgEventViewMessage(string value) : base(value)
    {
        
    }
}

public class CloseRadixDataInputViewMessage : ValueChangedMessage<string>
{
    public CloseRadixDataInputViewMessage(string value) : base(value)
    {
        
    }
}