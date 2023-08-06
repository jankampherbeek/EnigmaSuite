using System.Windows;

namespace Enigma.Frontend.Ui.Views;

public partial class ResearchHarmonicDetailsWindow
{
    public ResearchHarmonicDetailsWindow()
    {
        InitializeComponent();
    }
    
    private void CloseClick(object sender, RoutedEventArgs e)
    {
        Close();
    }
}