using ActionsImporter.Models;
using NUnit.Framework;

namespace ActionsImporter.UnitTests.Models;

[TestFixture]
public class StringExtensionsTests
{
    [TestCase("name with spaces", "\"name with spaces\"")]
    [TestCase("name-with-no-spaces", "name-with-no-spaces")]
    public void EscapeIfNeeded_ReturnsExpected(string input, string expected)
    {
        Assert.AreEqual(expected, input.EscapeIfNeeded());
    }
}
