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
    class GenericTest
    {
        public static void Iniciar()
        {
            IWebDriver driver = null;
            try
            {
                // Código introdutório que acessa a main page do projeto Selenium e baixa os arquivos 
                //necessários para começar o projeto.
                driver = GenericSelenium.IniciarWebsite("http://www.seleniumhq.org/", EDRIVERS.CHROME);
                GenericSelenium.GetElementIfPresentById(driver, "menu_download").Click();
                GenericSelenium.getCellByTableTitle(driver, "Language", 1, 3).Click();
                GenericSelenium.getCellByTableTitle(driver, "Browser", 1, 0).Click();
                // Navega para a pagina de download do Chrome Driver.
                GenericSelenium.getElementsContainsTextByTag(driver, "ChromeDriver", "a")[2].Click();
                GenericSelenium.getElementsContainsTextByTag(driver, "ChromeDriver", "a")[3].Click();
                GenericSelenium.getElementsContainsTextByTag(driver, "chromedriver_win32", "a")[0].Click();
                GenericSelenium.returnPages(driver,6);
            }
            catch (Exception ex)
            {
                if (driver != null)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}

