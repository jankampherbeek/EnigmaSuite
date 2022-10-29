// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Configuration;
using Enigma.Domain.Constants;
using Enigma.Domain.Research;
using Enigma.Persistency.FileHandling;
using Enigma.Research.Parsers;


namespace Enigma.Research.Handlers;

public interface IProjectCreationHandler
{
    public bool CreateProject(ResearchProject project, out int errorCode);
}



public class ProjectCreationHandler : IProjectCreationHandler
{

    private readonly ApplicationSettings _applicationSettings;
    private readonly IResearchProjectParser _researchProjectParser;
    private readonly ITextFileWriter _textFileWriter;

    public ProjectCreationHandler(IResearchProjectParser researchProjectParser, ITextFileWriter textFileWriter)
    {
        _researchProjectParser = researchProjectParser;
        _textFileWriter = textFileWriter;
        _applicationSettings = ApplicationSettings.Instance;
    }

    public bool CreateProject(ResearchProject project, out int errorCode)
    {

        errorCode = 0;
        if (FolderExists(project.Name))
        {
            errorCode = ErrorCodes.ERR_RESEARCH_PROJFOLDER_EXISTS;
            return false;
        }
        if (!CreateFolder(project.Name))
        {
            errorCode = ErrorCodes.ERR_RESEARCH_CANNOT_CREATE_PROJFOLDER;
            return false;
        }
        if (!ParseJson(project, out string jsonText))
        {
            errorCode = ErrorCodes.ERR_RESEARCH_CANNOT_PARSE_PROJECT_2_JSON;
            return false;
        }
        if (!WriteJsonToFile(jsonText, project))
        {
            errorCode = ErrorCodes.ERR_RESEARCH_CANNOT_WRITE_JSON_4_PROJECT;
            return false;
        }
        return true;
    }

    private bool FolderExists(string projectName)
    {
        string projPath = _applicationSettings.LocationProjectFiles + Path.DirectorySeparatorChar + projectName;
        return Directory.Exists(projPath);
    }

    private bool CreateFolder(string projectName)
    {
        string projPath = _applicationSettings.LocationProjectFiles + Path.DirectorySeparatorChar + projectName;
        try
        {
            Directory.CreateDirectory(projPath);
        } catch(Exception e) {
            // TODO log error
            return false;
        }
        return true;
    }

    private bool ParseJson(ResearchProject project, out string jsonText)
    {
        jsonText = "";
        try
        {
            jsonText = _researchProjectParser.Marshall(project);
        }
        catch (Exception e)
        {
            // TODO log error
            return false;
        }
        return true;   
    }


    private bool WriteJsonToFile(string jsonText, ResearchProject project)
    {
        string projPath = _applicationSettings.LocationProjectFiles + Path.DirectorySeparatorChar + project.Name + Path.DirectorySeparatorChar + project.Identification + "_definition.json";
        try
        {
            _textFileWriter.WriteFile(projPath, jsonText);
        }
        catch (Exception e)
        {
            // TODO log error
            return false;
        }
        return true;

    }

}