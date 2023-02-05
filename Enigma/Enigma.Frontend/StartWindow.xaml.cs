// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Api.Interfaces;
using Enigma.Domain.Configuration;
using Enigma.Domain.Constants;
using Enigma.Domain.Exceptions;
using Enigma.Frontend.Helpers.Support;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System.IO;
using System.Windows;


namespace Enigma.Frontend.Ui;


/// <summary>Splash window, starts the application.</summary>
/// <remarks>No separate controller for this view.</remarks>
public partial class StartWindow : Window
{

    public StartWindow()
    {
        InitializeComponent();
        this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        PopulateStaticTexts();
        ShowSplashAndFinishView();


    }

    private void PopulateStaticTexts()
    {
        tbExplanation.Text = Rosetta.TextForId("startwindow.checking"); 
    }


    private void ShowSplashAndFinishView()
    {
        Show();
        HandleCheckForConfig();
        HandleCheckDirForSettings();
        // Thread.Sleep(500);
        // TODO check for update
        Hide();
        MainWindow mainWindow = new();              // Todo 0.1 add check for exceptions and show warning to user if an exception occurs.
        mainWindow.ShowDialog();
        Application.Current.Shutdown(0);
    }



    private static void HandleCheckForConfig()
    {
        bool result = true;
        if (!File.Exists(EnigmaConstants.CONFIG_LOCATION))
        {
            IConfigurationApi configApi = App.ServiceProvider.GetRequiredService<IConfigurationApi>();
            AstroConfig config = configApi.GetDefaultConfiguration();
            result = configApi.WriteConfig(config);
        }
        if (!result)
        {
            string errorText = "Could not start Enigma Astrology Research. There is a problem with the configuration file. Please check if you have write access to the disk.";
            Log.Error("StartWindow.xaml.cs.HandleCheckForConfig(): " + errorText);
            throw new EnigmaException(errorText);
        }

    }

    private static void HandleCheckDirForSettings()        // todo move to backend
    {
        ApplicationSettings? settings = ApplicationSettings.Instance;
        if (!Directory.Exists(settings.LocationEnigmaRoot)) Directory.CreateDirectory(settings.LocationEnigmaRoot);
        if (!Directory.Exists(settings.LocationExportFiles)) Directory.CreateDirectory(settings.LocationExportFiles);
        if (!Directory.Exists(settings.LocationProjectFiles)) Directory.CreateDirectory(settings.LocationProjectFiles);
        if (!Directory.Exists(settings.LocationDataFiles)) Directory.CreateDirectory(settings.LocationDataFiles);
        if (!Directory.Exists(settings.LocationSeFiles)) Directory.CreateDirectory(settings.LocationSeFiles);
    }

}

