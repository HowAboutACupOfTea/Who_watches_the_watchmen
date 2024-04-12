using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace TourOfHeroesTester;

internal class HeroeDetailsPage_Tests
{
    private IWebDriver driver;

    [SetUp]
    public void TestInitialize()
    {
        driver = SharedMethodsForTesting.GetWebDriver();
        driver.Navigate().GoToUrl("https://files.perry.fyi/hero");
        NavigateToHeroeDetailsPage();
    }

    private void NavigateToHeroeDetailsPage()
    {
        DashboardPage.GetTopHeroes(driver).First().Click();
        Assert.That(driver.Url, Is.EqualTo("https://files.perry.fyi/hero/detail/13"));
    }

    [TearDown]
    public void TestCleanup()
    {
        // After the test is done, we close the browser.
        driver?.Quit();
    }

    [TestCase("")]
    [TestCase(" ")]
    [TestCase("a")]
    [TestCase("TestHero")]
    [TestCase("Test Hero")]
    public void Hero_Name_Gets_Changed(string expectedHeroName)
    {
        //Arrange
        //Act
        HeroDetailsPage.TypeIntoEmptiedHeroNameField(driver, expectedHeroName);
        string actualHeroName = HeroDetailsPage.GetDisplayedHeroName(driver);

        //Assert
        Assert.That(actualHeroName, Is.EqualTo(expectedHeroName.ToUpper()));
    }

    [TestCase("")]
    [TestCase(" ")]
    [TestCase("a")]
    [TestCase("TestHero")]
    [TestCase("Test Hero")]
    public void Hero_Name_Gets_Changed_And_Saved(string expectedHeroName)
    {
        //Arrange
        //Act
        HeroDetailsPage.TypeIntoEmptiedHeroNameField(driver, expectedHeroName);
        HeroDetailsPage.ClickSaveButton(driver);
        string actualHeroName = DashboardPage.GetNameOfFirstTopHeroe(driver);

        //Assert
        Assert.That(actualHeroName, Is.EqualTo(expectedHeroName));
    }

    [Test]
    public void Navigate_Back()
    {
        //Arrange
        //Act
        HeroDetailsPage.ClickNavigateBackButton(driver);

        //Assert
        Assert.That(driver.Url, Is.EqualTo("https://files.perry.fyi/hero/dashboard"));
    }

    [Test]
    public void Saving_Navigates_Back()
    {
        //Arrange
        //Act
        HeroDetailsPage.ClickSaveButton(driver);
        HeroDetailsPage.WaitUntilNextPageLoaded(driver);

        //Assert
        Assert.That(driver.Url, Is.EqualTo("https://files.perry.fyi/hero/dashboard"));
    }
}
