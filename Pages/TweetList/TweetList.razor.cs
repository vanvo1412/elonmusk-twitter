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
        [Inject] protected ITweetService ProjectService { get; set; }

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

        

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            var data = await ProjectService.GetTweetsListAsync(10);
            _data = data;
        }

        
    }
}