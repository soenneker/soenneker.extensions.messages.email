using Soenneker.Tests.HostedUnit;

namespace Soenneker.Extensions.Messages.Email.Tests;

[ClassDataSource<Host>(Shared = SharedType.PerTestSession)]
public class EmailMessagesExtensionTests : HostedUnitTest
{
    public EmailMessagesExtensionTests(Host host) : base(host)
    {
    }

    [Test]
    public void Default()
    {

    }
}
