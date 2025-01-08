using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium;
using Serilog;
using System.Diagnostics;
using WebUITests.Drivers;
using WebUITests.PageObjects;
using WebUITests.Tests.Base;

namespace WebUITests.Tests;

[TestFixture]
[Category("AboutEhuPageTest")]
[Parallelizable]
public class AboutEhuPageTest : BaseTest
{
	private IWebDriver driver = null!;
	private Stopwatch stopwatch = null!;
	private AboutPage aboutPage = null!;

	[SetUp]
	public void Setup()
	{
		Log.Information("Initializing test setup for AboutEhuPageTest...");
		driver = WebDriverSingleton.GetDriver();
		stopwatch = new Stopwatch();
		aboutPage = new AboutPage(driver);
	}

	[Test]
	[TestCase("https://en.ehu.lt/", "About")]
	public void VerifyNavigationToAboutEHUPage(string url, string text)
	{
		Test ??= extent.CreateTest("VerifyNavigationToAboutEHUPage",
			$"Validates navigation and title/header on the About page with URL: {url}");

		Log.Information("Test 'VerifyNavigationToAboutEHUPage' started.");
		stopwatch.Start();

		try
		{
			Test.Log(AventStack.ExtentReports.Status.Info, "Navigating to URL: " + url);
			aboutPage.NavigateTo(url);

			Test.Log(AventStack.ExtentReports.Status.Info, "Clicking 'About' link.");
			aboutPage.ClickAboutLink();

			Test.Log(AventStack.ExtentReports.Status.Info, "Validating page title and header.");
			aboutPage.GetPageTitle()
				.Should().Be(text, because: "the page title should match the 'About' section.");
			aboutPage.GetHeaderText()
				.Should().Contain(text, because: "the header text should contain the expected value.");

			Test.Pass("All assertions passed successfully.");
		}
		catch (Exception ex)
		{
			Test.Fail("Test failed due to exception: " + ex.Message);
			Test.AddScreenCaptureFromPath(ExtentManager.CaptureScreenshot("VerifyNavigationToAboutEHUPage", driver));
			Log.Error(ex, "An error occurred during the test execution.");
			throw;
		}
		finally
		{
			stopwatch.Stop();
			Log.Information("Execution Time: {ElapsedMilliseconds}ms", stopwatch.ElapsedMilliseconds);
			Test.Log(AventStack.ExtentReports.Status.Info, $"Execution Time: {stopwatch.ElapsedMilliseconds}ms");
		}
	}

	[TearDown]
	public void Teardown()
	{
		Log.Information("Tearing down the test environment.");
		WebDriverSingleton.QuitDriver();
		Log.Information("Test teardown completed.");
	}
}
