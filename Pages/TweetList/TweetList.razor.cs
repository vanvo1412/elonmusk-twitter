using System.Collections.Generic;
using System.Threading.Tasks;
using TwitterCodingChallenge.Models;
using TwitterCodingChallenge.Services;
using Microsoft.AspNetCore.Components;
using AntDesign;
using Microsoft.JSInterop;
using System;
using System.Linq;

namespace TwitterCodingChallenge.Pages.List
{
    public partial class TweetList
    {
        [Inject]
        public IJSRuntime JS { get; set; }
        [Inject] protected ITweetService TweetService { get; set; }

        private readonly ListGridType _listGridType = new ListGridType
        {
            Gutter = 16,
            Xs = 1,
            Sm = 2,
            Md = 3,
            Lg = 3,
            Xl = 4,
            Xxl = 4
        };

        private List<TweetItem> _data = new List<TweetItem>();
        bool submitting = false;
        string _value = "";

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            _data = await TweetService.GetTweetsListAsync(10);
        }

        async void OnSubmit()
        {
            submitting = true;

            await Task.Delay(1000);

            var newTweet = new TweetItem()
            {
                Id = Guid.NewGuid(),
                Name = "Elon Musk",
                ProfileImage = "https://pbs.twimg.com/profile_images/1383184766959120385/MM9DHPWC_400x400.jpg",
                Username = "elonmusk",
                Content = _value,
                CreatedAt = DateTime.Now,
            };

            _data.Add(newTweet);
            await TweetService.CreateNewTweet(newTweet);

            submitting = false;
            _value = "";
            await InvokeAsync(StateHasChanged);
        }

        async void OnRetweetClick(TweetItem tweet)
        {
            submitting = true;
            var tweetToLinkId = tweet.ParentTweetId != Guid.Empty ? tweet.ParentTweetId : tweet.Id;
            var newTweet = new TweetItem()
            {
                Id = Guid.NewGuid(),
                Name = "Elon Musk",
                ProfileImage = "https://pbs.twimg.com/profile_images/1383184766959120385/MM9DHPWC_400x400.jpg",
                Username = "elonmusk",
                Content = _value,
                CreatedAt = DateTime.Now,
                ParentTweetId = tweetToLinkId
            };

            //Persist reweet count into database using message queue
            await TweetService.CreateNewTweet(newTweet);
            await TweetService.UpdateReweetCount(tweetToLinkId);

            // Relect UI changes
            _data.Add(newTweet);
            _data = _data.OrderByDescending(x => x.RetweetCount).ToList();
            await InvokeAsync(StateHasChanged);
            submitting = false;

        }
        async void OnCommentClick(TweetItem tweet)
        {
            await InvokeAsync(StateHasChanged);
        }
    }
}