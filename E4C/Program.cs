using System;
using System.Windows;
using SimpleInjector;
using E4C.be.astron;
using E4C.be.sefacade;
using E4C.views;


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

        private static Container Bootstrap()
        {
            // Create the container.
            var container = new Container();

            // Register types:
            container.Register<ICalendarCalc, CalendarCalc>(Lifestyle.Singleton);
            container.Register<IObliquityNutationCalc, ObliquityNutationCalc>(Lifestyle.Singleton);
            container.Register<ISePosCelPointFacade, SePosCelPointFacade>(Lifestyle.Singleton);
            container.Register<ISeDateTimeFacade, SeDateTimeFacade> (Lifestyle.Singleton);

            // Register windows and view models:
            container.Register<MainWindow>();
            container.Register<MainWindowViewModel>();
            container.Register<DashboardCalc>();
            container.Register<DashboardCalcViewModel>();
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
            catch (Exception ex)
            {
                //Log the exception and exit
            }
        }
    }
}