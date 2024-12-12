using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium;
using Serilog;
using TechTalk.SpecFlow;
using WebUITests.Drivers;
using WebUITests.PageObjects;

namespace WebUITests.BDD.Steps;

[Binding]
public class LanguageChangeSteps
{
    private readonly IWebDriver driver;
    private readonly HomePage homePage;

    public LanguageChangeSteps()
    {
        driver = WebDriverSingleton.GetDriver();
        homePage = new HomePage(driver);
    }

    [Given(@"I am on the language change homepage ""(.*)""")]
    public void GivenIAmOnTheLanguageChangeHomepage(string url)
    {
        Log.Information("Navigating to homepage: {Url}", url);
        homePage.NavigateTo(url);
    }

    [When(@"I click the language switcher")]
    public void WhenIClickTheLanguageSwitcher()
    {
        Log.Information("Clicking the language switcher.");
        homePage.ClickLanguageSwitcher();
    }

    [When(@"I select the Lithuanian language")]
    public void WhenISelectTheLithuanianLanguage()
    {
        Log.Information("Selecting Lithuanian language.");
        homePage.SelectLithuanianLanguage();
    }

    [Then(@"the current URL should contain ""(.*)""")]
    public void ThenTheCurrentUrlShouldContain(string expectedUrl)
    {
        Log.Information("Validating the current URL contains: {ExpectedUrl}", expectedUrl);
        homePage.GetCurrentUrl()
            .Should().Contain(expectedUrl, "the URL should switch to the Lithuanian version of the site after changing language.");
    }

    [Then(@"the header text should contain ""(.*)""")]
    public void ThenTheHeaderTextShouldContain(string expectedHeader)
    {
        Log.Information("Validating the header text contains: {ExpectedHeader}", expectedHeader);
        homePage.GetHeaderText()
            .Should().Contain(expectedHeader, "the header text should match the Lithuanian version after changing language.");
    }

    [AfterScenario]
    public void Teardown()
    {
        Log.Information("Tearing down the browser instance.");
        WebDriverSingleton.QuitDriver();
    }
}
