// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Engima.Domain.Research;
using Enigma.Api.Interfaces;
using Enigma.Domain.Charts;
using Enigma.Domain.Constants;
using Enigma.Domain.Research;
using Enigma.Frontend.Ui.Interfaces;
using Enigma.Persistency.Interfaces;
using Enigma.Research.Interfaces;
using Serilog;
using System.Collections.Generic;
using System.Windows;

namespace Enigma.Frontend.Ui.ResearchProjects;
public class ProjectInputController
{
    public string ProjectName { get; set; }
    public string ProjectIdentifier { get; set; }
    public string ProjectDescription { get; set; }
    public string DataFileName { get; set; }
    public ControlGroupTypes ControlGroupType { get; set; }
    public string ControlGroupMultiplication { get; set; }
    public List<int> ActualErrorCodes { get; set; }

    private readonly IFileManagementApi _fileManagementApi;
    private readonly IDataNameForDataGridFactory _dataNameForDataGridFactory;
    private readonly IProjectCreationHandler _projectCreationHandler;

    public ProjectInputController(IFileManagementApi fileManagementApi,
        IDataNameForDataGridFactory dataNameForDataGridFactory,
        IProjectCreationHandler projectCreationHandler)
    {
        _fileManagementApi = fileManagementApi;
        _dataNameForDataGridFactory = dataNameForDataGridFactory;
        _projectCreationHandler = projectCreationHandler;
        ProjectName = "";
        ProjectIdentifier = "";
        ProjectDescription = "";
        DataFileName = "";
        ControlGroupType = ControlGroupTypes.StandardShift;
        ControlGroupMultiplication = "1";
        ActualErrorCodes = new();
    }

    public List<PresentableDataName> GetDataNames()
    {
        string path = @"c:\enigma_ar\data\";        // TODO release 0.2 replace hardcoded path to data with path from settings.
        List<string> fullPathDataNames = _fileManagementApi.GetDataNames(path);
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
        if (ProjectIdentifier == null || ProjectIdentifier.Trim().Length == 0)
        {
            noErrors = false;
            ActualErrorCodes.Add(ErrorCodes.ERR_RESEARCH_IDENTIFICATION_INVALID);
            ProjectIdentifier = "";
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
            ResearchProject project = new(ProjectName, ProjectIdentifier, ProjectDescription, DataFileName, ControlGroupType, multiplication);
            _projectCreationHandler.CreateProject(project, out int errorCode);
            if (errorCode != 0)
            {
                noErrors = false;
                Log.Error("Error while creating project, Enigma errorcode: {errorCode}, project: {@project}", errorCode, project);
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


}