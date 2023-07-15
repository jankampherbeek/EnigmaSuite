// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Frontend.Helpers.Support;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Enigma.Frontend.Ui.Charts;

/// <summary>Window for the results of calculated harmonics.</summary>
public partial class ChartHarmonicsWindow
{
    private readonly ChartHarmonicsController _controller;
    private readonly string _emptyHeader = "";

    public ChartHarmonicsWindow(ChartHarmonicsController controller)
    {
        InitializeComponent();
        _controller = controller;
    }

    public void Populate()
    {
        PopulateTexts();
        PopulateData(2);
    }

    private void CalcCLick(object sender, RoutedEventArgs e)
    {
        tboxInputHarmonicNr.Background = Brushes.White;
        string inputValue = tboxInputHarmonicNr.Text;
        if (HarmonicNumberIsValid(inputValue, out double harmonicNumber))
        {
            PopulateData(harmonicNumber);
        }
        else
        {
            tboxInputHarmonicNr.Background = Brushes.Yellow;
        }

    }

    private static bool HarmonicNumberIsValid(string inputNumber, out double parsedharmonicNumber)
    {
        bool result = double.TryParse(inputNumber, out double harmonicNumber);
        if (result)
        {
            result = 1.0 < harmonicNumber;
        }
        if (!result)
        {
            harmonicNumber = -1;
        }
        parsedharmonicNumber = harmonicNumber;
        return result;
    }

    private void CloseClick(object sender, RoutedEventArgs e)
    {
        Close();
    }

    private void HelpClick(object sender, RoutedEventArgs e)
    {
        ChartHarmonicsController.ShowHelp();
    }

    private void PopulateTexts()
    {
        Title = Rosetta.TextForId("charts.harmonics.title");
        tbFormTitle.Text = Rosetta.TextForId("charts.harmonics.formtitle");
        tbSubTitleChartId.Text = _controller.RetrieveChartName();
        tbSubTitleHarmonicNumber.Text = Rosetta.TextForId("charts.harmonics.titlenumber");
        tbInputHarmonicNr.Text = Rosetta.TextForId("charts.harmonics.inputharmonicnr");
        btnCalculate.Content = Rosetta.TextForId("common.btncalc");
        btnHelp.Content = Rosetta.TextForId("common.btnhelp");
        btnClose.Content = Rosetta.TextForId("common.btnclose");
    }

    private void PopulateData(double harmonicNumber)
    {
        tbDescriptionText.Text = _controller.DescriptiveText();
        tbSubTitleHarmonicNumber.Text = Rosetta.TextForId("charts.harmonics.titlenumber") + " " + harmonicNumber;
        dgHarmonics.ItemsSource = _controller.RetrieveAndFormatHarmonics(harmonicNumber);
        dgHarmonics.GridLinesVisibility = dgHarmonics.GridLinesVisibility = DataGridGridLinesVisibility.None;
        dgHarmonics.Columns[0].Header = _emptyHeader;
        dgHarmonics.Columns[1].Header = Rosetta.TextForId("charts.harmonics.radixpos");
        dgHarmonics.Columns[2].Header = _emptyHeader;
        dgHarmonics.Columns[3].Header = Rosetta.TextForId("charts.harmonics.harmonicpos");
        dgHarmonics.Columns[4].Header = _emptyHeader;
        dgHarmonics.Columns[0].MaxWidth = 20;
        dgHarmonics.Columns[2].MaxWidth = 20;
        dgHarmonics.Columns[4].MaxWidth = 20;
        dgHarmonics.Columns[0].CellStyle = FindResource("GlyphColumnStyle") as Style;
        dgHarmonics.Columns[2].CellStyle = FindResource("GlyphColumnStyle") as Style;
        dgHarmonics.Columns[4].CellStyle = FindResource("GlyphColumnStyle") as Style;
        dgHarmonics.HorizontalAlignment = HorizontalAlignment.Right;
    }

}
