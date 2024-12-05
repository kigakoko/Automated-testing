using OpenQA.Selenium;

namespace WebUITests.PageObjects;

public class AboutPage(IWebDriver driver) : BasePage(driver)
{
	private IWebElement AboutLink => Driver.FindElement(By.LinkText("About"));
	private IWebElement Header => Driver.FindElement(By.TagName("h1"));

	public void ClickAboutLink()
	{
		AboutLink.Click();
	}

	public string GetHeaderText()
	{
		return Header.Text;
	}
}
