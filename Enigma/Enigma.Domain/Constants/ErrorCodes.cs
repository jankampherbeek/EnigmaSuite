// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Domain.Constants;

/// <summary>CommonFormula errcodes.</summary>
public static class ErrorCodes
{
    /// <summary>Indication that no error occurred.</summary>
    public const int NONE = 0;
    /// <summary>Error: date is invalid.</summary>
    public const int INVALID_DATE = 1000;
    /// <summary>Error: geographic latitude is invalid.</summary>
    public const int INVALID_GEOLAT = 1001;
    /// <summary>Error: geographic longitude is invalid.</summary>
    public const int INVALID_GEOLON = 1002;
    /// <summary>Error: geographic longitude for lmt is invalid.</summary>
    public const int INVALID_GEOLON_LMT = 1003;
    /// <summary>Error: time is invalid.</summary>
    public const int INVALID_TIME = 1004;
    /// <summary>Error: offset for UT is invalid.</summary>
    public const int INVALID_OFFSET = 1005;
    /// <summary>Error: directory could not be created.</summary>
    public const int DIR_COULD_NOT_BE_CREATED = 1006;
    /// <summary>Error: expected file could not be found.</summary>
    public const int FILE_NOT_FOUND = 1007;
    /// <summary>Error: conversion from csv to Json was invalid.</summary>
    public const int CSV_JSON_CONVERSION = 1008;


    // errors for research project
    /// <summary>Error: research name is invalid. </summary>
    public const int RESEARCH_NAME_INVALID = 1009;
    /// <summary>Error: research name is not unique. </summary>
    public const int RESEARCH_NAME_NOT_UNIQUE = 1010;
    /// <summary>Error: research identification is invalid.</summary>
    public const int RESEARCH_IDENTIFICATION_INVALID = 1011;
    /// <summary>Error: research description is invalid.</summary>
    public const int RESEARCH_DESCRIPTION = 1012;
    /// <summary>Error: multiplication for control group is invalid.</summary>
    public const int RESEARCH_MULTIPLICATION = 1013;
    /// <summary>Error: projectfolder already exists.</summary>
    public const int RESEARCH_PROJFOLDER_EXISTS = 1014;
    /// <summary>Error: could not create projectfolder.</summary>
    public const int RESEARCH_CANNOT_CREATE_PROJFOLDER = 1015;
    /// <summary>Error: could not parse project to JSON.</summary>
    public const int RESEARCH_CANNOT_PARSE_PROJECT2_JSON = 1016;
    /// <summary>Error: could not write JSON for project to file.</summary>
    public const int RESEARCH_CANNOT_WRITE_JSON4_PROJECT = 1017;
    /// <summary>Error: could not copy datafile to project.</summary>
    public const int RESEARCH_CANNOT_COPY_DATAFILE = 1018;
    /// <summary>Error: could not create results folder as subfolder in projectfolder.</summary>
    public const int RESEARCH_CANNOT_CREATE_RESULTSFOLDER = 1019;

    // general errors

    /// <summary>Error: startdate is invalid.</summary>
    public const int INVALID_STARTDATE = 1020;
    /// <summary>Error: enddate is invalid.</summary>
    public const int INVALID_ENDDATE = 1021;
}