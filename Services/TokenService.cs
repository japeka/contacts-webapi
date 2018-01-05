using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ContactsWebApi.Models;
using Newtonsoft.Json;
using ContactsWebApi.Config;

namespace ContactsWebApi.Services
{
    public class TokenService : ITokenService
    {
        private readonly AzureSettings _settings;
        public TokenService()
        {
            _settings = new AzureSettings();
        }

        public async Task<AccessToken> GetToken(LoginCredentials loginCredentials)
        {
            if (_settings?.DirectoryId != null) { 
                var authenticationParams = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("client_id", _settings.ApplicationId),
                    new KeyValuePair<string, string>("resource", _settings.Resource),
                    new KeyValuePair<string, string>("username", loginCredentials.Username),
                    new KeyValuePair<string, string>("password", loginCredentials.Password),
                    new KeyValuePair<string, string>("grant_type", _settings.GrantType),
                    new KeyValuePair<string, string>("client_secret", _settings.Key)
                };
                return await this.RequestAccessToken(authenticationParams, _settings.EndPoint);
            } else {
                return null;
            }
        }

        public async Task<AccessToken> RequestAccessToken(IEnumerable<KeyValuePair<string, string>> requestParams,
            string endpoint)
        {
            AccessToken token;
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Cache-Control", "no-cache");
                HttpContent content = new FormUrlEncodedContent(requestParams);
                var response = await httpClient.PostAsync(endpoint, content);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    token = JsonConvert.DeserializeObject<AccessToken>(data);
                }
                else {  return null; }
            }
            return token;
        }
    }
}
