using System;
using System.Collections.Generic;
using System.Linq;
using ShareMusic.Data;
using ShareMusic.Data.Entities;
using ShareMusic.Models.Songs;
using ShareMusic.Services.Interfaces;

namespace ShareMusic.Services
{
    public class SongsService : ISongsService
    {
        private readonly ShareMusicDbContext context;

        public SongsService(ShareMusicDbContext context)
        {
            this.context = context;
        }

        public int CreateSong(string songTitle, IList<string> artists)
        {
            songTitle = songTitle.Trim();
            List<string> songArtists = artists.Select(x => x.Trim()).Distinct().ToList();

            var similarSongs = this.context.Songs
                .Where(x => x.Name == songTitle)
                .Select(x => new
                {
                    x.Id,
                    Artists = x.Artists.Select(a => a.Artist.Name)
                }).ToList();

            foreach (var song in similarSongs)
            {
                if (!song.Artists.Except(songArtists).Any() && !songArtists.Except(song.Artists).Any())
                {
                    return song.Id;
                }
            }

            Song dbSong = new Song { Name = songTitle, CreatedOn = DateTime.UtcNow };
            List<SongArtist> dbSongArtists = new List<SongArtist>();

            for (int i = 0; i < songArtists.Count; i++)
            {
                Artist dbArtist = this.context.Artists.FirstOrDefault(a => a.Name == songArtists[i]) ?? new Artist { Name = songArtists[i], CreatedOn = DateTime.UtcNow };
                SongArtist dbSongArtist = new SongArtist { Artist = dbArtist, Song = dbSong };
                dbSongArtists.Add(dbSongArtist);
            }

            dbSong.Artists = dbSongArtists;

            this.context.Songs.Add(dbSong);
            this.context.SaveChanges();

            return dbSong.Id;
        }

        public void UpdateSongsSystemData(int songId)
        {
            Song song = this.context.Songs.Find(songId);
            List<string> artists = this.context.SongArtists.Where(sa => sa.SongId == songId).Select(sa => sa.Artist.Name).ToList();

            if (song == null)
            {
                return;
            }

            List<string> searchTerms = new List<string>();
            foreach (var artist in artists)
            {
                searchTerms.Add(artist);
            }

            searchTerms.Add(song.Name);

            song.SearchTerms = string.Join(" ", searchTerms.Distinct());
            this.context.SaveChanges();
        }
    }
}
