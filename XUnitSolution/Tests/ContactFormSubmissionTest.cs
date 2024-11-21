using Common;
using OpenQA.Selenium;
using System.Diagnostics;
using System.Text.RegularExpressions;
using XUnitSolution.Drivers;

namespace XUnitSolution.Tests;

public class ContactPageInfoTest : IDisposable
{
	private readonly IWebDriver driver;
	private readonly Stopwatch stopwatch;

	public ContactPageInfoTest()
	{
		driver = WebDriverManager.GetFirefoxDriver();
		driver.Manage().Window.Maximize();
		stopwatch = new Stopwatch();
	}

	[Theory]
	[Trait("Category", "ContactPageInfoTest")]
	[InlineData("https://en.ehu.lt/contacts/", @"[\w\.\-]+@[\w\-]+(\.[\w]{2,3})+", @"\+\d{1,3}\s\d\s\d{3}\s\d{4}")]
	public void VerifyContactPageInformation(string url, string emailPattern, string phonePattern)
	{
		stopwatch.Start();
		driver.Navigate().GoToUrl(url);

		string pageSource = driver.PageSource;

		var emailMatch = Regex.Match(pageSource, emailPattern);
		Assert.True(emailMatch.Success, "No valid email address found on the page.");

		var phoneMatch = Regex.Match(pageSource, phonePattern);
		Assert.True(phoneMatch.Success, "No valid phone number found on the page.");

		Assert.Contains("Facebook", pageSource, StringComparison.OrdinalIgnoreCase);
		stopwatch.Stop();
		TestLogger.LogExecutionTime("XUnit, VerifyContactPageInformation", stopwatch);
	}

	public void Dispose()
	{
		driver.Quit();
	}
}
