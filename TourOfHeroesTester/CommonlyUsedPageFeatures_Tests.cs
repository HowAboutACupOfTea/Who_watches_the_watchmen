using OpenQA.Selenium;

namespace TourOfHeroesTester;

internal class CommonlyUsedPageFeatures_Tests
{
    private IWebDriver driver;

    [SetUp]
    public void TestInitialize()
    {
        driver = SharedMethodsForTesting.GetWebDriver();
        driver.Navigate().GoToUrl("https://files.perry.fyi/hero");
    }

    [TearDown]
    public void TestCleanup()
    {
        // After the test is done, we close the browser.
        driver?.Quit();
    }

    [Test]
    public void Website_Has_A_Specific_Title()
    {
        string title = DashboardPage.GetPageTitle(driver);
        Assert.That(title, Is.EqualTo("Tour of Heroes"));
    }

    [TestCase(0, "https://files.perry.fyi/hero/dashboard")]
    [TestCase(1, "https://files.perry.fyi/hero/heroes")]
    public void Navigate_To_Link(int indexOfNavigationElement, string destination)
    {
        //Arrange
        var navigation = CommonlyUsedPageFeatures.GetNavigationElements(driver);

        //Act
        navigation[indexOfNavigationElement].Click();

        //Assert
        Assert.That(driver.Url, Is.EqualTo(destination));
    }
}
