// See https://aka.ms/new-console-template for more information
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.IO;
using Xunit;
using OpenQA.Selenium.Support.UI;
using LoremNET;



namespace SeleniumPrueba
{
    class Program
    {
        private const string baseUrl = "http://10.200.4.106/";
        private static int _Numero = 0;
        private static string AMBIENTE = "LOCAL";

        private static int Get_Numero()
        {
            return _Numero++;
        }

        static void Main(string[] args)
        {
            SeleniumUnitTest test = new SeleniumUnitTest();

            test.Initialize();
            //test.TestTextFieldAndButtonClick();
            int Numero = Get_Numero();



            Console.WriteLine("Seleccionar rol (Enlace (1),Admin (2),Coordinador de Dependencia (3))");
            int ROL = Convert.ToInt32(Console.ReadLine());
            switch (ROL)
            {
                case 1:
                    test.TestLogin("Edgar.leal","hola");
                     Get_Numero();
                    test.Captura("Login", Get_Numero().ToString());
                    test.ClickLogin();
                    test.DarAltaEnlace();
                    break;
                case 2:
                    test.TestLogin("leleal", "");

                    test.Captura("Login", Get_Numero().ToString());
                    test.ClickLogin();
                    test.DarAltaEnlace();
                    test.AsignaEnlace();
                    break;
                 case 3:
                    test.TestLogin("fernando.aguilar", "hola");

                    test.Captura("Login", Get_Numero().ToString());
                    test.ClickLogin();
                    break;

            }
          
            

            //test.Captura("Login", Numero.ToString());
            //test.Cleanup();

        }
        public class SeleniumUnitTest
        {
            private IWebDriver driver;

            public void Initialize()
            {
                // Inicializar el WebDriver (puedes configurarlo según tus necesidades)
                driver = new ChromeDriver();
            }

            public void TestTextFieldAndButtonClick()
            {

                switch (1)
                {
                    case 1:
                        // Navegar a la página web
                        driver.Navigate().GoToUrl("https://www.google.com");

                        // Encontrar el campo de entrada de texto por su selector CSS
                        IWebElement textField = driver.FindElement(By.CssSelector("textarea[type='search']"));

                        // Escribir texto en el campo de entrada
                        textField.SendKeys("Texto de prueba");

                        // Encontrar el botón por su selector CSS
                        IWebElement button = driver.FindElement(By.CssSelector("input[type='submit']"));
                        System.Threading.Thread.Sleep(3000);
                        // Hacer clic en el botón
                        button.Click();

                        // Realizar afirmaciones (assertions) para verificar el resultado
                        string expectedText = "Lorem Ipsum - All the facts - Lipsum generator";
                        string loremText = Lorem.Words(50);
                        //string actualText = driver.FindElement(By.CssSelector(".result")).Text;
                        string actualText = driver.FindElement(By.CssSelector("h3")).Text.Trim();
                        Assert.Equal(expectedText, actualText);

                        break;
                    default:
                        Console.WriteLine("");
                        break;
                }
            }
            public void Cleanup()
            {
                // Cerrar el navegador al final de la prueba
                driver.Quit();
            }

            public void TestLogin(string Usr,string pass)
            {
                // Navegar a la URL
                driver.Navigate().GoToUrl(baseUrl);

                // Encontrar el campo de usuario por su ID
                IWebElement userField = driver.FindElement(By.Id("usrPlaceholder"));

                // Introducir el usuario "leleal" en el campo
                userField.SendKeys(Usr);

                IWebElement passField = driver.FindElement(By.Id("pswPlaceholder"));

                // Introducir el usuario "leleal" en el campo
                passField.SendKeys(pass);

                // Puedes agregar más acciones o afirmaciones aquí según tu caso de prueba
            }

