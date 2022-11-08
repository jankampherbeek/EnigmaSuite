// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Api.Interfaces;
using Enigma.Domain.Charts;
using Enigma.Domain.Constants;
using Enigma.Domain.RequestResponse;
using Enigma.Domain.Research;
using Enigma.Frontend.Ui.Interfaces;
using Enigma.Frontend.Ui.Support;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System.Collections.Generic;
using System.Windows;

namespace Enigma.Frontend.Ui.ResearchProjects;
public class ProjectInputController
{
    public string ProjectName { get; set; } = "";
    public string ProjectIdentifier { get; set; } = "";
    public string ProjectDescription { get; set; } = "";
    public string DataFileName { get; set; } = "";
    public ControlGroupTypes ControlGroupType { get; set; } = ControlGroupTypes.StandardShift;
    public string ControlGroupMultiplication { get; set; } = "";
    public List<int> ActualErrorCodes { get; set; }

    private readonly IFileManagementApi _fileManagementApi;
    private readonly IDataNameForDataGridFactory _dataNameForDataGridFactory;
    private readonly IProjectCreationApi _projectCreationApi;

    public ProjectInputController(IFileManagementApi fileManagementApi,
        IDataNameForDataGridFactory dataNameForDataGridFactory,
        IProjectCreationApi projectCreationApi)
    {
        _fileManagementApi = fileManagementApi;
        _dataNameForDataGridFactory = dataNameForDataGridFactory;
        _projectCreationApi = projectCreationApi;

        ActualErrorCodes = new();
    }

    public List<PresentableDataName> GetDataNames()
    {
        List<string> fullPathDataNames = _fileManagementApi.GetDataNames();
        return _dataNameForDataGridFactory.CreateDataNamesForDataGrid(fullPathDataNames);
    }

    public bool ProcessInput()
    {
        bool noErrors = true;
        ActualErrorCodes = new List<int>();

        if (ProjectName == null || ProjectName.Trim().Length == 0)
        {
            noErrors = false;
            ActualErrorCodes.Add(ErrorCodes.ERR_RESEARCH_NAME_INVALID);
            ProjectName = "";
        }
        if (ProjectDescription == null || ProjectDescription.Trim().Length == 0)
        {
            noErrors = false;
            ActualErrorCodes.Add(ErrorCodes.ERR_RESEARCH_DESCRIPTION);
            ProjectDescription = "";
        }
        bool isNumeric = int.TryParse(ControlGroupMultiplication, out int multiplication);
        if (!isNumeric || 0 >= multiplication || multiplication > 10)
        {
            noErrors = false;
            ActualErrorCodes.Add(ErrorCodes.ERR_RESEARCH_MULTIPLICATION);
        }
        if (noErrors)
        {
            ResearchProject project = new(ProjectName, ProjectDescription, DataFileName, ControlGroupType, multiplication);
            ResultMessage resultMessage = _projectCreationApi.CreateProject(project);
            if (resultMessage.ErrorCode != 0)
            {
                noErrors = false;
                Log.Error("Error while creating project, Enigma errorcode: {errorCode}, project: {@project}", resultMessage.ErrorCode, project);
                MessageBox.Show("An error occurred");         // TODO use RB
            }
            else
            {
                Log.Information("Created project {projectName}", ProjectName);
                MessageBox.Show("Project has been saved.");  // TODO use RB
            }
        }
        return noErrors;
    }

    public static void ShowHelp()
    {
        HelpWindow helpWindow = App.ServiceProvider.GetRequiredService<HelpWindow>();
        helpWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        helpWindow.SetHelpPage("ProjectImport");
        helpWindow.ShowDialog();
    }
}