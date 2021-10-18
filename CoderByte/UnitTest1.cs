using NUnit.Framework;
using System;
using OpenQA.Selenium.Chrome;
using System.IO;
using System.Net;

namespace CoderByte
{
    public class Tests
    {
        TestFramework tf;

        [OneTimeSetUp]
        public void Setup()
        {
            string Path = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            tf = new TestFramework(new ChromeDriver(Path + @"\Drivers\"));
        }

        [Test]
        public void TestFaceBookURL()
        {
            Assert.AreNotEqual(tf.hrefAmountOnPage("http://www.crawco.co.uk", "https://www.facebook.com/crawfordandco"), 0);
        }

        
        [Test]
        public void TestApiUser4()
        {
            var user = tf.CheckReqresUserNamesByID(4);
            Assert.Multiple(() =>
            {
                Assert.That(user.Firstname, Is.EqualTo("Eve"));
                Assert.That(user.Lastname, Is.EqualTo("Holt"));
            });
        }

        [Test]
        public void TestApiUser6()
        {
            var user = tf.CheckReqresUserNamesByID(6);
            Assert.Multiple(() =>
            {
                Assert.That(user.Firstname, Is.EqualTo("Sergio"));
                Assert.That(user.Lastname, Is.EqualTo("Ramos"));
            });
            //This Test is designed to fail as Firstname is Tracey, not Sergio
        }

        [Test]
        public void TestApiUser23()
        {
            /*
             https://www.selenium.dev/documentation/worst_practices/http_response_codes/
             Selenium doesn't include a simple way to check a pages error codes unless they're represented in page
             text, header or title, so plain C# will be used to check the page for a protocol error code, 
             then it will be visited by selenium
            */
            Assert.Multiple(() =>
            {
               WebException ex = Assert.Throws<WebException>(() => { tf.CheckReqresUserNamesByID(23); });
               Assert.That(ex.Status, Is.EqualTo(WebExceptionStatus.ProtocolError));
            });

        }

        [OneTimeTearDown]
        public void TearDown()
        {
            tf.Teardown();
        }
    }
}