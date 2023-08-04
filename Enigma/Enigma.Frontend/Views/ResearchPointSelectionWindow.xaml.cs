using System.Windows;
using Enigma.Frontend.Ui.Support;

namespace Enigma.Frontend.Ui.Views;

public partial class ResearchPointSelectionWindow : Window
{
    public ResearchPointSelectionWindow()
    {
        InitializeComponent();
    }
    
            
    private void CloseClick(object sender, RoutedEventArgs e)
    {
        Close();
    }

}