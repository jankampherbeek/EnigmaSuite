// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Helpers.Interfaces;
using Enigma.Domain.Persistency;
using Enigma.Domain.Research;
using Newtonsoft.Json;
using Serilog;

namespace Enigma.Core.Helpers.Conversions;



/// <inheritdoc/>
public class InputDataConverter : IInputDataConverter
{
    /// <inheritdoc/>
    public string MarshallInputItem(StandardInputItem inputItem)
    {
        return JsonConvert.SerializeObject(inputItem, Formatting.Indented);
    }

    /// <inheritdoc/>
    public StandardInputItem UnMarshallInputItem(string jsonString)
    {
        return JsonConvert.DeserializeObject<StandardInputItem>(jsonString);
    }

    public string MarshallStandardInput(StandardInput standardInput)
    {
        return JsonConvert.SerializeObject(standardInput, Formatting.Indented);
    }

    public StandardInput UnMarshallStandardInput(string jsonString)
    {
        return JsonConvert.DeserializeObject<StandardInput>(jsonString);
    }

   



}