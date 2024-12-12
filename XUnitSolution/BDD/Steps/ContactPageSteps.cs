using FluentAssertions;
using OpenQA.Selenium;
using Serilog;
using TechTalk.SpecFlow;
using XUnitSolution.Drivers;
using XUnitSolution.PageObjects;

namespace XUnitSolution.BDD.Steps;

[Binding]
public class ContactPageSteps
{
    private readonly IWebDriver driver;
    private readonly ContactPage contactPage;

    public ContactPageSteps()
    {
        driver = WebDriverSingleton.GetDriver();
        contactPage = new ContactPage(driver);
    }

    [Given(@"I am on the contact page ""(.*)""")]
    public void GivenIAmOnTheContactPage(string url)
    {
        Log.Information("Navigating to the contact page: {Url}", url);
        contactPage.NavigateTo(url);
    }

    [Then(@"the page should contain a valid email matching the pattern ""(.*)""")]
    public void ThenThePageShouldContainAValidEmailMatchingThePattern(string emailPattern)
    {
        Log.Information("Validating email pattern: {EmailPattern}", emailPattern);
        contactPage.ContainsValidEmail(emailPattern)
            .Should().BeTrue("a valid email address should be present on the page.");
    }

    [Then(@"the page should contain a valid phone number matching the pattern ""(.*)""")]
    public void ThenThePageShouldContainAValidPhoneNumberMatchingThePattern(string phonePattern)
    {
        Log.Information("Validating phone pattern: {PhonePattern}", phonePattern);
        contactPage.ContainsValidPhoneNumber(phonePattern)
            .Should().BeTrue("a valid phone number should be present on the page.");
    }

    [Then(@"the page should contain a reference to ""(.*)""")]
    public void ThenThePageShouldContainAReferenceTo(string reference)
    {
        Log.Information("Validating presence of reference: {Reference}", reference);
        contactPage.ContainsFacebookLink()
            .Should().BeTrue($"the page should contain a link or reference to {reference}.");
    }

    [AfterScenario]
    public void Teardown()
    {
        Log.Information("Tearing down the browser instance.");
        WebDriverSingleton.QuitDriver();
    }
}
