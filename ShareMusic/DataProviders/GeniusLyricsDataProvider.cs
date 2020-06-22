using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using ShareMusic.DataProviders.Interfaces;
using ShareMusic.Models.GeniusLyricsDataProvider.Search;
using ShareMusic.Models.GeniusLyricsDataProvider.Get;
using System.Linq;

namespace ShareMusic.DataProviders
{
    public class GeniusLyricsDataProvider : IGeniusLyricsDataProvider
    {
        private const string BaseUrl = "https://api.genius.com";

        private readonly IConfiguration configuration;
        private string bearerToken;
        private HttpClient http;
        private bool initialized;

        public GeniusLyricsDataProvider(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.bearerToken = this.configuration.GetSection("GeniusLyricsBearerToken").Value;
        }


        public string AskForLyrics(string songTitle, string artist)
        {
            this.Initialize();
            songTitle = songTitle.Trim();
            artist = artist.Trim();

            string apiSearchPath = this.SearchForLyrics(songTitle, artist);
            if (string.IsNullOrEmpty(apiSearchPath))
            {
                return string.Empty;
            }

            string lyricsContent = this.GetLyrics(apiSearchPath);
            return lyricsContent;
        }

        private void Initialize()
        {
            if (this.initialized)
            {
                return;
            }

            this.initialized = true;
            this.http = new HttpClient();

            this.http.DefaultRequestHeaders.Clear();
            this.http.DefaultRequestHeaders.Add("Accept", "application/json");
            this.http.DefaultRequestHeaders.Add("AcceptLanguage", "bg-BG");
            this.http.DefaultRequestHeaders.Add("Connection", "Keep-Alive");
            this.http.DefaultRequestHeaders.Add("CacheControl", "no-cache");
            this.http.DefaultRequestHeaders.Add("ProtocolVersion", "1.1");
            this.http.DefaultRequestHeaders.Add("Authorization", $"Bearer {this.bearerToken}");
        }

        private string SearchForLyrics(string songTitle, string artist)
        {
            try
            {
                string result = this.RequestGet(BaseUrl + $"/search?q={artist} {songTitle}");
                SearchLyricsModel searchLyricsResponse = JsonSerializer.Deserialize<SearchLyricsModel>(result);

                string apiPath = string.Empty;
                if (searchLyricsResponse.meta.status == 200)
                {
                    apiPath = searchLyricsResponse.response.hits
                        .Where(h => h.type == "song")
                        .FirstOrDefault(h => h.result.lyrics_state == "complete" && h.result.title == songTitle).result.api_path;
                }

                return apiPath;
            }
            catch (Exception e)
            {
                return string.Empty;
            }
        }

        private string GetLyrics(string apiSearchPath) 
        {
            try
            {
                string result = this.RequestGet(BaseUrl + apiSearchPath + "?text_format=html");
                GetLyricsModel getLyricsResponse = JsonSerializer.Deserialize<GetLyricsModel>(result);

                string embedContent = string.Empty;
                if (getLyricsResponse.meta.status == 200 && getLyricsResponse.response.song.lyrics_state == "complete")
                {
                    embedContent = getLyricsResponse.response.song.embed_content;
                }

                return embedContent;
            }
            catch (Exception e)
            {
                return string.Empty;
            }
        }

        private string RequestGet(string url) 
        {
            HttpResponseMessage response = this.http.GetAsync(url).GetAwaiter().GetResult();
            byte[] byteArray = response.Content.ReadAsByteArrayAsync().GetAwaiter().GetResult();
            string responseString = Encoding.UTF8.GetString(byteArray, 0, byteArray.Length);
            return responseString;
        }
    }
}
