using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Text.Json;
using System.IO;

namespace SeleniumTestAutomation.InitialClass
{
    public class WebTesting
    {
        [TestFixture]
        public class SeleniumTests
        {
            private IWebDriver driver; // create variable to access IWebDriver
            private HomePage homePage; // create variable to access homePage
            private ModernPage modernWorkPage; // create variable to access modernWorkPage
            private int maxRetries;
            private string url;
            [OneTimeSetUp]
            public void Setup() // called once before each new test is called
            {
                driver = new ChromeDriver(); // initializing the IwebDriver to ChromeDriver
                driver.Manage().Window.Maximize(); // maxinize the browzer window 
                string projectDirectory = Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).FullName).FullName; // get the solution directory information 
                string jsonconfigPath = Path.Combine(projectDirectory, "config.json"); // concatinate the config.json so we can map to get the info from the file.
                string jsonText = File.ReadAllText(jsonconfigPath); // read the values at the location
                JsonDocument jsonDocument = JsonDocument.Parse(jsonText); // get the values into a document at the file location.
                JsonElement rootElement = jsonDocument.RootElement; // get a list of all the roots in the document. 
                url = rootElement.GetProperty("url").GetString(); // get the value in the root url
                maxRetries = int.Parse(rootElement.GetProperty("maxRetry").GetString()); // get the value in the root maxRetry
                jsonDocument.Dispose(); // close the JSON document as the reading is done.
                homePage = new HomePage(driver); // initializing the HomePage and passing the driver info  
                modernWorkPage = new ModernPage(driver); // initializing the ModernPage and passing the driver info 
            }

            [Test, Order(1)]
            public void launchAndGoToHomePage()
            {   
                var driverValue = homePage.LaunchSpanishPointWebsite(maxRetries,url);
                Assert.AreEqual("Spanish Point Technologies Ltd.", driverValue.Title);   
                //==================================================================
                // USAGE OF getting driver gives us comfort in using it for future purpose than cahnging the internal methods
                //==================================================================
            }

            [Test, Order(2)]
            public void goToModernPage()
            {
                //=======================================================================
                //ALTERNATE APPROACH IS USING driver.navigate() to the correspnding url if the hover is having exceptions
                //=======================================================================
                var count = homePage.NavigateToModern();
                if (count > 0)
                {
                    Assert.Pass("Page Successfully navigated");
                }
                else
                {
                    Assert.Fail("Page Navigation Failed");
                }
            }

            [Test, Order(3)]
            public void findContentAndCollebration()
            {
                var result = modernWorkPage.ScrapeContentPage();
                if (result.Count == 1 && result[0].Contains("Exception"))
                {
                    Assert.Fail(result[0]);
                }
                else
                {
                    Assert.AreEqual("Content & Collaboration", result[0]);
                    Assert.IsTrue(result[1].StartsWith("Spanish Point customers tell us that people are their most important asset"));

                }
            }

            [OneTimeTearDown]
            public void Teardown()
            {
                driver.Dispose();
            }
        }
    }
}