using NUnit.Framework;
using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.IO;

namespace CoderByte
{
    public class Tests
    {
        IWebDriver driver;
        string Path;
        [SetUp]
        public void Setup()
        {
            Path = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            driver = new ChromeDriver(Path+@"\Drivers\");
        }

        [Test]
        public void Test1()
        {
            driver.Navigate().GoToUrl("http://www.crawco.co.uk");
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}