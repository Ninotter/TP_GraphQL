using Bogus;
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

            modelBuilder.Entity<Game>()
            .Property(e => e.Platforms)
            .HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries));

            modelBuilder.Entity<Game>()
            .Property(e => e.Genres)
            .HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries));


            modelBuilder.Entity<Studio>()
                .HasMany(studio => studio.Games)
                .WithMany(game => game.Studios)
                .UsingEntity("StudioGames");

            var idsGame = 1;
            var stock = new Faker<Game>()
                .RuleFor(m => m.Id, f => idsGame++)
                .RuleFor(m => m.Name, f => f.Commerce.ProductName())
                .RuleFor(m => m.Genres, f => f.Make(4, () => f.Lorem.Word()).ToArray())
                .RuleFor(m => m.Platforms, f => f.Make(4, () => f.Lorem.Word()).ToArray())
                .RuleFor(m => m.PublicationDate, f => f.Date.Future());

            modelBuilder
                .Entity<Game>()
                .HasData(stock.GenerateBetween(200, 200));
        }
    }
}