            public void ClickLogin()
            {
                Console.WriteLine("STEP: 2 -> Dar click");
                // Encontrar el botón por su texto "Ingresar"
                IWebElement ingresarButton = null;
                //IWebElement ingresarButton = driver.FindElement(By.XPath());
                //LOGIN
                FunctionAgregar(ingresarButton, "//*[@id=\"root\"]/div[1]/div/div[2]/div/div/div/div[6]/button",5000);
                System.Threading.Thread.Sleep(5000);
                ///
                if (AMBIENTE == "LOCAL")
                {
                    FunctionAgregar(ingresarButton, "/html/body/div[2]/div[3]/div/div/div[2]/div/div[1]/div/div",3000);
                }


                //SELECCIONAR EL MENU
                FunctionAgregar(ingresarButton, "//*[@id=\"root\"]/div/header/div/div/div[1]/button",2000);

                //SELECCIONAR EL ALMACEN
                FunctionAgregar(ingresarButton, "/html/body/div[2]/div[3]/div/div/div/ul/div[2]/div",2000);


                FunctionAgregar(ingresarButton, "/html/body/div[2]/div[3]/div/div/div/ul/div[2]/div[2]/div/div/div/div/div",2000);

                FunctionAgregar(ingresarButton, "/html/body/div[2]/div[3]/div/div/div/ul/div[2]/div[2]/div/div/div/div/div[2]/div/div/div/div[1]",4000);


            }

