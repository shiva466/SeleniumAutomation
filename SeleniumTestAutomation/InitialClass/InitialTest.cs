using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
namespace SeleniumTestAutomation.InitialClass
{
    public class InitialTest
    {
        [TestFixture]
        public class SeleniumTests
        {
            private IWebDriver driver;
            private HomePage homePage;
            private ModernPage modernWorkPage;

            [SetUp]
            public void Setup()
            {
                driver = new ChromeDriver();
                driver.Manage().Window.Maximize();
                homePage = new HomePage(driver);
                modernWorkPage = new ModernPage(driver);
            }

            [Test]
            public void TestHomePage()
            {
                homePage.LaunchSpanishPointWebsite();
                ModernPage modernPage = homePage.NavigateToModern();
                modernWorkPage.ScrapeContentPage();
                // Additional assertions or test steps related to the home page
            }

            
            [TearDown]
            public void Teardown()
            {
                //driver.Quit();
            }
        }
    }
}
