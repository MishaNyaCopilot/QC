using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using UItestsForQatl.SearchProductInCatalog.Classes;

namespace UItestsForQatl.SearchProductInCatalog.Tests;

[TestClass]
public class SearchTest
{
    private string _url;
    private IWebDriver _driver;
    private SearchClass _searchClass;
    private JObject _testData;
    
    [TestInitialize]
    public void TestInitialize()
    {
        _url = "http://shop.qatl.ru/";
        _driver = new ChromeDriver();
        _driver.Navigate().GoToUrl(_url);
        _driver.Manage().Window.Maximize();
        _searchClass = new SearchClass(_driver);
        _testData = JObject.Parse(
            File.ReadAllText("../../../SearchProductInCatalog/Config/SearchConfig.json")
        );
    }

    [TestMethod]
    public void Search()
    {
        string itemNameToSearch = _testData["Search CASIO GA-1000-1AER"]["Search"].ToString();
        int expectedCount = _testData["Search CASIO GA-1000-1AER"]["ExpectedCount"].ToObject<int>();
        
        _searchClass.FillSearchInput(itemNameToSearch);
        _searchClass.ClickSearchConfirm();
        
        var container = _searchClass.GetFoundItemsContainer();
        var count = container.FindElements(By.CssSelector(":scope > div")).Count;
        
        Assert.AreEqual(count, expectedCount, "Only one model should be output");
    }

    [TestCleanup]
    public void TestCleanup()
    {
        _driver.Close();
    }
}