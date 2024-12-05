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
[Parallelizable]
[Category("LanguageChangeTest")]
public class LanguageChangeTest : BaseTest
{
	private IWebDriver driver = null!;
	private Stopwatch stopwatch = null!;
	private HomePage homePage = null!;

	[SetUp]
	public void Setup()
	{
		Log.Information("Initializing test setup for LanguageChangeTest...");
		driver = WebDriverSingleton.GetDriver();
		stopwatch = new Stopwatch();
		homePage = new HomePage(driver);
		Log.Information("Test setup completed.");
	}

	[Test]
	[TestCase("https://en.ehu.lt/")]
	public void VerifyLanguageChangeToLithuanian(string url)
	{
		Log.Information("Test 'VerifyLanguageChangeToLithuanian' started.");
		stopwatch.Start();

		try
		{
			Log.Information("Navigating to URL: {Url}", url);
			homePage.NavigateTo(url);

			Log.Information("Clicking on the language switcher.");
			homePage.ClickLanguageSwitcher();

			Log.Information("Selecting Lithuanian language.");
			homePage.SelectLithuanianLanguage();

			Log.Information("Validating the current URL.");
			homePage.GetCurrentUrl()
				.Should().Contain("https://lt.ehu.lt/", "the URL should switch to the Lithuanian version of the site after changing language.");

			Log.Information("Validating the header text.");
			homePage.GetHeaderText()
				.Should().Contain("Kodėl EHU?\r\nKas daro EHU unikaliu?", "the header text should match the Lithuanian version after changing language.");

			Log.Information("Assertions passed successfully for test 'VerifyLanguageChangeToLithuanian'.");
		}
		catch (Exception ex)
		{
			Log.Error(ex, "An error occurred during the test execution.");
			throw;
		}
		finally
		{
			stopwatch.Stop();
			Log.Information("NUnit, VerifyLanguageChangeToLithuanian", stopwatch.ElapsedMilliseconds);
		}
	}

	[TearDown]
	public void Teardown()
	{
		Log.Information("Tearing down the test environment for LanguageChangeTest.");
		WebDriverSingleton.QuitDriver();
		Log.Information("Test teardown completed.");
	}
}
