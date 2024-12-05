using OpenQA.Selenium;

namespace WebUITests.PageObjects;

public abstract class BasePage(IWebDriver driver)
{
	protected readonly IWebDriver Driver = driver;

	public void NavigateTo(string url)
	{
		Driver.Navigate().GoToUrl(url);
	}

	public string GetPageTitle()
	{
		return Driver.Title;
	}

	public string GetCurrentUrl()
	{
		return Driver.Url;
	}
}
