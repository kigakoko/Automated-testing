using Common;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Diagnostics;
using WebUITests.Drivers;
using WebUITests.PageObjects;

namespace WebUITests.Tests;

[TestFixture]
[Parallelizable]
[Category("ContactPageInfoTest")]
public class ContactPageInfoTest
{
	private IWebDriver driver = null!;
	private Stopwatch stopwatch = null!;
	private ContactPage contactPage = null!;

	[SetUp]
	public void Setup()
	{
		driver = WebDriverSingleton.GetDriver();
		stopwatch = new Stopwatch();
		contactPage = new ContactPage(driver);
	}

	[Test]
	[TestCase("https://en.ehu.lt/contacts/", @"[\w\.\-]+@[\w\-]+(\.[\w]{2,3})+", @"\+\d{1,3}\s\d\s\d{3}\s\d{4}")]
	public void VerifyContactPageInformation(string url, string emailPattern, string phonePattern)
	{
		stopwatch.Start();

		contactPage.NavigateTo(url);

		Assert.That(contactPage.ContainsValidEmail(emailPattern), Is.True, "No valid email address found on the page.");
		Assert.That(contactPage.ContainsValidPhoneNumber(phonePattern), Is.True, "No valid phone number found on the page.");
		Assert.That(contactPage.ContainsFacebookLink(), Is.True, "Facebook link not found on the page.");

		stopwatch.Stop();
		TestLogger.LogExecutionTime("NUnit, VerifyContactPageInformation", stopwatch);
	}

	[TearDown]
	public void Teardown()
	{
		WebDriverSingleton.QuitDriver();
	}
}
