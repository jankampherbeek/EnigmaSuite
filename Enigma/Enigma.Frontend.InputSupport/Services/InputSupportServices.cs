// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Frontend.InputSupport.Conversions;
using Enigma.Frontend.InputSupport.Validations;
using Microsoft.Extensions.DependencyInjection;

namespace
    Enigma.Frontend.InputSupport.Services;

public static class InputSupportServices
{
    public static void RegisterInputSUpportServices(this ServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IDateValidator, DateValidator> ();
        serviceCollection.AddSingleton<ITimeValidator, TimeValidator> ();
        serviceCollection.AddSingleton<ISexagesimalConversions, SexagesimalConversions> ();
        serviceCollection.AddSingleton<IValueRangeConverter, ValueRangeConverter> ();
    }
}