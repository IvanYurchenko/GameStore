#define PAGINATIONSEEDING

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using GameStore.DAL.Entities;

namespace GameStore.DAL.Contexts
{
    public class GameStoreDbInitializer : DropCreateDatabaseAlways<GameStoreDbContext>
    {
        protected override void Seed(GameStoreDbContext context)
        {
            InitGenres(context);
            InitPlatformTypes(context);
            InitPublishers(context);
            InitComments(context);
            InitGames(context);

            // Seeding for testing pagination
#if PAGINATIONSEEDING
            const int gamesNumber = 65;

            var random = new Random();
            for (int i = 0; i < gamesNumber; i++)
            {
                var game = new Game
                {
                    Key = "key" + i,
                    Name = "Game " + i,
                    Description = "Some description here. Some description here. Some description here. Some description here. Some description here. Some description here. Some description here. Some description here. ",
                    PublisherId = random.Next(1, context.Publishers.Count()),
                    Price = (decimal)(random.Next(1000)/100.0),
                    UnitsInStock = (short)random.Next(200),
                    Discontinued = random.Next(10) % 3 == 0,
                    Genres = new List<Genre>(),
                    PlatformTypes = new List<PlatformType>(),
                    PublicationDate = DateTime.Now.AddDays(-random.Next(500)),
                    AddedDate = DateTime.Now
                };

                //populate genres
                var gMax = random.Next(context.Genres.Count());
                var genres = context.Genres.Select(item => item.GenreId).ToList();
                for (int g = 0; g < gMax; g++)
                {
                    int index = random.Next(0, genres.Count());
                    game.Genres.Add(context.Genres.Find(genres[index]));
                    genres.RemoveAt(index);
                }

                //populate platforms
                var pMax = random.Next(context.PlatformTypes.Count());
                var platforms = context.PlatformTypes.Select(item => item.PlatformTypeId).ToList();
                for (int p = 0; p < pMax; p++)
                {
                    int index = random.Next(0, platforms.Count());
                    game.PlatformTypes.Add(context.PlatformTypes.Find(platforms[index]));
                    platforms.RemoveAt(index);
                }

                context.Games.Add(game);
            }

            context.SaveChanges();
#endif

            base.Seed(context);
        }

        private void InitGenres(GameStoreDbContext context)
        {
            context.Genres.Add(new Genre {Name = "Action", ParentGenreId = null});
            context.Genres.Add(new Genre {Name = "Shooter", ParentGenreId = null});
            context.Genres.Add(new Genre {Name = "Action-Adventure", ParentGenreId = null});
            context.Genres.Add(new Genre {Name = "Adventure", ParentGenreId = null});
            context.Genres.Add(new Genre {Name = "Role-playing", ParentGenreId = null});
            context.Genres.Add(new Genre {Name = "Simulation", ParentGenreId = null});
            context.Genres.Add(new Genre {Name = "Strategy", ParentGenreId = null});
            context.Genres.Add(new Genre {Name = "Sports", ParentGenreId = null});
            context.Genres.Add(new Genre {Name = "Other", ParentGenreId = null});

            #region Role-playing subgenres

            context.Genres.Add(new Genre {Name = "Action RPG", ParentGenreId = 5});
            context.Genres.Add(new Genre {Name = "MMO RPG", ParentGenreId = 5});
            context.Genres.Add(new Genre {Name = "Tactical RPG", ParentGenreId = 5});

            #endregion

            #region Sports subgenres

            context.Genres.Add(new Genre {Name = "Racing", ParentGenreId = 8});
            context.Genres.Add(new Genre {Name = "Competitive", ParentGenreId = 8});

            #endregion

            context.SaveChanges();
        }

        private void InitPlatformTypes(GameStoreDbContext context)
        {
            context.PlatformTypes.Add(new PlatformType {Type = "Mobile"});
            context.PlatformTypes.Add(new PlatformType {Type = "Browser"});
            context.PlatformTypes.Add(new PlatformType {Type = "Desktop"});
            context.PlatformTypes.Add(new PlatformType {Type = "Console"});
            context.SaveChanges();
        }

