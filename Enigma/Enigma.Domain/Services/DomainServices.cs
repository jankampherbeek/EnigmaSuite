// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Analysis;
using Enigma.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Enigma.Domain.Services;

public static class DomainServices
{
    public static void RegisterDomainServices(this ServiceCollection serviceCollection)
    {

        serviceCollection.AddTransient<IOrbDefinitions, OrbDefinitions>();
    }
}

