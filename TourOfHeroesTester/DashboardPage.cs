using OpenQA.Selenium;
using System.Collections.ObjectModel;

namespace TourOfHeroesTester;

internal static class DashboardPage
{
    internal static string GetPageTitle(IWebDriver driver)
    {
        return driver.Title;
    }

    internal static ReadOnlyCollection<IWebElement> GetTopHeroes(IWebDriver driver)
    {
        IWebElement topHeroesMenu = driver.FindElement(By.ClassName("heroes-menu"));
        return topHeroesMenu.FindElements(By.CssSelector("a"));
    }

    internal static string GetNameOfFirstTopHeroe(IWebDriver driver)
    {
        var topHeroes = GetTopHeroes(driver);
        string heroNameWithAdditionalSpaces = topHeroes.First().GetAttribute("textContent");
        return heroNameWithAdditionalSpaces[1..^1];
    }

    internal static void TypeIntoHeroSearchField(IWebDriver driver, string input)
    {
        IWebElement heroSearch = driver.FindElement(By.Id("search-component"));
        IWebElement searchInput = heroSearch.FindElement(By.Id("search-box"));
        searchInput.SendKeys(input);
    }

    internal static ReadOnlyCollection<IWebElement> GetHeroSearchResult(IWebDriver driver)
    {
        IWebElement searchResult = driver.FindElement(By.ClassName("search-result"));
        return searchResult.FindElements(By.CssSelector("a"));
    }
}
