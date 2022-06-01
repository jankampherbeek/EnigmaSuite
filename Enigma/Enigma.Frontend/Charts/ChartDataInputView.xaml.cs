// Jan Kampherbeek, (c) 2022.
// The Enigma Suite is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Frontend.Support;
using System.Windows;


namespace Enigma.Frontend.Charts;

/// <summary>
/// Interaction logic for ChartDataInputView.xaml
/// </summary>
public partial class ChartDataInputView : Window
{
    private readonly string EMPTY_STRING = "";
    private IRosetta _rosetta;
    private HelpWindow _helpWindow;
    public ChartDataInputView(IRosetta rosetta, HelpWindow helpWindow)
    {
        InitializeComponent();
        _rosetta = rosetta;
        _helpWindow = helpWindow;
        PopulateTexts();
    }

    public void PopulateTexts()
    {
        FormTitle.Text = _rosetta.TextForId("charts.datainput.formtitle");
        GeneralTxt.Text = _rosetta.TextForId("charts.datainput.general");
        NameIdTxt.Text = _rosetta.TextForId("charts.datainput.nameid");
        SubjectTxt.Text = _rosetta.TextForId("charts.datainput.subject");
        RatingTxt.Text = _rosetta.TextForId("charts.datainput.rating");
        SourceTxt.Text = _rosetta.TextForId("charts.datainput.source");
        DescriptionTxt.Text = _rosetta.TextForId("charts.datainput.description");
        LocationTxt.Text = _rosetta.TextForId("common.location");
        LocationNameTxt.Text = _rosetta.TextForId("common.location.name");
        LongitudeTxt.Text = _rosetta.TextForId("common.location.longitude");
        LatitudeTxt.Text = _rosetta.TextForId("common.location.latitude");
        DateTimeTxt.Text = _rosetta.TextForId("charts.datainput.datetime");
        DateTxt.Text = _rosetta.TextForId("common.date");
        CalTxt.Text = _rosetta.TextForId("common.calendar");
        YearCountTxt.Text = _rosetta.TextForId("common.yearcount");
        TimeTxt.Text = _rosetta.TextForId("common.time");
        DstTxt.Text = _rosetta.TextForId("common.time.dst");
        TimeZoneTxt.Text = _rosetta.TextForId("common.timezone");
        LmtTxt.Text = _rosetta.TextForId("common.time.lmt");
        BtnCalculate.Content = _rosetta.TextForId("common.btncalc");
        BtnClose.Content = _rosetta.TextForId("common.btnclose");
        BtnHelp.Content = _rosetta.TextForId("common.btnhelp");
    }

    private void CalculateClick(object sender, RoutedEventArgs e)
    {
     
    }

    private void CloseClick(object sender, RoutedEventArgs e)
    {
        Close();
    }

    private void HelpClick(object sender, RoutedEventArgs e)
    {
    //    _helpWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
    //    _helpWindow.SetUri("CalcJd");
    //    _helpWindow.ShowDialog();
    }

}




