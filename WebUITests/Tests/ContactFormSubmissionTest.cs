using Common;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Diagnostics;
using System.Text.RegularExpressions;
using WebUITests.Drivers;

namespace WebUITests.Tests;

[TestFixture]
[Parallelizable]
[Category("ContactPageInfoTest")]
public class ContactPageInfoTest
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
	[TestCase("https://en.ehu.lt/contacts/", @"[\w\.\-]+@[\w\-]+(\.[\w]{2,3})+", @"\+\d{1,3}\s\d\s\d{3}\s\d{4}")]
	public void VerifyContactPageInformation(string url, string emailPattern, string phonePattern)
	{
		stopwatch.Start();
		driver.Navigate().GoToUrl(url);

		string pageSource = driver.PageSource;

		var emailMatch = Regex.Match(pageSource, emailPattern);
		Assert.That(emailMatch.Success, Is.True, "No valid email address found on the page.");

		var phoneMatch = Regex.Match(pageSource, phonePattern);
		Assert.That(phoneMatch.Success, Is.True, "No valid phone number found on the page.");

		Assert.That(pageSource, Does.Contain("Facebook"), "Facebook link not found on the page.");
		stopwatch.Stop();
		TestLogger.LogExecutionTime("NUnit, VerifyContactPageInformation", stopwatch);
	}

	[TearDown]
	public void Teardown()
	{
		driver.Quit();
	}
}
