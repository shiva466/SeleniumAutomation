using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
namespace SeleniumTestAutomation.InitialClass
{
    public class InitialTest
    {
        public IWebDriver driver;

        [OneTimeSetUp]
        public void Initialise()
        {
            // Create a new instance of the Chrome driver
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
        }
        [OneTimeTearDown]
        public void Close()
        {
            // Close the browser
           // driver.Quit();
        }
    }
}
