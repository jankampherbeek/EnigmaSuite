// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Frontend.Helpers.Support;
using System.Windows;

namespace Enigma.Frontend.Ui.Charts.Progressive;

/// <summary>Interaction logic for input screen for symbloc directions.</summary>
public partial class ChartProgSymInput : Window
{
    public ChartProgSymInput()
    {
        InitializeComponent();
    }

    public void Populate()
    {
        PopulateTexts();
    }


    private void PopulateTexts()
    {
        this.Title = Rosetta.TextForId("charts.prog.syminput.title");
        FormTitle.Text = Rosetta.TextForId("charts.prog.syminput.formtitle");
        tbMoment.Text = Rosetta.TextForId("charts.prog.syminput.moment");
        tbOrbAspects.Text = Rosetta.TextForId("charts.prog.syminput.orbaspects");
        tbRate.Text = Rosetta.TextForId("charts.prog.syminput.rate");
        BtnCalculate.Content = Rosetta.TextForId("common.btncalc");
        BtnCancel.Content = Rosetta.TextForId("common.btncancel");
        BtnHelp.Content = Rosetta.TextForId("common.btnhelp");
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