            public IWebElement FunctionAgregar(IWebElement Botton,string XPATH, int Timeout )
            {
                IWebElement ingresarButton = driver.FindElement(By.XPath(XPATH));
                System.Threading.Thread.Sleep(Timeout);
                ingresarButton.Click();

                return ingresarButton;

            }
            public void Captura(string Carpeta, string Nombre)
            {
                try
                {

                
                // Tomar una captura de pantalla
                Screenshot screenshot = ((ITakesScreenshot)driver).GetScreenshot();

                // Define la ruta de la carpeta donde deseas guardar la captura de pantalla
                string folderPath = @"C:\Users\Pc\source\repos\SeleniumC#\Screenshots\" + Carpeta;

                // Asegúrate de que la carpeta de destino exista o créala si es necesario
                Directory.CreateDirectory(folderPath);

                // Define el nombre del archivo de la captura de pantalla (puedes personalizarlo)
                string screenshotFileName = Nombre +".png";

                // Combina la ruta de la carpeta con el nombre del archivo
                string screenshotFilePath = Path.Combine(folderPath, screenshotFileName);

                // Guarda la captura de pantalla en el archivo especificado
                screenshot.SaveAsFile(screenshotFilePath, ScreenshotImageFormat.Png);

                Console.WriteLine("Captura de pantalla guardada en: " + screenshotFilePath);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Ocurrió un error: " + ex.Message);
                }
                finally
                {
                    // Cierra el navegador
                    //driver.Quit();
                }

            }
            public void DarAltaEnlace()
            {
                Console.WriteLine("STEP: 3 -> Dar Alta Muebles");
                Console.WriteLine("STEP: 3.1 -> STEP 1");
                Random random = new Random();
                // Encontrar el botón por su texto "Ingresar"
                IWebElement ingresarButton = null;
                IWebElement selectElement = null;
                //BUTON DE DAR ALTA
                System.Threading.Thread.Sleep(5000);
                FunctionAgregar(ingresarButton, "/html/body/div/div/div/div/div[3]/div[5]/div/div/button", 5000);
                System.Threading.Thread.Sleep(5000);
                IWebElement cantidad = FunctionAgregar(ingresarButton, "/html/body/div/div/div/div/div[3]/div/div[2]/div[1]/div[1]/form/div/div/input", 1000);

                cantidad.SendKeys(random.NextInt64(100).ToString());

                IWebElement NoActivo = FunctionAgregar(ingresarButton, "/html/body/div/div/div/div/div[3]/div/div[2]/div[1]/div[2]/form/div/div/input", 1000);

                NoActivo.SendKeys(random.NextInt64(100).ToString());


                // Espera a que aparezcan las opciones de autocompletado (ajusta el tiempo según sea necesario)
                Thread.Sleep(TimeSpan.FromSeconds(2));

                // Localiza todos los elementos <input> con aria-autocomplete="list"
                IList<IWebElement> autocompleteElements = driver.FindElements(By.CssSelector("input[aria-autocomplete='list']"));

                // Realiza acciones en cada elemento encontrado
                foreach (IWebElement autocompleteElement in autocompleteElements)
                {


                    // Intenta localizar y hacer clic en el primer elemento de la lista desplegable (ul[role='listbox'] li:first-child)
                    try
                    {
                        autocompleteElement.SendKeys("a");

                        // Pausa de 4 segundos (Thread.Sleep) - Esto puede no ser necesario, depende de la velocidad de carga de la página
                        Thread.Sleep(TimeSpan.FromSeconds(2));
                        // Envía la tecla de flecha hacia abajo para abrir la lista de opciones
                        autocompleteElement.SendKeys(Keys.ArrowDown);

                        // Envía la tecla "Enter" para seleccionar la primera opción
                        autocompleteElement.SendKeys(Keys.Enter);
                        // Puedes realizar otras acciones según tus necesidades.
                    }
                    catch (NoSuchElementException)
                    {
                        // Manejo de excepciones si no se encuentra el elemento en la lista desplegable
                        // Puedes agregar lógica de manejo de errores aquí
                    }
                }



                IWebElement depre = FunctionAgregar(ingresarButton, "/html/body/div/div/div/div/div[3]/div/div[2]/div[3]/div[3]/form/div/div/input", 1000);

                depre.SendKeys(random.NextInt64(100).ToString());



                IWebElement costoSinIVA = FunctionAgregar(ingresarButton, "/html/body/div/div/div/div/div[3]/div/div[2]/div[5]/div[1]/form/div/div/input", 1000);

                costoSinIVA.SendKeys(random.NextInt64(100).ToString());

                IWebElement costoConIVA = FunctionAgregar(ingresarButton, "/html/body/div/div/div/div/div[3]/div/div[2]/div[5]/div[2]/form/div/div/input", 1000);

                costoConIVA.SendKeys(random.NextInt64(100).ToString());


                // Localiza el elemento de entrada de fecha por su ID
                IList<IWebElement> dateInputElements = driver.FindElements(By.CssSelector("input[type='date']"));

                // Obtiene la fecha de hoy en el formato deseado (yyyy-MM-dd)
                string fechaHoy = DateTime.Now.ToString("dd-MM-yyyy");

                // Modifica cada elemento de entrada de fecha
                foreach (var dateInputElement in dateInputElements)
                {
                    // Borra cualquier valor existente en el campo de entrada
                    dateInputElement.Clear();
                    Thread.Sleep(TimeSpan.FromSeconds(2));
                    // Envía las teclas con la fecha actual al campo de entrada
                    dateInputElement.SendKeys(fechaHoy);
                }


                FunctionAgregar(ingresarButton, "//*[@id=\"root\"]/div/div/div/div[3]/div/div[3]/div/button[3]/p", 5000);


                Thread.Sleep(TimeSpan.FromSeconds(2));
                Console.WriteLine("STEP: 3.2 -> STEP 2");

                // Localiza todos los elementos <input> con aria-autocomplete="list"
                IList<IWebElement> autocompleteElements3 = driver.FindElements(By.CssSelector("input[aria-autocomplete='list']"));

                // Realiza acciones en cada elemento encontrado
                foreach (IWebElement autocompleteElement in autocompleteElements3)
                {

                    try
                    {
                        if (autocompleteElement.GetAttribute("value") != "Adjudicacion")
                        {
                            Thread.Sleep(TimeSpan.FromSeconds(2));
                            var item_found = autocompleteElement.FindElement(By.Id("TipoAltaAutocomplete"));
                            
                            autocompleteElement.SendKeys("s");
                            item_found.Clear();
                            item_found.SendKeys("Gasto corriente");
                            autocompleteElement.SendKeys(Keys.ArrowDown);
                            Thread.Sleep(TimeSpan.FromSeconds(2));
                            autocompleteElement.SendKeys(Keys.Enter);
                        }
                       
                    }
                    catch (NoSuchElementException)
                    {
                        // Manejo de excepciones si no se encuentra el elemento en la lista desplegable
                        // Puedes agregar lógica de manejo de errores aquí
                    }
                }

                // Localiza el elemento de entrada de fecha por su ID
                IList<IWebElement> dateInputElements2 = driver.FindElements(By.CssSelector("input[type='number']"));


                foreach (var item2 in dateInputElements2)
                {
                    Thread.Sleep(TimeSpan.FromSeconds(4));
                    item2.SendKeys(random.NextInt64(100).ToString());
                }

                var Folio = FunctionAgregar(ingresarButton, "/html/body/div/div/div/div/div[3]/div/div[2]/div[3]/div[2]/form/div/div/input", 2000);
                Folio.SendKeys("Random");

                var Serie = FunctionAgregar(ingresarButton, "/html/body/div/div/div/div/div[3]/div/div[2]/div[4]/div[4]/form/div/div/input", 2000);
                Serie.SendKeys("Random");

                var ValorFactura = FunctionAgregar(ingresarButton, "/html/body/div/div/div/div/div[3]/div/div[2]/div[5]/div[1]/form/div/div/input", 2000);
                ValorFactura.SendKeys(random.NextInt64(100).ToString());

                var Descripcion = FunctionAgregar(ingresarButton, "/html/body/div/div/div/div/div[3]/div/div[2]/div[5]/div[2]/form/div/div/input", 2000);
                Descripcion.SendKeys("Random");
                

                var Condiciones = FunctionAgregar(ingresarButton, "/html/body/div/div/div/div/div[3]/div/div[2]/div[5]/div[3]/form/div/div/div", 2000);
                Thread.Sleep(TimeSpan.FromSeconds(2));
                FunctionAgregar(ingresarButton, "//*[@id=\"menu-\"]/div[3]/ul/li[2]", 1000);

                FunctionAgregar(ingresarButton, "//*[@id=\"root\"]/div/div/div/div[3]/div/div[3]/div/button[3]", 3000);

                Console.WriteLine("STEP: 3.3 -> STEP 3");
                var CodigoContable = FunctionAgregar(ingresarButton, "/html/body/div/div/div/div/div[3]/div/div[2]/div[1]/div[1]/form/div/div/input", 2000);
                CodigoContable.SendKeys("Random");

                // Localiza el elemento de entrada de fecha por su ID
                 dateInputElements = driver.FindElements(By.CssSelector("input[type='date']"));


                // Modifica cada elemento de entrada de fecha
                foreach (var dateInputElement in dateInputElements)
                {
                    // Borra cualquier valor existente en el campo de entrada
                    dateInputElement.Clear();
                    Thread.Sleep(TimeSpan.FromSeconds(2));
                    // Envía las teclas con la fecha actual al campo de entrada
                    dateInputElement.SendKeys(fechaHoy);
                }

                // Localiza el elemento de entrada de fecha por su ID
                dateInputElements2 = driver.FindElements(By.CssSelector("input[type='number']"));


                foreach (var item2 in dateInputElements2)
                {
                    Thread.Sleep(TimeSpan.FromSeconds(4));
                    item2.SendKeys(random.NextInt64(100).ToString());
                }

                var NoFactura = FunctionAgregar(ingresarButton, "/html/body/div/div/div/div/div[3]/div/div[2]/div[2]/div/form/div/div/input", 2000);
                NoFactura.SendKeys(random.NextInt64(100).ToString());



            }
            public void AsignaEnlace()
            {
                Thread.Sleep(TimeSpan.FromSeconds(5));
                // Localiza todos los elementos <input> con aria-autocomplete="list"
                IList<IWebElement> autocompleteElements = driver.FindElements(By.CssSelector("input[aria-autocomplete='list']"));

                // Realiza acciones en cada elemento encontrado
                foreach (IWebElement autocompleteElement in autocompleteElements)
                {

                    try
                    {
                        Thread.Sleep(TimeSpan.FromSeconds(2));
                        autocompleteElement.SendKeys("Enrique");
                        autocompleteElement.SendKeys(Keys.ArrowDown);
                        Thread.Sleep(TimeSpan.FromSeconds(2));
                        autocompleteElement.SendKeys(Keys.Enter);
                    }
                    catch (NoSuchElementException)
                    {
                        // Manejo de excepciones si no se encuentra el elemento en la lista desplegable
                        // Puedes agregar lógica de manejo de errores aquí
                    }
                }
            }
        }
           


    }
}
   