
// Enigma Astrology Research.
// Jan Kampherbeek, (c) 2024.
// All Enigma software is open source.
// Please check the file copyright.txt in the root of the source for further details.

using Enigma.Core.Handlers;
using Enigma.Core.Persistency;

namespace Enigma.Test.Core.Handlers;

[TestFixture]
public class TestResourceBundleHandler
{
    private IResourceBundleHandler handler = new Enigma.Core.Handlers.TestResourceBundleHandler(new TextFileReader());


    [Test]
    public void TestRb()
    {
        string currentDir = AppDomain.CurrentDomain.BaseDirectory;
        string relativePath = currentDir + @"res\lang\rb-en.txt";
        Dictionary<string, string> rbItems = handler.GetAllTextsFromResourceBundle(relativePath);
        Assert.That(rbItems["test-line-1"], Is.EqualTo("First line for testing"));
    }
    
    
}