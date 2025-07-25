// Enigma Astrology Research.
// Copyright (c) 2025 Jan Kampherbeek.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Windows;
using System.Windows.Media;
using Enigma.Domain.Constants;
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
        InitializeComponent();
        DefineColors();
        // Get the ViewModel from the DI container and set as DataContext
        var viewModel = App.ServiceProvider.GetRequiredService<ZodiacDivisionsViewModel>();
        DataContext = viewModel;
        
        // Trigger the calculation when the window loads
        Loaded += (sender, e) => 
        {
            viewModel.CalcCommand.Execute(null);
        };
    }

    private void DefineColors()
    {
        Header.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString(ColorSettings.HEADER_COLOR)!;
        
    }

    
    
} 