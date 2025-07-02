
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System;
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
    static string chattxtxpath = "//h1[normalize-space()='Chat']";
    
    string[] messages = {"message 1", "message 2", "message 3 !@#$%^"};
    
    private IWebDriver _driver;

    private ChatPage userLogin()
    {
        LoginPage loginPage = new LoginPage(_driver);
        _driver.Navigate().GoToUrl(url);
        WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        Assert.That(wait.Until(ExpectedConditions.UrlContains(url)), "Login failed");
        return loginPage.login(name, password);
    }

    [OneTimeSetUp]
    public void Setup()
    {
        _driver = new ChromeDriver();
    }
    
    [Test]
    public void LoginTest()
    {
        userLogin().assertUser(name);
    }

    [Test]
    public void FileExchangeTest()
    {
        ChatPage chatPage = userLogin();
        
    }

    [Test]
    public void SendMessagesTest()
    {
        ChatPage chatPage = userLogin();
        chatPage.openChat();
        foreach (string message in messages)
        {
            chatPage.SendMessage(message);
        }
    }

    [OneTimeTearDown]
    public void TearDown()
    {
        _driver.Quit();
    }
}