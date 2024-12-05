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
[Category("AboutEHUPageTest")]
[Parallelizable]
public class AboutEHUPageTest : BaseTest
{
	private IWebDriver driver = null!;
	private Stopwatch stopwatch = null!;
	private AboutPage aboutPage = null!;

	[SetUp]
	public void Setup()
	{
		Log.Information("Initializing test setup for AboutEHUPageTest...");
		driver = WebDriverSingleton.GetDriver();
		stopwatch = new Stopwatch();
		aboutPage = new AboutPage(driver);
		Log.Information("Test setup completed.");
	}

	[Test]
	[TestCase("https://en.ehu.lt/", "About")]
	public void VerifyNavigationToAboutEHUPage(string url, string text)
	{
		Log.Information("Test 'VerifyNavigationToAboutEHUPage' started.");
		stopwatch.Start();

		try
		{
			Log.Information("Navigating to URL: {Url}", url);
			aboutPage.NavigateTo(url);

			Log.Information("Clicking 'About' link.");
			aboutPage.ClickAboutLink();

			Log.Information("Validating page title is '{ExpectedTitle}'.", text);
			aboutPage.GetPageTitle()
				.Should().Be(text, because: "the page title should match the 'About' section.");

			Log.Information("Validating header text contains '{ExpectedText}'.", text);
			aboutPage.GetHeaderText()
				.Should().Contain(text, because: "the header text should contain the expected value.");

			Log.Information("Assertions passed successfully for test 'VerifyNavigationToAboutEHUPage'.");
		}
		catch (Exception ex)
		{
			Log.Error(ex, "An error occurred during the test execution.");
			throw;
		}
		finally
		{
			stopwatch.Stop();
			Log.Information("NUnit, VerifyNavigationToAboutEHUPage", stopwatch.ElapsedMilliseconds);
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
