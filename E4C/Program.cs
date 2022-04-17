// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using E4C.be.persistency;
using E4C.calc.seph.secalculations;
using E4C.Models.Creators;
using E4C.Models.Domain;
using E4C.Models.UiHelpers;
using E4C.ViewModels;
using E4C.Views;
using E4C.domain.shared.references;
using SimpleInjector;
using System;
using E4C.calc.seph;
using E4C.calc.specifics;
using E4C.core.astron.obliquity;
using E4C.core.api;
using E4C.core.astron.coordinateconversion;
using E4C.core.facades;
using E4C.calc.seph.sefacade;
using E4C.shared.references;

namespace E4C
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            var container = Bootstrap();

            // Any additional other configuration, e.g. of your desired MVVM toolkit.

            RunApplication(container);
        }

        /// <summary>
        /// Container for dependency injection, using SimpleInjector.
        /// </summary>
        /// <returns>The constructed container.</returns>
        private static Container Bootstrap()
        {
            // Create the container.
            var container = new Container();

            // Register types:
            container.Register<IRosetta, Rosetta>(Lifestyle.Singleton);
            container.Register<ITextFromFileReader, TextFromFileReader>(Lifestyle.Singleton);
            container.Register<IObliquityCalc, ObliquityCalc>(Lifestyle.Singleton);
            container.Register<ISouthPointCalculator, SouthPointCalculator>(Lifestyle.Singleton);
            container.Register<IObliqueLongitudeCalculator, ObliqueLongitudeCalculator>(Lifestyle.Singleton);
            container.Register<ICalcUtFacade, CalcUtFacade>(Lifestyle.Singleton);
            container.Register<ISePosHousesFacade, SePosHousesFacade>(Lifestyle.Singleton);
            container.Register<IPositionSolSysPointSECalc, PositionSolSysPointSECalc>(Lifestyle.Singleton);
            container.Register<IMundanePositionsCalculator, MundanePositionsCalculator>(Lifestyle.Singleton);
            container.Register<IHorizontalCoordinatesFacade, HorizontalCoordinatesFacade>(Lifestyle.Singleton);
            container.Register<ICoTransFacade, CoTransFacade>(Lifestyle.Singleton);
            container.Register<IJulDayFacade, JulDayFacade>(Lifestyle.Singleton);
            container.Register<IRevJulFacade, RevJulFacade>(Lifestyle.Singleton);
            container.Register<IDateConversionFacade, DateConversionFacade>(Lifestyle.Singleton);
            container.Register<IFlagDefinitions, FlagDefinitions>(Lifestyle.Singleton);
            container.Register<IDateTimeValidations, DateTimeValidations>(Lifestyle.Singleton);
            container.Register<ILocationValidations, LocationValidations>(Lifestyle.Singleton);
            container.Register<ICalendarSpecifications, CalendarSpecifications>(Lifestyle.Singleton);
            container.Register<IYearCountSpecifications, YearCountSpecifications>(Lifestyle.Singleton);
            container.Register<IChartCategorySpecifications, ChartCategorySpecifications>(Lifestyle.Singleton);
            container.Register<IRoddenRatingSpecifications, RoddenRatingSpecifications>(Lifestyle.Singleton);
            container.Register<ITimeZoneSpecifications, TimeZoneSpecifications>(Lifestyle.Singleton);
            container.Register<ISexagesimalConversions, SexagesimalConversions>(Lifestyle.Singleton);
            container.Register<IDateConversions, DateConversions>(Lifestyle.Singleton);
            container.Register<IChartsStock, ChartsStock>(Lifestyle.Singleton);
            container.Register<ITextAssembler, TextAssembler>(Lifestyle.Singleton);
            container.Register<IIntRangeCreator, IntRangeCreator>(Lifestyle.Singleton);
            container.Register<ILocationFactory, LocationFactory>(Lifestyle.Singleton);
            container.Register<IDateFactory, DateFactory>(Lifestyle.Singleton);
            container.Register<ITimeFactory, TimeFactory>(Lifestyle.Singleton);
            container.Register<IDateTimeFactory, DateTimeFactory>(Lifestyle.Singleton);
            container.Register<ISolarSystemPointSpecifications, SolarSystemPointSpecifications>(Lifestyle.Singleton);
            container.Register<IAyanamshaSpecifications, AyanamshaSpecifications>(Lifestyle.Singleton);
            container.Register<IHouseSystemSpecs, HouseSystemSpecs>(Lifestyle.Singleton);
            container.Register<IAstronApi, AstronApi> (Lifestyle.Singleton);
            container.Register<ICoordinateConversionCalc, CoordinateConversionCalc> (Lifestyle.Singleton);
            container.Register<ICoordinateConversionHandler, CoordinateConversionHandler> ( Lifestyle.Singleton);
            container.Register<IObliquityCalc, ObliquityCalc>(Lifestyle.Singleton);
            container.Register<IObliquityHandler, ObliquityHandler> (Lifestyle.Singleton);
            container.Register<ICalcUtFacade, CalcUtFacade> (Lifestyle.Singleton);
            container.Register<ICoTransFacade, CoTransFacade> (Lifestyle.Singleton);


            // Register windows and view models:
            container.Register<MainWindow>();
            container.Register<MainWindowViewModel>();
            container.Register<CalcStartView>();
            container.Register<ICalcStartViewModel, CalcStartViewModel>();
            container.Register<DashboardCharts>();
            container.Register<DashboardChartsViewModel>();
            container.Register<ChartsDataInputView>();
            container.Register<ChartsDataInputViewModel>();
            container.Register<CalcJdView>();
            container.Register<CalcJdViewModel>();
            container.Register<CalcObliquityView>();
            container.Register<CalcObliquityViewModel>();

            container.Verify();

            return container;
        }

        private static void RunApplication(Container container)
        {
            try
            {
                SeInitializer.SetEphePath("./se");
                var app = new App();
                //    app.InitializeComponent();
                var mainWindow = container.GetInstance<MainWindow>();
                app.Run(mainWindow);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error to log in Program.RunApplication : " + e.Message);
                //Log the exception and exit
            }
        }
    }
}