using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SeleniumTestAutomation.InitialClass
{
    public class ModernPage
    {
        private readonly IWebDriver driver;

        public ModernPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void ScrapeContentPage()
        {
            IWebElement modernElement1 = driver.FindElement(By.XPath("/html/body/div[2]/div[1]/section/section/div[3]/div/div/div/div[2]/div/div[1]/ul/li[2]/a/span"));
            modernElement1.Click();
            IJavaScriptExecutor jsExecutor = (IJavaScriptExecutor)driver;
            jsExecutor.ExecuteScript("arguments[0].scrollIntoView();", modernElement1);
            
            Thread.Sleep(TimeSpan.FromSeconds(2));
            IWebElement header = driver.FindElement(By.XPath("/html/body/div[2]/div[1]/section/section/div[3]/div/div/div/div[2]/div/div[2]/div/div[2]/div[2]/div/div[2]/div/div/h3"));
            IWebElement description = driver.FindElement(By.XPath("/html/body/div[2]/div[1]/section/section/div[3]/div/div/div/div[2]/div/div[2]/div/div[2]/div[2]/div/div[2]/div/div/div[1]/div/p"));
            Console.WriteLine(header.Text);
            Console.WriteLine(description.Text);
            Assert.AreEqual("Content & Collaboration", header.Text);
            Assert.IsTrue(description.Text.StartsWith("Spanish Point customers tell us that people are their most important asset"));
            //IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            //String texttemp = (String)js.ExecuteScript("return document.getElementsByClassName('vc_custom_heading wpb_animate_when_almost_visible wpb_fadeIn fadeIn vc_custom_1632996631435 wpb_start_animation animated')[0].innerText;");
            //Assert.AreEqual("Content & Collaboration", texttemp);
            //string textdescription = (string)js.ExecuteScript(@"
            //let collhtml = document.getElementsByClassName('wpb_wrapper');
            //let x = 0;
            //for (let i = 0; i < collhtml.length; i++) {
            //if (collhtml[i].innerText.startsWith('Spanish Point customers')) {
            //x = i;
            //break;
            //}
            //}
            //return collhtml[x].innerText;
            //");
            //Assert.IsTrue(textdescription.StartsWith("Spanish Point customers tell us that people are their most important asset"));

        }
    }

}
