// See https://aka.ms/new-console-template for more information

using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace GoogleSearchTest;
{
    [TestFixture]
    public class GoogleSearchTest
    {
        private IWebDriver driver;

        [Setup]
        public void Setup()
        {
            driver = new ChromeDriver();
        }

        [Test]
        public void GoogleTitleCheck()
        {
            driver.Navigate().GoToUrl("https://www.google.com");
            Assert.assertEqual("Google", driver.Title);
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}
