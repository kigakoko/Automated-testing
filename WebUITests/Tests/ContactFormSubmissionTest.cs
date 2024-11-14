using NUnit.Framework;
using OpenQA.Selenium;
using System.Text.RegularExpressions;
using WebUITests.Drivers;

namespace WebUITests.Tests;

[TestFixture]
public class ContactPageInfoTest
{
	private IWebDriver driver = null!;

	[SetUp]
	public void Setup()
	{
		driver = WebDriverManager.GetFirefoxDriver();
		driver.Manage().Window.Maximize();
	}

	[Test]
	public void VerifyContactPageInformation()
	{
		driver.Navigate().GoToUrl("https://en.ehu.lt/contacts/");

		string pageSource = driver.PageSource;

		string emailPattern = @"[\w\.\-]+@[\w\-]+(\.[\w]{2,3})+";
		string phonePattern = @"\+\d{1,3}\s\d\s\d{3}\s\d{4}";

		var emailMatch = Regex.Match(pageSource, emailPattern);
		Assert.That(emailMatch.Success, Is.True, "No valid email address found on the page.");

		var phoneMatch = Regex.Match(pageSource, phonePattern);
		Assert.That(phoneMatch.Success, Is.True, "No valid phone number found on the page.");

		Assert.That(pageSource, Does.Contain("Facebook"), "Facebook link not found on the page.");
	}

	[TearDown]
	public void Teardown()
	{
		driver.Quit();
	}
}
