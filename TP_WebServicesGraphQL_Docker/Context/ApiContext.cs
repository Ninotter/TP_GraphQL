using Microsoft.EntityFrameworkCore;
using System;
using TP_WebServicesGraphQL_Docker.Model;

namespace TP_WebServicesGraphQL_Docker.Context
{
    public class ApiContext : DbContext
    {
        public DbSet<Game> Games { get; set; }
        public DbSet<Studio> Studios { get; set; }

        public DbSet<Editor> Editors { get; set; }

        public ApiContext(DbContextOptions<ApiContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Game>()
               .HasMany(game => game.Editors)
               .WithMany(editor => editor.Games)
               .UsingEntity("GameEditors");

            modelBuilder.Entity<Game>().OwnsOne(game => game.Platforms);

            modelBuilder.Entity<Game>().OwnsOne(game => game.Genres);


            modelBuilder.Entity<Studio>()
                .HasMany(studio => studio.Games)
                .WithMany(game => game.Studios)
                .UsingEntity("StudioGames");
        }
    }
}
