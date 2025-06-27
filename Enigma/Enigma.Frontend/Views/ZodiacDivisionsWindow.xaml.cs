// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Windows;
using Enigma.Frontend.Ui.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace Enigma.Frontend.Ui.Views;

/// <summary>
/// Interaction logic for ZodiacDivisionsWindow.xaml
/// </summary>
public partial class ZodiacDivisionsWindow : Window
{
    public ZodiacDivisionsWindow()
    {
        System.Diagnostics.Debug.WriteLine("ZodiacDivisionsWindow constructor called");
        InitializeComponent();
        
        // Get the ViewModel from the DI container and set as DataContext
        var viewModel = App.ServiceProvider.GetRequiredService<ZodiacDivisionsViewModel>();
        System.Diagnostics.Debug.WriteLine("ViewModel retrieved from DI container");
        DataContext = viewModel;
        
        // Trigger the calculation when the window loads
        Loaded += (sender, e) => 
        {
            System.Diagnostics.Debug.WriteLine("Window Loaded event fired, executing OkCommand");
            viewModel.OkCommand.Execute(null);
        };
    }
} 