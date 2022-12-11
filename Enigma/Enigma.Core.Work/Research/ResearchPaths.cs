// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Work.Research.Interfaces;
using Enigma.Domain.Configuration;

namespace Enigma.Core.Work.Research;

/// <inherit/>
public class ResearchPaths: IResearchPaths {

   private readonly string _dateTimeFormat = "yyyy/MM/dd HH:mm:ss";


   public string DataPath(String projName, bool useControlGroup)
    {
        return ConstructDataPath(projName, useControlGroup);
    }

    /// <inherit/>
    public string ResultPath(string projName, string methodName, bool useControlGroup)
    {
        return ConstructResultPath(projName, methodName, useControlGroup);
    }

    private string ConstructDataPath(string projName, bool useControlGroup)
    {   
        ApplicationSettings _appSettings = ApplicationSettings.Instance;
        string projFiles = _appSettings.LocationProjectFiles;
        string dataFilename = useControlGroup ? "controldata" : "testdata";
        return projFiles + @"\" + projName + @"\" + dataFilename + ".json";
    }

    private string ConstructResultPath(string projName, string methodName, bool useControlGroup)
    {
        ApplicationSettings _appSettings = ApplicationSettings.Instance;
        string projFiles = _appSettings.LocationProjectFiles;
        string dateTimeStamp = DateTime.Now.ToString(_dateTimeFormat);
        string prefix = useControlGroup ? "controldataresult_" : "testdataresult_";
        return projFiles + @"\" + projName + @"\results" + @"\" + prefix + methodName + "_" + dateTimeStamp + ".json";
    }
}


