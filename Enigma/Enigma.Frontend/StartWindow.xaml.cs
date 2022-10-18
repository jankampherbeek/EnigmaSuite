// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Domain.Configuration;
using Enigma.Configuration.Handlers;
using Enigma.Domain.Constants;
using Enigma.Domain.Exceptions;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System.Threading;
using System.Windows;


namespace Enigma.Frontend;


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
        Name.Text = "Enigma Astrology Research 0.1";   // TODO use resource bundle
    }


    private void ShowSplashAndFinishView()
    {
        Show();
        HandleCheckForConfig();
        HandleCheckDirForSettings();
        Thread.Sleep(500);

        // TODO remove sleep
        // TODO check for update
        Hide();
        MainWindow mainWindow = new();
        mainWindow.ShowDialog();
        Application.Current.Shutdown(0);


    }



    private void HandleCheckForConfig()
    {
        bool result = true;
        if (!File.Exists(EnigmaConstants.CONFIG_LOCATION))
        {
            IConfigWriter configWriter = App.ServiceProvider.GetRequiredService<IConfigWriter>();
            result = configWriter.WriteDefaultConfig();
        }
        if (!result)
        {
            throw new StartupException("Could not start Enigma Research. There is a problem with the configuration file. Please check if you have write access to the disk.");
        }

    }

    private void HandleCheckDirForSettings()
    {
        ApplicationSettings? settings = ApplicationSettings.Instance;
        if (!Directory.Exists(settings.LocationEnigmaRoot)) Directory.CreateDirectory(settings.LocationEnigmaRoot);
        if (!Directory.Exists(settings.LocationExportFiles)) Directory.CreateDirectory(settings.LocationExportFiles);
        if (!Directory.Exists(settings.LocationProjectFiles)) Directory.CreateDirectory(settings.LocationProjectFiles);
        if (!Directory.Exists(settings.LocationDataFiles)) Directory.CreateDirectory(settings.LocationDataFiles);
        if (!Directory.Exists(settings.LocationSeFiles)) Directory.CreateDirectory(settings.LocationSeFiles);
    }

}

