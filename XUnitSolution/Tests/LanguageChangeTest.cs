using Common;
using OpenQA.Selenium;
using System.Diagnostics;
using XUnitSolution.Drivers;

namespace XUnitSolution.Tests;

public class LanguageChangeTest : IDisposable
{
	private readonly IWebDriver driver;
	private readonly Stopwatch stopwatch;

	public LanguageChangeTest()
	{
		driver = WebDriverManager.GetFirefoxDriver();
		driver.Manage().Window.Maximize();
		stopwatch = new Stopwatch();
	}

	[Theory]
	[Trait("Category", "LanguageChangeTest")]
	[InlineData("https://en.ehu.lt/", ".language-switcher", "LT")]
	public void VerifyLanguageChangeToLithuanian(string url, string selector, string language)
	{
		stopwatch.Start();
		driver.Navigate().GoToUrl(url);

		IWebElement languageSwitcher = driver.FindElement(By.CssSelector(selector));
		languageSwitcher.Click();

		IWebElement lithuanianOption = driver.FindElement(By.LinkText(language));
		lithuanianOption.Click();

		Assert.Contains("https://lt.ehu.lt/", driver.Url);

		IWebElement header = driver.FindElement(By.TagName("h1"));
		Assert.Contains("Kodėl EHU?\r\nKas daro EHU unikaliu?", header.Text);
		stopwatch.Stop();
		TestLogger.LogExecutionTime("XUnit, VerifyLanguageChangeToLithuanian", stopwatch);
	}

	public void Dispose()
	{
		driver.Quit();
	}
}
