using Common;
using FluentAssertions;
using OpenQA.Selenium;
using Serilog;
using System.Diagnostics;
using XUnitSolution.Drivers;
using XUnitSolution.PageObjects;
using XUnitSolution.Tests.Base;

namespace XUnitSolution.Tests;

public class AboutEHUPageTest : BaseTest, IDisposable
{
	private readonly IWebDriver driver;
	private readonly Stopwatch stopwatch;
	private readonly AboutPage aboutPage;

	public AboutEHUPageTest() : base()
	{
		LoggerSetup.ConfigureLogging();
		driver = WebDriverSingleton.GetDriver();
		stopwatch = new Stopwatch();
		aboutPage = new AboutPage(driver);
	}

	[Theory]
	[Trait("Category", "AboutEHUPageTest")]
	[InlineData("https://en.ehu.lt/", "About")]
	public void VerifyNavigationToAboutEHUPage(string url, string text)
	{
		Log.Information("Test 'VerifyNavigationToAboutEHUPage' started.");
		stopwatch.Start();

		try
		{
			Log.Information("Navigating to URL: {Url}", url);
			driver.Navigate().GoToUrl(url);

			Log.Information("Clicking 'About' link.");
			aboutPage.ClickAboutLink();

			Log.Information("Validating page title is '{ExpectedTitle}'.", text);
			driver.Title
				.Should().Be(text, because: "the page title should match the expected 'About' text after navigation");

			Log.Information("Validating header text contains '{ExpectedText}'.", text);
			aboutPage.GetHeaderText()
				.Should().Contain(text, because: "the header text should contain the expected 'About' text after clicking the link");

			Log.Information("Test 'VerifyNavigationToAboutEHUPage' passed successfully.");
		}
		catch (Exception ex)
		{
			Log.Error(ex, "An error occurred during the test execution.");
			throw;
		}
		finally
		{
			stopwatch.Stop();
			Log.Information("XUnit, VerifyNavigationToAboutEHUPage", stopwatch.ElapsedMilliseconds);
		}
	}

	public void Dispose()
	{
		Log.Information("Test teardown started.");
		WebDriverSingleton.QuitDriver();
		Log.Information("Test teardown completed.");
	}
}
