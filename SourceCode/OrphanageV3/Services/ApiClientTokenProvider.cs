using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Authentication;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telerik.WinControls.UI.Diagrams;

namespace OrphanageV3.Services
{
    public static class ApiClientTokenProvider
    {
        private static string _hostUri;
        public static string AccessToken { get; private set; }
        private static double remainSeconds = 0;
        private static Timer _timer;

        public static event EventHandler AccessTokenExpired;

        public static event EventHandler MustLogin;

        public static TimeSpan RemainTime
        {
            get
            {
                return TimeSpan.FromSeconds(remainSeconds);
            }
        }

        static ApiClientTokenProvider()
        {
            RefreshHostUri();
            _timer = new Timer(delegate
            {
                remainSeconds--;
                if (AccessToken != null && remainSeconds <= 0)
                {
                    _timer.Change(Timeout.Infinite, Timeout.Infinite);
                    AccessTokenExpired?.Invoke(null, null);
                }
            }, null, Timeout.Infinite, Timeout.Infinite);
        }

        public static void RefreshHostUri()
        {
            var url = Properties.Settings.Default.OrphanageServiceURL;
            if (url.EndsWith("/"))
                url = url.Substring(0, url.Length - 1);
            _hostUri = url;
        }

        public static async Task SetToken(string username, string password)
        {
            var ret = await GetTokenDictionary(username, password);
            try
            {
                ApiClientTokenProvider.AccessToken = ret["access_token"];
                remainSeconds = Convert.ToDouble(ret["expires_in"]);
                //start the timer
                _timer.Change(0, 1000);
            }
            catch
            {
                AccessToken = null;
            }
        }

        public static void DeleteToken()
        {
            AccessToken = null;
            remainSeconds = 0;
            //stop the timer
            _timer.Change(Timeout.Infinite, Timeout.Infinite);
        }

        public static void RaiseMustLoginEvent()
        {
            MustLogin?.Invoke(null, null);
        }

        public static async Task<Dictionary<string, string>> GetTokenDictionary(
            string userName, string password)
        {
            HttpResponseMessage response;
            var pairs = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>( "grant_type", "password" ),
                    new KeyValuePair<string, string>( "username", userName ),
                    new KeyValuePair<string, string> ( "password", password )
                };
            var content = new FormUrlEncodedContent(pairs);

            using (var client = new HttpClient())
            {
                //client.Timeout = new TimeSpan(0, 0, 10);
                var tokenEndpoint = new Uri(new Uri(_hostUri), "Token");
                response = await client.PostAsync(tokenEndpoint, content);

                var responseContent = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    throw new AuthenticationException(string.Format("Error cannot get Token: {0}", responseContent));
                }

                var ret = GetTokenDictionary(responseContent);
                return ret;
            }
        }

        private static Dictionary<string, string> GetTokenDictionary(
            string responseContent)
        {
            Dictionary<string, string> tokenDictionary =
                JsonConvert.DeserializeObject<Dictionary<string, string>>(
                responseContent);
            return tokenDictionary;
        }
    }

    public partial class ApiClient : IApiClient
    {
        partial void PrepareRequest(HttpClient client, HttpRequestMessage request, string url)
        {
            if (ApiClientTokenProvider.AccessToken != null)
            {
                client.DefaultRequestHeaders.Authorization
                      = new AuthenticationHeaderValue("Bearer", ApiClientTokenProvider.AccessToken);
            }
            else
            {
                ApiClientTokenProvider.RaiseMustLoginEvent();
                //await ApiClientProvider.SetToken("مدير", "0000");
                //if (ApiClientProvider.AccessToken != null)
                //{
                //    client.DefaultRequestHeaders.Authorization
                //            = new AuthenticationHeaderValue("Bearer", ApiClientProvider.AccessToken);
                //}
                //else
                //{
                //    client.Dispose();
                //    throw new AuthenticationException();
                //}
            }
        }
    }
}