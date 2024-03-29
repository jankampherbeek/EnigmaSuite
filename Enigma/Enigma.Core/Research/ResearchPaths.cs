﻿// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Dtos;

namespace Enigma.Core.Research;

/// <summary>File paths for research.</summary>
public interface IResearchPaths                    
{
    /// <summary>Path to data files.</summary>
    /// <param name="projName">Name for project.</param>
    /// <param name="useControlGroup">True if data contains a controlgroup, false if data contains testcases.</param>
    /// <returns>String with full path to the required data, including the filename.</returns>
    public string DataPath(string projName, bool useControlGroup);

    /// <summary>Path to result files with positions.</summary>
    /// <param name="projName">Name for project.</param>
    /// <param name="methodName">Name for method.</param>
    /// <param name="useControlGroup">True if result is based on a controlgroup, false if result is based on testcases.</param>
    /// <returns>String with full path for the results, including the filename.</returns>
    public string ResultPath(string projName, string methodName, bool useControlGroup);

    /// <summary>Path to result files with countings.</summary>
    /// <param name="projName">Name for project.</param>
    /// <param name="methodName">Name for method.</param>
    /// <param name="useControlGroup">True if result is based on a controlgroup, false if result is based on testcases.</param>
    /// <returns>String with full path for the results, including the filename.</returns>
    public string CountResultsPath(string projName, string methodName, bool useControlGroup);

    /// <summary>Path to result files with the sums of all countings.</summary>
    /// <param name="projName">Name for project.</param>
    /// <param name="methodName">Name for method.</param>
    /// <param name="useControlGroup">True if result is based on a controlgroup, false if result is based on testcases.</param>
    /// <returns>String with full path for the summed totals, including the filename.</returns>
    public string SummedResultsPath(string projName, string methodName, bool useControlGroup);

}

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


