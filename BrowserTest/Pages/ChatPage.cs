using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;

namespace BrowserTest.Pages;

public class ChatPage
{
    private IWebDriver _driver;
    private Logger _logger;
    
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
    
    [FindsBy(How = How.XPath, Using = "//div[@data-tid='message-pane-list-runway']/child::div[last()]/div/div/div//p")]
    private IWebElement lastmessagetxt;
    
    [FindsBy(How = How.XPath, Using = "//div[@data-id='conversation-folder-header-Favorites']/following-sibling::div")]
    private IWebElement chatbtn;
    
    [FindsBy(How = How.XPath, Using = "//div[@data-tid='chat-title']//li/span/span")]
    private IWebElement chatname;
    
    [FindsBy(How = How.XPath, Using = "//body//div//li[2]")]
    private IWebElement uplooadfromdevicebtn;


    public ChatPage(IWebDriver driver, Logger logger)
    {
        _driver = driver; 
        PageFactory.InitElements(driver, this);
        _logger = logger;
    }
    
    public void assertUser(string email)
    {
        try
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            wait.Until(d => useravatarbtn.Displayed);
            useravatarbtn.Click();
            Assert.That(useremail.Text, Is.EqualTo(email), "User email is incorrect");
            _logger.testInfo("User email is correct");
        } catch (Exception e)
        {
            _logger.testFail(e, "");
            Assert.Fail(e.Message);
        }
    }
    
    public void openChat()
    {
        try
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            Assert.That(wait.Until(d => chatbtn.Displayed), Is.True,
                "Chat button is not present");
            _logger.testInfo("Chat button is present");
            chatbtn.Click();
            Assert.That(chatname.Displayed, Is.True, "Chat window is not opened");
            Assert.That(chatname.Text, Is.EqualTo("Safetica QA (You)"),
                "Wrong chat is opened");
            _logger.testInfo("Correct chat is opened");
        } catch (Exception e)
        {
            _logger.testFail(e, "");
            Assert.Fail(e.Message);
        }
    }
    
    public void SendMessage()
    {
        try
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            Assert.That(messagebox.Displayed, Is.True, "Message box is not present");
            string message = DateTime.Now.ToString("dd-MM-yyyy-HH:mm:ss");
            messagebox.SendKeys(message + Keys.Enter);
            Assert.That(wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(
                    "//p[text()=\'" + message + "\']"))).Displayed, Is.True,
                "Message was not sent");
            _logger.testInfo("Message" + message + " was sent");
        }
        catch (Exception e)
        {
            _logger.testFail(e, "");
            Assert.Fail(e.Message);
        }
    }

    public void SendFile(string filePath)
    {
        try
        {
            plusbtn.Click();
            attachfilebtn.Click();
            uplooadfromdevicebtn.Click();
            //TODO select file to be uploaded from the popup
            //TODO can send string to the element
        } catch (Exception e)
        {
            _logger.testFail(e, "");
            Assert.Fail(e.Message);
        }
    }

    public void DownloadFileFromLastMessage(string filePath)
    {
        try
        {
            //TODO
        } catch (Exception e)
        {
            _logger.testFail(e, "");
            Assert.Fail(e.Message);
        }
    }
}