// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.Generic;
using System.Linq;
using Enigma.Api;
using Enigma.Api.Research;
using Enigma.Domain.References;
using Enigma.Domain.Research;
using Enigma.Domain.Responses;
using Enigma.Frontend.Ui.PresentationFactories;
using Enigma.Frontend.Ui.Support;


namespace Enigma.Frontend.Ui.Models;

/// <summary>Model for input new project</summary>
public class ProjectInputModel(
    IDataFileManagementApi fileManagementApi,
    IProjectCreationApi projectCreationApi,
    IDataNameForPresentationFactory dataNameForPresentationFactory)
{
    private readonly Rosetta _rosetta = Rosetta.Instance;

    public List<string> GetDataNames()
    {
        var allData = fileManagementApi.GetDataNames();
        return dataNameForPresentationFactory.CreateDataNamesForListView(allData);
    }

    public static List<string> GetCgMultiplicationFactors()
    {
        return
        [
            "1",
            "10",
            "100",
            "1000"
        ];
    }
    
    public List<string> GetControlGroupTypeNames()
    {
        return ControlGroupTypesExtensions.AllDetails().Select(  cGroup => _rosetta.GetText(cGroup.RbKey)).ToList();
    }

    public ResultMessage SaveProject(ResearchProject project)
    {
        return projectCreationApi.CreateProject(project);
    }

    /// <summary>Gets the datafile ID for a given datafile name</summary>
    /// <param name="datafileName">The name of the datafile</param>
    /// <returns>The ID of the datafile, or -1 if not found</returns>
    public int GetDatafileId(string datafileName)
    {
        var dataFile = fileManagementApi.ReadDataFile(datafileName);
        return dataFile?.Id ?? -1;
    }
}