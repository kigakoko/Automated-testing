using FluentAssertions;
using OpenQA.Selenium;
using Serilog;
using System.Diagnostics;
using XUnitSolution.Drivers;
using XUnitSolution.PageObjects;
using XUnitSolution.Tests.Base;

namespace XUnitSolution.Tests;

public class ContactPageInfoTest : BaseTest, IDisposable
{
	private readonly IWebDriver driver;
	private readonly Stopwatch stopwatch;
	private readonly ContactPage contactPage;

	public ContactPageInfoTest() : base()
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
		Log.Information("Test 'VerifyContactPageInformation' started.");

		stopwatch.Start();

		try
		{
			Log.Information("Navigating to URL: {Url}", url);
			driver.Navigate().GoToUrl(url);

			Log.Information("Validating the presence of a valid email address on the contact page.");
			contactPage.ContainsValidEmail(emailPattern)
				.Should().BeTrue("a valid email address should be present on the contact page");

			Log.Information("Validating the presence of a valid phone number on the contact page.");
			contactPage.ContainsValidPhoneNumber(phonePattern)
				.Should().BeTrue("a valid phone number should be present on the contact page");

			Log.Information("Validating the presence of a Facebook link on the contact page.");
			contactPage.ContainsFacebookLink()
				.Should().BeTrue("a link to Facebook should be present on the contact page");

			Log.Information("Test 'VerifyContactPageInformation' passed successfully.");
		}
		catch (Exception ex)
		{
			Log.Error(ex, "An error occurred during the test execution.");
			throw;
		}
		finally
		{
			stopwatch.Stop();
			Log.Information("XUnit, VerifyContactPageInformation", stopwatch.ElapsedMilliseconds);
		}
	}

	public void Dispose()
	{
		Log.Information("Test teardown started.");
		WebDriverSingleton.QuitDriver();
		Log.Information("Test teardown completed.");
	}
}
