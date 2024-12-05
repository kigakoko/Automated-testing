using Common;
using NUnit.Framework;

namespace WebUITests.Tests.Base;

[SetUpFixture]
public class BaseTest
{
    [OneTimeSetUp]
    public void GlobalSetup()
    {
        LoggerSetup.ConfigureLogging();
    }
}
