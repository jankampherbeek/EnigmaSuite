// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.Generic;
using System.Linq;
using System.Windows.Documents;
using Enigma.Api;
using Enigma.Domain.References;
using Enigma.Domain.Research;
using Enigma.Domain.Responses;
using Enigma.Frontend.Ui.PresentationFactories;
using Enigma.Frontend.Ui.Support;


namespace Enigma.Frontend.Ui.Models;

/// <summary>Model for input new project</summary>
public class ProjectInputModel
{
    private Rosetta _rosetta = Rosetta.Instance;
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

    public List<string> GetCgMultiplicationFactors()
    {
        return new List<string> 
        {
            "1",
            "10",
            "100",
            "1000"
        };
    }
    
    public List<string> GetControlGroupTypeNames()
    {
        return ControlGroupTypesExtensions.AllDetails().Select(  cGroup => _rosetta.GetText(cGroup.RbKey)).ToList();
    }

    public ResultMessage SaveProject(ResearchProject project)
    {
        return _projectCreationApi.CreateProject(project);
    }
    
    
}