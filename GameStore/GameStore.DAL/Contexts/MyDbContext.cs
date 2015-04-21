using System;
using System.Collections.Generic;
using System.Data.Entity;
using GameStore.DAL.Entities;

namespace GameStore.DAL.Contexts
{
    public class MyDbContext : DbContext
    {
        public MyDbContext()
            : base("name=DbConnectionString")
        {
        }

        public DbSet<Game> Games { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<PlatformType> PlatformTypes { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<Publisher> Publishers { get; set; }

        public void Seed(MyDbContext myContext)
        {
            myContext.Database.ExecuteSqlCommand(@"CREATE UNIQUE INDEX LX_Game_Key ON [Games] ([Key])");
            myContext.Database.ExecuteSqlCommand(@"CREATE UNIQUE INDEX LX_Genre_Name ON [Genres] ([Name])");
            myContext.Database.ExecuteSqlCommand(@"CREATE UNIQUE INDEX LX_PlatformType_Type ON [PlatformTypes] ([Type])");
            myContext.Database.ExecuteSqlCommand(@"CREATE UNIQUE INDEX LX_Publisher_CompanyName ON [Publishers] ([CompanyName])");

            #region Genre Seeding

            var genreList = new List<Genre>
            {
                new Genre {GenreId = 1, Name = "Shooter"},
                new Genre {GenreId = 2, Name = "Strategy"},
                new Genre {GenreId = 3, Name = "RTS", ParentGenreId = 2},
                new Genre {GenreId = 4, Name = "Action"},
            };

            foreach (var genre in genreList)
            {
                myContext.Genres.Add(genre);
            }
            myContext.SaveChanges();

            #endregion

            #region PlatformType Seeding

            var platformTypeList = new List<PlatformType>
            {
                new PlatformType {Type = "Mobile"},
                new PlatformType {Type = "Desktop"},
                new PlatformType {Type = "Browser"},
            };

            foreach (var platformType in platformTypeList)
            {
                myContext.PlatformTypes.Add(platformType);
            }
            myContext.SaveChanges();

            #endregion

            #region Publisher Seeding
            var publisherList = new List<Publisher>
            {
                new Publisher
                {
                    CompanyName = "Company A",
                    Description = "Description A",
                    HomePage = "http://www.homepageA.com/"
                },
                new Publisher
                {
                    CompanyName = "Company B",
                    Description = "Description B",
                    HomePage = "http://www.homepageB.com/"
                },
                new Publisher
                {
                    CompanyName = "Company C",
                    Description = "Description C",
                    HomePage = "http://www.homepageC.com/"
                },
                new Publisher
                {
                    CompanyName = "Company D",
                    Description = "Description D",
                    HomePage = "http://www.homepageD.com/"
                },
            };

            foreach (var publisher in publisherList)
            {
                myContext.Publishers.Add(publisher);
            }
            myContext.SaveChanges();
            #endregion

            #region Game Seeding

            var gameList = new List<Game>();
            gameList.Add(
                new Game
                {
                    GameId = 1,
                    Key = "cs",
                    Name = "Counter Strike",
                    Description = "The most popular shooter. ",
                    Genres = new List<Genre>
                    {
                        myContext.Genres.Find(1)
                    },
                    PlatformTypes = new List<PlatformType>
                    {
                        myContext.PlatformTypes.Find(2)
                    },
                    Publisher = myContext.Publishers.Find(1),
                    AddedDate = DateTime.Now,
                });
            gameList.Add(
                new Game
                {
                    GameId = 2,
                    Key = "dota",
                    Name = "Dota",
                    Description = "Dota description here. ",
                    Genres = new List<Genre>
                    {
                        myContext.Genres.Find(3)
                    },
                    PlatformTypes = new List<PlatformType>
                    {
                        myContext.PlatformTypes.Find(2)
                    },
                    Publisher = myContext.Publishers.Find(1),
                    AddedDate = DateTime.Now,
                });
            gameList.Add(
                new Game
                {
                    GameId = 3,
                    Key = "sc2",
                    Name = "Starcraft II",
                    Description = "Starcraft 2 description here. ",
                    Genres = new List<Genre>
                    {
                        myContext.Genres.Find(3)
                    },
                    PlatformTypes = new List<PlatformType>
                    {
                        myContext.PlatformTypes.Find(2)
                    },
                    Publisher = myContext.Publishers.Find(1),
                    AddedDate = DateTime.Now,
                });

            foreach (var game in gameList)
            {
                myContext.Games.Add(game);
            }
            myContext.SaveChanges();

            #endregion

            #region Comment Seeding

            var commentList = new List<Comment>
            {
                new Comment {CommentId = 1, GameId = 1, Name = "Cs comment", Body = "My favorite shooter."},
                new Comment {CommentId = 2, GameId = 1, Name = "Cs comment 2", Body = "Mine too. ", ParentCommentId = 1},
                new Comment {CommentId = 3, GameId = 2, Name = "Dota comment", Body = "My favorite game."}
            };

            foreach (var comment in commentList)
            {
                myContext.Comments.Add(comment);
            }
            myContext.SaveChanges();

            #endregion

        }

        public class DropCreateIfModelChangesInitializer : DropCreateDatabaseIfModelChanges<MyDbContext>
        {
            protected override void Seed(MyDbContext context)
            {
                context.Seed(context);

                base.Seed(context);
            }
        }

        public class DropCreateAlwaysInitializer : DropCreateDatabaseAlways<MyDbContext>
        {
            protected override void Seed(MyDbContext context)
            {
                context.Seed(context);

                base.Seed(context);
            }
        }

        public class CreateInitializer : CreateDatabaseIfNotExists<MyDbContext>
        {
            protected override void Seed(MyDbContext context)
            {
                context.Seed(context);

                base.Seed(context);
            }
        }

        static MyDbContext()
        {
#if DEBUG
            Database.SetInitializer(new DropCreateAlwaysInitializer());
#else
        Database.SetInitializer<MyDbContext> (new CreateInitializer ());
#endif
        }
    }
}