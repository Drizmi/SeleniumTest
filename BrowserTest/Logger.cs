using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework.Internal.Execution;
using OpenQA.Selenium;

namespace BrowserTest;

public class Logger
{
    private IWebDriver _driver;
    private ExtentReports _extent;
    private ExtentSparkReporter _spark;
    private ExtentTest _test;
    private string _testName;

    
    public Logger(IWebDriver driver, ExtentReports extent)
    {
        _driver = driver;
        _extent = extent;
    }
    
    public string TakeScreenshot(string testName)
    {
        try
        {
            ITakesScreenshot ts = (ITakesScreenshot)_driver;
            string filename = @"C:\Users\Michael\RiderProjects\SeleniumTest\BrowserTest\Screenshots\" + testName + 
                              DateTime.Now.ToString("-dd-MM-yyyy-HH-mm-ss") + ".png";
            ts.GetScreenshot().SaveAsFile(filename);
            Console.WriteLine("Screenshot saved as " + filename);
            return filename;
        } catch (InvalidCastException e)
        {
            Console.WriteLine("Screenshot: " + e.ToString());
            return "";
        }
    }
    
    public void testStart(string testName)
    {
        _testName = testName;
        _spark = new ExtentSparkReporter(@"C:\Users\Michael\RiderProjects\SeleniumTest\BrowserTest\Reports\" +
                                         testName + DateTime.Now.ToString("-dd-MM-yyyy-HH-mm-ss") + ".html");
        _extent.AttachReporter(_spark);
        _test = _extent.CreateTest(testName);
        testInfo(testName + " started");
    }
    
    public void testInfo( string msg)
    {
        _test.Info(msg);
        _extent.Flush();
    }
    
    public void testPass(string msg)
    {
        _test.Pass(msg);
        _extent.Flush();
    }
    
    public void testFail(Exception e, string msg)
    {
        _test.Fail(e.Message);
        _test.AddScreenCaptureFromPath(TakeScreenshot(_testName));
        _extent.Flush();
    }
}