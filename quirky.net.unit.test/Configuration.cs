using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using quirky.net.Contracts;

namespace quirky.net.unit.test
{

    public class Configuration : IConfiguration
    {
        public Configuration()
        {
            Username = System.Configuration.ConfigurationManager.AppSettings["username"];
            Password = System.Configuration.ConfigurationManager.AppSettings["password"];
            Client_Secret = System.Configuration.ConfigurationManager.AppSettings["client_secret"];
            Client_Id = System.Configuration.ConfigurationManager.AppSettings["client_id"];
            GrantType = System.Configuration.ConfigurationManager.AppSettings["grant_type"];
            BaseUrl = System.Configuration.ConfigurationManager.AppSettings["base_url"];
            if (string.IsNullOrEmpty(BaseUrl)) BaseUrl = "https://winkapi.quirky.com";
            User_Agent = System.Configuration.ConfigurationManager.AppSettings["user_agent"];
            if (string.IsNullOrEmpty(User_Agent)) User_Agent = "quirky.net";

            if (string.IsNullOrEmpty(Username)) throw new NullReferenceException("Username");
            if (string.IsNullOrEmpty(Password)) throw new NullReferenceException("Password");
            if (string.IsNullOrEmpty(Client_Secret)) throw new NullReferenceException("Client_Secret");
            if (string.IsNullOrEmpty(Client_Id)) throw new NullReferenceException("Client_Id");
            if (string.IsNullOrEmpty(GrantType)) throw new NullReferenceException("GrantType");

        }

        public string Username { get; private set; }
        public string Password { get; private set; }
        public string Client_Secret { get; private set; }
        public string Client_Id { get; private set; }
        public string GrantType { get; private set; }

        public string BaseUrl { get; private set; }

        public string User_Agent { get; private set; }
    }
}
