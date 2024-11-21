using OpenQA.Selenium.Firefox;
using OpenQA.Selenium;

namespace XUnitSolution.Drivers;

public static class WebDriverManager
{
	public static IWebDriver GetFirefoxDriver()
	{
		FirefoxOptions options = new();
		return new FirefoxDriver(options);
	}
}
