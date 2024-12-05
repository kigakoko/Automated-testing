using Common;
using OpenQA.Selenium;
using System.Diagnostics;
using XUnitSolution.Drivers;
using XUnitSolution.PageObjects;

namespace XUnitSolution.Tests;

public class SearchFunctionalityTest : IDisposable
{
	private readonly IWebDriver driver;
	private readonly Stopwatch stopwatch;
	private readonly SearchPage searchPage;

	public SearchFunctionalityTest()
	{
		driver = WebDriverSingleton.GetDriver();
		stopwatch = new Stopwatch();
		searchPage = new SearchPage(driver, "search-filter__result-count");
	}

	[Theory]
	[Trait("Category", "SearchFunctionalityTest")]
	[InlineData("https://en.ehu.lt/", "study programs")]
	public void VerifySearchFunctionality(string url, string searchTerm)
	{
		stopwatch.Start();

		searchPage.Search(url, searchTerm);

		string searchUrl = $"{url}?s={Uri.EscapeDataString(searchTerm)}";
		Assert.Equal(searchUrl, driver.Url);

		string resultsText = searchPage.GetSearchResultsText();
		Assert.Contains("results found.", resultsText);

		stopwatch.Stop();
		TestLogger.LogExecutionTime("XUnit, VerifySearchFunctionality", stopwatch);
	}

	public void Dispose()
	{
		WebDriverSingleton.QuitDriver();
	}
}
