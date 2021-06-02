using CocktailsApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace CocktailsApi.Clients
{
    public class CocktailsClient
    {
        private HttpClient _client;
        private static string _adress;
        private static string _apikey;

        public CocktailsClient()
        {
            _adress = Constants.adress;
            _apikey = Constants.apikey;

            _client = new HttpClient();
            _client.BaseAddress = new Uri(_adress);
        }

        public async Task<Cocktails> GetCocktailByName(string name)
        {
            var responce = await _client.GetAsync($"/api/json/v1/{_apikey}/search.php?s={name}");

            var content = responce.Content.ReadAsStringAsync().Result;

            var result = JsonConvert.DeserializeObject<Cocktails>(content);

            return result;
        }
        public async Task<FilterCocktails> GetCocktailsByIngridient(string noun)
        {
            var responce = await _client.GetAsync($"/api/json/v1/1/filter.php?i={noun}");

            var content = responce.Content.ReadAsStringAsync().Result;

            var result = JsonConvert.DeserializeObject<FilterCocktails>(content);

            return result;

        }

        public async Task<Cocktails> GetRandomCocktail()
        {
            var responce = await _client.GetAsync($"/api/json/v1/1/random.php");

            var content = responce.Content.ReadAsStringAsync().Result;

            var result = JsonConvert.DeserializeObject<Cocktails>(content);

            return result;
        }

    }
}