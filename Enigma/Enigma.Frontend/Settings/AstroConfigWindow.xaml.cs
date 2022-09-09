// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Windows;

namespace Enigma.Frontend.Settings;

/// <summary>
/// Interaction logic for AstroConfigWindow.xaml
/// </summary>
public partial class AstroConfigWindow : Window
{
    private AstroConfigController _controller;
    public AstroConfigWindow(AstroConfigController controller)
    {
        InitializeComponent(); 
        _controller = controller;
    }

    public void CancelClick(object sender, RoutedEventArgs e)
    {
        Close();
    }
    public void HelpClick(object sender, RoutedEventArgs e)
    {
      
    }

    public void OkClick(object sender, RoutedEventArgs e)
    {

    }

    public void AyanamshaChanged(object sender, RoutedEventArgs e)
    {

    }

    public void HouseSystemChanged(object sender, RoutedEventArgs e)
    {

    }

    public void ObserverPosChanged(object sender, RoutedEventArgs e)
    {

    }

    public void ZodiacTypeChanged(object sender, RoutedEventArgs e)
    {

    }

}
