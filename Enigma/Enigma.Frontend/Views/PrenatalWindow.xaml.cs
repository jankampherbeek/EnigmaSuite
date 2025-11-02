// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2025.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Windows;
using System.Windows.Media;
using Enigma.Domain.Constants;
using Enigma.Frontend.Ui.ViewModels;

namespace Enigma.Frontend.Ui.Views;

/// <summary>View for Prenatal</summary>
public partial class PrenatalWindow
{
    public PrenatalWindow()
    {
        InitializeComponent();
        DefineColors();
    }
    
    private void DefineColors()
    {
        Header.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString(ColorSettings.HEADER_COLOR)!;
        ChartNameText.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString(ColorSettings.HEADER_COLOR)!;
    }

    
    private void UseAspectsCheckBox_Checked(object sender, RoutedEventArgs e)
    {
        if (DataContext is PrenatalViewModel viewModel)
        {
            viewModel.UpdateAspects(true);
        }
    }
    
    private void UseAspectsCheckBox_UnChecked(object sender, RoutedEventArgs e)
    {
        if (DataContext is PrenatalViewModel viewModel)
        {
            viewModel.UpdateAspects(false);
        }
    }
    
    private void UseEclipsesCheckBox_Checked(object sender, RoutedEventArgs e)
    {
        if (DataContext is PrenatalViewModel viewModel)
        {
            viewModel.UpdateEclipses(true);
        }
    }
    
    private void UseEclipsesCheckBox_UnChecked(object sender, RoutedEventArgs e)
    {
        if (DataContext is PrenatalViewModel viewModel)
        {
            viewModel.UpdateEclipses(false);
        }
    }
    
    private void UseRetrogradeDirectCheckBox_Checked(object sender, RoutedEventArgs e)
    {
        if (DataContext is PrenatalViewModel viewModel)
        {
            viewModel.UpdateRetrogradeDirect(true);
        }
    }
    
    private void UseRetrogradeDirectCheckBox_UnChecked(object sender, RoutedEventArgs e)
    {
        if (DataContext is PrenatalViewModel viewModel)
        {
            viewModel.UpdateRetrogradeDirect(false);
        }
    }
    
    private void UseIngressesCheckBox_Checked(object sender, RoutedEventArgs e)
    {
        if (DataContext is PrenatalViewModel viewModel)
        {
            viewModel.UpdateIngresses(true);
        }
    }
    
    private void UseIngressesCheckBox_UnChecked(object sender, RoutedEventArgs e)
    {
        if (DataContext is PrenatalViewModel viewModel)
        {
            viewModel.UpdateIngresses(false);
        }
    }
    
}
