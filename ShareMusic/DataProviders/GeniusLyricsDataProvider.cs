using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using ShareMusic.DataProviders.Interfaces;
using ShareMusic.Models.GeniusLyricsDataProvider;
using System.Linq;

namespace ShareMusic.DataProviders
{
    public class GeniusLyricsDataProvider : IGeniusLyricsDataProvider
    {
        private const string BaseUrl = "https://api.genius.com/";

        private readonly IConfiguration configuration;
        private string bearerToken;
        private HttpClient http;
        private bool initialized;

        public GeniusLyricsDataProvider(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.bearerToken = this.configuration.GetSection("GeniusLyricsBearerToken").Value;
        }


        public void GetLyrics(string songTitle, string artist)
        {
            this.Initialize();
            songTitle = songTitle.Trim();
            artist = artist.Trim();

            string apiSearchPath = this.AskForLyrics(songTitle, artist);
            if (string.IsNullOrEmpty(apiSearchPath))
            {
                return;
            }


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

        private string AskForLyrics(string songTitle, string artist)
        {
            try
            {
                string result = this.RequestGet(BaseUrl + $"search?q={artist} {songTitle}");
                RootObject geniusLyricsResponse = JsonSerializer.Deserialize<RootObject>(result);

                string apiPath = string.Empty;
                if (geniusLyricsResponse.meta.status == 200)
                {
                    apiPath = geniusLyricsResponse.response.hits
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

        private string RequestGet(string url) 
        {
            HttpResponseMessage response = this.http.GetAsync(url).GetAwaiter().GetResult();
            byte[] byteArray = response.Content.ReadAsByteArrayAsync().GetAwaiter().GetResult();
            string responseString = Encoding.UTF8.GetString(byteArray, 0, byteArray.Length);
            return responseString;
        }
    }
}
