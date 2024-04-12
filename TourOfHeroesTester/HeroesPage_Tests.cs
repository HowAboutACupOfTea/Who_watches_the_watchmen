using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace TourOfHeroesTester;

internal class HeroesPage_Tests
{
    private IWebDriver driver;

    [SetUp]
    public void TestInitialize()
    {
        driver = SharedMethodsForTesting.GetWebDriver();
        driver.Navigate().GoToUrl("https://files.perry.fyi/hero");
        NavigateToHeroesPage();
    }

    private void NavigateToHeroesPage()
    {
        var navigation = CommonlyUsedPageFeatures.GetNavigationElements(driver);
        navigation[1].Click();
        Assert.That(driver.Url, Is.EqualTo("https://files.perry.fyi/hero/heroes"));
    }

    [TearDown]
    public void TestCleanup()
    {
        // After the test is done, we close the browser.
        driver?.Quit();
    }

    [TestCase("a", 10)]
    [TestCase("TestHero", 10)]
    [TestCase("Test Hero", 10)]
    public void Add_Hero(string expectedHeroName, int expectedHeroCount)
    {
        int count = expectedHeroName.Length;

        //Arrange
        //Act
        HeroesPage.TypeIntoAddHeroField(driver, expectedHeroName);
        HeroesPage.ClickAddHeroButton(driver);
        HeroesPage.WaitUntilHeroGotCreated(driver);
        var heroes = HeroesPage.GetHeroes(driver);
        int actualHeroCount = heroes.Count;
        string actualHeroName = HeroesPage.GetNewestHero(driver).Text.Remove(0, 3);
        
        //Assert
        Assert.Multiple(() =>
        {
            Assert.That(actualHeroName, Is.EqualTo(expectedHeroName));
            Assert.That(actualHeroCount, Is.EqualTo(expectedHeroCount));
        });
    }

    [TestCase(" ")]
    [TestCase("")]
    public void Hero_Names_Need_At_Least_One_Character_That_Is_Not_Space(string heroName)
    {
        //Arrange
        //Act
        HeroesPage.TypeIntoAddHeroField(driver, heroName);
        HeroesPage.ClickAddHeroButton(driver);
        var heroes = HeroesPage.GetHeroes(driver);
        int actualHeroCount = heroes.Count;

        //Assert
        Assert.That(actualHeroCount, Is.EqualTo(9));
    }

    [TestCase(0)]
    [TestCase(1)]
    [TestCase(7)]
    [TestCase(8)]
    public void Delete_Hero(int indexOfHero)
    {
        //Arrange
        var heroes = HeroesPage.GetHeroes(driver);
        int heroCount = heroes.Count;

        //Act
        HeroesPage.ClickDeleteHeroButton(driver, indexOfHero);
        int actualHeroCount = HeroesPage.GetHeroes(driver).Count;

        //Assert
        Assert.That(actualHeroCount, Is.EqualTo(heroCount - 1));
    }
}
