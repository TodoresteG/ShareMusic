using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShareMusic.Data.Entities;

namespace ShareMusic.Data
{
    public class ShareMusicDbContext : IdentityDbContext
    {
        public ShareMusicDbContext(DbContextOptions<ShareMusicDbContext> options)
            : base(options)
        {
        }

        public DbSet<Artist> Artists { get; set; }

        public DbSet<Group> Groups { get; set; }

        public DbSet<GroupRequest> GroupRequests { get; set; }

        public DbSet<GroupSong> GroupSongs { get; set; }

        public DbSet<GroupUser> GroupUsers { get; set; }

        public DbSet<Playlist> Playlists { get; set; }

        public DbSet<PlaylistSong> PlaylistSongs { get; set; }

        public DbSet<Request> Requests { get; set; }

        public DbSet<Song> Songs { get; set; }

        public DbSet<SongArtist> SongArtists { get; set; }

        public DbSet<SongMetadata> SongMetadata { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
