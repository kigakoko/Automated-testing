using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium;

namespace WebUITests.Builders;

public class WebDriverBuilder
{
	private string _browser = "Firefox";
	private bool _headless = false;
	private bool _maximizeWindow = true;

	public WebDriverBuilder SetBrowser(string browser)
	{
		_browser = browser;
		return this;
	}

	public WebDriverBuilder SetHeadless(bool headless)
	{
		_headless = headless;
		return this;
	}

	public WebDriverBuilder SetMaximizeWindow(bool maximizeWindow)
	{
		_maximizeWindow = maximizeWindow;
		return this;
	}

	public IWebDriver Build()
	{
		if (_browser == "Firefox")
		{
			var firefoxOptions = new FirefoxOptions();

			if (_headless)
			{
				firefoxOptions.AddArgument("--headless");
			}

			var driver = new FirefoxDriver(firefoxOptions);

			if (_maximizeWindow)
			{
				driver.Manage().Window.Maximize();
			}

			return driver;
		}
		else if (_browser == "Chrome")
		{
			var chromeOptions = new ChromeOptions();

			if (_headless)
			{
				chromeOptions.AddArgument("--headless");
			}

			var driver = new ChromeDriver(chromeOptions);

			if (_maximizeWindow)
			{
				driver.Manage().Window.Maximize();
			}

			return driver;
		}
		else
		{
			throw new InvalidOperationException("Unsupported browser: " + _browser);
		}
	}
}
