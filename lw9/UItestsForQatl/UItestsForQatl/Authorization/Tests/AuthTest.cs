using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using UItestsForQatl.Authorization.Classes;

namespace UItestsForQatl.Authorization.Tests;

[TestClass]
public class AuthTest
{
    private string _url;
    private IWebDriver _driver;
    private AuthClass _authClass;
    private JObject _testData;
    
    [TestInitialize]
    public void TestInitialize()
    {
        _url = "http://shop.qatl.ru/";
        _driver = new ChromeDriver();
        _driver.Navigate().GoToUrl(_url);
        _driver.Manage().Window.Maximize();
        _authClass = new AuthClass(_driver);
        _testData = JObject.Parse(
            File.ReadAllText("../../../Authorization/Config/AuthConfig.json")
        );
    }

    [TestMethod]
    [DataRow("Authorization on the website")]
    [DataRow("Authorization on the website with invalid user")]
    public void Auth(string testName)
    {
        string validLogin = _testData[testName]["Login"].ToString();
        string validPassword = _testData[testName]["Password"].ToString();
        string message = _testData[testName]["ExpectedMessage"].ToString();
        
        _authClass.ClickDrop();
        _authClass.ClickAccountSignIn();
        _authClass.FillLoginInput(validLogin);
        _authClass.FillPasswordInput(validPassword);
        _authClass.ClickSignIn();

        var alert = _authClass.GetAlertDiv();
        Assert.AreEqual(alert.Text, message, "Expected message: Вы успешно авторизованы");
    }

    [TestCleanup]
    public void TestCleanup()
    {
        _driver.Close();
    }
}