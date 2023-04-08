// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Research;
using Enigma.Frontend.Helpers.Support;
using System.Collections.Generic;
using System.Windows;
using static Enigma.Frontend.Ui.Research.PointSelectController;

namespace Enigma.Frontend.Ui.Research;

/// <summary>Code behind for dialog window that enables the selection of points to include in research.</summary>
public partial class PointSelectWindow : Window
{
    private readonly PointSelectController _controller;
    private bool _allCelPointsSelected = false;
    private bool _completed = false;
    private int _minimalNrOfPoints = 1;
    private ResearchMethods _researchMethod = ResearchMethods.None;
    public List<SelectableChartPointDetails> SelectedCelPoints { get; } = new();
    public bool SelectedUseCusps { get; set; } = false;

    public PointSelectWindow(PointSelectController controller)
    {
        InitializeComponent();
        _controller = controller;
        PopulateTexts();
    }

    public void SetMinimalNrOfPoints(int minNumber)
    {
        _minimalNrOfPoints = minNumber;
    }

    public void SetResearchMethod(ResearchMethods researchMethod)
    {
        _researchMethod = researchMethod;
    }


    public bool IsCompleted()
    {
        return _completed;
    }

    private void PopulateTexts()
    {
        Title = Rosetta.TextForId("pointselectwindow.title");
        tbFormTitle.Text = Rosetta.TextForId("pointselectwindow.formtitle");
        tbExplanation.Text = Rosetta.TextForId("pointselectwindow.explanation");
        cBoxAllCelPoints.Content = Rosetta.TextForId("pointselectwindow.checkallcelpoints");
        cBoxIncludeAllCusps.Content = Rosetta.TextForId("pointselectwindow.allcusps");
        btnHelp.Content = Rosetta.TextForId("common.btnhelp");
        btnCancel.Content = Rosetta.TextForId("common.btncancel");
        btnOk.Content = Rosetta.TextForId("common.btnok");
    }

    public void PopulateData()
    {
        List<SelectableChartPointDetails> celPointDetails = _controller.GetAllCelPointDetails(_researchMethod);
        lbCelPoints.ItemsSource = celPointDetails;
        cBoxAllCelPoints.IsChecked = false;
        cBoxIncludeAllCusps.IsChecked = false;
        cBoxIncludeAllCusps.IsEnabled = _controller.enableCusps;
    }

    private void AllCelPointsClick(object sender, RoutedEventArgs e)
    {
        if (_allCelPointsSelected)
        {
            lbCelPoints.UnselectAll();
            _allCelPointsSelected = false;
        }
        else
        {
            lbCelPoints.SelectAll();
            _allCelPointsSelected = true;
        }
    }

    private void OkClick(object sender, RoutedEventArgs e)
    {
        int countOfPoints = 0;
        SelectedCelPoints.Clear();
        var selectedCPs = lbCelPoints.SelectedItems;
        foreach (var selectedCP in selectedCPs)
        {
            SelectedCelPoints.Add((SelectableChartPointDetails)selectedCP);
            countOfPoints++;
        }

        SelectedUseCusps = cBoxIncludeAllCusps.IsChecked == true;
        if (countOfPoints >= _minimalNrOfPoints)
        {
            _completed = true;
            Close();
        }
        else
        {
            string warning = Rosetta.TextForId("pointselectwindow.warningnrofpoints") + " " + _minimalNrOfPoints.ToString();
            MessageBox.Show(warning);
        }
    }


    private void CancelClick(object sender, RoutedEventArgs e)
    {
        _completed = false;
        Close();
    }

    private void HelpClick(object sender, RoutedEventArgs e)
    {
        PointSelectController.ShowHelp();
    }
}
