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
using OpenQA.Selenium.Support.UI;

namespace SeleniumTestAutomation
{
    public class HomePage
    {
        private readonly IWebDriver _driver;

        public HomePage(IWebDriver driver) // parameterised constructor for HomePage setting the private _driver
        {
            _driver = driver;
        }

        // Declare variables
        public IWebElement elementToHover => _driver.FindElement(By.XPath("/html/body/div[1]/div/div/ul/li[1]/a/span")); // hover to solution & services      
        public IWebElement modernElement => _driver.FindElement(By.XPath("/html/body/div[1]/div/div/ul/li[1]/ul/li[1]/a"));// click on Modern work
        public ReadOnlyCollection<IWebElement> elements => _driver.FindElements(By.XPath("/html/body/div[2]/div[1]/section/section/div[3]/div/div/div/div[2]/div/div[1]/ul/li[2]/a/span"));// to check if the navigation to the new page is successful

        // Methods
        public IWebDriver LaunchSpanishPointWebsite(int maxRetries,string url)
        {
            
            int retryCount = 0;
            bool success = false;
            while (retryCount < maxRetries && !success)// page launch retry mechanism
            {
                try
                {
                    _driver.Navigate().GoToUrl(url);
                    success = true;
                }
                catch (Exception ex)
                {
                    retryCount++;
                }
            }

            if (success)
            {
                return _driver;
            }
            else
            {
                throw new Exception("Navigation failed after maximum retries.");
            }
        }

        public int NavigateToModern()
        {
            Actions actions = new Actions(_driver);
            actions.MoveToElement(elementToHover).Perform();
            Thread.Sleep(TimeSpan.FromSeconds(1));
            modernElement.Click();
            return elements.Count;
            
        }

    }
}