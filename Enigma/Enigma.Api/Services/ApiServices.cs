// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Engima.Api.Persistency;
using Engima.Api.Research;
using Enigma.Api.Analysis;
using Enigma.Api.Astron;
using Enigma.Api.Interfaces;
using Enigma.Configuration.Services;
using Enigma.Core.Analysis.Services;
using Enigma.Core.Calc.Api.DateTime;
using Enigma.Core.Calc.Services;
using Enigma.Core.Handlers.Services;
using Enigma.Domain.Services;
using Enigma.Frontend.Helpers.Services;
using Enigma.Persistency.Services;
using Enigma.Research.Services;
using Microsoft.Extensions.DependencyInjection;

namespace
    Enigma.Api.Services;

public static class ApiServices
{
    public static void RegisterApiServices(this ServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<IAspectsApi, AspectsApi>();
        serviceCollection.AddTransient<ICalcDateTimeApi, CalcDateTimeApi>();
        serviceCollection.AddTransient<IChartAllPositionsApi, ChartAllPositionsApi>();
        serviceCollection.AddTransient<ICheckDateTimeApi, CheckDateTimeApi>();
        serviceCollection.AddTransient<ICoordinateConversionApi, CoordinateConversionApi>();
        serviceCollection.AddTransient<IDataHandlerApi, DataHandlerApi>();
        serviceCollection.AddTransient<IFileManagementApi, FileManagementApi>();
        serviceCollection.AddTransient<IHorizontalApi, HorizontalApi>();
        serviceCollection.AddTransient<IHousesApi, HousesApi>();
        serviceCollection.AddTransient<IJulianDayApi, JulianDayApi>();
        serviceCollection.AddTransient<IObliqueLongitudeApi, ObliqueLongitudeApi>();
        serviceCollection.AddTransient<IObliquityApi, ObliquityApi>();
        serviceCollection.AddTransient<IProjectCreationApi, ProjectCreationApi>();

        serviceCollection.RegisterCalculationServices();
        serviceCollection.RegisterDomainServices();
        serviceCollection.RegisterInputSupportServices();
        serviceCollection.RegisterAnalysisServices();
        serviceCollection.RegisterPersistencyServices();
        serviceCollection.RegisterConfigurationServices();
        serviceCollection.RegisterResearchServices();
        serviceCollection.RegisterHandlerServices();
    }
}