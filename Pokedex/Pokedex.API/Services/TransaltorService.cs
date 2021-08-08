using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Pokedex.API.Enums;
using Pokedex.API.Interfaces;
using Pokedex.API.Models;

namespace Pokedex.API.Services
{
    public record TranslatorServiceResult(bool Success, string Text);

    public class TransaltorService : ITranslator
    {
        private readonly ILogger<TransaltorService> _logger;
        private readonly HttpClient _client;

        public TransaltorService(ILogger<TransaltorService> logger, HttpClient client)
        {
            _logger = logger;
            _client = client;
        }

        public async Task<string> TranslateDescription(Pokemon pokemon)
        {
            if (pokemon.IsLegendary || pokemon.Habitat.ToLower() == "rare")
            {
                return await GetTranslation(pokemon.Description, TranslationType.Yoda);
            }
            return await GetTranslation(pokemon.Description, TranslationType.Shakespeare);
        }

        private async Task<HttpResponseMessage> GetApiResponse(string url, string text)
        {
            dynamic jsonPayload = new { text };

            var json = JsonSerializer.Serialize(jsonPayload);

            return await _client.PostAsync(url, new StringContent(json, Encoding.UTF8, "application/json"));
        }

        private async Task<string> GetTranslation(string text, TranslationType translationType)
        {
            var response = translationType switch
            {
                TranslationType.Shakespeare => await GetApiResponse("translate/shakespeare.json", text),
                TranslationType.Yoda => await GetApiResponse("translate/yoda.json", text)
            };

            _logger.LogInformation($"Status code of {response.StatusCode} for API call to translate {nameof(translationType)}.");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var translationResponse = JsonSerializer.Deserialize<TranslationResponse>(json);
                return translationResponse.Contents.Translated;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
            {
                return "Too many requests to API endpoint. Please try again later";
            }
            else
            {
                return "Translation failed";
            }
        }
    }
}
