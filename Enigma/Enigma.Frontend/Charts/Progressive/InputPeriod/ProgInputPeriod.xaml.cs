// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Constants;
using Enigma.Frontend.Helpers.Support;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Media;

namespace Enigma.Frontend.Ui.Charts.Progressive.InputPeriod;


public partial class ProgInputPeriod : Window
{
    private readonly ProgInputPeriodController _controller;
    
    public ProgInputPeriod()
    {
        InitializeComponent();
        _controller = App.ServiceProvider.GetRequiredService<ProgInputPeriodController>();
        PopulateTexts();
    }


    private void PopulateTexts()
    {
        this.Title = Rosetta.TextForId("charts.prog.period.title");
        FormTitle.Text = Rosetta.TextForId("charts.prog.period.formtitle");
        tbExplanation.Text = Rosetta.TextForId("charts.prog.period.explanation");
        tbStartDatePeriod.Text = Rosetta.TextForId("charts.prog.period.startdate");
        tbEndDatePeriod.Text = Rosetta.TextForId("charts.prog.period.enddate");
        btnHelp.Content = Rosetta.TextForId("common.btnhelp");
        btnCancel.Content = Rosetta.TextForId("common.btncancel");
        btnOk.Content = Rosetta.TextForId("common.btnok");
    }


    private void OkClick(object sender, RoutedEventArgs e)
    {
        TransferValues();
        bool calculationOk = _controller.ProcessInput();
        if (calculationOk)
        {
            Close();
        }
        else
        {
            HandleErrors();
        }
    }

    private void TransferValues()
    {
        _controller.InputStartDate = tbStartDatePeriodValue.Text;
        _controller.InputEndDate = tbEndDatePeriodValue.Text;
    }

    private void HandleErrors()
    {
        tbStartDatePeriodValue.Background = _controller.ActualErrorCodes.Contains(ErrorCodes.ERR_INVALID_STARTDATE) ? Brushes.Yellow : Brushes.White;
        tbEndDatePeriodValue.Background = _controller.ActualErrorCodes.Contains(ErrorCodes.ERR_INVALID_ENDDATE) ? Brushes.Yellow : Brushes.White;
    }

}

