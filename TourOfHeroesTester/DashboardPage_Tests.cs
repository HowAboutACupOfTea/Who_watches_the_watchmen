using OpenQA.Selenium;

namespace TourOfHeroesTester;

internal class DashboardPage_Tests
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

    [TestCase(0, "Bombasto")]
    [TestCase(1, "Celeritas")]
    [TestCase(2, "Magneta")]
    [TestCase(3, "RubberMan")]
    public void Top_Heroes_Are_Shown(int index, string heroName)
    {
        //Arrange
        //Act
        var heroes = DashboardPage.GetTopHeroes(driver);

        //Assert
        Assert.That(heroes[index].Text, Is.EqualTo(heroName));
        
    }

    [TestCase(0, 13)]
    [TestCase(1, 14)]
    [TestCase(2, 15)]
    [TestCase(3, 16)]
    public void Navigate_To_Detail_Page_Of_Top_Hero(int index, int heroId)
    {
        //Arrange
        string detailPageUrl = "https://files.perry.fyi/hero/detail/" + heroId;

        //Act
        var topHeroes = DashboardPage.GetTopHeroes(driver);
        topHeroes[index].Click();

        //Assert
        Assert.That(driver.Url, Is.EqualTo(detailPageUrl));
    }

    [TestCase("a", 7)]
    [TestCase("b", 2)]
    [TestCase("y", 1)]
    [TestCase("z", 0)]
    [TestCase(" ", 0)]
    [TestCase("", 0)]
    [TestCase("A", 7)]
    [TestCase("B", 2)]
    [TestCase("Y", 1)]
    [TestCase("Z", 0)]
    [TestCase("Dr. Nice", 1)]
    [TestCase("Tornado", 1)]
    public void Hero_Search_Shows_Heroes(string searchText, int shownHeroesCount)
    {
        //Arrange
        //Act
        DashboardPage.TypeIntoHeroSearchField(driver, searchText);
        var searchResults = DashboardPage.GetHeroSearchResult(driver);

        //Assert
        Assert.That(searchResults, Has.Count.EqualTo(shownHeroesCount));
    }

    [TestCase(" ")]
    [TestCase("")]
    [TestCase("z")]
    public void No_Navigation_Without_Matching_Hero(string searchText)
    {
        //Arrange
        //Act
        DashboardPage.TypeIntoHeroSearchField(driver, searchText);
        var searchResults = DashboardPage.GetHeroSearchResult(driver);

        //Assert
        Assert.That(searchResults, Has.Count.EqualTo(0));
    }

    [TestCase(12, "Dr. Nice")]
    [TestCase(20, "Tornado")]
    public void Navigate_To_Matching_Hero(int heroId, string searchText)
    {
        //Arrange
        string detailPageUrl = "https://files.perry.fyi/hero/detail/" + heroId;

        //Act
        DashboardPage.TypeIntoHeroSearchField(driver, searchText);
        var searchResults = DashboardPage.GetHeroSearchResult(driver);
        searchResults.First().Click();

        //Assert
        Assert.That(driver.Url, Is.EqualTo(detailPageUrl));
    }
}
