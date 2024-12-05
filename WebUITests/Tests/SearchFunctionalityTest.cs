using Common;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Diagnostics;
using WebUITests.Drivers;
using WebUITests.PageObjects;

namespace WebUITests.Tests;

[TestFixture]
[Parallelizable]
[Category("SearchFunctionalityTest")]
public class SearchFunctionalityTest
{
	private IWebDriver driver = null!;
	private Stopwatch stopwatch = null!;
	private SearchPage searchPage = null!;

	[SetUp]
	public void Setup()
	{
		driver = WebDriverSingleton.GetDriver();
		stopwatch = new Stopwatch();
	}

	[Test]
	[TestCase("https://en.ehu.lt/", "study programs", "search-filter__result-count")]
	public void VerifySearchFunctionality(string url, string searchTerm, string className)
	{
		stopwatch.Start();

		searchPage = new SearchPage(driver, className);
		searchPage.NavigateTo(url);
		searchPage.Search(url, searchTerm);

		Assert.That(searchPage.GetCurrentUrl(), Is.EqualTo($"{url}?s={Uri.EscapeDataString(searchTerm)}"));
		Assert.That(searchPage.GetSearchResultsText(), Does.Contain("results found."));

		stopwatch.Stop();
		TestLogger.LogExecutionTime("NUnit, VerifySearchFunctionality", stopwatch);
	}

	[TearDown]
	public void Teardown()
	{
		WebDriverSingleton.QuitDriver();
	}
}
