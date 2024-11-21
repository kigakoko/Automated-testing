using Common;
using OpenQA.Selenium;
using System.Diagnostics;
using XUnitSolution.Drivers;

namespace XUnitSolution.Tests;

public class AboutEHUPageTest : IDisposable
{
	private readonly IWebDriver driver;
	private readonly Stopwatch stopwatch;

	public AboutEHUPageTest()
	{
		driver = WebDriverManager.GetFirefoxDriver();
		driver.Manage().Window.Maximize();
		stopwatch = new Stopwatch();
	}

	[Theory]
	[Trait("Category", "AboutEHUPageTest")]
	[InlineData("https://en.ehu.lt/", "About")]
	public void VerifyNavigationToAboutEHUPage(string url, string text)
	{
		stopwatch.Start();
		driver.Navigate().GoToUrl(url);

		IWebElement aboutLink = driver.FindElement(By.LinkText(text));
		aboutLink.Click();

		Assert.Equal(text, driver.Title);

		IWebElement header = driver.FindElement(By.TagName("h1"));
		Assert.Contains(text, header.Text);
		stopwatch.Stop();
		TestLogger.LogExecutionTime("XUnit, VerifyNavigationToAboutEHUPage", stopwatch);
	}

	public void Dispose()
	{
		driver.Quit();
	}
}
