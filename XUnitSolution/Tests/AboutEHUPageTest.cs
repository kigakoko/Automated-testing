using Common;
using OpenQA.Selenium;
using System.Diagnostics;
using XUnitSolution.Drivers;
using XUnitSolution.PageObjects;

namespace XUnitSolution.Tests;

public class AboutEHUPageTest : IDisposable
{
	private readonly IWebDriver driver;
	private readonly Stopwatch stopwatch;
	private readonly AboutPage aboutPage;

	public AboutEHUPageTest()
	{
		driver = WebDriverSingleton.GetDriver();
		stopwatch = new Stopwatch();
		aboutPage = new AboutPage(driver);
	}

	[Theory]
	[Trait("Category", "AboutEHUPageTest")]
	[InlineData("https://en.ehu.lt/", "About")]
	public void VerifyNavigationToAboutEHUPage(string url, string text)
	{
		stopwatch.Start();

		driver.Navigate().GoToUrl(url);
		aboutPage.ClickAboutLink();

		Assert.Equal(text, driver.Title);
		Assert.Contains(text, aboutPage.GetHeaderText());

		stopwatch.Stop();
		TestLogger.LogExecutionTime("XUnit, VerifyNavigationToAboutEHUPage", stopwatch);
	}

	public void Dispose()
	{
		WebDriverSingleton.QuitDriver();
	}
}
