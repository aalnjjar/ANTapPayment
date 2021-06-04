using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ANTapPayment.Helpers
{
    internal class HttpClientFactory
    {

        private readonly HttpClient _httpClient;
        private const string baseUri = "https://api.tap.company/v2/";
        public HttpClientFactory(string authToken)
        {
            _httpClient = new HttpClient { BaseAddress = new Uri(baseUri) };
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
        }


        public async Task<T> GetAsync<T>(string endpoint, string args = null)
        {
            var response = await _httpClient.GetAsync($"{endpoint}?{args}");
            if (!response.IsSuccessStatusCode)
                return default(T);
            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(result);
        }

        public async Task<T> PostFormAsync<T>(string endpoint, FormUrlEncodedContent formContent)
        {
            var response = await _httpClient.PostAsync($"{endpoint}", formContent);
            if (!response.IsSuccessStatusCode)
                return default(T);

            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(result);
        }


        public async Task<T> PostAsync<T>(string endpoint, dynamic jObject)
        {
            var stringContent = new StringContent(jObject.ToString());
            var response = await _httpClient.PostAsync($"{endpoint}", stringContent);
            if (!response.IsSuccessStatusCode)
                return default(T);

            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(result);
        }

    }
}
