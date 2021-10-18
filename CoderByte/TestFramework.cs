using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace CoderByte
{
    class TestFramework
    {
        private IWebDriver Driver;

        public TestFramework(IWebDriver driver)
        {
            Driver = driver;
        }

        public int hrefAmountOnPage(string url, string href)
        {
            Driver.Navigate().GoToUrl(url);
            return Driver.FindElements(By.XPath($"//a[@href='{href}']")).Count;
        }

        public (string Firstname, string Lastname) CheckReqresUserNamesByID(int id)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"https://reqres.in/api/users/{id}");
                request.Method = "HEAD";
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            }
            finally
            {
                Driver.Navigate().GoToUrl($"https://reqres.in/api/users/{id}");
            }
            JObject user = JObject.Parse(Driver.FindElement(By.TagName("pre")).Text);
            string first_name = (string)user["data"]["first_name"];
            string last_name = (string)user["data"]["last_name"];

            return (first_name, last_name);

        }


        public void Teardown()
        {
            Driver.Quit();
        }
    }
}
