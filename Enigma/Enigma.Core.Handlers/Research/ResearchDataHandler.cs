// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Handlers.Interfaces;
using Enigma.Core.Work.Research.Interfaces;
using Enigma.Domain.Persistency;

namespace Enigma.Core.Handlers.Research;

/// <inheritdoc/>
public class ResearchDataHandler: IResearchDataHandler
{

    private IInputDataConverter _inputDataConverter;

    public ResearchDataHandler(IInputDataConverter inputDataConverter)
    {
        _inputDataConverter = inputDataConverter;
    }

    /// <inheritdoc/>
    public StandardInput GetStandardInputFromJson(string json)
    {
        // TODO handler errors   
        return _inputDataConverter.UnMarshallStandardInput(json);
    }

}