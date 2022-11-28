// Jan Kampherbeek, (c) 2022.
// Enigma is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Facades.Se;

namespace Enigma.Core.Handlers.Calc;

public class SeHandler: ISeHandler
{
    public void SetupSe(string pathToSeFiles)
    {
        SeInitializer.SetEphePath(pathToSeFiles);
    }

}
