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
using OpenQA.Selenium.Interactions;
using System.IO;



namespace BasicSelenium
{
  class GenericSelenium
  {
    public const string DRIVER_CHROME = @"../../Drivers/chromedriver_win32";
    public const string DRIVER_IE = @"../../Drivers/IEDriverServer_win32";


    static void Main(string[] args)
    {
        //GenericTest.Iniciar();
        //InitialTest.TesteInicial();
        ExtracaoInformacaoGoogleMaps.Iniciar();
        //Teste.Iniciar();
        
    }


    public static IWebDriver IniciarWebsite(string url, EDRIVERS navegador)
    {
      string pathIe = @DRIVER_IE;
      string pathChrome = @DRIVER_CHROME;
      IWebDriver driver = null;
      switch (navegador)
      {
        case EDRIVERS.IE:
          var options = new InternetExplorerOptions();
          var driverServiceieIE = InternetExplorerDriverService.CreateDefaultService(pathIe);
          //driverServiceieIE.HideCommandPromptWindow = true;
          options.IntroduceInstabilityByIgnoringProtectedModeSettings = true;
          options.EnablePersistentHover = false;
          driver = new InternetExplorerDriver(driverServiceieIE, options);
          break;

        case EDRIVERS.CHROME:
          var driverService = ChromeDriverService.CreateDefaultService(pathChrome);
          //driverService.HideCommandPromptWindow = true;
          ChromeOptions optionsChrome = new ChromeOptions();
          optionsChrome.AddArgument("--log-level=3");
          optionsChrome.AddArgument("--start-maximized");
          driver = new ChromeDriver(driverService, optionsChrome);
          break;

        default:
          Console.WriteLine("Não encontrado");
          break;
      }
      driver.Navigate().GoToUrl(url);
      return driver;
    }

    public static IWebElement pegarElementoPai(IWebDriver Driver, IWebElement childElement)
    {
        if (isAlertPresent(Driver))
        {
            Driver.SwitchTo().Alert().Dismiss();
        }

        IJavaScriptExecutor executor = (IJavaScriptExecutor)Driver;
        IWebElement parentElement = (IWebElement)executor.ExecuteScript("return arguments[0].parentNode;", childElement);
        return parentElement;
    }

    public static IWebElement getCellByTableTitle(IWebDriver Driver, String header, int rowIndex, int columnIndex)
    {
        if (isAlertPresent(Driver))
        {
            Driver.SwitchTo().Alert().Dismiss();
        }

        WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(5));
        wait.Until<IWebElement>(ExpectedConditions.ElementIsVisible(By.XPath("//table")));
        IWebElement table = Driver.FindElements(By.XPath("//table[thead/tr/*[contains(text(), '"+header+"')]]")).FirstOrDefault();
        return (IWebElement)table.FindElements(By.XPath("./tbody/tr"))[rowIndex].FindElements(By.XPath("./td"))[columnIndex];
    }


