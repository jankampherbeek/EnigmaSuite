// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Documents;

namespace Enigma.Frontend.Ui.ResearchProjects;

/// <summary>Window for overview of projects and the possibility to open a project.</summary>
public partial class ProjectsOverviewWindow : Window
{
    private ProjectsOverviewController _controller;
    public ProjectsOverviewWindow()
    {
        InitializeComponent();
        _controller = App.ServiceProvider.GetRequiredService<ProjectsOverviewController>();
        PopulateData();
    }

    public void PopulateData()
    {
        List<ProjectItem> projects = _controller.GetAllProjectItems();
        lbProjects.ItemsSource = projects;
    }


    private void CancelClick(object sender, RoutedEventArgs e)
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




