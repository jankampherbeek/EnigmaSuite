// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


using Enigma.Core.Research.Interfaces;
using Enigma.Domain.Dtos;

namespace Enigma.Core.Research.Helpers;

/// <inherit/>
public sealed class ResearchPaths : IResearchPaths
{

    public string DataPath(string projName, bool useControlGroup)
    {
        return ConstructDataPath(projName, useControlGroup);
    }

    /// <inherit/>
    public string ResultPath(string projName, string methodName, bool useControlGroup)
    {
        return ConstructResultPath(projName, methodName, useControlGroup);
    }

    /// <inherit/>
    public string CountResultsPath(string projName, string methodName, bool useControlGroup)
    {
        return ConstructCountResultsPath(projName, methodName, useControlGroup);
    }

    /// <inherit/>
    public string SummedResultsPath(string projName, string methodName, bool useControlGroup)
    {
        return ConstructSummedResultsPath(projName, methodName, useControlGroup);
    }

    private static string ConstructDataPath(string projName, bool useControlGroup)
    {
        ApplicationSettings appSettings = ApplicationSettings.Instance;
        string projFiles = appSettings.LocationProjectFiles;
        string dataFilename = useControlGroup ? "controldata" : "testdata";
        return projFiles + @"\" + projName + @"\" + dataFilename + ".json";
    }

    private static string ConstructResultPath(string projName, string methodName, bool useControlGroup)
    {
        ApplicationSettings appSettings = ApplicationSettings.Instance;
        string projFiles = appSettings.LocationProjectFiles;
        string dateTimeStamp = ConstructDateTimeStamp();
        string prefix = useControlGroup ? "controldataresult_" : "testdataresult_";
        return projFiles + @"\" + projName + @"\results" + @"\" + prefix + methodName + "_positions_" + dateTimeStamp + ".json";
    }

    private static string ConstructCountResultsPath(string projName, string methodName, bool useControlGroup)
    {
        ApplicationSettings appSettings = ApplicationSettings.Instance;
        string projFiles = appSettings.LocationProjectFiles;
        string dateTimeStamp = ConstructDateTimeStamp();
        string prefix = useControlGroup ? "controldataresult_" : "testdataresult_";
        return projFiles + @"\" + projName + @"\results" + @"\" + prefix + methodName + "_counts_" + dateTimeStamp + ".json";
    }

    private static string ConstructSummedResultsPath(string projName, string methodName, bool useControlGroup)
    {
        ApplicationSettings appSettings = ApplicationSettings.Instance;
        string projFiles = appSettings.LocationProjectFiles;
        string dateTimeStamp = ConstructDateTimeStamp();
        string prefix = useControlGroup ? "controlsummedresult_" : "testsummedresult_";
        return projFiles + @"\" + projName + @"\results" + @"\" + prefix + methodName + "_counts_" + dateTimeStamp + ".txt";

    }

    private static string ConstructDateTimeStamp()
    {
        DateTime dateTime = DateTime.Now;
        string year = dateTime.Year.ToString();
        string month = dateTime.Month.ToString();
        string day = dateTime.Day.ToString();
        string hour = dateTime.Hour.ToString();
        string minute = dateTime.Minute.ToString();
        string second = dateTime.Second.ToString();
        return year + "-" + month + "-" + day + " " + hour + "-" + minute + "-" + second;
    }
}


