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
                .UsingEntity("GameStudios");

            //Bogus fake data


            var gameStock = new GameFaker();
            var generatedGames = gameStock.GenerateBetween(200, 200);
            var studioStock = new StudioFaker();
            var generatedStudios = studioStock.GenerateBetween(200, 200);
            var editorStock = new EditorFaker();
            var generatedEditors = editorStock.GenerateBetween(200, 200);

            //throws seeding error
            //Random r = new Random();
            //foreach (Game g in generatedGames)
            //{
            //    var thisGameEditors = generatedEditors.OrderBy(x => r.Next()).Take(r.Next(0,2));
            //    var thisGameStudios = generatedStudios.OrderBy(x => r.Next()).Take(r.Next(0,3));

            //    g.Editors = thisGameEditors.ToList();
            //    g.Studios = thisGameStudios.ToList();
            //    foreach (Editor editor in g.Editors)
            //    {
            //        if (editor.Games is null) editor.Games = new List<Game>();
            //        editor.Games.Add(g);
            //    }
            //    foreach (Studio studio in g.Studios)
            //    {
            //        if (studio.Games is null) studio.Games = new List<Game>();
            //        studio.Games.Add(g);
            //    }
            //}
            modelBuilder
                .Entity<Game>()
                .HasData(generatedGames);

            modelBuilder
                .Entity<Studio>()
                .HasData(generatedStudios);

            modelBuilder
               .Entity<Editor>()
               .HasData(generatedEditors);
        }

        private class GameFaker : Faker<Game>
        {
            StudioFaker _studioFaker = new StudioFaker();
            EditorFaker _editorsFaker = new EditorFaker();

            public GameFaker()
            {
                var idsGame = 0;

                this
                    .RuleFor(m => m.Id, f => ++idsGame)
                    .RuleFor(m => m.Name, f => f.Commerce.ProductName())
                    .RuleFor(m => m.Genres, f => f.Make(4, () => f.Lorem.Word()).ToArray())
                    .RuleFor(m => m.Platforms, f => f.Make(4, () => f.Lorem.Word()).ToArray())
                    .RuleFor(m => m.PublicationDate, f => f.Date.Future());
                    //Throws seeding error
                    ////.RuleFor(m => m.Studios, f => _studioFaker.GenerateBetween(0, 4))
                    ////.RuleFor(m => m.Editors, f => _editorsFaker.GenerateBetween(0, 2));
            }
        }

        private class StudioFaker : Faker<Studio>
        {
            public StudioFaker()
            {
                var idsStudio = 0;
                this
                    .RuleFor(m => m.Id, f => ++idsStudio)
                    .RuleFor(m => m.Name, f => f.Company.CompanyName());
            }
        }

        private class EditorFaker : Faker<Editor>
        {
            public EditorFaker()
            {
                var idsEditor = 0;
                this
                    .RuleFor(m => m.Id, f => ++idsEditor)
                    .RuleFor(m => m.Name, f => f.Company.CompanyName());
            }
        }
    }
}
