using Common;
using OpenQA.Selenium;
using System.Diagnostics;
using XUnitSolution.Drivers;
using XUnitSolution.PageObjects;

namespace XUnitSolution.Tests;

public class LanguageChangeTest : IDisposable
{
	private readonly IWebDriver driver;
	private readonly Stopwatch stopwatch;
	private readonly HomePage homePage;

	public LanguageChangeTest()
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
		stopwatch.Start();

		driver.Navigate().GoToUrl(url);
		homePage.ClickLanguageSwitcher();
		homePage.SelectLithuanianLanguage();

		Assert.Contains("https://lt.ehu.lt/", driver.Url);

		string headerText = homePage.GetHeaderText();
		Assert.Contains("Kodėl EHU?\r\nKas daro EHU unikaliu?", headerText);

		stopwatch.Stop();
		TestLogger.LogExecutionTime("XUnit, VerifyLanguageChangeToLithuanian", stopwatch);
	}

	public void Dispose()
	{
		WebDriverSingleton.QuitDriver();
	}
}
