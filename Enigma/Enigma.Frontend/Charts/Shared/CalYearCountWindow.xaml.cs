// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Windows;
using Microsoft.Extensions.DependencyInjection;

namespace Enigma.Frontend.Ui.Charts.Shared;

public partial class CalYearCountWindow : Window
{
    private readonly CalYearCountController _controller;
    
    public readonly CalYearCountVo BindingVo = new();
    public CalYearCountWindow()
    {
        InitializeComponent();
        _controller = App.ServiceProvider.GetRequiredService<CalYearCountController>();
        this.DataContext = BindingVo;
    }
}

public class CalYearCountVo
{
    public string FormTitle { get; set; } = "Calendar and yearcount"; 
}