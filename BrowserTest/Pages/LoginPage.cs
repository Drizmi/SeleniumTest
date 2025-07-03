using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using OpenQA.Selenium.Support.UI;

namespace BrowserTest.Pages;

public class LoginPage
{
    private IWebDriver _driver;
    private Logger _logger;
    
    [FindsBy(How = How.XPath, Using = "//input[@name='loginfmt']")]
    private IWebElement emailtxt;
    
    [FindsBy(How = How.XPath, Using = "//input[@value='Next']")]
    private IWebElement nextbtn;
    
    [FindsBy(How = How.XPath, Using = "//input[@name='passwd']")]
    private IWebElement passwordtxt;
    
    [FindsBy(How = How.XPath, Using = "//input[@value='Sign in']")]
    private IWebElement signinbtn;
    
    [FindsBy(How = How.XPath, Using = "//input[@value='No']")]
    private IWebElement denybtn;
    

    public LoginPage(IWebDriver driver, Logger logger)
    {
        _driver = driver; 
        PageFactory.InitElements(driver, this);
        _logger = logger;
    }

    public ChatPage login(string email, string password)
    {
        try
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            Assert.That(wait.Until(ExpectedConditions.UrlContains("https://login.microsoftonline.com")),
                "This is not microsoft login page");
            Assert.That(_driver.Title, Is.EqualTo("Sign in to your account"),
                "This is not a login page");
            ;
            Assert.That(wait.Until(d => emailtxt.Displayed), Is.True,
                "Insert Email text field is not present.");
            _logger.testInfo("Microsoft teams login page is loaded");
            emailtxt.SendKeys(email);
            nextbtn.Click();
            //assertion
            Assert.That(wait.Until(d => passwordtxt.Displayed), Is.True,
                "Insert Password text field is not present.");
            passwordtxt.SendKeys(password);
            signinbtn.Click();
            wait.Until(d => denybtn.Displayed);
            if (denybtn.Displayed) denybtn.Click();
            _logger.testInfo("User is logging in");
            return (new ChatPage(_driver, _logger));
        } catch (Exception e)
        {
            _logger.testFail(e, "");
            Assert.Fail(e.Message);
            return new ChatPage(_driver, _logger);
        }
    }

}