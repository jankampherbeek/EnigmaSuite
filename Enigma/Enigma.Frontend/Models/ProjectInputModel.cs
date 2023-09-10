// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.Generic;
using System.Linq;
using Enigma.Api.Interfaces;
using Enigma.Domain.References;
using Enigma.Domain.RequestResponse;
using Enigma.Domain.Research;
using Enigma.Frontend.Ui.Interfaces;


namespace Enigma.Frontend.Ui.Models;

/// <summary>Model for input new project</summary>
public class ProjectInputModel
{
    private readonly IDataFileManagementApi _fileManagementApi;
    private readonly IProjectCreationApi _projectCreationApi;
    private readonly IDataNameForPresentationFactory _dataNameForPresentationFactory;    

    public ProjectInputModel(IDataFileManagementApi fileManagementApi,
        IProjectCreationApi projectCreationApi,
        IDataNameForPresentationFactory dataNameForPresentationFactory)
    {
        _fileManagementApi = fileManagementApi;
        _projectCreationApi = projectCreationApi;
        _dataNameForPresentationFactory = dataNameForPresentationFactory;
    }

    public List<string> GetDataNames()
    {
        IEnumerable<string> fullPathDataNames = _fileManagementApi.GetDataNames();
        return _dataNameForPresentationFactory.CreateDataNamesForListView(fullPathDataNames);
    }
    
    public static List<string> GetControlGroupTypeNames()
    {
        return ControlGroupTypesExtensions.AllDetails().Select(cGroup => cGroup.Text).ToList();
    }

    public ResultMessage SaveProject(ResearchProject project)
    {
        return _projectCreationApi.CreateProject(project);
    }
    
    
}