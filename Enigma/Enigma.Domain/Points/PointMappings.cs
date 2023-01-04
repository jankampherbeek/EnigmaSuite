// Jan Kampherbeek, (c) 2022, 2023.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Interfaces;

namespace Enigma.Domain.Points;

/// <inheritdoc/>
public class PointMappings: IPointMappings
{

    private readonly int _offsetCelPoints = 0;
    private readonly int _offsetZodiacPoints = 1000;
    private readonly int _offsetArabicPoints = 2000;
    private readonly int _offsetMundanePoints = 3000;
    private readonly int _offsetCusps = 4000;
    private readonly PointDefinitions _pointDefinitions;

    /// <inheritdoc/>
    public PointMappings(PointDefinitions pointDefinitions)
    {
        _pointDefinitions = pointDefinitions;
    }

    /// <inheritdoc/>
    public int IndexForCelPoint(CelPoints point)
    {
        return (int)point + _offsetCelPoints;
    }

    /// <inheritdoc/>
    public int IndexForZodiacPoint(ZodiacPoints point)
    {
        return (int)point + _offsetZodiacPoints;
    }

    /// <inheritdoc/>
    public int IndexForArabicPoint(ArabicPoints point)
    {
        return (int)point + _offsetArabicPoints;
    }

    /// <inheritdoc/>
    public int IndexForMundanePoint(MundanePoints point)
    {
        return (int)point + _offsetMundanePoints;
    }

    /// <inheritdoc/>
    public int IndexForCusp(int cuspNr)
    {
        return cuspNr -1 + _offsetCusps;
    }

    /// <inheritdoc/>
    public GeneralPoint GeneralPointForIndex(int index)
    {
       foreach (GeneralPoint point in _pointDefinitions.AllGenericPoints)
       {
            if (point.Index == index) return point; 
       }
        throw new ArgumentException("Could not find GeneralPoint for index: " + index);
    }

    /// <inheritdoc/>
    public CelPoints CelPointForIndex(int index)
    {
        if (index >= 0 && index < _offsetZodiacPoints)
        {
            return CelPoints.Neptune.CelestialPointForIndex(index);
              
        }
        throw new ArgumentException("PointMappings.CelPointForIndex(): index not in range for CelPoint: " + index);
    }

    /// <inheritdoc/>
    public ZodiacPoints ZodiacPointForIndex(int index)
    {
        if (index >= _offsetZodiacPoints && index < _offsetArabicPoints)
        {
            return ZodiacPoints.None.ZodiacPointForIndex(index - _offsetZodiacPoints);

        }
        throw new ArgumentException("PointMappings.ZodiacPointForIndex(): index not in range for ZodiacPoint: " + index);
    }

    /// <inheritdoc/>
    public ArabicPoints ArabicPointForIndex(int index)
    {
        if (index >= _offsetArabicPoints && index < _offsetMundanePoints)
        {
            return ArabicPoints.None.ArabicPointForIndex(index - _offsetArabicPoints);

        }
        throw new ArgumentException("PointMappings.ArabicPointForIndex(): index not in range for Arabic Point:  " + index);
    }

    /// <inheritdoc/>
    public MundanePoints MundanePointForIndex(int index)
    {
        if (index >= _offsetMundanePoints && index < _offsetCusps)
        {
            return MundanePoints.None.MundanePointForIndex(index - _offsetMundanePoints);

        }
        throw new ArgumentException("PointMappings.MundanePointForIndex(): index not in range for Mundane Point: " + index);
    }

    /// <inheritdoc/>
    public PointTypes PointTypeForIndex(int index)
    {
        if (index >= _offsetCelPoints)
        {
            if (index < _offsetZodiacPoints) return PointTypes.CelestialPoint;
            if (index < _offsetArabicPoints) return PointTypes.ZodiacalPoint;
            if (index < _offsetMundanePoints) return PointTypes.ArabicPoint;
            if (index < _offsetCusps) return PointTypes.MundaneSpecialPoint;
            if (index < 5000) return PointTypes.Cusp;
        }
        return PointTypes.None;
    }

}
