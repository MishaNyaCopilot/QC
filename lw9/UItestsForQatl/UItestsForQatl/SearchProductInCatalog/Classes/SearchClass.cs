using OpenQA.Selenium;

namespace UItestsForQatl.SearchProductInCatalog.Classes;

public class SearchClass
{
    private readonly IWebDriver _webDriver;

    private readonly By _searchInput = By.XPath("//input[@class='typeahead tt-hint']");
    private readonly By _searchConfirm = By.XPath("/html/body/div[3]/div/div/div[2]/div/form/input");
    private readonly By _foundItemsContainer = By.XPath("//div[@class='product-one']");
    

    public SearchClass(IWebDriver webDriver)
    {
        _webDriver = webDriver;
    }

    public void ClickSearchConfirm()
    {
        var element = GetSearchConfirm();
        element.Click();
    }
    
    public void FillSearchInput(string input)
    {
        var element = GetSearchInput();
        element.SendKeys(input);
    }
    
    public IWebElement GetSearchInput()
    {
        return _webDriver.FindElement(_searchInput);
    }
    
    public IWebElement GetSearchConfirm()
    {
        return _webDriver.FindElement(_searchConfirm);
    }
    
    public IWebElement GetFoundItemsContainer()
    {
        return _webDriver.FindElement(_foundItemsContainer);
    }
}