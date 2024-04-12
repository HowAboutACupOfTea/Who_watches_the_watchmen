using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Collections.ObjectModel;

namespace TourOfHeroesTester;

internal static class HeroesPage
{
    internal static void TypeIntoAddHeroField(IWebDriver driver, string heroName)
    {
        IWebElement heroInput = driver.FindElement(By.Id("new-hero"));
        heroInput.SendKeys(heroName);
    }

    internal static void ClickAddHeroButton(IWebDriver driver)
    {
        IWebElement addHeroButton = driver.FindElement(By.ClassName("add-button"));
        addHeroButton.Click();
    }

    internal static ReadOnlyCollection<IWebElement> GetHeroes(IWebDriver driver)
    {
        IWebElement heroes = driver.FindElement(By.ClassName("heroes"));
        return heroes.FindElements(By.CssSelector("li"));
    }

    internal static IWebElement GetNewestHero(IWebDriver driver)
    {
        var heroes = GetHeroes(driver);
        return heroes.Last().FindElement(By.CssSelector("a"));
    }

    internal static void ClickDeleteHeroButton(IWebDriver driver, int indexOfHero)
    {
        var heroes = GetHeroes(driver);
        var sth = heroes[indexOfHero].FindElement(By.ClassName("delete"));
        sth.Click();
    }

    internal static void WaitUntilHeroGotCreated(IWebDriver driver)
    {
        WebDriverWait wait = new(driver, TimeSpan.FromSeconds(5));
        wait.Until(drv => HeroesPage.GetNewestHero(driver).Text.StartsWith("21"));
    }
}
