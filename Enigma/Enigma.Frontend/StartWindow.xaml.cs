// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Microsoft.Extensions.DependencyInjection;
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
        Name.Text = "Enigma Research 0.1";
    }


    private void ShowSplashAndFinishView()
    {
        Show();
        Thread.Sleep(1500);

        MainWindow? mainWindow = App.ServiceProvider.GetService<MainWindow>();
        if (mainWindow != null)
        {
            mainWindow.Show();
            Close();
        }
        else
        {
            // todo log error and show warning for user
        }

    }


}

