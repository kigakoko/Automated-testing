using OpenQA.Selenium;
using XUnitSolution.Builders;

namespace XUnitSolution.Drivers;

public sealed class WebDriverSingleton
{
	private static readonly ThreadLocal<IWebDriver> driver = new(() =>
	{
		return new WebDriverBuilder()
				.SetBrowser("Firefox")
				.SetHeadless(true)
				.SetMaximizeWindow(true)
				.Build();
	});

	private WebDriverSingleton() { }

	public static IWebDriver GetDriver()
	{
		return driver.Value!;
	}

	public static void QuitDriver()
	{
		driver.Value?.Quit();
	}
}
