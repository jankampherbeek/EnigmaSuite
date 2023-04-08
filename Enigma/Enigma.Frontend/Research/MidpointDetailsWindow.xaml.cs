// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Configuration;
using Enigma.Frontend.Helpers.Support;
using Enigma.Frontend.Ui.State;
using System.Windows;

namespace Enigma.Frontend.Ui.Research;

/// <summary>Code-behind for MidPointDetails.</summary>
public partial class MidpointDetailsWindow : Window
{
    private readonly AstroConfig _astroConfig;
    private bool _completed = false;
    public int DialDivision { get; set; } = 1;
    public double Orb { get; set; }

    public MidpointDetailsWindow()
    {
        InitializeComponent();
        _astroConfig = CurrentConfig.Instance.GetConfig();
        PopulateTexts();
        PopulateData();
    }

    public bool IsCompleted()
    {
        return _completed;
    }


    private void PopulateTexts()
    {
        Title = Rosetta.TextForId("midpointdetailswindow.title");
        tbFormTitle.Text = Rosetta.TextForId("midpointdetailswindow.formtitle");
        tbExplanation.Text = Rosetta.TextForId("midpointdetailswindow.explanation");
        tbDialSize.Text = Rosetta.TextForId("midpointdetailswindow.dialsize");
        tbOrb.Text = Rosetta.TextForId("midpointdetailswindow.orb");
        tbOrbDegrees.Text = Rosetta.TextForId("midpointdetailswindow.orbdegrees");
        tbOrbMinutes.Text = Rosetta.TextForId("midpointdetailswindow.orbminutes");
        btnHelp.Content = Rosetta.TextForId("common.btnhelp");
        btnCancel.Content = Rosetta.TextForId("common.btncancel");
        btnOk.Content = Rosetta.TextForId("common.btnok");

    }

    private void PopulateData()
    {
        comboDialSize.Items.Clear();
        comboDialSize.Items.Add(Rosetta.TextForId("midpointdetailswindow.dialsize360"));
        comboDialSize.Items.Add(Rosetta.TextForId("midpointdetailswindow.dialsize90"));
        comboDialSize.Items.Add(Rosetta.TextForId("midpointdetailswindow.dialsize45"));
        comboDialSize.SelectedIndex = 0;
        double orb = _astroConfig.BaseOrbMidpoints;
        string degreeText = ((int)orb).ToString();
        string minuteText = ((int)((orb - ((int)orb)) * 60.0)).ToString();
        tboxOrbDegrees.Text = degreeText;
        tboxOrbMinutes.Text = minuteText;
    }

    private void CancelClick(object sender, RoutedEventArgs e)
    {
        _completed = false;
        Close();
    }

    private void HelpClick(object sender, RoutedEventArgs e)
    {
        MidpointDetailsController.ShowHelp();
    }

    private void OkClick(object sender, RoutedEventArgs e)
    {
        if (comboDialSize.SelectedIndex == 1) DialDivision = 4;
        if (comboDialSize.SelectedIndex == 2) DialDivision = 8;
        string degreeTxt = tboxOrbDegrees.Text;
        string minuteTxt = tboxOrbMinutes.Text;
        bool degreeCorrect = int.TryParse(degreeTxt, out int degreeValue);
        bool minuteCorrect = int.TryParse(minuteTxt, out int minuteValue);
        if (degreeCorrect && minuteCorrect && degreeValue >= 0 && degreeValue < 10 && minuteValue >= 0 && minuteValue < 60)
        {
            Orb = degreeValue + minuteValue / 60.0;
            _completed = true;
            Close();
        }
        else
        {
            string warning = Rosetta.TextForId("midpointdetailswindow.warningorb");
            MessageBox.Show(warning);
        }
    }
}

