using System;
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

        public int CreateSong(AddSongInputModel inputModel)
        {
            Song song = new Song
            {
                Name = inputModel.Song,
                CreatedOn = DateTime.UtcNow,
            };

            Artist artist = new Artist
            {
                Name = inputModel.Artist,
                CreatedOn = DateTime.UtcNow,
            };

            SongArtist songArtist = new SongArtist
            {
                ArtistId = artist.Id,
                SongId = song.Id,
                CreatedOn = DateTime.UtcNow,
            };

            this.context.Songs.Add(song);
            this.context.Artists.Add(artist);
            this.context.SongArtists.Add(songArtist);
            this.context.SaveChanges();

            return song.Id;
        }
    }
}
