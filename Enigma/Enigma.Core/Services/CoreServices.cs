﻿// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Analysis;
using Enigma.Core.Analysis.Helpers;
using Enigma.Core.Calc;
using Enigma.Core.Communication;
using Enigma.Core.Configuration;
using Enigma.Core.Handlers;
using Enigma.Core.Interfaces;
using Enigma.Core.Persistency;
using Enigma.Core.Research.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Enigma.Core.Services;


/// <summary>
/// Definitions for Dependency Injection for classes and interfaces for handlers.
/// </summary>

public static class CoreServices
{
    public static void RegisterHandlerServices(this ServiceCollection serviceCollection)
    {

        serviceCollection.AddTransient<IAspectOrbConstructor, AspectOrbConstructor>();
        serviceCollection.AddTransient<IAspectPointSelector, AspectPointSelector>();
        serviceCollection.AddTransient<IAspectsHandler, AspectsHandler>();
        serviceCollection.AddTransient<IAstroConfigParser, AstroConfigParser>();
        serviceCollection.AddTransient<IBaseMidpointsCreator, BaseMidpointsCreator>();
        serviceCollection.AddTransient<ICalcChartsRangeHandler, CalcChartsRangeHandler>();
        serviceCollection.AddTransient<ICalcHelioPos, CalcHelioPos>();
        serviceCollection.AddTransient<ICalculatedDistance, CalculatedDistance>();
        serviceCollection.AddTransient<ICelPointSeCalc, CelPointSeCalc>();
        serviceCollection.AddTransient<ICelPointsElementsCalc, CelPointsElementsCalc>();
        serviceCollection.AddTransient<ICelPointsHandler, CelPointsHandler>();
        serviceCollection.AddTransient<IChartAllPositionsHandler, ChartAllPositionsHandler>();
        serviceCollection.AddTransient<IChartDataDao, ChartDataDao>();
        serviceCollection.AddTransient<IChartPointsMapping, ChartPointsMapping>();
        serviceCollection.AddTransient<ICheckedProgAspects, CheckedProgAspects>();
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
        serviceCollection.AddTransient<ICalculatedDistance, CalculatedDistance>();
        serviceCollection.AddTransient<IEventDataDao, EventDataDao>();
        serviceCollection.AddTransient<IFileCopier, FileCopier>();
        serviceCollection.AddTransient<IFilePersistencyHandler, FilePersistencyHandler>();
        serviceCollection.AddTransient<IFixedTimeKey, FixedTimeKey>();
        serviceCollection.AddTransient<IFoldersInfo, FoldersInfo>();
        serviceCollection.AddTransient<IFullPointPosFactory, FullPointPosFactory>();
        serviceCollection.AddTransient<IHarmonicsCalculator, HarmonicsCalculator>();
        serviceCollection.AddTransient<IHarmonicsHandler, HarmonicsHandler>();
        serviceCollection.AddTransient<IHorizontalCalc, HorizontalCalc>();
        serviceCollection.AddTransient<IHorizontalHandler, HorizontalHandler>();
        serviceCollection.AddTransient<IHousesCalc, HousesCalc>();
        serviceCollection.AddTransient<IHousesHandler, HousesHandler>();
        serviceCollection.AddTransient<IHttpRequester, HttpRequester>();
        serviceCollection.AddTransient<IInterChartEventDao, InterChartEventDao>();
        serviceCollection.AddTransient<IInterChartPeriodDao, InterChartPeriodDao>();
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
        serviceCollection.AddTransient<IPeriodDataDao, PeriodDataDao>();
        serviceCollection.AddTransient<IPeriodSupportChecker, PeriodSupportChecker>();
        serviceCollection.AddTransient<IPlacidusTimeKey, PlacidusTimeKey>();
        serviceCollection.AddTransient<IPointsForMidpoints, PointsForMidpoints>();        
        serviceCollection.AddTransient<IPositionFinder, PositionFinder>();
        serviceCollection.AddTransient<IProgAspectsHandler, ProgAspectsHandler>();
        serviceCollection.AddTransient<ISeFlags, SeFlags>();
        serviceCollection.AddTransient<ISeHandler, SeHandler>();
        serviceCollection.AddTransient<ISolarArcCalculator, SolarArcCalculator>();
        serviceCollection.AddTransient<ISouthPointCalculator, SouthPointCalculator>();
        serviceCollection.AddTransient<ITextFileReader, TextFileReader>();
        serviceCollection.AddTransient<ITextFileWriter, TextFileWriter>();
        serviceCollection.AddTransient<ITimeCheckedConversion, TimeCheckedConversion>();
        serviceCollection.AddTransient<ITimeKeyCalculator, TimeKeyCalculator>();
        serviceCollection.AddTransient<IZodiacPointsCalc, ZodiacPointsCalc>();

        serviceCollection.RegisterResearchServices();

    }



}