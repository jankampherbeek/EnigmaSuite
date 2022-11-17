// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Frontend.Ui.Interfaces;
using System.Windows;


namespace Enigma.Frontend.Ui.Charts; 


/// <summary>
/// Interaction logic for ChartMidpointsWindow.xaml
/// </summary>
public partial class ChartMidpointsWindow : Window
{
    private ChartMidpointsController _controller;
    private IRosetta _rosetta;


    public ChartMidpointsWindow(ChartMidpointsController controller, IRosetta rosetta)
    {
        InitializeComponent();
        _controller = controller;
        _rosetta = rosetta; 
    }




    private void CloseClick(object sender, RoutedEventArgs e)
    {
        Hide();
    }

    private void HelpClick(object sender, RoutedEventArgs e)
    {
        //  ChartAspectsController.ShowHelp();
    }


}
