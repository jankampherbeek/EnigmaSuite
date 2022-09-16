// Jan Kampherbeek, (c) 2022.
// Enigma Research is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Domain.Constants;

public interface IConfigCheck
{
    public bool DoesConfigExist();
}


public class ConfigCheck : IConfigCheck
{
    public bool DoesConfigExist()
    {
        return File.Exists(EnigmaConstants.CONFIG_LOCATION);
    }
}