using Soenneker.Tests.FixturedUnit;
using Xunit;

namespace Soenneker.Extensions.Messages.Email.Tests;

[Collection("Collection")]
public class EmailMessagesExtensionTests : FixturedUnitTest
{
    public EmailMessagesExtensionTests(Fixture fixture, ITestOutputHelper output) : base(fixture, output)
    {
    }

    [Fact]
    public void Default()
    {

    }
}
