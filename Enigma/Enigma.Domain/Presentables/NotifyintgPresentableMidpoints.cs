// Enigma Astrology Research.
// Jan Kampherbeek, using an example from Gökhan Yu, (c) 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Domain.Presentables;

using System.ComponentModel;

/// <summary>Wrapper class to support the sorting of midpoints.</summary>
public sealed class NotifyingPresentableMidpoints: INotifyPropertyChanged
{
    
    private const double TOLERANCE = 0.00000001;
    private PresentableOccupiedMidpoint _midpoints;

    public NotifyingPresentableMidpoints(PresentableOccupiedMidpoint midpoints)
    {
      _midpoints = midpoints;
    }

    
    
    
    
    //    public PresentableOccupiedMidpoint(char point1Glyph, char point2Glyph, char pointOccGlyph, string orbText, double orbExactness)
  
    public char Point1Glyph
    {
      get => _midpoints.Point1Glyph;
      set
      {
        if (_midpoints.Point1Glyph == value) return;
        _midpoints = _midpoints with { Point1Glyph = value };
        OnPropertyChanged(nameof(Point1Glyph));
      }
    }

    public char Point2Glyph
    {
      get => _midpoints.Point2Glyph;
      set
      {
        if (_midpoints.Point2Glyph == value) return;
        _midpoints = _midpoints with { Point2Glyph = value };
        OnPropertyChanged(nameof(Point2Glyph));
      }
    }

    public char PointOccGlyph
    {
      get => _midpoints.PointOccGlyph;
      set
      {
        if (_midpoints.PointOccGlyph == value) return;
        _midpoints = _midpoints with { PointOccGlyph = value };
        OnPropertyChanged(nameof(PointOccGlyph));
      }
    }
    
    public string OrbText
    {
      get => _midpoints.OrbText;
      set
      {
        if (_midpoints.OrbText == value) return;
        _midpoints = _midpoints with { OrbText = value };
        OnPropertyChanged(nameof(OrbText));
      }
    }

    public double OrbExactness
    {
      get => _midpoints.OrbExactness;
      set
      {
        if (Math.Abs(_midpoints.OrbExactness - value) < TOLERANCE) return;
        _midpoints = _midpoints with { OrbExactness = value };
        OnPropertyChanged(nameof(OrbExactness));
      }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged(string propertyName)
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
  }
