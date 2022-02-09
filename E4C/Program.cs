// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using E4C.be.persistency;
using E4C.Models.Astron;
using E4C.Models.Domain;
using E4C.Models.SeFacade;
using E4C.Models.Validations;
using E4C.ViewModels;
using E4C.Views;
using E4C.Views.ViewHelpers;
using SimpleInjector;
using System;


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
            container.Register<ICalendarCalc, CalendarCalc>(Lifestyle.Singleton);
            container.Register<IObliquityNutationCalc, ObliquityNutationCalc>(Lifestyle.Singleton);
            container.Register<ISePosCelPointFacade, SePosCelPointFacade>(Lifestyle.Singleton);
            container.Register<ISeDateTimeFacade, SeDateTimeFacade>(Lifestyle.Singleton);
            container.Register<IDateTimeValidations, DateTimeValidations>(Lifestyle.Singleton);
            container.Register<ILocationValidations, LocationValidations>(Lifestyle.Singleton);
            container.Register<ICalendarSpecifications, CalendarSpecifications>(Lifestyle.Singleton);
            container.Register<IYearCountSpecifications, YearCountSpecifications>(Lifestyle.Singleton);
            container.Register<IChartCategorySpecifications, ChartCategorySpecifications>(Lifestyle.Singleton);
            container.Register<IRoddenRatingSpecifications, RoddenRatingSpecifications>(Lifestyle.Singleton);
            container.Register<ITimeZoneSpecifications, TimeZoneSpecifications>(Lifestyle.Singleton);

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