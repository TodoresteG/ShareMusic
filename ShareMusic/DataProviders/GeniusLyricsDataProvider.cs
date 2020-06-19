using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Microsoft.Extensions.Configuration;
using ShareMusic.DataProviders.Interfaces;

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
            string httpResult = this.AskForLyrics(songTitle, artist);
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
                return result;
            }
            catch (Exception)
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
