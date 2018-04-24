using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

namespace OrphanageV3.Services
{
    public static class ApiClientProvider
    {
        private static string _hostUri;
        public static string AccessToken { get; private set; }

        static ApiClientProvider()
        {
            RefreshHostUri();
        }

        public static void RefreshHostUri()
        {
            var url = Properties.Settings.Default.OrphanageServiceURL;
            if (url.EndsWith("/"))
                url = url.Substring(0, url.Length - 1);
            _hostUri = url;
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
                client.Timeout = new TimeSpan(0, 0, 10);
                var tokenEndpoint = new Uri(new Uri(_hostUri), "Token");
                response = await client.PostAsync(tokenEndpoint, content);
            }

            var responseContent = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new AuthenticationException(string.Format("Error: {0}", responseContent));
            }

            return GetTokenDictionary(responseContent);
        }

        private static Dictionary<string, string> GetTokenDictionary(
            string responseContent)
        {
            Dictionary<string, string> tokenDictionary =
                JsonConvert.DeserializeObject<Dictionary<string, string>>(
                responseContent);
            try
            {
                AccessToken = tokenDictionary["access_token"];
            }
            catch
            {
                AccessToken = null;
            }
            return tokenDictionary;
        }
    }

    public partial class ApiClient : IApiClient
    {
        async partial void PrepareRequest(HttpClient client, HttpRequestMessage request, string url)
        {
            if (ApiClientProvider.AccessToken != null)
            {
                client.DefaultRequestHeaders.Authorization
                      = new AuthenticationHeaderValue("Bearer", ApiClientProvider.AccessToken);
            }
            else
            {
                //TODO show login dialog

                var ret = await ApiClientProvider.GetTokenDictionary("مدير", "0000");
                if (ret.ContainsKey("access_token"))
                {
                    client.DefaultRequestHeaders.Authorization
                            = new AuthenticationHeaderValue("Bearer", ApiClientProvider.AccessToken);
                }
                else
                {
                    client.Dispose();
                    throw new AuthenticationException();
                }
            }
        }
    }
}