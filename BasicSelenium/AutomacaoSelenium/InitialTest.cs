using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;

namespace BasicSelenium
{
    class InitialTest
    {
        public static void Iniciar()
        {
            IWebDriver driver = null;
            try
            {
                // Código introdutório que acessa a main page do projeto Selenium e baixa os arquivos necessários para começar o projeto.
                driver = GenericSelenium.IniciarWebsite("http://www.seleniumhq.org/", EDRIVERS.CHROME);
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
           
                driver.FindElement(By.Id("menu_download")).Click();
                IWebElement downloadAPI = GenericSelenium.getCellByTableTitle(driver, "Language", 1, 3);
                IWebElement DriverPage = GenericSelenium.getCellByTableTitle(driver, "Browser", 1, 0);
                downloadAPI.Click();

                // Navega para a pagina de download do Chrome Driver.
                DriverPage.Click();
                driver.FindElements(By.XPath("//div[@id='sites-canvas-main-content']//a[Contains(Text(),'ChromeDriver'))]"))[0].Click();
                driver.FindElements(By.XPath("//div[@id='sites-canvas-main-content']//a[Contains(Text(),'ChromeDriver'))]"))[0].Click();
                wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//a[Contains(Text(),'ChromeDriver_win32.zip')]"))).Click();
            }
            catch (Exception ex)
            {
                if (driver != null)
                {
                    Console.WriteLine(ex.Message);
                    //driver.Quit();
                }
            }
        }
    }
    public enum EDRIVERS
    {
        CHROME, IE
    }
}

