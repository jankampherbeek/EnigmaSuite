﻿// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Research;
using Enigma.Domain.Persistables;

namespace Enigma.Core.Handlers;

/// <summary>Handle research data.</summary>
public interface IResearchDataHandler
{
    /// <summary>Convert Json text into StandardInput object.</summary>
    /// <param name="json">The Json text.</param>
    /// <returns>The resulting StandardInput object.</returns>
    public StandardInput GetStandardInputFromJson(string json);
}

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
        // TODO 0.3 handle errors   
        return _inputDataConverter.UnMarshallStandardInput(json);
    }

}