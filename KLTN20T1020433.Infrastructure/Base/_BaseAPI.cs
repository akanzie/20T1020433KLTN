using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace KLTN20T1020433.DataLayers.API
{
    public abstract class _BaseApi
    {
        protected readonly HttpClient _httpClient;
        protected readonly string _baseUrl;

        public _BaseApi(string baseUrl)
        {
            _httpClient = new HttpClient();
            _baseUrl = baseUrl;
        }

        protected async Task<T> GetAsync<T>(string endpoint)
        {
            HttpResponseMessage response = await _httpClient.GetAsync(_baseUrl + endpoint);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to retrieve data from API. Status code: {response.StatusCode}");
            }

            using (var stream = await response.Content.ReadAsStreamAsync())
            {
                return await JsonSerializer.DeserializeAsync<T>(stream);
            }
        }        
    }
}
