using NUnit.Framework;
using OpenQA.Selenium;
using WebUITests.Drivers;

namespace WebUITests.Tests;

[TestFixture]
public class AboutEHUPageTest
{
	IWebDriver driver = null!;

	[SetUp]
	public void Setup()
	{
		driver = WebDriverManager.GetFirefoxDriver();
		driver.Manage().Window.Maximize();
	}

	[Test]
	public void VerifyNavigationToAboutEHUPage()
	{
		driver.Navigate().GoToUrl("https://en.ehu.lt/");

		IWebElement aboutLink = driver.FindElement(By.LinkText("About"));
		aboutLink.Click();

		Assert.That(driver.Title, Is.EqualTo("About"));

		IWebElement header = driver.FindElement(By.TagName("h1"));
		Assert.That(header.Text, Does.Contain("About"));
	}

	[TearDown]
	public void Teardown()
	{
		driver.Quit();
	}
}
