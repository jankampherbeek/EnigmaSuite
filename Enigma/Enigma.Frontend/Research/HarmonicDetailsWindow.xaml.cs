// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Frontend.Helpers.Support;
using System.Windows;


namespace Enigma.Frontend.Ui.Research;

/// <summary>Code-behind for HarmonicDetails.</summary>
public partial class HarmonicDetailsWindow : Window
{
    private bool _completed = false;
    public double HarmonicNumber { get; set; }
    public double Orb { get; set; }

    public HarmonicDetailsWindow()
    {
        InitializeComponent();
        PopulateTexts();
        PopulateData();
    }

    public bool IsCompleted()
    {
        return _completed;
    }


    private void PopulateTexts()
    {
        Title = Rosetta.TextForId("harmonicdetailswindow.title");
        tbFormTitle.Text = Rosetta.TextForId("harmonicdetailswindow.formtitle");
        tbExplanation.Text = Rosetta.TextForId("harmonicdetailswindow.explanation");
        tbHarmonicNumber.Text = Rosetta.TextForId("harmonicdetailswindow.harmonicnr");
        tbOrb.Text = Rosetta.TextForId("harmonicdetailswindow.orb");
        tbOrbDegrees.Text = Rosetta.TextForId("harmonicdetailswindow.orbdegrees");
        tbOrbMinutes.Text = Rosetta.TextForId("harmonicdetailswindow.orbminutes");
        btnHelp.Content = Rosetta.TextForId("common.btnhelp");
        btnCancel.Content = Rosetta.TextForId("common.btncancel");
        btnOk.Content = Rosetta.TextForId("common.btnok");

    }

    private void PopulateData()
    {
        double orb = 1.0;       // TODO  0.3 use Orb from configuration
        string degreeText = ((int)orb).ToString();
        string minuteText = ((int)((orb - ((int)orb)) * 60.0)).ToString();
        tboxOrbDegrees.Text = degreeText;
        tboxOrbMinutes.Text = minuteText;
        tboxHarmonicNumber.Text = "";
    }

    private void CancelClick(object sender, RoutedEventArgs e)
    {
        _completed = false;
        Close();
    }

    private void HelpClick(object sender, RoutedEventArgs e)
    {
        HarmonicDetailsController.ShowHelp();
    }

    private void OkClick(object sender, RoutedEventArgs e)
    {
        string harmonicNumberText = tboxHarmonicNumber.Text;
        bool harmNrCorrect = double.TryParse(harmonicNumberText, out double harmNrValue);


        string degreeTxt = tboxOrbDegrees.Text;
        string minuteTxt = tboxOrbMinutes.Text;
        bool degreeCorrect = int.TryParse(degreeTxt, out int degreeValue);
        bool minuteCorrect = int.TryParse(minuteTxt, out int minuteValue);
        if (harmNrCorrect && harmNrValue > 1.0)
        {
            if (degreeCorrect && minuteCorrect && degreeValue >= 0 && degreeValue < 10 && minuteValue >= 0 && minuteValue < 60)
            {
                HarmonicNumber = harmNrValue;
                Orb = degreeValue + minuteValue / 60.0;
                _completed = true;
                Close();
            }
            else
            {
                string warning = Rosetta.TextForId("harmonicdetailswindow.warningorb");
                MessageBox.Show(warning);
            }
        }
        else
        {
            string warning = Rosetta.TextForId("harmonicdetailswindow.warningharmonicnumber");
            MessageBox.Show(warning);
        }

    }
}
