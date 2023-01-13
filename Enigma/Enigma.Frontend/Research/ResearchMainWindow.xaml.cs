// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Frontend.Helpers.Support;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Windows;

namespace Enigma.Frontend.Ui.Research;

/// <summary>Window for overview of projects and the possibility to open a project.</summary>
public partial class ResearchMainWindow : Window
{
    private readonly ResearchMainController _controller;
    private readonly Rosetta _rosetta = Rosetta.Instance;


    public ResearchMainWindow()
    {
        InitializeComponent();
        _controller = App.ServiceProvider.GetRequiredService<ResearchMainController>();
        PopulateTexts();
        PopulateMenu();
        PopulateData();
    }


    public void PopulateTexts()
    {
        Title = _rosetta.TextForId("researchmainwindow.title");
        tbExplText.Text = _rosetta.TextForId("researchmainwindow.expltext");
        tbFormTitle.Text = _rosetta.TextForId("researchmainwindow.formtitle");
        btnClose.Content = _rosetta.TextForId("common.btnclose");
        btnHelp.Content = _rosetta.TextForId("common.btnhelp");
        btnOpen.Content = _rosetta.TextForId("common.btnopen");
        btnNew.Content = _rosetta.TextForId("common.btnnew");
    }

    public void PopulateMenu()
    {
        miGeneral.Header = _rosetta.TextForId("researchmainwindow.menu.migeneral");
        miGeneralClose.Header = _rosetta.TextForId("researchmainwindow.menu.migeneral.close");
        miGeneralConfiguration.Header = _rosetta.TextForId("researchmainwindow.menu.migeneral.configuration");
        miGeneralSettings.Header = _rosetta.TextForId("researchmainwindow.menu.migeneral.settings");
        miData.Header = _rosetta.TextForId("researchmainwindow.menu.midata");
        miDataOverview.Header = _rosetta.TextForId("researchmainwindow.menu.midata.overview");
        miDataImport.Header = _rosetta.TextForId("researchmainwindow.menu.midata.import");
        miProjects.Header = _rosetta.TextForId("researchmainwindow.menu.miprojects");
        miProjectsNew.Header = _rosetta.TextForId("researchmainwindow.menu.miprojects.new");
        miHelp.Header = _rosetta.TextForId("researchmainwindow.menu.mihelp");
        miHelpAbout.Header = _rosetta.TextForId("researchmainwindow.menu.mihelp.about");
        miHelpPage.Header = _rosetta.TextForId("researchmainwindow.menu.mihelp.page");
        miHelpManual.Header = _rosetta.TextForId("researchmainwindow.menu.mihelp.manual");
    }


    public void PopulateData()
    {
        List<ProjectItem> projects = _controller.GetAllProjectItems();
        lbProjects.ItemsSource = projects;
    }

    private void GeneralSettingsClick(object sender, RoutedEventArgs e)
    {
        _controller.ShowAppSettings();
    }

    private void GeneralConfigurationClick(object sender, RoutedEventArgs e)
    {
        _controller.ShowAstroConfig();
    }

    private void GeneralCloseClick(object sender, RoutedEventArgs e)
    {
        Close();
    }

    private void ProjectsNewClick(object sender, RoutedEventArgs e)
    {
        _controller.NewProject();
    }

    private void DataOverviewClick(object sender, RoutedEventArgs e)
    {
        _controller.ShowDataOverview();
    }

    private void DataImportClick(object sender, RoutedEventArgs e)
    {
        _controller.ShowDataImport();
    }




    private void CloseClick(object sender, RoutedEventArgs e)
    {
        _controller.HandleClose();
        Close();
    }

    private void HelpClick(object sender, RoutedEventArgs e)
    {
        _controller.ShowHelp();
    }

    private void HelpManualClick(object sender, RoutedEventArgs e)
    {
        MessageBox.Show("Help - Manual not yet implemented.");
    }

    private void HelpAboutClick(object sender, RoutedEventArgs e)
    {
        _controller.ShowAbout();
    }

    private void OpenClick(object sender, RoutedEventArgs e)
    {
        if (lbProjects.SelectedItem is ProjectItem selectedProject)
        {
            _controller.OpenProject(selectedProject);
        }
    }

    private void NewClick(object sender, RoutedEventArgs e)
    {
        _controller.NewProject();
        PopulateData();
    }

}




