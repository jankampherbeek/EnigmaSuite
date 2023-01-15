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
    private bool _allMundanePointsSelected = false;
    private bool _completed = false;
    private int _minimalNrOfPoints = 1;
    private ResearchMethods _researchMethod = ResearchMethods.None;
    public List<SelectableCelPointDetails> SelectedCelPoints { get; } = new();
    public List<SelectableMundanePointDetails> SelectedMundanePoints { get; } = new();
    public bool SelectedUseCusps { get; set; } = false;

    public PointSelectWindow(PointSelectController controller)
    {
        InitializeComponent();
        _controller = controller;
        PopulateTexts();
        PopulateData();
    }

    public void SetMinimalNrOfPoints(int minNumber)
    {
        _minimalNrOfPoints = minNumber;
    }

    public void SetResearchMethod(ResearchMethods researchMethod)
    {
        _researchMethod = researchMethod;
        if (_researchMethod == ResearchMethods.CountPosInHouses)
        {
            lbMundanePoints.IsEnabled = false;
            cBoxAllMundanePoints.IsEnabled = false;
            cBoxIncludeAllCusps.IsEnabled = false;
        }
    }


    public bool IsCompleted()
    {
        return _completed;
    }

    private void PopulateTexts()
    {
        Title = Rosetta.TextForId("pointselectwindow.title");
        tbFormTitle.Text = Rosetta.TextForId("pointselectwindow.formtitle");
        tbCelPoints.Text = Rosetta.TextForId("pointselectwindow.celpoints");
        tbMundanePoints.Text = Rosetta.TextForId("pointselectwindow.mundanepoints");
        tbExplanation.Text = Rosetta.TextForId("pointselectwindow.explanation");
        cBoxAllCelPoints.Content = Rosetta.TextForId("pointselectwindow.checkallcelpoints");
        cBoxAllMundanePoints.Content = Rosetta.TextForId("pointselectwindow.checkallmundanepoints");
        cBoxIncludeAllCusps.Content = Rosetta.TextForId("pointselectwindow.allcusps");

        btnHelp.Content = Rosetta.TextForId("common.btnhelp");
        btnCancel.Content = Rosetta.TextForId("common.btncancel");
        btnOk.Content = Rosetta.TextForId("common.btnok");
    }

    private void PopulateData()
    {
        List<SelectableCelPointDetails> celPointDetails = _controller.GetAllCelPointDetails();
        lbCelPoints.ItemsSource = celPointDetails;
        List<SelectableMundanePointDetails> mundanePointDetails = _controller.GetAllMundanePointDetails();
        lbMundanePoints.ItemsSource = mundanePointDetails;



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

    private void AllMundanePointsClick(object sender, RoutedEventArgs e)
    {
        if (_allMundanePointsSelected)
        {
            lbMundanePoints.UnselectAll();
            _allMundanePointsSelected = false;
        }
        else
        {
            lbMundanePoints.SelectAll();
            _allMundanePointsSelected = true;
        }
    }

    private void OkClick(object sender, RoutedEventArgs e)
    {
        int countOfPoints = 0;
        SelectedCelPoints.Clear();
        var selectedCPs = lbCelPoints.SelectedItems;
        foreach (var selectedCP in selectedCPs)
        {
            SelectedCelPoints.Add((SelectableCelPointDetails)selectedCP);
            countOfPoints++;
        }

        SelectedMundanePoints.Clear();
        var selectedMPs = lbMundanePoints.SelectedItems;
        foreach (var selectedMP in selectedMPs)
        {
            SelectedMundanePoints.Add((SelectableMundanePointDetails)selectedMP);
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
        _controller.ShowHelp();
    }
}
