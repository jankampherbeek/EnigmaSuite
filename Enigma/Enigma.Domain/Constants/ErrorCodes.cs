// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2022, 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Domain.Constants;

/// <summary>CommonFormula errcodes.</summary>
public static class ErrorCodes
{
    /// <summary>Indication that no error occurred.</summary>
    public const int None = 0;
    /// <summary>Error: date is invalid.</summary>
    public const int InvalidDate = 1000;
    /// <summary>Error: geographic latitude is invalid.</summary>
    public const int InvalidGeolat = 1001;
    /// <summary>Error: geographic longitude is invalid.</summary>
    public const int InvalidGeolon = 1002;
    /// <summary>Error: geographic longitude for lmt is invalid.</summary>
    public const int InvalidGeolonLmt = 1003;
    /// <summary>Error: time is invalid.</summary>
    public const int InvalidTime = 1004;
    /// <summary>Error: offset for UT is invalid.</summary>
    public const int InvalidOffset = 1005;
    /// <summary>Error: directory could not be created.</summary>
    public const int DirCouldNotBeCreated = 1006;
    /// <summary>Error: expected file could not be found.</summary>
    public const int FileNotFound = 1007;
    /// <summary>Error: conversion from csv to Json was invalid.</summary>
    public const int CsvJsonConversion = 1008;


    // errors for research project
    /// <summary>Error: research name is invalid. </summary>
    public const int ResearchNameInvalid = 1009;
    /// <summary>Error: research name is not unique. </summary>
    public const int ResearchNameNotUnique = 1010;
    /// <summary>Error: research identification is invalid.</summary>
    public const int ResearchIdentificationInvalid = 1011;
    /// <summary>Error: research description is invalid.</summary>
    public const int ResearchDescription = 1012;
    /// <summary>Error: multiplication for control group is invalid.</summary>
    public const int ResearchMultiplication = 1013;
    /// <summary>Error: projectfolder already exists.</summary>
    public const int ResearchProjfolderExists = 1014;
    /// <summary>Error: could not create projectfolder.</summary>
    public const int ResearchCannotCreateProjfolder = 1015;
    /// <summary>Error: could not parse project to JSON.</summary>
    public const int ResearchCannotParseProject2Json = 1016;
    /// <summary>Error: could not write JSON for project to file.</summary>
    public const int ResearchCannotWriteJson4Project = 1017;
    /// <summary>Error: could not copy datafile to project.</summary>
    public const int ResearchCannotCopyDatafile = 1018;
    /// <summary>Error: could not create results folder as subfolder in projectfolder.</summary>
    public const int ResearchCannotCreateResultsfolder = 1019;

    // general errors

    /// <summary>Error: startdate is invalid.</summary>
    public const int InvalidStartdate = 1020;
    /// <summary>Error: enddate is invalid.</summary>
    public const int InvalidEnddate = 1021;
}