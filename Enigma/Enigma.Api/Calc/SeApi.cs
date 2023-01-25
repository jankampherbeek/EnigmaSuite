// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Api.Interfaces;
using Enigma.Core.Handlers.Interfaces;
using Serilog;

namespace Enigma.Api.Calc;

/// <inheritdoc/>
public sealed class SeApi : ISeApi
{
    private readonly ISeHandler _seHandler;

    public SeApi(ISeHandler seHandler) => _seHandler = seHandler;


    /// <inheritdoc/>
    public void SetupSe(string pathToSeFiles)
    {
        Log.Information("SeApi SetupSe: Setting up CommonSE with path {p}.", pathToSeFiles);
        _seHandler.SetupSe(pathToSeFiles);
    }

    public void CloseSe()
    {
        _seHandler.CloseSe();
    }
}
