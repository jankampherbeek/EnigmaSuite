// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Calc;
using Enigma.Core.Handlers.Analysis;
using Enigma.Core.Handlers.Analysis.Helpers;
using Enigma.Core.Handlers.Calc;
using Enigma.Core.Handlers.Calc.Celestialpoints;
using Enigma.Core.Handlers.Calc.CelestialPoints;
using Enigma.Core.Handlers.Calc.CelestialPoints.Helpers;
using Enigma.Core.Handlers.Calc.Coordinates;
using Enigma.Core.Handlers.Calc.Coordinates.Helpers;
using Enigma.Core.Handlers.Calc.DateTime;
using Enigma.Core.Handlers.Calc.DateTime.Helpers;
using Enigma.Core.Handlers.Calc.Helpers;
using Enigma.Core.Handlers.Calc.Mundane.Helpers;
using Enigma.Core.Handlers.Calc.Specials;
using Enigma.Core.Handlers.Calc.Specials.Helpers;
using Enigma.Core.Handlers.Communication;
using Enigma.Core.Handlers.Communication.Helpers;
using Enigma.Core.Handlers.Configuration;
using Enigma.Core.Handlers.Configuration.Helpers;
using Enigma.Core.Handlers.Configuration.Interfaces;
using Enigma.Core.Handlers.Interfaces;
using Enigma.Core.Handlers.Persistency;
using Enigma.Core.Handlers.Persistency.Daos;
using Enigma.Core.Handlers.Persistency.Helpers;
using Enigma.Core.Handlers.Research.Services;
using EnigmaCore.Handlers.Calc.CelestialPoints.Helpers;
using Microsoft.Extensions.DependencyInjection;

namespace Enigma.Core.Handlers.Services;


/// <summary>
/// Definitions for Dependency Injection for classes and interfaces for handlers.
/// </summary>

public static class HandlerServices
{
    public static void RegisterHandlerServices(this ServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<IPointsForMidpoints, PointsForMidpoints>();
        serviceCollection.AddTransient<IAspectOrbConstructor, AspectOrbConstructor>();
        serviceCollection.AddTransient<IAspectPointSelector, AspectPointSelector>();
        serviceCollection.AddTransient<IAspectsHandler, AspectsHandler>();
        serviceCollection.AddTransient<IAstroConfigParser, AstroConfigParser>();
        serviceCollection.AddTransient<IBaseMidpointsCreator, BaseMidpointsCreator>();
        serviceCollection.AddTransient<ICalcChartsRangeHandler, CalcChartsRangeHandler>();
        serviceCollection.AddTransient<ICalcHelioPos, CalcHelioPos>();
        serviceCollection.AddTransient<ICelPointSECalc, CelPointSECalc>();
        serviceCollection.AddTransient<ICelPointsElementsCalc, CelPointsElementsCalc>();
        serviceCollection.AddTransient<ICelPointsHandler, CelPointsHandler>();
        serviceCollection.AddTransient<IChartAllPositionsHandler, ChartAllPositionsHandler>();
        serviceCollection.AddTransient<IChartDataDao, ChartDataDao>();
        serviceCollection.AddTransient<IChartPointsMapping, ChartPointsMapping>();
        serviceCollection.AddTransient<ICommunicationHandler, CommunicationHandler>();
        serviceCollection.AddTransient<IConfigReader, ConfigReader>();
        serviceCollection.AddTransient<IConfigurationHandler, ConfigurationHandler>();
        serviceCollection.AddTransient<IConfigWriter, ConfigWriter>();
        serviceCollection.AddTransient<ICoordinateConversionCalc, CoordinateConversionCalc>();
        serviceCollection.AddTransient<ICoordinateConversionHandler, CoordinateConversionHandler>();
        serviceCollection.AddTransient<ICsv2JsonConverter, Csv2JsonConverter>();
        serviceCollection.AddTransient<IDataFilePreparationHandler, DataFilePreparationHandler>();
        serviceCollection.AddTransient<IDataFilePreparator, DataFilePreparator>();
        serviceCollection.AddTransient<IDataImportHandler, DataImportHandler>();
        serviceCollection.AddTransient<IDataNamesHandler, DataNamesHandler>();
        serviceCollection.AddTransient<IDateCheckedConversion, DateCheckedConversion>();
        serviceCollection.AddTransient<IDateTimeCalc, DateTimeCalc>();
        serviceCollection.AddTransient<IDateTimeHandler, DateTimeHandler>();
        serviceCollection.AddTransient<IDateTimeValidator, DateTimeValidator>();
        serviceCollection.AddTransient<IDefaultConfiguration, DefaultConfiguration>();
        serviceCollection.AddTransient<IDistanceCalculator, DistanceCalculator>();
        serviceCollection.AddTransient<IFileCopier, FileCopier>();
        serviceCollection.AddTransient<IFilePersistencyHandler, FilePersistencyHandler>();
        serviceCollection.AddTransient<IFoldersInfo, FoldersInfo>();
        serviceCollection.AddTransient<IFullPointPosFactory, FullPointPosFactory>();
        serviceCollection.AddTransient<IHarmonicsCalculator, HarmonicsCalculator>();
        serviceCollection.AddTransient<IHarmonicsHandler, HarmonicsHandler>();
        serviceCollection.AddTransient<IHorizontalCalc, HorizontalCalc>();
        serviceCollection.AddTransient<IHorizontalHandler, HorizontalHandler>();
        serviceCollection.AddTransient<IHousesCalc, HousesCalc>();
        serviceCollection.AddTransient<IHousesHandler, HousesHandler>();
        serviceCollection.AddTransient<IHttpRequester, HttpRequester>();
        serviceCollection.AddTransient<IJulDayCalc, JulDayCalc>();
        serviceCollection.AddTransient<IJulDayHandler, JulDayHandler>();
        serviceCollection.AddTransient<ILocationCheckedConversion, LocationCheckedConversion>();
        serviceCollection.AddTransient<ILotsCalculator, LotsCalculator>();
        serviceCollection.AddTransient<IMidpointsHandler, MidpointsHandler>();
        serviceCollection.AddTransient<IObliqueLongitudeCalculator, ObliqueLongitudeCalculator>();
        serviceCollection.AddTransient<IObliqueLongitudeHandler, ObliqueLongitudeHandler>();
        serviceCollection.AddTransient<IObliquityCalc, ObliquityCalc>();
        serviceCollection.AddTransient<IObliquityHandler, ObliquityHandler>();
        serviceCollection.AddTransient<IOccupiedMidpointsFinder, OccupiedMidpointsFinder>();
        serviceCollection.AddTransient<ISeFlags, SeFlags>();
        serviceCollection.AddTransient<ISeHandler, SeHandler>();
        serviceCollection.AddTransient<ISouthPointCalculator, SouthPointCalculator>();
        serviceCollection.AddTransient<ITextFileReader, TextFileReader>();
        serviceCollection.AddTransient<ITextFileWriter, TextFileWriter>();
        serviceCollection.AddTransient<ITimeCheckedConversion, TimeCheckedConversion>();
        serviceCollection.AddTransient<IZodiacPointsCalc, ZodiacPointsCalc>();

        serviceCollection.RegisterResearchServices();

    }



}