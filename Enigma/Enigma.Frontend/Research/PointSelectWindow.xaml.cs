// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
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
    private PointSelectController _controller;
    private Rosetta _rosetta = Rosetta.Instance;
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
        Title = _rosetta.TextForId("pointselectwindow.title");
        tbFormTitle.Text = _rosetta.TextForId("pointselectwindow.formtitle");
        tbCelPoints.Text = _rosetta.TextForId("pointselectwindow.celpoints");
        tbMundanePoints.Text = _rosetta.TextForId("pointselectwindow.mundanepoints");
        tbExplanation.Text = _rosetta.TextForId("pointselectwindow.explanation");
        cBoxAllCelPoints.Content = _rosetta.TextForId("pointselectwindow.checkallcelpoints");
        cBoxAllMundanePoints.Content = _rosetta.TextForId("pointselectwindow.checkallmundanepoints");
        cBoxIncludeAllCusps.Content = _rosetta.TextForId("pointselectwindow.allcusps");

        btnHelp.Content = _rosetta.TextForId("common.btnhelp");
        btnCancel.Content = _rosetta.TextForId("common.btncancel");
        btnOk.Content = _rosetta.TextForId("common.btnok");
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
        } else
        {
            string warning = _rosetta.TextForId("pointselectwindow.warningnrofpoints") + " " + _minimalNrOfPoints.ToString();
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
