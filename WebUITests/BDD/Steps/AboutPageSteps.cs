using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium;
using Serilog;
using TechTalk.SpecFlow;
using WebUITests.Drivers;
using WebUITests.PageObjects;
[assembly: Parallelizable(ParallelScope.Fixtures)]

namespace WebUITests.BDD.Steps;

[Binding]
public class AboutEhuPageSteps
{
    private readonly IWebDriver driver;
    private readonly AboutPage aboutPage;

    public AboutEhuPageSteps()
    {
        driver = WebDriverSingleton.GetDriver();
        aboutPage = new AboutPage(driver);
    }

    [Given(@"I am on the homepage ""(.*)""")]
    public void GivenIAmOnTheHomepage(string url)
    {
        Log.Information("Navigating to homepage: {Url}", url);
        aboutPage.NavigateTo(url);
    }

    [When(@"I click the ""(.*)"" link")]
    public void WhenIClickTheLink(string linkText)
    {
        Log.Information("Clicking the '{LinkText}' link.", linkText);
        aboutPage.ClickAboutLink();
    }

    [Then(@"the page title should be ""(.*)""")]
    public void ThenThePageTitleShouldBe(string expectedTitle)
    {
        Log.Information("Validating page title is '{ExpectedTitle}'.", expectedTitle);
        aboutPage.GetPageTitle()
            .Should().Be(expectedTitle, because: "the page title should match the expected value.");
    }

    [Then(@"the page header should contain ""(.*)""")]
    public void ThenThePageHeaderShouldContain(string expectedHeader)
    {
        Log.Information("Validating page header contains '{ExpectedHeader}'.", expectedHeader);
        aboutPage.GetHeaderText()
            .Should().Contain(expectedHeader, because: "the page header should contain the expected value.");
    }

    [AfterScenario]
    public void Teardown()
    {
        Log.Information("Tearing down the browser instance.");
        WebDriverSingleton.QuitDriver();
    }
}
