using AventStack.ExtentReports;
using Common;
using NUnit.Framework;

namespace WebUITests.Tests.Base;

[SetUpFixture]
public class BaseTest
{
	protected static readonly ExtentReports extent = ExtentManager.GetInstance();
	private static readonly ThreadLocal<ExtentTest> threadLocalTest = new();

	protected static ExtentTest Test
	{
		get => threadLocalTest.Value!;
		set => threadLocalTest.Value = value;
	}

	[OneTimeSetUp]
    public void GlobalSetup()
    {
        LoggerSetup.ConfigureLogging();
	}

	[OneTimeTearDown]
	public void GlobalTeardown()
	{
		ExtentManager.FlushReport();
	}
}
