using Common;
using OpenQA.Selenium;
using System.Diagnostics;
using XUnitSolution.Drivers;
using XUnitSolution.PageObjects;

namespace XUnitSolution.Tests;

public class ContactPageInfoTest : IDisposable
{
	private readonly IWebDriver driver;
	private readonly Stopwatch stopwatch;
	private readonly ContactPage contactPage;

	public ContactPageInfoTest()
	{
		driver = WebDriverSingleton.GetDriver();
		stopwatch = new Stopwatch();
		contactPage = new ContactPage(driver);
	}

	[Theory]
	[Trait("Category", "ContactPageInfoTest")]
	[InlineData("https://en.ehu.lt/contacts/", @"[\w\.\-]+@[\w\-]+(\.[\w]{2,3})+", @"\+\d{1,3}\s\d\s\d{3}\s\d{4}")]
	public void VerifyContactPageInformation(string url, string emailPattern, string phonePattern)
	{
		stopwatch.Start();

		driver.Navigate().GoToUrl(url);

		Assert.True(contactPage.ContainsValidEmail(emailPattern), "No valid email address found on the page.");
		Assert.True(contactPage.ContainsValidPhoneNumber(phonePattern), "No valid phone number found on the page.");
		Assert.True(contactPage.ContainsFacebookLink(), "Facebook link not found on the page.");

		stopwatch.Stop();
		TestLogger.LogExecutionTime("XUnit, VerifyContactPageInformation", stopwatch);
	}

	public void Dispose()
	{
		WebDriverSingleton.QuitDriver();
	}
}
