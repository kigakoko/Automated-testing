using OpenQA.Selenium.Firefox;
using OpenQA.Selenium;

namespace WebUITests.Drivers;

public static class WebDriverManager
{
	public static IWebDriver GetFirefoxDriver()
	{
		FirefoxOptions options = new();
		options.AddArgument("--start-maximized");

		return new FirefoxDriver(options);
	}
}
