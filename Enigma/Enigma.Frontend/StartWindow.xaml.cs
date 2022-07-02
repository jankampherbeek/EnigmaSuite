// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.


using System.Windows;


namespace Enigma.Frontend;


/// <summary>Dashboard, start window for application, provides access to the four main functionalities of the Enigma Suite.</summary>
public partial class StartWindow : Window
{

    public StartWindow()
    {        
        InitializeComponent();
        this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        PopulateStaticTexts();
    }

    private void PopulateStaticTexts()
    {
        NameVersion.Text = "Enigma Charts 2.0";
    }


    private void SendInitializationMsg()
    {
        // TODO send message to StateMachine, the view is closed by the State Machine 
    }

    /*


    private void BtnCharts_Click(object sender, RoutedEventArgs e)
    {
        ChartsStartView? chartsStartView = App.ServiceProvider.GetService<ChartsStartView>();
        if (chartsStartView != null)
        {
            chartsStartView.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            chartsStartView.Show();
        }
    }


    }*/
}

