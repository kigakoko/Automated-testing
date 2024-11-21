using Common;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Diagnostics;
using WebUITests.Drivers;

namespace WebUITests.Tests;

[TestFixture]
[Parallelizable]
[Category("SearchFunctionalityTest")]
public class SearchFunctionalityTest
{
	IWebDriver driver = null!;
	private Stopwatch stopwatch = null!;

	[SetUp]
	public void Setup()
	{
		driver = WebDriverManager.GetFirefoxDriver();
		driver.Manage().Window.Maximize();
		stopwatch = new Stopwatch();
	}

	[Test]
	[TestCase("https://en.ehu.lt/", "study programs", "search-filter__result-count")]
	public void VerifySearchFunctionality(string url, string searchTerm, string className)
	{
		stopwatch.Start();
		driver.Navigate().GoToUrl(url);

		string searchUrl = $"{url}?s={Uri.EscapeDataString(searchTerm)}";
		driver.Navigate().GoToUrl(searchUrl);

		Assert.That(driver.Url, Is.EqualTo(searchUrl));

		IWebElement searchResults = driver.FindElement(By.ClassName(className));
		Assert.That(searchResults.Text, Does.Contain("results found."));
		stopwatch.Stop();
		TestLogger.LogExecutionTime("NUnit, VerifySearchFunctionality", stopwatch);
	}

	[TearDown]
	public void Teardown()
	{
		driver.Quit();
	}
}
