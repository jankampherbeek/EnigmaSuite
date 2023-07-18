// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Api.Interfaces;
using Enigma.Domain.Charts;
using Enigma.Domain.Constants;
using Enigma.Domain.RequestResponse;
using Enigma.Domain.Research;
using Enigma.Frontend.Helpers.Support;
using Enigma.Frontend.Ui.Interfaces;
using Enigma.Frontend.Ui.Support;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System.Collections.Generic;
using System.Windows;
using Enigma.Frontend.Ui.State;
using Enigma.Frontend.Ui.Views;

namespace Enigma.Frontend.Ui.Research;
public class ProjectInputController
{
    public string ProjectName { get; set; } = "";
    public string ProjectIdentifier { get; set; } = "";
    public string ProjectDescription { get; set; } = "";
    public string DataFileName { get; set; } = "";
    public ControlGroupTypes ControlGroupType { get; set; } = ControlGroupTypes.StandardShift;
    public string ControlGroupMultiplication { get; set; } = "";
    public List<int> ActualErrorCodes { get; set; }

    private readonly IDataFileManagementApi _fileManagementApi;
    private readonly IDataNameForPresentationFactory _dataNameForPresentationFactory;
    private readonly IProjectCreationApi _projectCreationApi;

    public ProjectInputController(IDataFileManagementApi fileManagementApi,
        IDataNameForPresentationFactory dataNameForPresentationFactory,
        IProjectCreationApi projectCreationApi)
    {
        _fileManagementApi = fileManagementApi;
        _dataNameForPresentationFactory = dataNameForPresentationFactory;
        _projectCreationApi = projectCreationApi;
        ActualErrorCodes = new();
    }

    public List<PresentableDataName> GetDataNames()
    {
        List<string> fullPathDataNames = _fileManagementApi.GetDataNames();
        return _dataNameForPresentationFactory.CreateDataNamesForDataGrid(fullPathDataNames);
    }

    public bool ProcessInput()
    {
        bool noErrors = true;
        ActualErrorCodes = new List<int>();

        if (ProjectName == null || ProjectName.Trim().Length == 0)
        {
            noErrors = false;
            ActualErrorCodes.Add(ErrorCodes.ResearchNameInvalid);
            ProjectName = "";
        }
        if (ProjectDescription == null || ProjectDescription.Trim().Length == 0)
        {
            noErrors = false;
            ActualErrorCodes.Add(ErrorCodes.ResearchDescription);
            ProjectDescription = "";
        }
        bool isNumeric = int.TryParse(ControlGroupMultiplication, out int multiplication);
        if (!isNumeric || 0 >= multiplication || multiplication > 10)
        {
            noErrors = false;
            ActualErrorCodes.Add(ErrorCodes.ResearchMultiplication);
        }
        if (noErrors)
        {
            ResearchProject project = new(ProjectName, ProjectDescription, DataFileName, ControlGroupType, multiplication);
            ResultMessage resultMessage = _projectCreationApi.CreateProject(project);
            if (resultMessage.ErrorCode != 0)
            {
                noErrors = false;
                Log.Error("Error while creating project, Enigma errorcode: {errorCode}, project: {@project}", resultMessage.ErrorCode, project);
                MessageBox.Show(Rosetta.TextForId("projectinputwindow.nameexists"));
            }
            else
            {
                Log.Information("Created project {projectName}", ProjectName);
                MessageBox.Show(Rosetta.TextForId("projectinputwindow.projectsaved"));
            }
        }
        return noErrors;
    }

    public static void ShowHelp()
    {
        DataVault.Instance.CurrentViewBase = "ProjectNew";
        HelpWindow helpWindow = new();
        helpWindow.ShowDialog();
    }

}