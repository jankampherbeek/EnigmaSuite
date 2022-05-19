using Enigma.Frontend.Calculators.JulDay;
using Enigma.Frontend.Calculators.Obliquity;
using Enigma.Frontend.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Enigma.Frontend.Calculators;

/// <summary>Start screen for the use of calculations.</summary>
public partial class CalcStartWindow : Window
{
    private HelpWindow _helpWindow;
    private JulDayView _julDayView;
    private ObliquityView _obliquityView;
    private IRosetta _rosetta;

    public CalcStartWindow(IRosetta rosetta, HelpWindow helpWindow, JulDayView julDayView, ObliquityView obliquity)
    {
        InitializeComponent();

        _rosetta = rosetta;
        _helpWindow = helpWindow;
        _julDayView = julDayView;
        _obliquityView = obliquity;
        PopulateTexts();
    }


    private void PopulateTexts()
    {
        Title = _rosetta.TextForId("calc.startview.title");
        FormTitle.Text = _rosetta.TextForId("calc.startview.formtitle");
        JulianDayTitle.Text = _rosetta.TextForId("calc.startview.jdtitle");
        ObliquityTitle.Text = _rosetta.TextForId("calc.startview.obliquitytitle");
        textJulianDay.Text = _rosetta.TextForId("calc.startview.jdexplanation");
        textObliquity.Text = _rosetta.TextForId("calc.startview.obliquityexplanation");
        btnJulianDay.Content = _rosetta.TextForId("calc.startview.btnjd");
        btnObliquity.Content = _rosetta.TextForId("calc.startview.btnobliquity");
        btnClose.Content = _rosetta.TextForId("common.btnclose");
        btnHelp.Content = _rosetta.TextForId("common.btnhelp");
    }

    private void btnJulDay_Click(object sender, RoutedEventArgs e)
    {
          
        _julDayView.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        _julDayView.ShowDialog();

    }

    private void btnObliquity_Click(object sender, RoutedEventArgs e)
    {
        _obliquityView.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        _obliquityView.ShowDialog();
    }

    private void btnClose_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }

    private void btnHelp_Click(object sender, RoutedEventArgs e)
    {
        _helpWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        _helpWindow.SetUri("CalcStart");
        _helpWindow.ShowDialog();
    }
}



