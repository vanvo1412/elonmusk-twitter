using System;
using System.Collections.Concurrent;
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
        Task<List<TweetItem>> GetTweetsListAsync(int count = 10);
        Task<List<TweetItem>> CreateNewTweet(TweetItem tweet);
        Task<bool> UpdateReweetCount(Guid tweetId);
    }

    public class TweetService : ITweetService
    {
        private ConcurrentStack<Guid> stack = new ConcurrentStack<Guid>();
        private List<TweetItem> tweets = new List<TweetItem>();
        private readonly HttpClient _httpClient;

        public TweetService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<TweetItem>> GetTweetsListAsync(int count = 10)
        {
            var data = await _httpClient.GetFromJsonAsync<TweetItem[]>("data/sample_tweets.json");
            tweets.AddRange(data);
            return count > 0 ? tweets.OrderByDescending(x => x.RetweetCount).Take(count).ToList(): tweets.ToList();
        }

        public async Task<List<TweetItem>> CreateNewTweet(TweetItem tweet)
        {
            tweets.Add(tweet);
            return tweets.OrderByDescending(x => x.RetweetCount).ToList();
        }

        public async Task<bool> UpdateReweetCount(Guid tweetId)
        {
            stack.Push(tweetId);

            if (!stack.IsEmpty)
            {
                Guid tweetToUpdateId;
                stack.TryPop(out tweetToUpdateId);

                //Simulate updating reweet count
                tweets.FirstOrDefault(x => x.Id == tweetId).RetweetCount += 1;

                return true;
            }
            return false;
        }
    }
}