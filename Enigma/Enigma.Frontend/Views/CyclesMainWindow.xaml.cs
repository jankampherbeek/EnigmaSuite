using System.Windows;

namespace Enigma.Frontend.Ui.ViewModels;

public partial class CyclesMainWindow : Window
{
    public CyclesMainWindow()
    {
        InitializeComponent();
    }
    
    private void CloseClick(object sender, RoutedEventArgs e)
    {
        Close();
    }
}