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
    public class HomePage
    {
        private readonly IWebDriver driver;

        public HomePage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void LaunchSpanishPointWebsite()
        {
            string jsonText = File.ReadAllText("C:\\Users\\chama\\Desktop\\Selenium\\SeleniumTestAutomation\\SeleniumTestAutomation\\jsconfig1.json");
            JsonDocument jsonDocument = JsonDocument.Parse(jsonText);
            JsonElement rootElement = jsonDocument.RootElement;
            JsonElement propertyValue = rootElement.GetProperty("url");
            string value = propertyValue.GetString();
            jsonDocument.Dispose();
            int maxRetries = 3;
            int retryCount = 0;
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

        public ModernPage NavigateToModern()
        {
            
            IWebElement elementToHover = driver.FindElement(By.XPath("/html/body/div[1]/div/div/ul/li[1]/a/span"));
            Actions actions = new Actions(driver);
            actions.MoveToElement(elementToHover).Perform();
            Thread.Sleep(TimeSpan.FromSeconds(2));
            IWebElement modernElement = driver.FindElement(By.XPath("/html/body/div[1]/div/div/ul/li[1]/ul/li[1]/a"));
            modernElement.Click();
            ReadOnlyCollection<IWebElement> elements = driver.FindElements(By.XPath("/html/body/div[2]/div[1]/section/section/div[3]/div/div/div/div[2]/div/div[1]/ul/li[2]/a/span"));

            if (elements.Count > 0)
            {
                Console.WriteLine("Element exists.");
            }
            else
            {
                Console.WriteLine("Element does not exist.");
            }
            return new ModernPage(driver);
        }

    }
}