    public static IList<IWebElement> getElementsContainsTextByTag(IWebDriver Driver, String Text, String Tag)
    {

        try
        {
            if (isAlertPresent(Driver))
            {
                Driver.SwitchTo().Alert().Dismiss();
            }

            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(5));
            wait.Until<IWebElement>(ExpectedConditions.ElementToBeClickable(By.XPath("//" + Tag + "[contains(text(), '" + Text + "')]")));
            IList<IWebElement> ListaElementos = Driver.FindElements(By.XPath("//" + Tag + "[contains(text(), '" + Text + "')]"));
            return ListaElementos;
        }
        catch (Exception ex)
        {
            return default(IList<IWebElement>);
        }

    }


    public static IWebElement GetElementIfPresentById(IWebDriver Driver, String id)
    {
        try
        {
            if (isAlertPresent(Driver))
            {
                Driver.SwitchTo().Alert().Dismiss();
            }

            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(5));
            wait.Until<IWebElement>(ExpectedConditions.ElementIsVisible(By.Id(id)));
            return Driver.FindElement(By.Id(id));
        }
        catch (Exception ex)
        {
            return default(IWebElement);
        }

    }

    public static IWebElement GetElementIfPresentByTag(IWebDriver Driver, String id)
    {
        try
        {
            if (isAlertPresent(Driver))
            {
                Driver.SwitchTo().Alert().Dismiss();
            }

            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(5));
            wait.Until<IWebElement>(ExpectedConditions.ElementIsVisible(By.Id(id)));
            return Driver.FindElement(By.Id(id));
        }
        catch (Exception ex)
        {
            return default(IWebElement);
        }

    }


    public static IWebElement GetElementIfPresentByCSS(IWebDriver Driver, String CSS)
    {
        try
        {
            if (isAlertPresent(Driver))
            {
                Driver.SwitchTo().Alert().Dismiss();
            }

            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(5));
            wait.Until<IWebElement>(ExpectedConditions.ElementIsVisible(By.CssSelector(CSS)));
            return Driver.FindElement(By.Id(CSS));
        }
        catch (Exception ex)
        {
            return default(IWebElement);
        }

    }


    public static IWebElement GetElementIfPresentByName(IWebDriver Driver, String name)
    {
        try
        {
            if (isAlertPresent(Driver))
            {
                Driver.SwitchTo().Alert().Dismiss();
            }

            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(5));
            wait.Until<IWebElement>(ExpectedConditions.ElementIsVisible(By.Name(name)));
            return Driver.FindElement(By.Id(name));
        }
        catch (Exception ex)
        {
            return default(IWebElement);
        }

    }


    public static IWebElement GetElementIfPresentByXpath(IWebDriver Driver, String xpath)
    {
        try
        {

            if (isAlertPresent(Driver))
            {
                Driver.SwitchTo().Alert().Dismiss();
            }

            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(5));
            wait.Until<IWebElement>(ExpectedConditions.ElementIsVisible(By.XPath(xpath)));
            return Driver.FindElement(By.Id(xpath));
        }
        catch (Exception ex)
        {
            return default(IWebElement);
        }

    }

    public static IWebElement GetElementIfPresentByPartialLinkText(IWebDriver Driver, String PartialLinkText)
    {
        try
        {
            if (isAlertPresent(Driver))
            {
                Driver.SwitchTo().Alert().Dismiss();
            }

            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(5));
            wait.Until<IWebElement>(ExpectedConditions.ElementIsVisible(By.PartialLinkText(PartialLinkText)));
            return Driver.FindElement(By.Id(PartialLinkText));
        }
        catch (Exception ex)
        {
            return default(IWebElement);
        }

    }

    public static IWebElement GetElementIfPresentByClassName(IWebDriver Driver, String ClassName)
    {
        try
        {

            if (isAlertPresent(Driver))
            {
                Driver.SwitchTo().Alert().Dismiss();
            }

            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(5));
            wait.Until<IWebElement>(ExpectedConditions.ElementIsVisible(By.ClassName(ClassName)));
            return Driver.FindElement(By.ClassName(ClassName));
        }
        catch (Exception ex)
        {
            return default(IWebElement);
        }
    }


    public static IWebElement GetElementIfPresentByAttribute(IWebDriver Driver, String Attribute)
    {

        try
        {

            if (isAlertPresent(Driver))
            {
                Driver.SwitchTo().Alert().Dismiss();
            }

            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(5));
            wait.Until<IWebElement>(ExpectedConditions.ElementIsVisible(By.XPath("//*[@" + Attribute + "]")));
            return Driver.FindElement(By.XPath("//*[@" + Attribute + "]"));
        }
        catch (Exception ex)
        {
            return default(IWebElement);
        }

    }

       public static IList<IWebElement> GetElementsOfGivenAttributeFromGivenClass(IWebDriver Driver, String Class, String Attribute )
    {

        if (isAlertPresent(Driver))
        {
            Driver.SwitchTo().Alert().Dismiss();
        }

        if (Driver.FindElements(By.XPath("//*[contains(@class,'" + Class + "')]//*[@" + Attribute + "]")).Count > 0)
        {
            Actions builder = new Actions(Driver);
            builder.MoveToElement(Driver.FindElement(By.ClassName("section-write-review-container")));//By.XPath("//*[contains(@class,'" + Class + "')]//*[@" + Attribute + "]")[0]
            builder.Perform();
            return Driver.FindElements(By.XPath("//*[contains(@class,'" + Class + "')]//*[@" + Attribute + "]"));
        }

        return default(IList<IWebElement>);
    }

       public static String MontarComentarios(IList<IWebElement> elementos)
       {
           String comentario = "";
           if ((elementos != null) && (elementos.Any()))
           {
               foreach (IWebElement elemento in elementos)
               {
                   comentario = " " + comentario + " " + elemento.Text;
               }

           }


           return comentario;
       }

      

      
    public static void ChangeContextTo(IWebDriver Driver, String Context)
    {
        if (isAlertPresent(Driver))
        {
            Driver.SwitchTo().Alert().Dismiss();
        }

        Driver.SwitchTo().Window(Context);

    }

    public static void GoToFirstPage(IWebDriver Driver)
    {
        if (isAlertPresent(Driver))
        {
            Driver.SwitchTo().Alert().Dismiss();
        }
        
        Driver.SwitchTo().Window(Driver.WindowHandles.First());
        WaitForLoad(Driver);
    }

    public static void GoToLastPage(IWebDriver Driver)
    {
        if (isAlertPresent(Driver))
        {
            Driver.SwitchTo().Alert().Dismiss();
        }
        Driver.SwitchTo().Window(Driver.WindowHandles.Last());
        WaitForLoad(Driver);
    }

    public static void returnPages(IWebDriver Driver, int NumberOfPages)
    {
           try
        {
            for (int i = 0; i < NumberOfPages;i++ )
                Driver.Navigate().Back();
        }
        catch(Exception ex)
        {
            Console.Write(ex.Message);
        }
           WaitForLoad(Driver);
        
    }


    public static void SaveCookieData(IWebDriver Driver) { 
        string dir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
        StreamWriter sw = new StreamWriter(@dir + "\\Cookies.data");						
        try		
        {		 
			File.Delete(@dir + "\\Cookies.data");									
            foreach(Cookie ck in Driver.Manage().Cookies.AllCookies)							
            {		
                sw.WriteLine((ck.Name+";"+ck.Value+";"+ck.Domain+";"+ck.Path+";"+ck.Expiry+";"+ck.Secure));																												
        }		
            sw.Flush();			
            sw.Close();					
        }catch(Exception ex)					
        {
            Console.WriteLine(ex.Message);
        }
    }

    public static void LoadCookieData(IWebDriver driver)
    {
        string dir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
        try
        {

            StreamReader sw = new StreamReader(@dir + "\\Cookies.data");
            driver.Manage().Cookies.DeleteAllCookies();
            String strline;
            while ((strline = sw.ReadLine()) != null)
            {
                String[] Tokenizertoken = strline.Split(';');
                if (Tokenizertoken.Count() == 6)
                {
                    String name = Tokenizertoken[0];
                    String value = Tokenizertoken[1];
                    String domain = Tokenizertoken[2];
                    String path = Tokenizertoken[3];
                    DateTime? expiry = null;

                    String val;
                    if (!(val = Tokenizertoken[4]).Equals(""))
                    {
                        expiry = Convert.ToDateTime(val);
                    }

                    Boolean isSecure;
                    Boolean.TryParse(Tokenizertoken[5], out isSecure);
                    Cookie ck = new Cookie(name, value, domain, path, expiry);                 
                    driver.Manage().Cookies.AddCookie(ck);			
                }

            }
            Console.WriteLine("teste");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }


    public static void GoToPageByTitle(IWebDriver Driver, string PageTitle)
    {
        string BaseWindow = Driver.CurrentWindowHandle;  
        IList<string> ListaPaginas = Driver.WindowHandles;
        foreach (string pagina in ListaPaginas)
        {
            if (pagina != BaseWindow)
            {
                if (Driver.SwitchTo().Window(pagina).Title.Contains(PageTitle))
                    break;
            }
        }
        WaitForLoad(Driver);
    }

    public static void login(IWebDriver Driver, string loginTag, string passwordTag, string login, string password)
    {
        if (isAlertPresent(Driver)) {
            Driver.SwitchTo().Alert().Dismiss();
        }
        GetElementIfPresentById(Driver, loginTag).Clear();
        GetElementIfPresentById(Driver, loginTag).SendKeys(login);
        GetElementIfPresentById(Driver, passwordTag).Clear();
        GetElementIfPresentById(Driver, passwordTag).SendKeys(password + Keys.Enter);
        WaitForLoad(Driver);   
    }


    public static void WaitForLoad(IWebDriver driver)
    {
        IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
        int timeoutSec = 15;
        WebDriverWait wait = new WebDriverWait(driver, new TimeSpan(0, 0, timeoutSec));
        wait.Until(wd => js.ExecuteScript("return document.readyState").ToString() == "complete");
    }

    public static bool isAlertPresent(IWebDriver driver)
    {
        try {
            driver.SwitchTo().Alert();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }






  }
}
