using Common;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Diagnostics;
using WebUITests.Drivers;
using WebUITests.PageObjects;

namespace WebUITests.Tests;

[TestFixture]
[Parallelizable]
[Category("LanguageChangeTest")]
public class LanguageChangeTest
{
	private IWebDriver driver = null!;
	private Stopwatch stopwatch = null!;
	private HomePage homePage = null!;

	[SetUp]
	public void Setup()
	{
		driver = WebDriverSingleton.GetDriver();
		stopwatch = new Stopwatch();
		homePage = new HomePage(driver);
	}

	[Test]
	[TestCase("https://en.ehu.lt/")]
	public void VerifyLanguageChangeToLithuanian(string url)
	{
		stopwatch.Start();

		homePage.NavigateTo(url);
		homePage.ClickLanguageSwitcher();
		homePage.SelectLithuanianLanguage();

		Assert.That(homePage.GetCurrentUrl(), Does.Contain("https://lt.ehu.lt/"));

		string headerText = homePage.GetHeaderText();
		Assert.That(headerText, Does.Contain("Kodėl EHU?\r\nKas daro EHU unikaliu?"));

		stopwatch.Stop();
		TestLogger.LogExecutionTime("NUnit, VerifyLanguageChangeToLithuanian", stopwatch);
	}

	[TearDown]
	public void Teardown()
	{
		WebDriverSingleton.QuitDriver();
	}
}
