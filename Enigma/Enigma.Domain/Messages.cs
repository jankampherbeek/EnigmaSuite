// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.


namespace Enigma.Domain;


/// <summary>Types of messages that can be sent from Windows to the State Machine.</summary>
public enum MessageTypes
{
    MsgOk, MsgValidate, MsgCancel
}


/// <summary>All windows that can send messages to the State Machine.</summary>
/// <remarks>The name of the window should, by convention, correspond with the entry in the enum.</remarks>
public enum SendingWindows
{
    StartWindow, MainWindow
}


/// <summary>A message that can be sent by a window to the State Machine.</summary>
public record WindowMsg
{
    public readonly MessageTypes MsgType;
    public readonly SendingWindows SendingWindow;
    public readonly string MsgData;

    public WindowMsg(MessageTypes msgType, SendingWindows sendingWindow, string msgData)
    {
        MsgType = msgType;
        SendingWindow = sendingWindow;
        MsgData = msgData;
    }
}