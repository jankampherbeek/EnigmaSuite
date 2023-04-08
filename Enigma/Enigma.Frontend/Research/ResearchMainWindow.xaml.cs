// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Frontend.Helpers.Support;
using Enigma.Frontend.Ui.Support;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Windows;

namespace Enigma.Frontend.Ui.Research;

/// <summary>Window for overview of projects and the possibility to open a project.</summary>
public partial class ResearchMainWindow : Window
{
    private readonly ResearchMainController _controller;


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
        Title = Rosetta.TextForId("researchmainwindow.title");
        tbExplText.Text = Rosetta.TextForId("researchmainwindow.expltext");
        tbFormTitle.Text = Rosetta.TextForId("researchmainwindow.formtitle");
        btnClose.Content = Rosetta.TextForId("common.btnclose");
        btnHelp.Content = Rosetta.TextForId("common.btnhelp");
        btnOpen.Content = Rosetta.TextForId("common.btnopen");
        btnNew.Content = Rosetta.TextForId("common.btnnew");
    }

    public void PopulateMenu()
    {
        miGeneral.Header = Rosetta.TextForId("researchmainwindow.menu.migeneral");
        miGeneralClose.Header = Rosetta.TextForId("researchmainwindow.menu.migeneral.close");
        miGeneralConfiguration.Header = Rosetta.TextForId("researchmainwindow.menu.migeneral.configuration");
        miGeneralSettings.Header = Rosetta.TextForId("researchmainwindow.menu.migeneral.settings");
        miData.Header = Rosetta.TextForId("researchmainwindow.menu.midata");
        miDataOverview.Header = Rosetta.TextForId("researchmainwindow.menu.midata.overview");
        miDataImport.Header = Rosetta.TextForId("researchmainwindow.menu.midata.import");
        miProjects.Header = Rosetta.TextForId("researchmainwindow.menu.miprojects");
        miProjectsNew.Header = Rosetta.TextForId("researchmainwindow.menu.miprojects.new");
        miHelp.Header = Rosetta.TextForId("researchmainwindow.menu.mihelp");
        miHelpAbout.Header = Rosetta.TextForId("researchmainwindow.menu.mihelp.about");
        miHelpPage.Header = Rosetta.TextForId("researchmainwindow.menu.mihelp.page");
        miHelpManual.Header = Rosetta.TextForId("researchmainwindow.menu.mihelp.manual");
    }


    public void PopulateData()
    {
        List<ProjectItem> projects = _controller.GetAllProjectItems();
        lbProjects.ItemsSource = projects;
    }

    private void GeneralSettingsClick(object sender, RoutedEventArgs e)
    {
        ResearchMainController.ShowAppSettings();
    }

    private void GeneralConfigurationClick(object sender, RoutedEventArgs e)
    {
        ResearchMainController.ShowAstroConfig();
    }

    private void GeneralCloseClick(object sender, RoutedEventArgs e)
    {
        Close();
    }

    private void ProjectsNewClick(object sender, RoutedEventArgs e)
    {
        ResearchMainController.NewProject();
    }

    private void DataOverviewClick(object sender, RoutedEventArgs e)
    {
        ResearchMainController.ShowDataOverview();
    }

    private void DataImportClick(object sender, RoutedEventArgs e)
    {
        ResearchMainController.ShowDataImport();
    }




    private void CloseClick(object sender, RoutedEventArgs e)
    {
        Close();
    }

    private void HelpClick(object sender, RoutedEventArgs e)
    {
        ResearchMainController.ShowHelp();
    }

    private void HelpManualClick(object sender, RoutedEventArgs e)
    {
        MessageBox.Show(Rosetta.TextForId("helpwindow.manual"));
    }

    private void HelpAboutClick(object sender, RoutedEventArgs e)
    {
        ResearchMainController.ShowAbout();
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
        ResearchMainController.NewProject();
        PopulateData();
    }

}




