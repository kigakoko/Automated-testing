using Common;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Diagnostics;
using WebUITests.Drivers;
using WebUITests.PageObjects;

namespace WebUITests.Tests;

[TestFixture]
[Category("AboutEHUPageTest")]
[Parallelizable]
public class AboutEHUPageTest
{
	private IWebDriver driver = null!;
	private Stopwatch stopwatch = null!;
	private AboutPage aboutPage = null!;

	[SetUp]
	public void Setup()
	{
		driver = WebDriverSingleton.GetDriver();
		stopwatch = new Stopwatch();
		aboutPage = new AboutPage(driver);
	}

	[Test]
	[TestCase("https://en.ehu.lt/", "About")]
	public void VerifyNavigationToAboutEHUPage(string url, string text)
	{
		stopwatch.Start();

		aboutPage.NavigateTo(url);
		aboutPage.ClickAboutLink();

		Assert.That(aboutPage.GetPageTitle(), Is.EqualTo(text));
		Assert.That(aboutPage.GetHeaderText(), Does.Contain(text));

		stopwatch.Stop();
		TestLogger.LogExecutionTime("NUnit, VerifyNavigationToAboutEHUPage", stopwatch);
	}

	[TearDown]
	public void Teardown()
	{
		WebDriverSingleton.QuitDriver();
	}
}
