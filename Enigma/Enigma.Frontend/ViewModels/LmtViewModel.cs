// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Enigma.Frontend.Ui.Models;

namespace Enigma.Frontend.Ui.ViewModels;

public class LmtViewModel: INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    private LmtModel m_LmtModel;

    public string LmtDescription { get; set; } = "Enter offset for LMT";
    public LmtModel LmtValues
    {
        get { return m_LmtModel;  }
        set
        {
            m_LmtModel = value;
            OnPropertyChanged("LmtValues");
        }
    }
    
    
    public void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}