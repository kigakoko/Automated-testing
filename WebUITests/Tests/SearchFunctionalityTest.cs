using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium;
using Serilog;
using System.Diagnostics;
using WebUITests.Drivers;
using WebUITests.PageObjects;
using WebUITests.Tests.Base;

namespace WebUITests.Tests
{
    [TestFixture]
	[Parallelizable]
	[Category("SearchFunctionalityTest")]
	public class SearchFunctionalityTest : BaseTest
	{
		private IWebDriver driver = null!;
		private Stopwatch stopwatch = null!;
		private SearchPage searchPage = null!;

		[SetUp]
		public void Setup()
		{
			Log.Information("Initializing test setup for SearchFunctionalityTest...");
			driver = WebDriverSingleton.GetDriver();
			stopwatch = new Stopwatch();
			Log.Information("Test setup completed.");
		}

		[Test]
		[TestCase("https://en.ehu.lt/", "study programs", "search-filter__result-count")]
		public void VerifySearchFunctionality(string url, string searchTerm, string className)
		{
			Log.Information("Test 'VerifySearchFunctionality' started.");
			stopwatch.Start();

			try
			{
				Log.Information("Navigating to URL: {Url}", url);
				searchPage = new SearchPage(driver, className);
				searchPage.NavigateTo(url);

				Log.Information("Performing search with term: {SearchTerm}", searchTerm);
				searchPage.Search(url, searchTerm);

				Log.Information("Validating that the current URL contains the search term.");
				searchPage.GetCurrentUrl()
					.Should().Be($"{url}?s={Uri.EscapeDataString(searchTerm)}", "the URL should include the search query after performing the search");

				Log.Information("Validating search results text.");
				searchPage.GetSearchResultsText()
					.Should().Contain("results found.", "search results should indicate that results were found on the page");

				Log.Information("Assertions passed successfully for test 'VerifySearchFunctionality'.");
			}
			catch (Exception ex)
			{
				Log.Error(ex, "An error occurred during the test execution.");
				throw;
			}
			finally
			{
				stopwatch.Stop();
				Log.Information("NUnit, VerifySearchFunctionality", stopwatch.ElapsedMilliseconds);
			}
		}

		[TearDown]
		public void Teardown()
		{
			Log.Information("Tearing down the test environment for SearchFunctionalityTest.");
			WebDriverSingleton.QuitDriver();
			Log.Information("Test teardown completed.");
		}
	}
}
