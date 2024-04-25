// Enigma Astrology Research.
// Gökhan Yu, (c) 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.ComponentModel;

namespace Enigma.Domain.Presentables
{

  /// <summary>Wrapper class to support sorting of aspects.</summary>
  public sealed class NotifyingPresentableAspects : INotifyPropertyChanged
  {
    private const double TOLERANCE = 0.00000001;
    private PresentableAspects _aspects;

    public NotifyingPresentableAspects(PresentableAspects aspects)
    {
      _aspects = aspects;
    }

    public string Point1Text
    {
      get => _aspects.Point1Text;
      set
      {
        if (_aspects.Point1Text == value) return;
        _aspects = _aspects with { Point1Text = value };
        OnPropertyChanged(nameof(Point1Text));
      }
    }

    public char Point1Glyph
    {
      get => _aspects.Point1Glyph;
      set
      {
        if (_aspects.Point1Glyph == value) return;
        _aspects = _aspects with { Point1Glyph = value };
        OnPropertyChanged(nameof(Point1Glyph));
      }
    }

    public string AspectText
    {
      get => _aspects.AspectText;
      set
      {
        if (_aspects.AspectText == value) return;
        _aspects = _aspects with { AspectText = value };
        OnPropertyChanged(nameof(AspectText));
      }
    }

    public char AspectGlyph
    {
      get => _aspects.AspectGlyph;
      set
      {
        if (_aspects.AspectGlyph == value) return;
        _aspects = _aspects with { AspectGlyph = value };
        OnPropertyChanged(nameof(AspectGlyph));
      }
    }

    public string Point2Text
    {
      get => _aspects.Point2Text;
      set
      {
        if (_aspects.Point2Text == value) return;
        _aspects = _aspects with { Point2Text = value };
        OnPropertyChanged(nameof(Point2Text));
      }
    }

    public char Point2Glyph
    {
      get => _aspects.Point2Glyph;
      set
      {
        if (_aspects.Point2Glyph == value) return;
        _aspects = _aspects with { Point2Glyph = value };
        OnPropertyChanged(nameof(Point2Glyph));
      }
    }

    public string OrbText
    {
      get => _aspects.OrbText;
      set
      {
        if (_aspects.OrbText == value) return;
        _aspects = _aspects with { OrbText = value };
        OnPropertyChanged(nameof(OrbText));
      }
    }

    public double OrbExactness
    {
      get => _aspects.OrbExactness;
      set
      {
        if (Math.Abs(_aspects.OrbExactness - value) < TOLERANCE) return;
        _aspects = _aspects with { OrbExactness = value };
        OnPropertyChanged(nameof(OrbExactness));
      }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged(string propertyName)
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
  }
}