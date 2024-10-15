using OpenQA.Selenium;

namespace UItestsForQatl.Authorization.Classes;

public class AuthClass
{
    private readonly IWebDriver _webDriver;

    private readonly By _accountButton = By.XPath("//a[@class='dropdown-toggle']");
    private readonly By _accountSignInButton = By.XPath("//a[@href='user/login']");
    private readonly By _loginInput = By.XPath("//input[@name='login']");
    private readonly By _passwordInput = By.XPath("//input[@name='password']");
    private readonly By _signInButton = By.XPath("//button[@class='btn btn-default']");
    private readonly By _alertDiv = By.XPath("/html/body/div[4]/div[1]/div/div/div");
    

    public AuthClass(IWebDriver webDriver)
    {
        _webDriver = webDriver;
    }

    public void ClickDrop()
    {
        var element = GetAccountButton();
        element.Click();
    }
    
    public void ClickAccountSignIn()
    {
        var element = GetAccountSignInButton();
        element.Click();
    }
    
    public void FillLoginInput(string login)
    {
        var element = GetLoginInput();
        element.SendKeys(login);
    }
    
    public void FillPasswordInput(string password)
    {
        var element = GetPasswordInput();
        element.SendKeys(password);
    }
    
    public void ClickSignIn()
    {
        var element = GetSignInButton();
        element.Click();
    }
    
    public IWebElement GetAccountButton()
    {
        return _webDriver.FindElement(_accountButton);
    }
    
    public IWebElement GetAccountSignInButton()
    {
        return _webDriver.FindElement(_accountSignInButton);
    }
    
    public IWebElement GetLoginInput()
    {
        return _webDriver.FindElement(_loginInput);
    }
    
    public IWebElement GetPasswordInput()
    {
        return _webDriver.FindElement(_passwordInput);
    }
    
    public IWebElement GetSignInButton()
    {
        return _webDriver.FindElement(_signInButton);
    }
    
    public IWebElement GetAlertDiv()
    {
        return _webDriver.FindElement(_alertDiv);
    }
}