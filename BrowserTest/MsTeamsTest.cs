
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using AventStack.ExtentReports;
using BrowserTest.Pages;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace BrowserTest;

public class Tests
{
    // static string url = "https://www.google.com/";
    static string url = "https://teams.microsoft.com/v2/";
    static string name = "qa@safeticaservices.onmicrosoft.com";
    static string password = "automation.Safetica2004";
    static string filePath = "BrowserTest/SendMe.txt";
    static string downloadFolder = "BrowserTest/Downloads";
    
    private IWebDriver _driver;
    private ExtentReports _extent;
    private Logger _logger;

    private ChatPage userLogin()
    {
        LoginPage loginPage = new LoginPage(_driver, _logger);
        _driver.Navigate().GoToUrl(url);
        WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        try
        {
            Assert.That(wait.Until(ExpectedConditions.UrlContains(url)));
            _logger.testInfo("Url is correct");
            _logger.testPass("Login page is opened");
        }
        catch (Exception e)
        {
            _logger.testFail(e, "");
        }

        //TODO part logging here too
        return loginPage.login(name, password);
    }

    [OneTimeSetUp]
    public void Setup()
    {
        _driver = new ChromeDriver();
        _extent = new ExtentReports();
        _logger = new Logger(_driver, _extent);
    }
    
    [Test]
    public void LoginTest()
    {
        string testName = "LoginTest";
        _logger.testStart(testName);
        try
        {
            userLogin().assertUser(name);
            _logger.testPass("Login successful");
        } catch (Exception e)
        {
            _logger.testFail(e, "");
            Assert.Fail(e.Message);
        }
    }

    [Test]
    public void FileExchangeTest()
    {
        string testName = "FileExchangeTest";
        _logger.testStart(testName);
        try
        {
            ChatPage chatPage = userLogin();
            chatPage.openChat();
            chatPage.SendFile(filePath);
            chatPage.DownloadFileFromLastMessage(filePath);
        }
        catch (Exception e)
        {
            _logger.testFail(e, "");
            Assert.Fail(e.Message);
        }
    }

    [Test]
    public void SendMessagesTest()
    {
        string testName = "SendMessagesTest";
        _logger.testStart(testName);
        try
        {
            ChatPage chatPage = userLogin();
            chatPage.openChat();
            for (int i = 0; i < 3; i++)
            {
                chatPage.SendMessage();
                _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(300);
            }
        } catch (Exception e)
        {
            _logger.testFail(e, "");
            Assert.Fail(e.Message);
        }
    }

    [OneTimeTearDown]
    public void TearDown()
    {
        _driver.Quit();
    }
}