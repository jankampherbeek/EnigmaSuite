// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Api.Charts.Prog.PrimDir;
using Enigma.Api.LocationAndTimezones;
using Enigma.Api.LocationAndTimeZones;
using Enigma.Core.Services;
using Enigma.Domain.Services;
using Enigma.Facades;
using Microsoft.Extensions.DependencyInjection;

namespace
    Enigma.Api.Services;

public static class ApiServices
{
    public static void RegisterApiServices(this ServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<IAnalysisSingleValuesApi, AnalysisSingleValuesApi>();
        serviceCollection.AddTransient<IAspectsApi, AspectsApi>();
        serviceCollection.AddTransient<ICalcChartsRangeApi, CalcChartsRangeApi>();
        serviceCollection.AddTransient<IChartAllPositionsApi, ChartAllPositionsApi>();
        serviceCollection.AddTransient<IChartDataPersistencyApi, ChartDataPersistencyApi>();
        serviceCollection.AddTransient<ICommunicationApi, CommunicationApi>();
        serviceCollection.AddTransient<IConfigurationApi, ConfigurationApi>();
        serviceCollection.AddTransient<ICoordinateConversionApi, CoordinateConversionApi>();
        serviceCollection.AddTransient<IDateTimeApi, DateTimeApi>();
        serviceCollection.AddTransient<IDataHandlerApi, DataHandlerApi>();
        serviceCollection.AddTransient<IDeclMidpointsApi, DeclMidpointsApi>();
        serviceCollection.AddTransient<IEventDataPersistencyApi, EventDataPersistencyApi>();
        serviceCollection.AddTransient<IFileAccessApi, FileAccessApi>();
        serviceCollection.AddTransient<IDataFileManagementApi, DataFileManagementApi>();
        serviceCollection.AddTransient<IHarmonicsApi, HarmonicsApi>();
        serviceCollection.AddTransient<IHorizontalApi, HorizontalApi>();
        serviceCollection.AddTransient<IHousesApi, HousesApi>();
        serviceCollection.AddTransient<ILocationApi, LocationApi>();
        serviceCollection.AddTransient<IMidpointsApi, MidpointsApi>();
        serviceCollection.AddTransient<IJulianDayApi, JulianDayApi>();
        serviceCollection.AddTransient<IObliqueLongitudeApi, ObliqueLongitudeApi>();
        serviceCollection.AddTransient<IObliquityApi, ObliquityApi>();
        serviceCollection.AddTransient<IOobCalApi, OobCalApi>();
        serviceCollection.AddTransient<IParallelsApi, ParallelsApi>();
        serviceCollection.AddTransient<IPdDataImportExportApi, PdDataImportExportApi>();             
        serviceCollection.AddTransient<IProgAspectsApi, ProgAspectsApi>();
        serviceCollection.AddTransient<IPrimDirApi, PrimDirApi>();
        serviceCollection.AddTransient<IProgSecDirEventApi, ProgSecDirEventApi>();
        serviceCollection.AddTransient<IProgSymDirEventApi, ProgSymDirEventApi>();
        serviceCollection.AddTransient<IProgTransitsEventApi, ProgTransitsEventApi>();
        serviceCollection.AddTransient<IRdbmsPrepApi, RdbmsPrepApi>();
        serviceCollection.AddTransient<IResearchPathApi, ResearchPathApi>();
        serviceCollection.AddTransient<IResearchPerformApi, ResearchPerformApi>();
        serviceCollection.AddTransient<IResourceBundleApi, ResourceBundleApi>();
        serviceCollection.AddTransient<IProjectCreationApi, ProjectCreationApi>();
        serviceCollection.AddTransient<IProjectsOverviewApi, ProjectsOverviewApi>();
        serviceCollection.AddTransient<IReferencesApi, ReferencesApi>();
        serviceCollection.AddTransient<ISeApi, SeApi>();
        serviceCollection.AddTransient<ITimezoneApi, TimezoneApi>();

        serviceCollection.RegisterFacadesServices();
        serviceCollection.RegisterDomainServices();
        serviceCollection.RegisterHandlerServices();
    }
}