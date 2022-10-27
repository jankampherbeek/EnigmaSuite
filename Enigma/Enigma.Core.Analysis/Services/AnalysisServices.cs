// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Analysis.Api;
using Enigma.Core.Analysis.Aspects;
using Enigma.Core.Analysis.Dto;
using Enigma.Core.Analysis.Midpoints;
using Microsoft.Extensions.DependencyInjection;

namespace
    Enigma.Core.Analysis.Services;

public static class AnalysisServices
{
    public static void RegisterAnalysisServices(this ServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<IAnalysisPointsMapping, AnalysisPointsMapping>();
        serviceCollection.AddTransient<IAspectChecker, AspectChecker>();
        serviceCollection.AddTransient<IAspectOrbConstructor, AspectOrbConstructor>();
        serviceCollection.AddTransient<IAspectsApi, AspectsApi>();
        serviceCollection.AddTransient<IMidpointChecker, MidpointChecker>();
        serviceCollection.AddTransient<IMundanePointToAnalysisPointMap, MundanePointToAnalysisPointMap>();
        serviceCollection.AddTransient<ISolSysPointToAnalysisPointMap, SolSysPointToAnalysisPointMap>();
    }
}