using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using quirky.net.Entities.Response;

namespace quirky.net.Extensions
{
    public static class HttpClientExtensions
    {

        public static async Task<T> GetAsTypeAsync<T>(this HttpClient client, string uri)
        {
            return await ConvertToType<T>(DateTime.UtcNow, await client.GetAsync(uri));
        }


        public static async Task<T> PostAsTypeAsync<T>(this HttpClient client, string uri, string data, string contentType)
        {
            return await ConvertToType<T>(DateTime.UtcNow, await client.PostAsync(uri, new StringContent(data, Encoding.UTF8, contentType)));
        }

        public static async Task<T> DeleteAsTypeAsync<T>(this HttpClient client, string uri)
        {
            return await ConvertToType<T>(DateTime.UtcNow, await client.DeleteAsync(uri));
        }

        public static async Task<T> PutAsTypeAsync<T>(this HttpClient client, string uri, string data, string contentType)
        {
            return await ConvertToType<T>(DateTime.UtcNow, await client.PutAsync(uri, new StringContent(data, Encoding.UTF8, contentType)));
        }

        private static T DeserializeResult<T>(string value)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(value);
            }
            catch (Exception)
            {
                return Activator.CreateInstance<T>();
            }
        }

        private static async Task<T> ConvertToType<T>(DateTime started, HttpResponseMessage message)
        {
            var startRead = DateTime.UtcNow;

            var resultContent = await message.Content.ReadAsStringAsync();
            var readTime = new TimeSpan(DateTime.UtcNow.Ticks - startRead.Ticks);
            var typeResult = default(T);

            if (!string.IsNullOrEmpty(resultContent))
                typeResult = DeserializeResult<T>(resultContent);
            else
                typeResult = Activator.CreateInstance<T>();

            var baseResult = typeResult as BaseResponse;
            if (baseResult != null)
            {
                baseResult.Message = resultContent;
                baseResult.StatusCode = (int)message.StatusCode;
                baseResult.Success = message.IsSuccessStatusCode;
                baseResult.StartTimeStamp = started;
                baseResult.EndTimeStamp = DateTime.UtcNow;
                baseResult.Duration = new TimeSpan(baseResult.EndTimeStamp.Ticks - started.Ticks);
                baseResult.ReadTimeSpan = readTime;
                
                foreach (var header in message.Headers)
                {
                    baseResult.Headers.Add(header.Key, header.Value);
                }
            }
            return typeResult;
        }

    }
}
