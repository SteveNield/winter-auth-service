using System;
using System.Configuration;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using NUnit.Framework;

namespace Winter.Auth.Service.Test.Integration
{
    [TestFixture]
    public class LoginTests
    {
        [Test]
        public void Can_Return_Response_With_Token_For_Valid_Credentials()
        {
            var client = new WebClient {BaseAddress = ConfigurationManager.AppSettings["Auth.Service.Endpoint"] };
            client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
            var userCredentials = "{ Username: 'TEST', Password: 'TEST' }";

            var response = client.UploadData("login", Encoding.UTF8.GetBytes(userCredentials));

            Assert.IsNotNull(response);
        }
    }
}
