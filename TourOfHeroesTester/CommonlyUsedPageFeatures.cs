using OpenQA.Selenium;
using System.Collections.ObjectModel;

namespace TourOfHeroesTester;

internal static class CommonlyUsedPageFeatures
{
    internal static ReadOnlyCollection<IWebElement> GetNavigationElements(IWebDriver driver)
    {
        IWebElement navigation = driver.FindElement(By.CssSelector("nav"));
        return navigation.FindElements(By.CssSelector("a"));
    }
}
