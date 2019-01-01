using LetsCookApp.Models;
using ModernHttpClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LetsCookApp.Managers.ApiProvider
{
   public class ApiProvider : IApiProvider
    {
        public ApiProvider()
        {
            HttpClientHandler handler = new HttpClientHandler();
            _httpClient = new HttpClient();
            TimeSpan ts = TimeSpan.FromMilliseconds(100000);
            _httpClient.Timeout = ts;
        }
        private readonly HttpClient _httpClient;

        async Task<HttpResponseMessage> Request(HttpMethod pMethod, string pUrl, string pJsonContent)
        {
            var httpRequestMessage = new HttpRequestMessage();
            httpRequestMessage.Method = pMethod;
            httpRequestMessage.RequestUri = new Uri(pUrl);

            switch (pMethod.Method)
            {
                case "POST":
                    HttpContent httpContent = new StringContent(pJsonContent, Encoding.UTF8, "application/json");
                    httpRequestMessage.Content = httpContent;
                    break;
                case "GET":
                    //HttpContent httpContentGet = new StringContent(pJsonContent, Encoding.UTF8, "application/json");
                    //httpRequestMessage.Content = httpContentGet;
                    break;
            }
            return await _httpClient.SendAsync(httpRequestMessage);
        }



      

        /// <summary>
        /// Post to the specified url the body.
        /// </summary>
        /// <param name="url">URL to post to.</param>
        /// <param name="body">Body of post.</param>
        /// <typeparam name="T">The Response type (return type) (Jsonable object).</typeparam>
        /// <typeparam name="TR">The RequestType (the body) (Jsonable object).</typeparam>
        public async Task<ApiResult<T>> Post<T, TR>(string url, TR body, Dictionary<string, string> headers = null)
        {
            HttpResponseMessage result = null;
            try
            {

                var json = JsonConvert.SerializeObject(body);
                var response = await Request(HttpMethod.Post, url, json);
               
                var rawResult = await response.Content.ReadAsStringAsync();
                try
                {
                    var deserialized = JsonConvert.DeserializeObject<T>(rawResult);
                    return new ApiResult<T>(rawResult, (int)response.StatusCode, deserialized);
                }
                catch (Exception ex)
                {
                    return new ApiResult<T>(rawResult, 501, Activator.CreateInstance<T>());
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error Message is :-" + ex.Message);
            }
            return new ApiResult<T>(null, null != result ? (int)result.StatusCode : 0, default(T));
        }


        public async Task<ApiResult<T>> Get<T, TR>(string url, TR body, Dictionary<string, string> headers = null)
        {
            HttpResponseMessage result = null;
            try
            {

              
                var response = await Request(HttpMethod.Get, url, null);

                var rawResult = await response.Content.ReadAsStringAsync();
                try
                {
                    var deserialized = JsonConvert.DeserializeObject<T>(rawResult);
                    return new ApiResult<T>(rawResult, (int)response.StatusCode, deserialized);
                }
                catch (Exception ex)
                {
                    return new ApiResult<T>(rawResult, 501, Activator.CreateInstance<T>());
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error Message is :-" + ex.Message);
            }
            return new ApiResult<T>(null, null != result ? (int)result.StatusCode : 0, default(T));
        }
        public ApiResult<T> Delete<T>(string url, Dictionary<string, string> headers = null)
        {
            HttpResponseMessage result = null;
            try
            {
                lock (_httpClient)
                {
                    if (headers != null)
                    {
                        AddHeadersToClient(headers);
                    }
                    result = _httpClient.DeleteAsync(url).Result;
                    if (headers != null)
                    {
                        RemoveHeadersFromClient(headers);
                    }
                }

                var rawResult = result.Content.ReadAsStringAsync().Result;
                try
                {
                    var deserialized = JsonConvert.DeserializeObject<T>(rawResult);
                    return new ApiResult<T>(rawResult, (int)result.StatusCode, deserialized);
                }
                catch (Exception ex)
                {
                    return new ApiResult<T>(rawResult, 501, Activator.CreateInstance<T>());
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error Message is :-" + e.Message);
            }

            return new ApiResult<T>(null, null != result ? (int)result.StatusCode : 0, default(T));
        }

        public ApiResult<T> Put<T, TR>(string url, TR body, Dictionary<string, string> headers = null)
        {
            HttpResponseMessage result = null;
            try
            {
                lock (_httpClient)
                {
                    if (headers != null)
                    {
                        AddHeadersToClient(headers);
                    }
                    result = _httpClient.PutAsync(url, new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json")).Result;
                    if (headers != null)
                    {
                        RemoveHeadersFromClient(headers);
                    }
                }

                var rawResult = result.Content.ReadAsStringAsync().Result;

                try
                {
                    var deserialized = JsonConvert.DeserializeObject<T>(rawResult);
                    return new ApiResult<T>(rawResult, (int)result.StatusCode, deserialized);
                }
                catch (Exception ex)
                {
                    return new ApiResult<T>(rawResult, 501, Activator.CreateInstance<T>());
                }

            }
            catch (Exception e)
            {
                Debug.WriteLine("Error Message is :-" + e.Message);
            }

            return new ApiResult<T>(null, null != result ? (int)result.StatusCode : 0, default(T));
        }


        void AddHeadersToClient(Dictionary<string, string> headers)
        {
            foreach (var kv in headers)
            {
                _httpClient.DefaultRequestHeaders.Add(kv.Key, kv.Value);
            }
            //_httpClient.DefaultRequestHeaders.Authorization= new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes($"{"LetsCookApp"}:{"LetsCookApp"}")));
        }

        void RemoveHeadersFromClient(Dictionary<string, string> headers)
        {
            foreach (var kv in headers)
            {
                _httpClient.DefaultRequestHeaders.Remove(kv.Key);
            }
        }

        public void SaveCookies(string path)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Loads the cookies.
        /// </summary>
        /// <param name="path">Path.</param>
        public void LoadCookies(string path)
        {
            throw new NotImplementedException();

        }

        public void DeleteCookies(string path)
        {
            throw new NotImplementedException();
        }

        public void ExpireCookies()
        {
            throw new NotImplementedException();
        }
    }
}
