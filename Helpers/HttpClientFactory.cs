using ANTapPayment.Models;
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
        private const string baseUri = "https://api.tap.company/";
        public HttpClientFactory(string authToken, string ApiVersion = "v2")
        {
            _httpClient = new HttpClient { BaseAddress = new Uri($"{baseUri}/{ApiVersion}") };
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
        }


        public async Task<GenericResponse<T, U>> GetAsync<T, U>(string endpoint, string args = null, string Lang = "EN")
        {
            _httpClient.DefaultRequestHeaders.TryAddWithoutValidation("lang_code", Lang);

            var response = await _httpClient.GetAsync($"{endpoint}?{args}");
            var result = new GenericResponse<T, U>();
            result.IsSuccess = response.IsSuccessStatusCode;
            var JsonResponse = await response.Content.ReadAsStringAsync();
            if (result.IsSuccess)
                result.SucsessResponse = JsonConvert.DeserializeObject<T>(JsonResponse);
            else
                result.FailureResponse = JsonConvert.DeserializeObject<U>(JsonResponse);
            return result;
        }

        public async Task<GenericResponse<T, U>> PostFormAsync<T, U>(string endpoint, FormUrlEncodedContent formContent, string Lang = "EN")
        {
            _httpClient.DefaultRequestHeaders.TryAddWithoutValidation("lang_code", Lang);
            var response = await _httpClient.PostAsync($"{endpoint}", formContent);
            var result = new GenericResponse<T, U>();
            result.IsSuccess = response.IsSuccessStatusCode;
            var JsonResponse = await response.Content.ReadAsStringAsync();
            if (result.IsSuccess)
                result.SucsessResponse = JsonConvert.DeserializeObject<T>(JsonResponse);
            else
                result.FailureResponse = JsonConvert.DeserializeObject<U>(JsonResponse);
            return result;
        }


        public async Task<GenericResponse<T, U>> PostAsync<T, U>(string endpoint, dynamic jObject, string Lang = "EN")
        {
            _httpClient.DefaultRequestHeaders.TryAddWithoutValidation("lang_code", Lang);
            string json = JsonConvert.SerializeObject(jObject);
            StringContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{endpoint}", httpContent);
            var result = new GenericResponse<T, U>();
            result.IsSuccess = response.IsSuccessStatusCode;
            var JsonResponse = await response.Content.ReadAsStringAsync();
            if (result.IsSuccess)
                result.SucsessResponse = JsonConvert.DeserializeObject<T>(JsonResponse);
            else
                result.FailureResponse = JsonConvert.DeserializeObject<U>(JsonResponse);

            return result;
        }

    }
}
