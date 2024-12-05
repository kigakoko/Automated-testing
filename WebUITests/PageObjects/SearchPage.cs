using OpenQA.Selenium;

namespace WebUITests.PageObjects;

public class SearchPage(IWebDriver driver, string resultsClassName) : BasePage(driver)
{
	private readonly By _searchResultsLocator = By.ClassName(resultsClassName);

	public void Search(string baseUrl, string searchTerm)
	{
		string searchUrl = $"{baseUrl}?s={Uri.EscapeDataString(searchTerm)}";
		Driver.Navigate().GoToUrl(searchUrl);
	}

	public string GetSearchResultsText()
	{
		return Driver.FindElement(_searchResultsLocator).Text;
	}
}
