using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium;
using Serilog;
using System.Diagnostics;
using WebUITests.Drivers;
using WebUITests.PageObjects;
using WebUITests.Tests.Base;

namespace WebUITests.Tests;

[TestFixture]
[Category("ContactPageInfoTest")]
[Parallelizable]
public class ContactPageInfoTest : BaseTest
{
	private IWebDriver driver = null!;
	private Stopwatch stopwatch = null!;
	private ContactPage contactPage = null!;

	[SetUp]
	public void Setup()
	{
		Log.Information("Initializing test setup for ContactPageInfoTest...");
		driver = WebDriverSingleton.GetDriver();
		stopwatch = new Stopwatch();
		contactPage = new ContactPage(driver);
		Log.Information("Test setup completed.");
	}

	[Test]
	[TestCase("https://en.ehu.lt/contacts/", @"[\w\.\-]+@[\w\-]+(\.[\w]{2,3})+", @"\+\d{1,3}\s\d\s\d{3}\s\d{4}")]
	public void VerifyContactPageInformation(string url, string emailPattern, string phonePattern)
	{
		Log.Information("Test 'VerifyContactPageInformation' started.");
		stopwatch.Start();

		try
		{
			Log.Information("Navigating to URL: {Url}", url);
			contactPage.NavigateTo(url);

			Log.Information("Validating email pattern.");
			contactPage.ContainsValidEmail(emailPattern)
				.Should().BeTrue("a valid email address should be present on the page.");

			Log.Information("Validating phone pattern.");
			contactPage.ContainsValidPhoneNumber(phonePattern)
				.Should().BeTrue("a valid phone number should be present on the page.");

			Log.Information("Validating Facebook link.");
			contactPage.ContainsFacebookLink()
				.Should().BeTrue("the page should contain a link or reference to Facebook.");

			Log.Information("Assertions passed successfully for test 'VerifyContactPageInformation'.");
		}
		catch (Exception ex)
		{
			Log.Error(ex, "An error occurred during the test execution.");
			throw;
		}
		finally
		{
			stopwatch.Stop();
			Log.Information("NUnit, VerifyContactPageInformation", stopwatch.ElapsedMilliseconds);
		}
	}

	[TearDown]
	public void Teardown()
	{
		Log.Information("Tearing down the test environment.");
		WebDriverSingleton.QuitDriver();
		Log.Information("Test teardown completed.");
	}
}
