// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Handlers.Research.Interfaces;
using Enigma.Domain.Persistency;

namespace Enigma.Core.Handlers.Research;

/// <inheritdoc/>
public sealed class ResearchDataHandler : IResearchDataHandler
{

    private readonly IInputDataConverter _inputDataConverter;

    public ResearchDataHandler(IInputDataConverter inputDataConverter)
    {
        _inputDataConverter = inputDataConverter;
    }

    /// <inheritdoc/>
    public StandardInput GetStandardInputFromJson(string json)
    {
        // TODO 0.2 handler errors   
        return _inputDataConverter.UnMarshallStandardInput(json);
    }

}