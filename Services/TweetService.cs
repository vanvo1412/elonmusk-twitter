using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TwitterCodingChallenge.Models;

namespace TwitterCodingChallenge.Services
{
    public interface ITweetService
    {
        Task<List<TweetItem>> GetTweetsListAsync(int count = 0);
    }

    public class TweetService : ITweetService
    {
        private readonly HttpClient _httpClient;

        public TweetService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<TweetItem>> GetTweetsListAsync(int count = 0)
        {
            var data = await _httpClient.GetFromJsonAsync<TweetItem[]>("data/sample_tweets.json");
            var result = new List<TweetItem>();
            result.AddRange(data);
            return count > 0 ? result.OrderByDescending(x => x.RetweetCount).Take(count).ToList(): result;
        }
    }
}