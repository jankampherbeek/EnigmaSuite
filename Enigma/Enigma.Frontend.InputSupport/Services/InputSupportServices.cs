// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Frontend.InputSupport.Conversions;
using Enigma.Frontend.InputSupport.InputParsers;
using Enigma.Frontend.InputSupport.Validations;
using Microsoft.Extensions.DependencyInjection;

namespace
    Enigma.Frontend.InputSupport.Services;

public static class InputSupportServices
{
    public static void RegisterInputSupportServices(this ServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IDateInputParser, DateInputParser>();
        serviceCollection.AddSingleton<IDateValidator, DateValidator> ();
        serviceCollection.AddSingleton<IGeoLatInputParser, GeoLatInputParser>();
        serviceCollection.AddSingleton<IGeoLatValidator, GeoLatValidator> ();
        serviceCollection.AddSingleton<IGeoLongInputParser, GeoLongInputParser>();
        serviceCollection.AddSingleton<IGeoLongValidator, GeoLongValidator>();
        serviceCollection.AddSingleton<ISexagesimalConversions, SexagesimalConversions> ();
        serviceCollection.AddSingleton<ITimeInputParser, TimeInputParser>();
        serviceCollection.AddSingleton<ITimeValidator, TimeValidator>();
        serviceCollection.AddSingleton<IValueRangeConverter, ValueRangeConverter> ();
    }
}