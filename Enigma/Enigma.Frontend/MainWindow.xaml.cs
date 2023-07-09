// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Constants;
using Enigma.Frontend.Helpers.Support;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;


namespace Enigma.Frontend.Ui;

/// <summary>Main view for the application.</summary>
public partial class MainWindow : Window
{
    private readonly MainController _controller;

    public MainWindow()
    {
        InitializeComponent();
        _controller = App.ServiceProvider.GetRequiredService<MainController>();
        PopulateTexts();
    }

    private void ChartsClick(object sender, RoutedEventArgs e)
    {
        _controller.ShowChartsMain();
    }

    private void ResearchClick(object sender, RoutedEventArgs e)
    {
        _controller.ShowResearchMain();
    }

    private void HelpClick(object sender, RoutedEventArgs e)
    {
        MainController.HelpClick();
    }



    private void ExitClick(object sender, RoutedEventArgs e)
    {
        Close();
    }

    private void PopulateTexts()
    {
        Title = Rosetta.TextForId("mainwindow.title") + " " + EnigmaConstants.EnigmaVersion;
        tbFormTitle.Text = Rosetta.TextForId("mainwindow.formtitle");
        tbSubTitle.Text = Rosetta.TextForId("mainwindow.subtitle");
        tbExplanation.Text = Rosetta.TextForId("mainwindow.explanation");
  
        btnExit.Content = Rosetta.TextForId("common.btnexit");
        btnHelp.Content = Rosetta.TextForId("common.btnhelp");
    }

}
