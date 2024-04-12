using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;

namespace TourOfHeroesTester;

internal static class SharedMethodsForTesting
{
    public static IWebDriver GetWebDriver()
    {
        // WebDriverManager will download the latest version of the Chrome driver and set the path to it.
        new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig(), VersionResolveStrategy.MatchingBrowser);

        // ChromeDriver is the class that allows us to interact with the browser.
        IWebDriver driver = new ChromeDriver();

        // Implicit wait is the amount of time the driver will wait for a page to load.
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

        return driver;
    }
}
