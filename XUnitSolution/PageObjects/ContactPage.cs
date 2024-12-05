using OpenQA.Selenium;
using System.Text.RegularExpressions;

namespace XUnitSolution.PageObjects;

public class ContactPage(IWebDriver driver) : BasePage(driver)
{
    public bool ContainsValidEmail(string emailPattern)
    {
        var emailMatch = Regex.Match(Driver.PageSource, emailPattern);
        return emailMatch.Success;
    }

    public bool ContainsValidPhoneNumber(string phonePattern)
    {
        var phoneMatch = Regex.Match(Driver.PageSource, phonePattern);
        return phoneMatch.Success;
    }

    public bool ContainsFacebookLink()
    {
        return Driver.PageSource.Contains("Facebook");
    }
}
