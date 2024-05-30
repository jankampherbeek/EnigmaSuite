// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023, 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Domain.Constants;

public static class StandardTexts
{
    // Error texts
    public const string TITLE_ERROR = "Something is wrong.";

    public const string ERROR_ASPECT_COLORLINE = "Make sure all new colors are correctly spelled.";
    public const string ERROR_DATAFILE_MISSING = "There is no datafile available. Please import a datafile.";
    public const string ERROR_DATE = "Enter a correct value for the date.";
    public const string ERROR_DEGREE = "Enter a proper value for the degrees.";
    public const string ERROR_DESCRIPTION = "Enter a value for the description.";
    public const string ERROR_GEOGRAPHIC_LATITUDE = "Enter a correct value for the geographic latitude.";
    public const string ERROR_GEOGRAPHIC_LONGITUDE = "Enter a correct value for the geographic longitude.";
    public const string ERROR_HARMONIC_NR = "Enter a proper positive number - 2..100000 - for the harmonic.";
    public const string ERROR_LMT_LONGITUDE = "Enter a correct value for the longitude for LMT.";
    public const string ERROR_MINUTE = "Enter a proper value for the minutes.";
    public const string ERROR_NAME = "Enter a value for the name.";
    public const string ERROR_ORB_ASPECTS = "Enter a proper value for the orbs for aspects";
    public const string ERROR_ORB_MIDPOINTS = "Enter a proper value for the orbs for midpoints.";
    public const string ERROR_ORB_MIDPOINTS_DECL = "Enter a proper value for the orbs for declination midpoints.";
    public const string ERROR_ORB_PARALLELS = "Enter a proper value for the orbs for parallels.";
    public const string ERROR_ORB_SECDIR = "Enter a proper value for the orbs for secondary directions.";
    public const string ERROR_ORB_SYMDIR = "Enter a proper value for the orbs for symbolic directions.";
    public const string ERROR_ORB_TRANSIT = "Enter a proper value for the orbs for transits.";
    public const string ERROR_TIME = "Enter a correct value for the time.";
    
    // Texts for the configuration
    public const string CFG_AYANAMSHA = "Ayanamsha";
    public const string CFG_BASE_ORB_ASPECTS = "BaseOrbAspects";
    public const string CFG_BASE_ORB_MIDPOINTS = "BaseOrbMidpoints";
    public const string CFG_HOUSE_SYSTEM = "HouseSystem";
    public const string CFG_OBSERVER_POSITION = "ObserverPosition";
    public const string CFG_ORB_METHOD = "OrbMethod";
    public const string CFG_ORB_PARALLELS = "OrbParallels";    
    public const string CFG_ORB_MIDPOINTS_DECL = "OrbDeclMidpoints"; 
    public const string CFG_PD_APPROACH = "ApproachPrimaryDirections";
    public const string CFG_PD_CONVERSE = "ConversePrimaryDirections";
    public const string CFG_PD_LATASP = "LatitudeAspectsPrimaryDirections";
    public const string CFG_PD_METHOD = "MethodPrimaryDirections";
    public const string CFG_PD_TIMEKEY = "TimeKeyPrimaryDirections";
    public const string CFG_PROJECTION_TYPE = "ProjectionType";
    public const string CFG_SCORB = "SC_ORB";
    public const string CFG_SMKEY = "SM_KEY";
    public const string CFG_SMORB = "SM_ORB";
    public const string CFG_TRORB = "TR_ORB";
    public const string CFG_USE_CUSPS_FOR_ASPECTS = "UseCuspsForAspects";
    public const string CFG_ZODIAC_TYPE = "ZodiacType";
    
    // Prefixes for configurations
    public const string PCF_ASPECTCOLOR = "AC_";
    public const string PCF_ASPECTS = "AT_";
    public const string PCF_CHARTPOINTS = "CP_";
    public const string PCF_SECONDARY = "SC_";
    public const string PCF_SECONDARYPOINTS = "SC_CP_";
    public const string PCF_SIGNIFICATORS = "PDSG_";
    public const string PCF_PROMISSORS = "PDPM_";
    public const string PCF_PDASPECTS = "PDAT_";
    public const string PCF_PDPLACEHOLDERPOINTS = "PD_XX";
    public const string PCF_PRIMARY = "PD_";    
    public const string PCF_SYMBOLIC = "SM_";
    public const string PCF_SYMBOLICPOINTS = "SM_CP_";
    public const string PCF_TRANSITPOINTS = "TR_CP_";
    public const string PCF_TRANSITS = "TR_";
    
}