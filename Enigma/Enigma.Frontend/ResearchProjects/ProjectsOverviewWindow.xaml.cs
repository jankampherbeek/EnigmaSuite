// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Frontend.Ui.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Windows;

namespace Enigma.Frontend.Ui.ResearchProjects;

/// <summary>Window for overview of projects and the possibility to open a project.</summary>
public partial class ProjectsOverviewWindow : Window
{
    private ProjectsOverviewController _controller;
    private IRosetta _rosetta;


    public ProjectsOverviewWindow()
    {
        InitializeComponent();
        _controller = App.ServiceProvider.GetRequiredService<ProjectsOverviewController>();
        _rosetta = App.ServiceProvider.GetRequiredService<IRosetta>();
        PopulateTexts();
        PopulateData();
    }


    public void PopulateTexts()
    {
        Title = _rosetta.TextForId("projectoverviewwindow.title");
        tbExplText.Text = _rosetta.TextForId("projectoverviewwindow.expltext");
        tbFormTitle.Text = _rosetta.TextForId("projectoverviewwindow.formtitle");
        btnClose.Content = _rosetta.TextForId("common.btnclose");
        btnHelp.Content = _rosetta.TextForId("common.btnhelp");
        btnInfo.Content = _rosetta.TextForId("common.btninfo");
        btnOpen.Content = _rosetta.TextForId("common.btnopen");
    }

    public void PopulateData()
    {
        List<ProjectItem> projects = _controller.GetAllProjectItems();
        lbProjects.ItemsSource = projects;
    }


    private void CloseClick(object sender, RoutedEventArgs e)
    {
        Close();
    }

    private void HelpClick(object sender, RoutedEventArgs e)
    {
        // show help
    }

    private void InfoClick(object sender, RoutedEventArgs e)
    {
        // show info
    }

    private void OpenClick(object sender, RoutedEventArgs e)
    {
        // open project
    }

}




