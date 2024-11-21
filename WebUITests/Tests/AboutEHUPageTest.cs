using Common;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Diagnostics;
using WebUITests.Drivers;

namespace WebUITests.Tests;

[TestFixture]
[Category("AboutEHUPageTest")]
[Parallelizable]
public class AboutEHUPageTest
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
	[TestCase("https://en.ehu.lt/", "About")]
	public void VerifyNavigationToAboutEHUPage(string url, string text)
	{
		stopwatch.Start();
		driver.Navigate().GoToUrl(url);

		IWebElement aboutLink = driver.FindElement(By.LinkText(text));
		aboutLink.Click();

		Assert.That(driver.Title, Is.EqualTo(text));

		IWebElement header = driver.FindElement(By.TagName("h1"));
		Assert.That(header.Text, Does.Contain(text));
		stopwatch.Stop();
		TestLogger.LogExecutionTime("NUnit, VerifyNavigationToAboutEHUPage", stopwatch);
	}

	[TearDown]
	public void Teardown()
	{
		driver.Quit();
	}
}
