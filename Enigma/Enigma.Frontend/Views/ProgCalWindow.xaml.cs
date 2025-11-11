// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2025.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Windows;
using Enigma.Frontend.Ui.ViewModels;

namespace Enigma.Frontend.Ui.Views;

/// <summary>View for Progressive Calendar</summary>
public partial class ProgCalWindow
{
    public ProgCalWindow()
    {
        InitializeComponent();
    }
    
    private void UseAspectsCheckBox_Checked(object sender, RoutedEventArgs e)
    {
        if (DataContext is ProgCalViewModel viewModel)
        {
            viewModel.UpdateAspects(true);
        }
    }
    
    private void UseAspectsCheckBox_UnChecked(object sender, RoutedEventArgs e)
    {
        if (DataContext is ProgCalViewModel viewModel)
        {
            viewModel.UpdateAspects(false);
        }
    }


    private void UseParallelsCheckBox_Checked(object sender, RoutedEventArgs e)
    {
        if (DataContext is ProgCalViewModel viewModel)
        {
            viewModel.UpdateParallels(true);
        }
    }
    
    private void UseParallelsCheckBox_UnChecked(object sender, RoutedEventArgs e)
    {
        if (DataContext is ProgCalViewModel viewModel)
        {
            viewModel.UpdateParallels(false);
        }
    }
    
    private void UseDeclEventsCheckBox_Checked(object sender, RoutedEventArgs e)
    {
        if (DataContext is ProgCalViewModel viewModel)
        {
            viewModel.UpdateDeclEvents(true);
        }
    }
    
    private void UseDeclEventsCheckBox_UnChecked(object sender, RoutedEventArgs e)
    {
        if (DataContext is ProgCalViewModel viewModel)
        {
            viewModel.UpdateDeclEvents(false);
        }
    }
    
    private void UseRetroDirectCheckBox_Checked(object sender, RoutedEventArgs e)
    {
        if (DataContext is ProgCalViewModel viewModel)
        {
            viewModel.UpdateRetroDirect(true);
        }
    }
    
    private void UseRetroDirectCheckBox_UnChecked(object sender, RoutedEventArgs e)
    {
        if (DataContext is ProgCalViewModel viewModel)
        {
            viewModel.UpdateRetroDirect(false);
        }
    }
}
