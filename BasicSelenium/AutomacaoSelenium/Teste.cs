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
using System.IO;

namespace BasicSelenium
{
    class Teste
    {

        public static void Iniciar()
        {
            IWebDriver driver = null;


            driver = GenericSelenium.IniciarWebsite("https://www.kabum.com.br/cgi-local/site/login/login.cgi", EDRIVERS.CHROME);
            GenericSelenium.LoadCookieData(driver);
            



            //GenericSelenium.login(driver, "textfield12", "textfield15", "", "");

           // GenericSelenium.SaveCookieData(driver);
        }
    }
}
