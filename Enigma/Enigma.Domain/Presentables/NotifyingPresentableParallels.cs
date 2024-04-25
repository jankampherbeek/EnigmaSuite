// Enigma Astrology Research.
// Jan Kampherbeek, using an example from Gökhan Yu, (c) 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

namespace Enigma.Domain.Presentables;

using System.ComponentModel;

/// <summary>Wrapper class to support the sorting of parallels.</summary>
public sealed class NotifyingPresentableParallels: INotifyPropertyChanged
{
    
    private const double TOLERANCE = 0.00000001;
    private PresentableParallels _parallels;

    public NotifyingPresentableParallels(PresentableParallels parallels)
    {
      _parallels = parallels;
    }

  
    public string Point1Text
    {
      get => _parallels.Point1Text;
      set
      {
        if (_parallels.Point1Text == value) return;
        _parallels = _parallels with { Point1Text = value };
        OnPropertyChanged(nameof(Point1Text));
      }
    }

    public char Point1Glyph
    {
      get => _parallels.Point1Glyph;
      set
      {
        if (_parallels.Point1Glyph == value) return;
        _parallels = _parallels with { Point1Glyph = value };
        OnPropertyChanged(nameof(Point1Glyph));
      }
    }

    public char TypeGlyph
    {
      get => _parallels.TypeGlyph;
      set
      {
        if (_parallels.TypeGlyph == value) return;
        _parallels = _parallels with { TypeGlyph = value };
        OnPropertyChanged(nameof(TypeGlyph));
      }
    }
    
    public string Point2Text
    {
      get => _parallels.Point2Text;
      set
      {
        if (_parallels.Point2Text == value) return;
        _parallels = _parallels with { Point2Text = value };
        OnPropertyChanged(nameof(Point2Text));
      }
    }

    public char Point2Glyph
    {
      get => _parallels.Point2Glyph;
      set
      {
        if (_parallels.Point2Glyph == value) return;
        _parallels = _parallels with { Point2Glyph = value };
        OnPropertyChanged(nameof(Point2Glyph));
      }
    }

    public string OrbText
    {
      get => _parallels.OrbText;
      set
      {
        if (_parallels.OrbText == value) return;
        _parallels = _parallels with { OrbText = value };
        OnPropertyChanged(nameof(OrbText));
      }
    }

    public double OrbExactness
    {
      get => _parallels.OrbExactness;
      set
      {
        if (Math.Abs(_parallels.OrbExactness - value) < TOLERANCE) return;
        _parallels = _parallels with { OrbExactness = value };
        OnPropertyChanged(nameof(OrbExactness));
      }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged(string propertyName)
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
  }
