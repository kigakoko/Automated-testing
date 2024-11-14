using NUnit.Framework;
using OpenQA.Selenium;
using WebUITests.Drivers;

namespace WebUITests.Tests;

[TestFixture]
public class LanguageChangeTest
{
	IWebDriver driver = null!;

	[SetUp]
	public void Setup()
	{
		driver = WebDriverManager.GetFirefoxDriver();
		driver.Manage().Window.Maximize();
	}

	[Test]
	public void VerifyLanguageChangeToLithuanian()
	{
		driver.Navigate().GoToUrl("https://en.ehu.lt/");

		IWebElement languageSwitcher = driver.FindElement(By.CssSelector(".language-switcher"));
		languageSwitcher.Click();
		IWebElement lithuanianOption = driver.FindElement(By.LinkText("LT"));
		lithuanianOption.Click();

		Assert.That(driver.Url, Does.Contain("https://lt.ehu.lt/"));

		IWebElement header = driver.FindElement(By.TagName("h1"));
		Assert.That(header.Text, Does.Contain("Kodėl EHU?\r\nKas daro EHU unikaliu?"));
	}

	[TearDown]
	public void Teardown()
	{
		driver.Quit();
	}
}
