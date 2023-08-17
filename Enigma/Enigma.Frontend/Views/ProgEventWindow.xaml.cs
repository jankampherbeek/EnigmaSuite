using System.Windows;

namespace Enigma.Frontend.Ui.Views;

public partial class ProgEventWindow : Window
{
    public ProgEventWindow()
    {
        InitializeComponent();
    }
    
    private void CloseClick(object sender, RoutedEventArgs e)
    {
        Close();
    }
    
}