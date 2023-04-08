// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Frontend.Ui.Support;

/// <summary>Simple sequence definer for the Id for Charts in the DataVault.</summary>
/// <remarks>The sequence is only valid during the active session.</remarks>
public sealed class ChartsIndexSequence
{


    private static int _lastIndex = 0;

    /// <summary>Return new id.</summary>
    /// <returns>New id, always a negative number.</returns>
    public static int NewSequenceId()
    {
        _lastIndex--;
        return _lastIndex;

    }


}