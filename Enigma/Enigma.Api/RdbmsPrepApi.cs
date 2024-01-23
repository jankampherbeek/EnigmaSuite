// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Persistency;

namespace Enigma.Api;

/// <summary>Api for the preparation of the relational database.</summary>
public interface IRdbmsPrepApi
{
    /// <summary>Start preparation for the database.</summary>
    /// <returns>True if preparation was successful, otherwise false.</returns>
    public bool PrepareRdbms();
}

/// <inheritdoc/>
public class RdbmsPrepApi: IRdbmsPrepApi
{
    private IRdbmsPreparator _preparator;
    
    public RdbmsPrepApi(IRdbmsPreparator preparator)
    {
        _preparator = preparator;
    }
    
    /// <inheritdoc/>
    public bool PrepareRdbms()
    {
        return _preparator.PreparaDatabase();
    }
}