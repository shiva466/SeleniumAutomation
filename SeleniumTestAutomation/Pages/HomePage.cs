using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using SeleniumTestAutomation.InitialClass;
using System.IO;
using System.Text.Json;
using System.Threading;
using System;
using OpenQA.Selenium.Interactions;
using System.Collections.ObjectModel;

namespace SeleniumTestAutomation
{
    [TestFixture]
    public class HomePage : InitialTest
    {
        [Test]
        public void LaunchSpanishPointWebsite()
        {
            string jsonText = File.ReadAllText("C:\\Users\\chama\\Desktop\\Selenium\\SeleniumTestAutomation\\SeleniumTestAutomation\\jsconfig1.json");
            JsonDocument jsonDocument = JsonDocument.Parse(jsonText);
            JsonElement rootElement = jsonDocument.RootElement;
            JsonElement propertyValue = rootElement.GetProperty("url");
            string value = propertyValue.GetString();
            jsonDocument.Dispose();
            int maxRetries = 3,retryCount = 0; 
            bool success = false;
            while (retryCount < maxRetries && !success)
            {
                try
                {
                    driver.Navigate().GoToUrl(value);
                    success = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error occurred during navigation: " + ex.Message);
                    retryCount++;
                }
            }
            if (success)
            {
                Console.WriteLine("Navigation successful.");
                Assert.AreEqual("Spanish Point Technologies Ltd.", driver.Title);
            }
            else
            {
                Console.WriteLine("Navigation failed after maximum retries.");
                throw new Exception("Navigation failed after maximum retries.");
            }
         
        }
        [Test]
        public void NavigateToModern()
        {

            try
            {
                IWebElement elementToHover = driver.FindElement(By.XPath("/html/body/div[1]/div/div/ul/li[1]/a/span"));
                Actions actions = new Actions(driver);
                actions.MoveToElement(elementToHover).Perform();
                Thread.Sleep(TimeSpan.FromSeconds(2));
                IWebElement modernElement = driver.FindElement(By.XPath("/html/body/div[1]/div/div/ul/li[1]/ul/li[1]/a"));
                modernElement.Click();
                ReadOnlyCollection<IWebElement> elements = driver.FindElements(By.XPath("/html/body/div[2]/div[1]/section/section/div[3]/div/div/div/div[2]/div/div[1]/ul/li[2]/a/span"));

                // Check if any elements were found
                if (elements.Count > 0)
                {
                    // Element exists
                    Console.WriteLine("Element exists.");
                }
                else
                {
                    // Element does not exist
                    Console.WriteLine("Element does not exist.");
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Assert.Fail("Navigation to the Content Scraping has failed due to " + e.Message);
            }
        }
        [Test]
        public void ScrapeContentPage()
        {
            IWebElement modernElement1 = driver.FindElement(By.XPath("/html/body/div[2]/div[1]/section/section/div[3]/div/div/div/div[2]/div/div[1]/ul/li[2]/a/span"));
            modernElement1.Click();
            Thread.Sleep(TimeSpan.FromSeconds(2));
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            String texttemp = (String)js.ExecuteScript("return document.getElementsByClassName('vc_custom_heading wpb_animate_when_almost_visible wpb_fadeIn fadeIn vc_custom_1632996631435 wpb_start_animation animated')[0].innerText;");
            Assert.AreEqual("Content & Collaboration", texttemp);
            string textdescription = (string)js.ExecuteScript(@"
            let collhtml = document.getElementsByClassName('wpb_wrapper');
            let x = 0;
            for (let i = 0; i < collhtml.length; i++) {
            if (collhtml[i].innerText.startsWith('Spanish Point customers')) {
            x = i;
            break;
            }
            }
            return collhtml[x].innerText;
            ");
            Assert.IsTrue(textdescription.StartsWith("Spanish Point customers tell us that people are their most important asset"));

        }
    }
}