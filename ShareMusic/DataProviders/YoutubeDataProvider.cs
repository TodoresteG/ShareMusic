﻿using ShareMusic.DataProviders.Interfaces;
using Google.Apis.YouTube.v3;
using Google.Apis.Services;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Google.Apis.YouTube.v3.Data;

namespace ShareMusic.DataProviders
{
    public class YoutubeDataProvider : IYoutubeDataProvider
    {
        private readonly YouTubeService youTubeService;
        private readonly IConfiguration configuration;

        public YoutubeDataProvider(IConfiguration configuration)
        {
            this.configuration = configuration;

            this.youTubeService = new YouTubeService(
                new BaseClientService.Initializer
                {
                    ApiKey = this.configuration.GetSection("YoutubeApiKey").Value,
                    GZipEnabled = true,
                    ApplicationName = "ShareMusic"
                });
        }

        public string SearchVideo(string artist, string song)
        {
            SearchResource.ListRequest searchRequest = this.youTubeService.Search.List("snippet");
            searchRequest.Q = $"{artist} {song}";

            SearchListResponse searchResponse = searchRequest.Execute();

            foreach (var item in searchResponse.Items)
            {
                switch (item.Id.Kind)
                {
                    case "youtube#video":
                        return item.Id.VideoId;
                }
            }

            return null;
        }
    }
}
