// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2025.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.


namespace Enigma.Domain.Dtos;

public record OrbitalElements{

    public double SemiMajorAxis { get; init; } 
    public double Eccentricity{ get; init; } 
    public double Inclination{ get; init; } 
    public double LongAscNode{ get; init; } 
    public double ArgPeriApsis{ get; init; } 
    public double LongPeriApsis{ get; init; } 
    public double MeanAnomalyEpoch{ get; init; } 
    public double TrueAnomalyEpoch{ get; init; } 
    public double EccAnomalyEpoch{ get; init; } 
    public double MeanLongEpoch{ get; init; } 
    public double SiderealOrbPeriodYears{ get; init; } 
    public double MeanDailyMotion{ get; init; } 
    public double TropicalPeriodYears{ get; init; } 
    public double SynodicPeriodDays{ get; init; } 
    public double TimePeriHelionPassage{ get; init; } 
    public double PeriHelionDistance{ get; init; } 
    public double ApHelionDistance{ get; init; } 


    public OrbitalElements(double[] elements)
    {
        SemiMajorAxis = elements[0];
        Eccentricity = elements[1];
        Inclination = elements[2];
        LongAscNode = elements[3];
        ArgPeriApsis = elements[4];
        LongPeriApsis = elements[5];
        MeanAnomalyEpoch = elements[6];
        TrueAnomalyEpoch = elements[7];
        EccAnomalyEpoch = elements[8];
        MeanLongEpoch = elements[9];
        SiderealOrbPeriodYears = elements[10];
        MeanDailyMotion = elements[11];
        TropicalPeriodYears = elements[12];
        SynodicPeriodDays = elements[13];
        TimePeriHelionPassage = elements[14];
        PeriHelionDistance = elements[15];
        ApHelionDistance = elements[16];
    }
}        
    
