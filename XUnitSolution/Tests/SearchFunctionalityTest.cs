using FluentAssertions;
using OpenQA.Selenium;
using Serilog;
using System.Diagnostics;
using XUnitSolution.Drivers;
using XUnitSolution.PageObjects;
using XUnitSolution.Tests.Base;

namespace XUnitSolution.Tests;

public class SearchFunctionalityTest : BaseTest, IDisposable
{
	private readonly IWebDriver driver;
	private readonly Stopwatch stopwatch;
	private readonly SearchPage searchPage;

	public SearchFunctionalityTest() : base()
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
		Log.Information("Test 'VerifySearchFunctionality' started.");

		stopwatch.Start();

		try
		{
			Log.Information("Performing search with term '{SearchTerm}' on URL: {Url}", searchTerm, url);
			searchPage.Search(url, searchTerm);

			string expectedUrl = $"{url}?s={Uri.EscapeDataString(searchTerm)}";
			Log.Information("Validating the URL: {ExpectedUrl}", expectedUrl);
			driver.Url.Should().Be(expectedUrl, "the search should navigate to the expected URL with the search query");

			string resultsText = searchPage.GetSearchResultsText();
			Log.Information("Validating the results text: {ResultsText}", resultsText);
			resultsText.Should().Contain("results found.", "the search results should indicate the number of results found");

			Log.Information("Test 'VerifySearchFunctionality' passed successfully.");
		}
		catch (Exception ex)
		{
			Log.Error(ex, "An error occurred during the test execution.");
			throw;
		}
		finally
		{
			stopwatch.Stop();
			Log.Information("XUnit, VerifySearchFunctionality", stopwatch.ElapsedMilliseconds);
		}
	}

	public void Dispose()
	{
		Log.Information("Test teardown started.");
		WebDriverSingleton.QuitDriver();
		Log.Information("Test teardown completed.");
	}
}
