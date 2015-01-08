using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using quirky.net.Contracts;
using quirky.net.Entities.Domain;
using quirky.net.Entities.Response;
using quirky.net.Extensions;

namespace quirky.net
{
    public class WinkClient
    {
        #region boilerplate / overhead

        private static string ContentType = "application/json";
        private HttpClientHandler handler = null;
        private HttpClient client = null;
        private IConfiguration _configuration = null;

        //private LoginData LoginData = null;
        public WinkClient(IConfiguration configuration)
        {
            _configuration = configuration;
            handler = new HttpClientHandler();
            client = new HttpClient(handler);

            handler.AllowAutoRedirect = true;
            handler.CookieContainer = new CookieContainer();
            handler.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            handler.ClientCertificateOptions = ClientCertificateOption.Automatic;

            if (!string.IsNullOrEmpty(_configuration.User_Agent))
            {
                client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", _configuration.User_Agent);
            }
        }

        private string FormatUrl(string resource, Dictionary<string, string> queryString = null)
        {
            var url = string.Format("{0}{1}", _configuration.BaseUrl, resource);
            if (queryString != null)
            {
                if (!url.Contains("?"))
                {
                    url = url + "?";
                }
                foreach (var n in queryString.Keys)
                {
                    url = url + string.Format("{0}={1}", n, System.Uri.EscapeUriString(queryString[n]));
                }
            }
            return url;
        }

        #endregion


        #region API

        public async Task<LoginResponse> Login()
        {
            var o = new
            {
                username = _configuration.Username,
                client_secret = _configuration.Client_Secret,
                password = _configuration.Password,
                client_id = _configuration.Client_Id,
                grant_type = _configuration.GrantType
            };
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(o);
            return await client.PostAsTypeAsync<LoginResponse>(FormatUrl("/oauth2/token"), json, ContentType).ContinueWith<LoginResponse>(t => SetHeader(t.Result));
        }


        public async Task<ListDevicesResponse> ListDevices()
        {
            return await client.GetAsTypeAsync<ListDevicesResponse, List<Device>>(FormatUrl("/users/me/wink_devices"), "Devices");
        }

        public async Task<Device> UpdateDevice(Device device)
        {
            return
                await
                    client.PutAsTypeAsync<Device>(
                        FormatUrl(string.Format("/{0}/{1}", device.Radio_Type, device.Local_Id)),
                        Newtonsoft.Json.JsonConvert.SerializeObject(device), ContentType);
        }
        public async Task<Device> RefreshDevice(Device device)
        {
            return
                await
                    client.PutAsTypeAsync<Device>(
                        FormatUrl(string.Format("/{0}/{1}/refresh", device.Radio_Type, device.Local_Id)),
                        Newtonsoft.Json.JsonConvert.SerializeObject(device), ContentType);
        }
        public async Task<BaseResponse> GetUser()
        {
            return await client.GetAsTypeAsync<BaseResponse>(FormatUrl("/users/me"));
        }

        private LoginResponse SetHeader(LoginResponse response)
        {
            //if (response.Success) LoginData = response.data;
            client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", string.Format("Bearer {0}", response.data.access_token));
            return response;
        }

        #endregion



    }
}