// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Handlers.Research.Interfaces;
using Enigma.Domain.Persistency;
using Newtonsoft.Json;

namespace Enigma.Core.Handlers.Research.Helpers;



/// <inheritdoc/>
public sealed class InputDataConverter : IInputDataConverter
{
    /// <inheritdoc/>
    public string MarshallInputItem(StandardInputItem inputItem)
    {
        return JsonConvert.SerializeObject(inputItem, Formatting.Indented);
    }

    /// <inheritdoc/>
    public StandardInputItem UnMarshallInputItem(string jsonString)
    {
        return JsonConvert.DeserializeObject<StandardInputItem>(jsonString)!;
    }

    public string MarshallStandardInput(StandardInput standardInput)
    {
        return JsonConvert.SerializeObject(standardInput, Formatting.Indented);
    }

    public StandardInput UnMarshallStandardInput(string jsonString)
    {
        return JsonConvert.DeserializeObject<StandardInput>(jsonString)!;
    }





}