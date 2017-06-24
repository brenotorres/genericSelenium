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
    class ExtracaoInformacaoGoogleMaps
    {

        public static void Iniciar()
        {
            IWebDriver driver = null;
            try
            {

                string dir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
                string restaurante;
                string address= ""; 
                string website = ""; 
                string phone= ""; 
                string openTime= ""; 
                string ComentariosPrincipais= "";
                string Nota = "";
                bool pulaRestaurante = false;

                // Read the file and display it line by line.
                System.IO.StreamReader file = new System.IO.StreamReader(@dir + "\\restaurantes.txt");
                StreamWriter sw = new StreamWriter(@dir + "\\restaurantesOutput.txt");
                driver = GenericSelenium.IniciarWebsite("https://www.google.com.br/maps/", EDRIVERS.CHROME);
                GenericSelenium.SaveCookieData(driver);

                while ((restaurante = file.ReadLine()) != null)
                {
                    driver.FindElement(By.Id("searchboxinput")).Clear();
                    driver.FindElement(By.Id("searchboxinput")).SendKeys(restaurante);
                    driver.FindElement(By.Id("searchbox-searchbutton")).Click();

                    System.Threading.Thread.Sleep(3000);

                    if (driver.FindElements(By.ClassName("section-result-text-content")).Count > 0)
                    {
                        if (GenericSelenium.getElementsContainsTextByTag(driver, restaurante, "span") != null)
                        {
                            GenericSelenium.getElementsContainsTextByTag(driver, restaurante, "span").FirstOrDefault().Click();
                        }
                        else
                        {
                            driver.FindElement(By.ClassName("section-result-text-content")).Click();
                        }
                    }


                    if (GenericSelenium.GetElementIfPresentByClassName(driver, "section-info-text") != null)
                    {
                        address = GenericSelenium.GetElementIfPresentByClassName(driver, "section-info-text").Text;
                    }
                    else
                    {
                        Console.WriteLine("Endereço não encontrado");
                        sw.WriteLine("Endereço não encontrado");
                        pulaRestaurante = true;
                    }

                    if (pulaRestaurante == false) { 

                    if (GenericSelenium.GetElementIfPresentByAttribute(driver, "data-attribution-url") != null)
                        website = GenericSelenium.GetElementIfPresentByAttribute(driver, "data-attribution-url").GetAttribute("data-attribution-url");

                    if (GenericSelenium.GetElementIfPresentByAttribute(driver, "data-href") != null)
                        phone = GenericSelenium.GetElementIfPresentByAttribute(driver, "data-href").GetAttribute("data-href");

                    if (GenericSelenium.GetElementIfPresentByClassName(driver, "section-info-hour-text") != null)
                        openTime = GenericSelenium.GetElementIfPresentByClassName(driver, "section-info-hour-text").Text;
                    
                    ComentariosPrincipais = GenericSelenium.MontarComentarios(GenericSelenium.GetElementsOfGivenAttributeFromGivenClass(driver, "section-review-snippet-line", "jsinstance"));

                    if (GenericSelenium.GetElementIfPresentByClassName(driver, "section-star-display") != null)
                        Nota = GenericSelenium.GetElementIfPresentByClassName(driver, "section-star-display").Text;

                    }

                    pulaRestaurante = false;


                    if(website.Length > 100){
                        website = "";
                    }

                    int index = openTime.IndexOfAny("0123456789".ToCharArray());

                    if (index<0)
                        index = 0;

                    Console.WriteLine("Carregando informações para : " + restaurante);
                    
                    Console.WriteLine("Endereço: " + address);
                    Console.WriteLine("Website: " + website);
                    Console.WriteLine("Telefone: " +phone);
                    Console.WriteLine("Horarios: " + openTime.Substring(index));
                    Console.WriteLine("Comentarios Principais: " + ComentariosPrincipais);
                    Console.WriteLine("Nota dos usuarios: " + Nota);

                    Console.WriteLine("----------------------------------------");

                    sw.WriteLine("Carregando informações para : " + restaurante);

                    sw.WriteLine("Endereço: " + address);
                    sw.WriteLine("Website: " + website);
                    sw.WriteLine("Telefone: " + phone);
                    sw.WriteLine("Horarios: " + openTime.Substring(index));
                    sw.WriteLine("Comentarios Principais: " + ComentariosPrincipais);
                    sw.WriteLine("Nota dos usuarios: " + Nota);
                    sw.WriteLine("----------------------------------------");
                    
                    address = " ";
                    website= " ";
                    phone = " ";
                    openTime=" ";
                    ComentariosPrincipais = " ";
                    Nota = " ";
                }
                file.Close();
                sw.Close();
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
