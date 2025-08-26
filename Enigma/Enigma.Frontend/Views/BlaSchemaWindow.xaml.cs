// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2025.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Windows;
using System.Windows.Controls;
using Enigma.Frontend.Ui.ViewModels;

namespace Enigma.Frontend.Ui.Views;

/// <summary>View for BLA Schema</summary>
public partial class BlaSchemaWindow
{
    public BlaSchemaWindow()
    {
        InitializeComponent();
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        // Trigger initial population when window loads
        if (DataContext is BlaSchemaViewModel viewModel)
        {
            viewModel.Populate();
        }
    }

    private void BlackMoonCorrectionComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        // Trigger population when Black Moon correction selection changes
        if (DataContext is BlaSchemaViewModel viewModel)
        {
            viewModel.Populate();
        }
    }

    private void HouseSystemComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        // Trigger population when House System selection changes
        if (DataContext is BlaSchemaViewModel viewModel)
        {
            viewModel.Populate();
        }
    }

    private void IncludeChironCheckBox_Changed(object sender, RoutedEventArgs e)
    {
        // Trigger population when Include Chiron checkbox changes
        if (DataContext is BlaSchemaViewModel viewModel)
        {
            viewModel.Populate();
        }
    }

    private void IncludeErisCheckBox_Changed(object sender, RoutedEventArgs e)
    {
        // Trigger population when Include Eris checkbox changes
        if (DataContext is BlaSchemaViewModel viewModel)
        {
            viewModel.Populate();
        }
    }
}
