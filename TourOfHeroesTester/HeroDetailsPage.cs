using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System.Collections.ObjectModel;

namespace TourOfHeroesTester;

internal class HeroDetailsPage
{
    internal static string GetDisplayedHeroName(IWebDriver driver)
    {
        IWebElement heading = driver.FindElement(By.CssSelector("h2"));
        string displayedHeading = heading.GetAttribute("textContent");
        return displayedHeading[0..^8];
    }
    
    internal static void TypeIntoEmptiedHeroNameField(IWebDriver driver, string input)
    {
        IWebElement heroNameInput = driver.FindElement(By.Id("hero-name"));        
        heroNameInput.SendKeys(Keys.Control + "a");
        heroNameInput.SendKeys(Keys.Backspace);
        heroNameInput.SendKeys(input);
    }

    internal static ReadOnlyCollection<IWebElement> GetNavigationElements(IWebDriver driver)
    {
        IWebElement heroDetails = driver.FindElement(By.CssSelector("app-hero-detail"));
        return heroDetails.FindElements(By.CssSelector("button"));
    }

    internal static void ClickNavigateBackButton(IWebDriver driver)
    {
        IWebElement navigateBackButton = GetNavigationElements(driver)[0];
        navigateBackButton.Click();
    }

    internal static void ClickSaveButton(IWebDriver driver)
    {
        IWebElement saveButton = GetNavigationElements(driver)[1];
        saveButton.Click();
    }

    internal static void WaitUntilNextPageLoaded(IWebDriver driver)
    {
        WebDriverWait wait = new(driver, TimeSpan.FromSeconds(5));
        wait.Until(DashboardPage.GetTopHeroes);
    }
}
