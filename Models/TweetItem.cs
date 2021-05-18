using System;
using System.Text.Json.Serialization;
using TwitterCodingChallenge.Utils;

namespace TwitterCodingChallenge.Models
{
    public class TweetItem
    {
        public Guid Id { get; set; }
        public Guid ParentTweetId { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string ProfileImage { get; set; }
        public string Content { get; set; }
        public int RetweetCount { get; set; }

        [JsonConverter(typeof(LongToDateTimeConverter))]
        public DateTime CreatedAt { get; set; }
    }
}