using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using System;
using System.Threading;

namespace SeleniumUiTests
{

    [TestClass]
    public class BooksUiTest
    {

        private string _basicURL = "https://localhost:7126/Library";
        ///CreateClient
        private WebDriver _browserDriver;
        public TestContext TestContext { get; set; }

        [TestInitialize]
        public void Initialize()
        {
            // _basicURL = (string)TestContext.Properties["webAppUrl"];
            

        }

        [Priority(0)]
        [TestMethod]
        [TestCategory("Selenium")]
        [DataRow(655, "TestingBook", "TestAuthor","It's a book all about testing" )]
        [DataRow(515, "HelloBook", "HelloAuthor", "It's a book all about testing")]
        [DataRow(425, "FancyBook", "FancyAuthor", "It's a book all about testing")]
        public void A_CreateBookTestUI(int id, string title, string author, string description)
        {
            //Arrange
            _browserDriver = new ChromeDriver();
            _browserDriver.Url = _basicURL;
            _browserDriver.Manage().Window.Maximize();
            _browserDriver.Navigate().GoToUrl(_basicURL + "/CreateClient");
            _browserDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);

            _browserDriver.FindElement(By.Id("Id")).SendKeys(id.ToString());
            _browserDriver.FindElement(By.Id("Title")).SendKeys(title);
            _browserDriver.FindElement(By.Id("Author")).SendKeys(author);
            _browserDriver.FindElement(By.Id("Description")).SendKeys(description);

            //Act
            _browserDriver.FindElement(By.CssSelector(".btn.btn-primary")).Click();
            _browserDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);
            //Assert
            Assert.IsTrue(_browserDriver.PageSource.Contains(title));

            //Cleanup
            string link = "/DeleteClient/" + id.ToString();
            _browserDriver.Navigate().GoToUrl(_basicURL + link);
            _browserDriver.FindElement(By.CssSelector(".btn.btn-danger")).Click();



        }

        [TestMethod]
        [TestCategory("Selenium")]
        [DataRow("/DeleteClient/655", "TestingBook", 655)]
        [DataRow("/DeleteClient/515", "HelloBook", 515)]
        [DataRow("/DeleteClient/425", "FancyBook", 425)]
        public void DeleteBookTestUI(string linkToDelete, string bookTitle, int id)
        {
           
            //Arrange
            _browserDriver = new ChromeDriver();
            _browserDriver.Url = _basicURL;
            _browserDriver.Manage().Window.Maximize();
            _browserDriver.Navigate().GoToUrl(_basicURL + "/CreateClient");
            _browserDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);

            _browserDriver.FindElement(By.Id("Id")).SendKeys(id.ToString());
            _browserDriver.FindElement(By.Id("Title")).SendKeys(bookTitle);
            _browserDriver.FindElement(By.CssSelector(".btn.btn-primary")).Click();
            _browserDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);

            _browserDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);
            _browserDriver.Navigate().GoToUrl(_basicURL + linkToDelete);

            //Act
            _browserDriver.FindElement(By.CssSelector(".btn.btn-danger")).Click();

            //Assert
            Assert.IsTrue(!_browserDriver.PageSource.Contains(bookTitle));

        }
    }

}
