// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Api.Interfaces;

namespace Enigma.Api.Calc;

/// <inheritdoc/>

public class SeApi : ISeApi
{
    private ISeHandler _seHandler;

    public SeApi(ISeHandler seHandler)
    {
        _seHandler = seHandler;
    }

    /// <inheritdoc/>
    public void SetupSe(string pathToSeFiles)
    {
        _seHandler.SetupSe(pathToSeFiles);
    }
}
