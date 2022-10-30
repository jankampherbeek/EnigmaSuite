// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Engima.Domain.Research;
using Enigma.Domain.Charts;
using Enigma.Domain.Constants;
using Enigma.Domain.Research;
using Enigma.Frontend.Interfaces;
using Enigma.Persistency.Interfaces;
using Enigma.Research.Interfaces;
using System.Collections.Generic;
using System.Windows;

namespace Enigma.Frontend.ResearchProjects;
public class ProjectInputController
{
    public string ProjectName { get; set; } 
    public string ProjectIdentifier { get; set; }
    public string ProjectDescription { get; set; }
    public string DataFileName { get; set; }
    public ControlGroupTypes ControlGroupType { get; set; }
    public string ControlGroupMultiplication { get; set; }
    public List<int> ActualErrorCodes { get; set; }

    private readonly IDataNameHandler _dataNameHandler;
    private readonly IDataNameForDataGridFactory _dataNameForDataGridFactory;
    private readonly IProjectCreationHandler _projectCreationHandler;

    public ProjectInputController(IDataNameHandler dataNameHandler,
        IDataNameForDataGridFactory dataNameForDataGridFactory, 
        IProjectCreationHandler projectCreationHandler)
    {
        _dataNameHandler = dataNameHandler;
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
        List<string> fullPathDataNames = _dataNameHandler.GetExistingDataNames(path);
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
        // TODO check if datafilename exists
        if (noErrors)
        {
            ResearchProject project = new(ProjectName, ProjectIdentifier, ProjectDescription, DataFileName, ControlGroupType, multiplication);
            _projectCreationHandler.CreateProject(project, out int errorCode);
            if (errorCode != 0)
            {
                MessageBox.Show("An error occurred");
                noErrors = false;
                // TODO handle errors
            }
            else
            {
                MessageBox.Show("Project has been saved.");

            }
        }
        return noErrors;
    }


}