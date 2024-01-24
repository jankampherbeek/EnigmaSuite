// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using System.Diagnostics;
using System.Windows;
using Enigma.Domain.Constants;

namespace Enigma.Frontend.Ui.Support;

// see discussion at: https://stackoverflow.com/questions/4580263/how-to-open-in-default-browser-in-c-sharp
public class UserManual
{
    public void ShowUserManual()
    {
        try
        {
            Process.Start("explorer", EnigmaConstants.USER_MANUAL);
        }
        catch (System.ComponentModel.Win32Exception noBrowser)
        {
            if (noBrowser.ErrorCode==-2147467259)
                MessageBox.Show("Could not find a browser.");
        }
        catch (System.Exception other)
        {
            MessageBox.Show("Could not access user manual.");
        }
    }
    
}