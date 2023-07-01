// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Frontend.Helpers.Support;
using Enigma.Frontend.Ui.Support;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Windows;

namespace Enigma.Frontend.Ui.Charts.Progressive;

/// <summary>Interaction logic for input screen for secundary progressions.</summary>
public partial class ChartProgSecInput : Window
{
    private readonly ChartProgSecInputController _controller;

    public ChartProgSecInput()
    {
        InitializeComponent();
        _controller = App.ServiceProvider.GetRequiredService<ChartProgSecInputController>();
    }

    public void Populate()
    {
        PopulateTexts();
        PopulateData();
    }

    private void PopulateTexts()
    {
        this.Title = Rosetta.TextForId("charts.prog.secinput.title");
        FormTitle.Text = Rosetta.TextForId("charts.prog.secinput.formtitle");
        tbMoment.Text = Rosetta.TextForId("charts.prog.secinput.moment");
        tbOrbAspects.Text = Rosetta.TextForId("charts.prog.secinput.orbaspects");
        tbSecPoints.Text = Rosetta.TextForId("charts.prog.secinput.secpoints");
        tbAspects.Text = Rosetta.TextForId("charts.prog.secinput.aspects");
        BtnCalculate.Content = Rosetta.TextForId("common.btncalc");
        BtnCancel.Content = Rosetta.TextForId("common.btncancel");
        BtnHelp.Content = Rosetta.TextForId("common.btnhelp");
    }

    public void PopulateData()
    {
        List<SelectableChartPointDetails> celPointDetails = _controller.GetChartPointDetails();
        lbSecPoints.ItemsSource = celPointDetails;
        List<SelectableAspectDetails> aspectDetails = _controller.GetAspectDetails();
        lbAspects.ItemsSource = aspectDetails;
    
    }

    private void CancelClick(object sender, RoutedEventArgs e)
    {
        Close();
    }

    private void HelpClick(object sender, RoutedEventArgs e)
    {
        //  ...Controller.ShowHelp();
    }

    private void CalculateClick(object sender, RoutedEventArgs e)
    {

    }
}
