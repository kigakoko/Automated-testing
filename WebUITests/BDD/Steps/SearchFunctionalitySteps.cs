using FluentAssertions;
using TechTalk.SpecFlow;
using OpenQA.Selenium;
using WebUITests.PageObjects;
using WebUITests.Drivers;

namespace WebUITests.BDD.Steps
{
    [Binding]
    public class SearchFunctionalitySteps
    {
        private readonly IWebDriver driver;
        private readonly SearchPage searchPage;

        public SearchFunctionalitySteps()
        {
            driver = WebDriverSingleton.GetDriver();
            searchPage = new SearchPage(driver, "search-filter__result-count");
        }

        [Given(@"I am on the search page ""(.*)""")]
        public void GivenIAmOnTheSearchPage(string url)
        {
            searchPage.NavigateTo(url);
        }

        [When(@"I perform a search with the term ""(.*)""")]
        public void WhenIPerformASearchWithTheTerm(string searchTerm)
        {
            searchPage.Search("https://en.ehu.lt/", searchTerm);
        }

        [Then(@"the current URL for search should contain ""(.*)""")]
        public void ThenTheCurrentUrlForSearchShouldContain(string expectedSearchQuery)
        {
            string expectedUrl = $"https://en.ehu.lt/?s={Uri.EscapeDataString(expectedSearchQuery)}";
            searchPage.GetCurrentUrl()
                .Should().Be(expectedUrl, "the URL should include the search query after performing the search");
        }

        [Then(@"the search results should indicate that results were found")]
        public void ThenTheSearchResultsShouldIndicateThatResultsWereFound()
        {
            searchPage.GetSearchResultsText()
                .Should().Contain("results found.", "search results should indicate that results were found on the page");
        }
    }
}
