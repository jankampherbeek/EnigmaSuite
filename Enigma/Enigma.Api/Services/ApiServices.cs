// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Engima.Api.Persistency;
using Engima.Api.Research;
using Enigma.Api.Analysis;
using Enigma.Api.Astron;
using Enigma.Api.Configuration;
using Enigma.Api.Interfaces;
using Enigma.Api.Calc;
using Enigma.Core.Handlers.Services;
using Enigma.Core.Work.Services;
using Enigma.Domain.Services;
using Enigma.Facades.Services;
using Microsoft.Extensions.DependencyInjection;
using Enigma.Api.Calc.CalcChartsRangeApi;

namespace
    Enigma.Api.Services;

public static class ApiServices
{
    public static void RegisterApiServices(this ServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<IAspectsApi, AspectsApi>();
        serviceCollection.AddTransient<ICalcChartsRangeApi, CalcChartsRangeApi>();
        serviceCollection.AddTransient<ICalcDateTimeApi, CalcDateTimeApi>();
        serviceCollection.AddTransient<IChartAllPositionsApi, ChartAllPositionsApi>();
        serviceCollection.AddTransient<IDateTimeApi, DateTimeApi>();
        serviceCollection.AddTransient<IConfigurationApi, ConfigurationApi>();
        serviceCollection.AddTransient<ICoordinateConversionApi, CoordinateConversionApi>();
        serviceCollection.AddTransient<IDataHandlerApi, DataHandlerApi>();
        serviceCollection.AddTransient<IFileManagementApi, FileManagementApi>();
        serviceCollection.AddTransient<IHarmonicsApi, HarmonicsApi>();
        serviceCollection.AddTransient<IHorizontalApi, HorizontalApi>();
        serviceCollection.AddTransient<IHousesApi, HousesApi>();
        serviceCollection.AddTransient<IMidpointsApi, MidpointsApi>();
        serviceCollection.AddTransient<IJulianDayApi, JulianDayApi>();
        serviceCollection.AddTransient<IObliqueLongitudeApi, ObliqueLongitudeApi>();
        serviceCollection.AddTransient<IObliquityApi, ObliquityApi>();
        serviceCollection.AddTransient<IResearchPerformApi, ResearchPerformApi>();
        serviceCollection.AddTransient<IProjectCreationApi, ProjectCreationApi>();
        serviceCollection.AddTransient<IProjectsOverviewApi, ProjectsOverviewApi>();
        serviceCollection.AddTransient<ISeApi, SeApi>();

        serviceCollection.RegisterFacadesServices();
        serviceCollection.RegisterDomainServices();
        serviceCollection.RegisterHandlerServices();
    }
}