using NUnit.Framework;
using OpenQA.Selenium;
using WebUITests.Drivers;

namespace WebUITests.Tests;

[TestFixture]
public class SearchFunctionalityTest
{
	IWebDriver driver = null!;

	[SetUp]
	public void Setup()
	{
		driver = WebDriverManager.GetFirefoxDriver();
		driver.Manage().Window.Maximize();
	}

	[Test]
	public void VerifySearchFunctionality()
	{
		driver.Navigate().GoToUrl("https://en.ehu.lt/");

		string searchTerm = "study programs";
		string searchUrl = $"https://en.ehu.lt/?s={Uri.EscapeDataString(searchTerm)}";
		driver.Navigate().GoToUrl(searchUrl);

		Assert.That(driver.Url, Is.EqualTo(searchUrl));

		IWebElement searchResults = driver.FindElement(By.ClassName("search-filter__result-count"));
		Assert.That(searchResults.Text, Does.Contain("results found."));
	}

	[TearDown]
	public void Teardown()
	{
		driver.Quit();
	}
}
