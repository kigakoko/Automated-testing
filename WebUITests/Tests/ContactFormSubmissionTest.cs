using AventStack.ExtentReports;
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
		Test ??= extent.CreateTest("VerifyContactPageInformation",
			$"Validates the email, phone number, and Facebook link on the contact page at: {url}");

		Log.Information("Test 'VerifyContactPageInformation' started.");
		stopwatch.Start();

		try
		{
			Test.Log(Status.Info, "Navigating to URL: " + url);
			contactPage.NavigateTo(url);

			Test.Log(Status.Info, "Validating email pattern.");
			contactPage.ContainsValidEmail(emailPattern)
				.Should().BeFalse("Email validation should fail on purpose for testing.");

			Test.Log(Status.Info, "Validating phone pattern.");
			contactPage.ContainsValidPhoneNumber(phonePattern)
				.Should().BeTrue("a valid phone number should be present on the page.");

			Test.Log(Status.Info, "Validating Facebook link.");
			contactPage.ContainsFacebookLink()
				.Should().BeTrue("the page should contain a link or reference to Facebook.");

			Test.Pass("All assertions passed successfully.");
		}
		catch (Exception ex)
		{
			Test.Fail("Test failed due to exception: " + ex.Message);
			Test.AddScreenCaptureFromPath(ExtentManager.CaptureScreenshot("VerifyContactPageInformation", driver));
			Log.Error(ex, "An error occurred during the test execution.");
			throw;
		}
		finally
		{
			stopwatch.Stop();
			Log.Information("Execution Time: {ElapsedMilliseconds}ms", stopwatch.ElapsedMilliseconds);
			Test.Log(Status.Info, $"Execution Time: {stopwatch.ElapsedMilliseconds}ms");
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
