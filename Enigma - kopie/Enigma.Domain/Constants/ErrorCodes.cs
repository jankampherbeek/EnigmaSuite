// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Domain.Constants;

/// <summary>Numeric errcodes.</summary>
public static class ErrorCodes
{
    /// <summary>Indication that no error occurred.</summary>
    public const int ERR_NONE = 0;
    /// <summary>Error: date is invalid.</summary>
    public const int ERR_INVALID_DATE = 1000;
    /// <summary>Error: geographic latitude is invalid.</summary>
    public const int ERR_INVALID_GEOLAT = 1001;
    /// <summary>Error: geographic longitude is invalid.</summary>
    public const int ERR_INVALID_GEOLON = 1002;
    /// <summary>Error: geographic longitude for lmt is invalid.</summary>
    public const int ERR_INVALID_GEOLON_LMT = 1003;
    /// <summary>Error: time is invalid.</summary>
    public const int ERR_INVALID_TIME = 1004;
    /// <summary>Error: offset for UT is invalid.</summary>
    public const int ERR_INVALID_OFFSET = 1005;
    /// <summary>Error: directory could not be created.</summary>
    public const int ERR_DIR_COULD_NOT_BE_CREATED = 1006;
    /// <summary>Error: expected file could not be found.</summary>
    public const int ERR_FILE_NOT_FOUND = 1007;
    /// <summary>Error: conversion from csv to Json was invalid.</summary>
    public const int ERR_CSV_JSON_CONVERSION = 1008;
}