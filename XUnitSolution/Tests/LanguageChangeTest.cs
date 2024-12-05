using FluentAssertions;
using OpenQA.Selenium;
using Serilog;
using System.Diagnostics;
using XUnitSolution.Drivers;
using XUnitSolution.PageObjects;
using XUnitSolution.Tests.Base;

namespace XUnitSolution.Tests;

public class LanguageChangeTest : BaseTest, IDisposable
{
	private readonly IWebDriver driver;
	private readonly Stopwatch stopwatch;
	private readonly HomePage homePage;

	public LanguageChangeTest() : base()
	{
		driver = WebDriverSingleton.GetDriver();
		stopwatch = new Stopwatch();
		homePage = new HomePage(driver);
	}

	[Theory]
	[Trait("Category", "LanguageChangeTest")]
	[InlineData("https://en.ehu.lt/")]
	public void VerifyLanguageChangeToLithuanian(string url)
	{
		Log.Information("Test 'VerifyLanguageChangeToLithuanian' started.");

		stopwatch.Start();

		try
		{
			Log.Information("Navigating to URL: {Url}", url);
			driver.Navigate().GoToUrl(url);

			Log.Information("Clicking on the language switcher.");
			homePage.ClickLanguageSwitcher();

			Log.Information("Selecting Lithuanian language.");
			homePage.SelectLithuanianLanguage();

			Log.Information("Validating the URL contains the Lithuanian version.");
			driver.Url.Should().Contain("https://lt.ehu.lt/", "the language switch should redirect to the Lithuanian version of the site");

			string headerText = homePage.GetHeaderText();
			Log.Information("Validating the header text: {HeaderText}", headerText);
			headerText.Should().Contain("Kodėl EHU?\r\nKas daro EHU unikaliu?", "the header text should reflect the Lithuanian language");

			Log.Information("Test 'VerifyLanguageChangeToLithuanian' passed successfully.");
		}
		catch (Exception ex)
		{
			Log.Error(ex, "An error occurred during the test execution.");
			throw;
		}
		finally
		{
			stopwatch.Stop();
			Log.Information("XUnit, VerifyLanguageChangeToLithuanian", stopwatch.ElapsedMilliseconds);
		}
	}

	public void Dispose()
	{
		Log.Information("Test teardown started.");
		WebDriverSingleton.QuitDriver();
		Log.Information("Test teardown completed.");
	}
}
