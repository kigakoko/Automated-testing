using Common;
using OpenQA.Selenium;
using System.Diagnostics;
using XUnitSolution.Drivers;

namespace XUnitSolution.Tests;

public class SearchFunctionalityTest : IDisposable
{
	private readonly IWebDriver driver;
	private readonly Stopwatch stopwatch;

	public SearchFunctionalityTest()
	{
		driver = WebDriverManager.GetFirefoxDriver();
		driver.Manage().Window.Maximize();
		stopwatch = new Stopwatch();
	}

	[Theory]
	[Trait("Category", "SearchFunctionalityTest")]
	[InlineData("https://en.ehu.lt/", "study programs", "search-filter__result-count")]
	public void VerifySearchFunctionality(string url, string searchTerm, string className)
	{
		stopwatch.Start();
		driver.Navigate().GoToUrl(url);

		string searchUrl = $"{url}?s={Uri.EscapeDataString(searchTerm)}";
		driver.Navigate().GoToUrl(searchUrl);

		Assert.Equal(searchUrl, driver.Url);

		IWebElement searchResults = driver.FindElement(By.ClassName(className));
		Assert.Contains("results found.", searchResults.Text);
		stopwatch.Stop();
		TestLogger.LogExecutionTime("XUnit, VerifySearchFunctionality", stopwatch);
	}

	public void Dispose()
	{
		driver.Quit();
	}
}
