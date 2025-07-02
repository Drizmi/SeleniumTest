using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;

namespace BrowserTest.Pages;

public class ChatPage
{
    private IWebDriver _driver;
    
    [FindsBy(How = How.XPath, Using = "//button[contains(@aria-label,'Your profile')]")]
    private IWebElement useravatarbtn;
    
    [FindsBy(How = How.XPath, Using = "//div[@aria-label='View your profile']//span[2]")]
    private IWebElement useremail;
    
    [FindsBy(How = How.XPath, Using = "//div[@placeholder='Type a message']")]
    private IWebElement messagebox;
    
    [FindsBy(How = How.XPath, Using = "//div[@aria-label='Actions for new message']/button[2]")]
    private IWebElement sendbtn;

    [FindsBy(How = How.XPath, Using = "//button[@title='Actions and apps']")]
    private IWebElement plusbtn;

    [FindsBy(How = How.XPath, Using = "//li[@title='Attach file (Alt+Shift+O)']")]
    private IWebElement attachfilebtn;
    
    [FindsBy(How = How.XPath, Using = "//div[@data-tid='message-pane-list-runway']/child::div[last()]/div/div/div")]
    private IWebElement lastmessage;
    
    [FindsBy(How = How.XPath, Using = "//div[@data-id='conversation-folder-header-Favorites']/following-sibling::div")]
    private IWebElement chatbtn;
    
    [FindsBy(How = How.XPath, Using = "//div[@data-tid='chat-title']//li/span/span")]
    private IWebElement chatname;

    

    
    
    public ChatPage(IWebDriver driver)
    {
        _driver = driver; PageFactory.InitElements(driver, this);
    }
    
    public void assertUser(string email)
    {
        useravatarbtn.Click();
        Assert.That(useremail.Text , Is.EqualTo(email), "User email is incorrect");
    }
    
    public void openChat()
    {
        WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        Assert.That(wait.Until(d => chatbtn.Displayed), Is.True, 
            "Chat button is not present");
        chatbtn.Click();
        Assert.That(chatname.Displayed, Is.True, "Chat window is not opened");
        Assert.That(chatname.Text, Is.EqualTo("Safetica QA (You)"), 
            "Wrong chat is opened");
    }
    
    public void SendMessage(string message)
    {
        WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        Assert.That(messagebox.Displayed, Is.True, "Message box is not present");
        messagebox.SendKeys(message + Keys.Enter);
        // sendbtn.Click();
        _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        Assert.That( lastmessage.Text, Is.EqualTo(message), 
            "Text of the message is incorrect");
    }
}