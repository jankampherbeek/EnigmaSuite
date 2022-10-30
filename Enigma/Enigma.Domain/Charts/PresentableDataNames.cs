// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Domain.Charts;

/// <summary>Data names to be shown in a data grid.</summary>
public record PresentableDataName
{
    public string DataName { get; }

    public PresentableDataName(string dataName)
    {
        DataName = dataName;
    }
}