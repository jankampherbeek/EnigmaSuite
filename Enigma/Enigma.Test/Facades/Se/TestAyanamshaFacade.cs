// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2023.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Api.Interfaces;
using Enigma.Domain.Calc.ChartItems;
using Enigma.Facades.Interfaces;
using Enigma.Facades.Se;
using Microsoft.Extensions.DependencyInjection;

namespace Enigma.Test.Facades.Se;

[TestFixture]
public class TestAyanamshaFacade  // TODO 0.1 Fix test for AyanamshaFacade
{
    private IAyanamshaFacade _facade;
    private readonly double _delta = 0.00000001;

    [Test]
    public void TestAyanamshaOffset()
  {
/*        string pathToSeFiles = "";                    // TODO 0.1 make path to CommonSE files configurable
        SeInitializer.SetEphePath(pathToSeFiles);
        SeInitializer.SetAyanamsha(Ayanamshas.Fagan);
        _facade = new AyanamshaFacade();

        double jdUt = 2434406.817713;
        double expectedOffset = 23.0;
        double ayanamshaOffset = _facade.GetAyanamshaOffset(jdUt);
        Assert.That(ayanamshaOffset, Is.EqualTo(expectedOffset).Within(_delta));
   */
        }
}