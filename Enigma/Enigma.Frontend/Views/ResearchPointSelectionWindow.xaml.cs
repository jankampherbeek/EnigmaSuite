// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Windows;

namespace Enigma.Frontend.Ui.Views;

/// <summary>View for point selection</summary>
public partial class ResearchPointSelectionWindow
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