// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Api.Analysis;
using Enigma.Api.Astron;
using Enigma.Api.Calc;
using Enigma.Api.Calc.CalcChartsRangeApi;
using Enigma.Api.Communication;
using Enigma.Api.Configuration;
using Enigma.Api.Interfaces;
using Enigma.Api.Peristency;
using Enigma.Api.Persistency;
using Enigma.Api.Research;
using Enigma.Core.Handlers.Services;
using Enigma.Domain.Services;
using Enigma.Facades.Services;
using Microsoft.Extensions.DependencyInjection;

namespace
    Enigma.Api.Services;

public static class ApiServices
{
    public static void RegisterApiServices(this ServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<IAspectsApi, AspectsApi>();
        serviceCollection.AddTransient<ICalcChartsRangeApi, CalcChartsRangeApi>();
        serviceCollection.AddTransient<IChartAllPositionsApi, ChartAllPositionsApi>();
        serviceCollection.AddTransient<IChartDataPersistencyApi, ChartDataPersistencyApi>();
        serviceCollection.AddTransient<ICommunicationApi, CommunicationApi>();
        serviceCollection.AddTransient<IConfigurationApi, ConfigurationApi>();
        serviceCollection.AddTransient<ICoordinateConversionApi, CoordinateConversionApi>();
        serviceCollection.AddTransient<IDateTimeApi, DateTimeApi>();
        serviceCollection.AddTransient<IDataHandlerApi, DataHandlerApi>();
        serviceCollection.AddTransient<IEventDataPersistencyApi, EventDataPersistencyApi>();
        serviceCollection.AddTransient<IFileAccessApi, FileAccessApi>();
        serviceCollection.AddTransient<IDataFileManagementApi, DataFileManagementApi>();
        serviceCollection.AddTransient<IHarmonicsApi, HarmonicsApi>();
        serviceCollection.AddTransient<IHorizontalApi, HorizontalApi>();
        serviceCollection.AddTransient<IHousesApi, HousesApi>();
        serviceCollection.AddTransient<IMidpointsApi, MidpointsApi>();
        serviceCollection.AddTransient<IJulianDayApi, JulianDayApi>();
        serviceCollection.AddTransient<IObliqueLongitudeApi, ObliqueLongitudeApi>();
        serviceCollection.AddTransient<IObliquityApi, ObliquityApi>();
        serviceCollection.AddTransient<IResearchPathApi, ResearchPathApi>();
        serviceCollection.AddTransient<IResearchPerformApi, ResearchPerformApi>();
        serviceCollection.AddTransient<IProjectCreationApi, ProjectCreationApi>();
        serviceCollection.AddTransient<IProjectsOverviewApi, ProjectsOverviewApi>();
        serviceCollection.AddTransient<ISeApi, SeApi>();

        serviceCollection.RegisterFacadesServices();
        serviceCollection.RegisterDomainServices();
        serviceCollection.RegisterHandlerServices();
    }
}