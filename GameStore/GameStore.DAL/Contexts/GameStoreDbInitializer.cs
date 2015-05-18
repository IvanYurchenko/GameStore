//#define DROPCREATEALWAYS
using System;
using System.Collections.Generic;
using System.Data.Entity;
using GameStore.DAL.Entities;

namespace GameStore.DAL.Contexts
{

#if DROPCREATEALWAYS
    public class GameStoreDbInitializer : DropCreateDatabaseAlways<GameStoreDbContext>
#else
    public class GameStoreDbInitializer : DropCreateDatabaseIfModelChanges<GameStoreDbContext>
#endif
    {
        protected override void Seed(GameStoreDbContext context)
        {
            InitGenres(context);
            InitPlatformTypes(context);
            InitPublishers(context);
            InitComments(context);
            InitGames(context);
            InitOrders(context);
            
            base.Seed(context);
        }

        private void InitGenres(GameStoreDbContext context)
        {
            context.Genres.Add(new Genre {GenreId = 1, Name = "Other", ParentGenreId = null});
            context.Genres.Add(new Genre {GenreId = 2, Name = "Action", ParentGenreId = null});
            context.Genres.Add(new Genre {GenreId = 3, Name = "Shooter", ParentGenreId = null});
            context.Genres.Add(new Genre {GenreId = 4, Name = "Action-Adventure", ParentGenreId = null});
            context.Genres.Add(new Genre {GenreId = 5, Name = "Adventure", ParentGenreId = null});
            context.Genres.Add(new Genre {GenreId = 6, Name = "Role-playing", ParentGenreId = null});
            context.Genres.Add(new Genre {GenreId = 7, Name = "Simulation", ParentGenreId = null});
            context.Genres.Add(new Genre {GenreId = 8, Name = "Strategy", ParentGenreId = null});
            context.Genres.Add(new Genre {GenreId = 9, Name = "Sports", ParentGenreId = null});

            #region Role-playing subgenres

            context.Genres.Add(new Genre {GenreId = 10, Name = "Action RPG", ParentGenreId = 6});
            context.Genres.Add(new Genre {GenreId = 11, Name = "MMO RPG", ParentGenreId = 6});
            context.Genres.Add(new Genre {GenreId = 12, Name = "Tactical RPG", ParentGenreId = 6});

            #endregion

            #region Sports subgenres

            context.Genres.Add(new Genre {GenreId = 13, Name = "Racing", ParentGenreId = 9});
            context.Genres.Add(new Genre {GenreId = 14, Name = "Competitive", ParentGenreId = 9});

            #endregion

            context.SaveChanges();
        }

        private void InitPlatformTypes(GameStoreDbContext context)
        {
            context.PlatformTypes.Add(new PlatformType {PlatformTypeId = 1, Type = "Mobile"});
            context.PlatformTypes.Add(new PlatformType {PlatformTypeId = 2, Type = "Browser"});
            context.PlatformTypes.Add(new PlatformType {PlatformTypeId = 3, Type = "Desktop"});
            context.PlatformTypes.Add(new PlatformType {PlatformTypeId = 4, Type = "Console"});

            context.SaveChanges();
        }

        private void InitPublishers(GameStoreDbContext context)
        {
            context.Publishers.Add(new Publisher
            {
                PublisherId = 1,
                CompanyName = "Default",
                HomePage = "http://mysite.com",
                Description = "It is a default publisher. ",
            });

            context.Publishers.Add(new Publisher
            {
                PublisherId = 2,
                CompanyName = "Nintendo",
                HomePage = "http://nintendo.com",
                Description = "Nintendo description here. ",
            });

            context.Publishers.Add(new Publisher
            {
                PublisherId = 3,
                CompanyName = "Ubisoft",
                HomePage = "http://www.ubisoft.com",
                Description = "Ubisoft description here. "
            });

            context.Publishers.Add(new Publisher
            {
                PublisherId = 4,
                CompanyName = "EA",
                HomePage = "http://www.ea.com",
                Description = "EA description here."
            });

            context.Publishers.Add(new Publisher
            {
                PublisherId = 5,
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
                Description =
                    "Need for Speed is a series of racing video games published by Electronic Arts (EA) and developed by several studios including the Canadian company EA Black Box and the British company Criterion Games. ",
                PublisherId = 5,
                Price = (decimal)49.99,
                UnitsInStock = 200,
                Discontinued = false,
                Genres = new List<Genre>
                {
                    context.Genres.Find(9)
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
                Publisher = context.Publishers.Find(5),
                PublicationDate = DateTime.Now.AddDays(-50),
                AddedDate = DateTime.Now,
            });

            context.Games.Add(new Game
            {
                Key = "cs",
                Name = "Counter Strike",
                Description =
                    "Counter-Strike is a first-person shooter video game developed by Valve Corporation. It was initially developed and released as a Half-Life modification by Minh \"Gooseman\" Le and Jess \"Cliffe\" Cliffe in 1999, before Le and Cliffe were hired and the game's intellectual property acquired. Counter-Strike was first released by Valve on the Microsoft Windows platform in 2000. The game later spawned a franchise, and is the first installment in the Counter-Strike series. Several remakes and Ports of Counter-Strike have been released on the Xbox console, as well as OS X and Linux.",
                PublisherId = 2,
                Price = (decimal)19.99,
                UnitsInStock = 1000,
                Discontinued = false,
                Genres = new List<Genre>
                {
                    context.Genres.Find(3)
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
                Publisher = context.Publishers.Find(2),
                PublicationDate = DateTime.Now.AddDays(-5),
                AddedDate = DateTime.Now
            });

            context.Games.Add(new Game
            {
                Key = "minesweeper",
                Name = "Minesweeper",
                Description =
                    "Minesweeper is a single-player puzzle video game. The objective of the game is to clear a rectangular board containing hidden \"mines\" without detonating any of them, with help from clues about the number of neighboring mines in each field. The game originates from the 1960s, and has been written for many computing platforms in use today. It has many variations and offshoots.",
                PublisherId = 3,
                Price = (decimal)0.99,
                UnitsInStock = 2000,
                Discontinued = false,
                Genres = new List<Genre>
                {
                    context.Genres.Find(2)
                },
                PlatformTypes = new List<PlatformType>
                {
                    context.PlatformTypes.Find(3)
                },
                Comments = new List<Comment>
                {
                    context.Comments.Find(4),
                },
                Publisher = context.Publishers.Find(3),
                PublicationDate = DateTime.Now.AddDays(-20),
                AddedDate = DateTime.Now
            });

            context.SaveChanges();
        }

        private void InitOrders(GameStoreDbContext context)
        {
            context.Orders.Add(new Order
            {
                IsPayed = true,
                OrderDate = DateTime.UtcNow,
                SessionKey = "someKey123",
                OrderItems = new List<OrderItem>(),
            });

            context.SaveChanges();
        }
    }
}