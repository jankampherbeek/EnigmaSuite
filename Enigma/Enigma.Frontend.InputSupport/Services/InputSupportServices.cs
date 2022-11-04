// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Frontend.Helpers.Conversions;
using Enigma.Frontend.Helpers.InputParsers;
using Enigma.Frontend.Helpers.Interfaces;
using Enigma.Frontend.Helpers.Validations;
using Microsoft.Extensions.DependencyInjection;

namespace
    Enigma.Frontend.Helpers.Services;

public static class InputSupportServices
{
    public static void RegisterInputSupportServices(this ServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IDateInputParser, DateInputParser>();
        serviceCollection.AddSingleton<IDateValidator, DateValidator>();
        serviceCollection.AddSingleton<IDoubleToDmsConversions, DoubleToDmsConversions>();
        serviceCollection.AddSingleton<IGeoLatInputParser, GeoLatInputParser>();
        serviceCollection.AddSingleton<IGeoLatValidator, GeoLatValidator>();
        serviceCollection.AddSingleton<IGeoLongInputParser, GeoLongInputParser>();
        serviceCollection.AddSingleton<IGeoLongValidator, GeoLongValidator>();
        serviceCollection.AddSingleton<ISexagesimalConversions, SexagesimalConversions>();
        serviceCollection.AddSingleton<ITimeInputParser, TimeInputParser>();
        serviceCollection.AddSingleton<ITimeValidator, TimeValidator>();
        serviceCollection.AddSingleton<IValueRangeConverter, ValueRangeConverter>();
    }
}