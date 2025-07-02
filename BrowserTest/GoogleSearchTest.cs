// See https://aka.ms/new-console-template for more information

using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace BrowserTest
{
    public class GoogleSearchTest
    {
        private IWebDriver driver;
        
        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
        }
        
        [Test]
        public void GoogleTitleCheck()
        {
            driver.Navigate().GoToUrl("https://www.google.com");
            Assert.That(driver.Title, Is.EqualTo("Google"));
        }
        
        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}
