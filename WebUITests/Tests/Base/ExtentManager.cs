using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports;
using OpenQA.Selenium;

namespace WebUITests.Tests.Base;

public static class ExtentManager
{
	private static ExtentReports _extent = null!;

	private static readonly string BaseDirectory = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\..");

	public static ExtentReports GetInstance()
	{
		if (_extent == null)
		{
			var reportDirectory = GetReportDirectory();

			var sparkReporter = new ExtentSparkReporter(Path.Combine(reportDirectory, "ExtentReport.html"))
			{
				Config =
				{
					DocumentTitle = "Automation Test Report",
					ReportName = "EHU Web Tests",
				}
			};

			_extent = new ExtentReports();
			_extent.AttachReporter(sparkReporter);

			_extent.AddSystemInfo("Environment", "QA");
			_extent.AddSystemInfo("Browser", "Firefox");
			_extent.AddSystemInfo("OS", Environment.OSVersion.ToString());
		}

		return _extent;
	}

	public static void FlushReport()
	{
		_extent.Flush();
	}

	public static string CaptureScreenshot(string testName, IWebDriver driver)
	{
		var screenshotDirectory = Path.Combine(GetReportDirectory(), "Screenshots");

		if (!Directory.Exists(screenshotDirectory))
		{
			Directory.CreateDirectory(screenshotDirectory);
		}

		var screenshot = ((ITakesScreenshot)driver).GetScreenshot();
		string filePath = Path.Combine(screenshotDirectory, $"{testName}_{DateTime.Now:yyyyMMdd}.png");

		screenshot.SaveAsFile(filePath);

		return Path.Combine("Screenshots", Path.GetFileName(filePath));
	}

	private static string GetReportDirectory()
	{
		var reportDirectory = Path.Combine(BaseDirectory, "Reports");

		if (!Directory.Exists(reportDirectory))
		{
			Directory.CreateDirectory(reportDirectory);
		}

		return reportDirectory;
	}
}