        private void InitPublishers(GameStoreDbContext context)
        {
            context.Publishers.Add(new Publisher
            {
                CompanyName = "Nintendo",
                HomePage = "http://nintendo.com",
                Description = "Nintendo description here. ",
            });

            context.Publishers.Add(new Publisher
            {
                CompanyName = "Ubisoft",
                HomePage = "http://www.ubisoft.com",
                Description = "Ubisoft description here. "
            });

            context.Publishers.Add(new Publisher
            {
                CompanyName = "EA",
                HomePage = "http://www.ea.com",
                Description = "EA description here."
            });

            context.Publishers.Add(new Publisher
            {
                CompanyName = "Sony",
                HomePage = "http://www.sony.com",
                Description = "Sony description here."
            });

            context.SaveChanges();
        }

        private void InitComments(GameStoreDbContext context)
        {
            context.Comments.Add(new Comment
            {
                CommentId = 1,
                Name = "John",
                Body = "It is really interesting game, I like it.",
                GameId = 1,
            });

            context.Comments.Add(new Comment
            {
                CommentId = 2,
                Name = "Bill",
                Body = "John, I like it too.",
                GameId = 1,
                ParentCommentId = 1
            });

            context.Comments.Add(new Comment
            {
                CommentId = 3,
                Name = "Ken",
                Body = "Cool game!",
                GameId = 2,
            });

            context.Comments.Add(new Comment
            {
                CommentId = 4,
                Name = "Amy",
                Body = "The best game ever.",
                GameId = 3,
            });
        }

        private void InitGames(GameStoreDbContext context)
        {
            context.Games.Add(new Game
            {
                Key = "nfs",
                Name = "Need for Speed",
                Description = "Need for Speed is a series of racing video games published by Electronic Arts (EA) and developed by several studios including the Canadian company EA Black Box and the British company Criterion Games. ",
                PublisherId = 4,
                Price = (decimal)49.99,
                UnitsInStock = 200,
                Discontinued = false,
                Genres = new List<Genre>
                {
                    context.Genres.Find(8)
                },
                PlatformTypes = new List<PlatformType>
                {
                    context.PlatformTypes.Find(1),
                    context.PlatformTypes.Find(3)
                },
                Comments = new List<Comment>
                {
                    context.Comments.Find(1),
                    context.Comments.Find(2)
                },
                Publisher = context.Publishers.Find(1),
                PublicationDate = DateTime.Now.AddDays(-50),
                AddedDate = DateTime.Now,
            });

            context.Games.Add(new Game
            {
                Key = "cs",
                Name = "Counter Strike",
                Description = "Counter-Strike is a first-person shooter video game developed by Valve Corporation. It was initially developed and released as a Half-Life modification by Minh \"Gooseman\" Le and Jess \"Cliffe\" Cliffe in 1999, before Le and Cliffe were hired and the game's intellectual property acquired. Counter-Strike was first released by Valve on the Microsoft Windows platform in 2000. The game later spawned a franchise, and is the first installment in the Counter-Strike series. Several remakes and Ports of Counter-Strike have been released on the Xbox console, as well as OS X and Linux.",
                PublisherId = 1,
                Price = (decimal)19.99,
                UnitsInStock = 1000,
                Discontinued = false,
                Genres = new List<Genre>
                {
                    context.Genres.Find(2)
                },
                PlatformTypes = new List<PlatformType>
                {
                    context.PlatformTypes.Find(2),
                    context.PlatformTypes.Find(3)
                },
                Comments = new List<Comment>
                {
                    context.Comments.Find(3),
                },
                Publisher = context.Publishers.Find(3),
                PublicationDate = DateTime.Now.AddDays(-5),
                AddedDate = DateTime.Now
            });

            context.Games.Add(new Game
            {
                Key = "minesweeper",
                Name = "Minesweeper",
                Description = "Minesweeper is a single-player puzzle video game. The objective of the game is to clear a rectangular board containing hidden \"mines\" without detonating any of them, with help from clues about the number of neighboring mines in each field. The game originates from the 1960s, and has been written for many computing platforms in use today. It has many variations and offshoots.",
                PublisherId = 2,
                Price = (decimal)0.99,
                UnitsInStock = 2000,
                Discontinued = false,
                Genres = new List<Genre>
                {
                    context.Genres.Find(1)
                },
                PlatformTypes = new List<PlatformType>
                {
                    context.PlatformTypes.Find(3)
                },
                Comments = new List<Comment>
                {
                    context.Comments.Find(4),
                },
                Publisher = context.Publishers.Find(2),
                PublicationDate = DateTime.Now.AddDays(-20),
                AddedDate = DateTime.Now
            });

            context.SaveChanges();
        }
    }
}