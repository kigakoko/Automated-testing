using OpenQA.Selenium;

namespace WebUITests.PageObjects;

public class HomePage(IWebDriver driver) : BasePage(driver)
{
	private readonly By _languageSwitcher = By.CssSelector(".language-switcher");
	private readonly By _lithuanianOption = By.LinkText("LT");
	private readonly By _headerTag = By.TagName("h1");

	public void ClickLanguageSwitcher()
	{
		Driver.FindElement(_languageSwitcher).Click();
	}

	public void SelectLithuanianLanguage()
	{
		Driver.FindElement(_lithuanianOption).Click();
	}

	public string GetHeaderText()
	{
		return Driver.FindElement(_headerTag).Text;
	}
}
