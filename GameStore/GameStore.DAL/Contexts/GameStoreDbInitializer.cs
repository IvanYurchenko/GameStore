#define DROPCREATEALWAYS
using System;
using System.Collections.Generic;
using System.Data.Entity;
using GameStore.Core.Enums;
using GameStore.DAL.Entities;
using GameStore.DAL.Entities.Localization;
using GameStore.DAL.Entities.Security;

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
            InitLanguages(context);
            InitGenres(context);
            InitPlatformTypes(context);
            InitPublishers(context);
            InitComments(context);
            InitGames(context);
            InitOrders(context);

            InitRoles(context);
            InitUsers(context);

            base.Seed(context);
        }

        private static void InitLanguages(GameStoreDbContext context)
        {
            context.Languages.Add(new Language
            {
                LanguageId = 1,
                Code = "en",
                Name = "English",
            });

            context.Languages.Add(new Language
            {
                LanguageId = 2,
                Code = "ru",
                Name = "Russian",
            });

            context.SaveChanges();
        }

        private static void InitGenres(GameStoreDbContext context)
        {
            context.Genres.Add(new Genre
            {
                GenreId = 1,
                ParentGenreId = null,
                GenreLocalizations = new List<GenreLocalization>
                {
                    new GenreLocalization
                    {
                        LanguageId = 1,
                        Language = context.Languages.Find(1), 
                        Name = "Other",
                    },
                    new GenreLocalization
                    {
                        LanguageId = 2,
                        Language = context.Languages.Find(2), 
                        Name = "Разное",
                    },
                }
            });

            context.Genres.Add(new Genre
            {
                GenreId = 2,
                ParentGenreId = null,
                GenreLocalizations = new List<GenreLocalization>
                {
                    new GenreLocalization
                    {
                        LanguageId = 1,
                        Language = context.Languages.Find(1), 
                        Name = "Action",
                    },
                    new GenreLocalization
                    {
                        LanguageId = 2,
                        Language = context.Languages.Find(2), 
                        Name = "Экшн",
                    },
                }
            });

            context.Genres.Add(new Genre
            {
                GenreId = 3,
                ParentGenreId = null,
                GenreLocalizations = new List<GenreLocalization>
                {
                    new GenreLocalization
                    {
                        LanguageId = 1,
                        Language = context.Languages.Find(1), 
                        Name = "Shooter",
                    },
                    new GenreLocalization
                    {
                        LanguageId = 2,
                        Language = context.Languages.Find(2), 
                        Name = "Стрелялка",
                    },
                }
            });

            context.Genres.Add(new Genre
            {
                GenreId = 4,
                ParentGenreId = null,
                GenreLocalizations = new List<GenreLocalization>
                {
                    new GenreLocalization
                    {
                        LanguageId = 1,
                        Language = context.Languages.Find(1), 
                        Name = "Action-Adventure",
                    },
                    new GenreLocalization
                    {
                        LanguageId = 2,
                        Language = context.Languages.Find(2), 
                        Name = "Действие-приключение",
                    },
                }
            });

            context.Genres.Add(new Genre
            {
                GenreId = 5,
                ParentGenreId = null,
                GenreLocalizations = new List<GenreLocalization>
                {
                    new GenreLocalization
                    {
                        LanguageId = 1,
                        Language = context.Languages.Find(1), 
                        Name = "Adventure",
                    },
                    new GenreLocalization
                    {
                        LanguageId = 2,
                        Language = context.Languages.Find(2), 
                        Name = "Приключение",
                    },
                }
            });

            context.Genres.Add(new Genre
            {
                GenreId = 6,
                ParentGenreId = null,
                GenreLocalizations = new List<GenreLocalization>
                {
                    new GenreLocalization
                    {
                        LanguageId = 1,
                        Language = context.Languages.Find(1), 
                        Name = "Role-playing",
                    },
                    new GenreLocalization
                    {
                        LanguageId = 2,
                        Language = context.Languages.Find(2), 
                        Name = "Ролевые",
                    },
                }
            });

            context.Genres.Add(new Genre
            {
                GenreId = 7,
                ParentGenreId = null,
                GenreLocalizations = new List<GenreLocalization>
                {
                    new GenreLocalization
                    {
                        LanguageId = 1,
                        Language = context.Languages.Find(1), 
                        Name = "Simulation",
                    },
                    new GenreLocalization
                    {
                        LanguageId = 2,
                        Language = context.Languages.Find(2), 
                        Name = "Симулятор",
                    },
                }
            });

            context.Genres.Add(new Genre
            {
                GenreId = 8,
                ParentGenreId = null,
                GenreLocalizations = new List<GenreLocalization>
                {
                    new GenreLocalization
                    {
                        LanguageId = 1,
                        Language = context.Languages.Find(1), 
                        Name = "Strategy",
                    },
                    new GenreLocalization
                    {
                        LanguageId = 2,
                        Language = context.Languages.Find(2), 
                        Name = "Стратегия",
                    },
                }
            });

            context.Genres.Add(new Genre
            {
                GenreId = 9,
                ParentGenreId = null,
                GenreLocalizations = new List<GenreLocalization>
                {
                    new GenreLocalization
                    {
                        LanguageId = 1,
                        Language = context.Languages.Find(1), 
                        Name = "Sports",
                    },
                    new GenreLocalization
                    {
                        LanguageId = 2,
                        Language = context.Languages.Find(2), 
                        Name = "Спорт",
                    },
                }
            });

            context.Genres.Add(new Genre
            {
                GenreId = 10,
                ParentGenreId = 6,
                GenreLocalizations = new List<GenreLocalization>
                {
                    new GenreLocalization
                    {
                        LanguageId = 1,
                        Language = context.Languages.Find(1), 
                        Name = "Action RPG",
                    },
                    new GenreLocalization
                    {
                        LanguageId = 2,
                        Language = context.Languages.Find(2), 
                        Name = "Экшен РПГ",
                    },
                }
            });

            context.Genres.Add(new Genre
            {
                GenreId = 11,
                ParentGenreId = 6,
                GenreLocalizations = new List<GenreLocalization>
                {
                    new GenreLocalization
                    {
                        LanguageId = 1,
                        Language = context.Languages.Find(1), 
                        Name = "MMO RPG",
                    },
                    new GenreLocalization
                    {
                        LanguageId = 2,
                        Language = context.Languages.Find(2), 
                        Name = "ММО РПГ",
                    },
                }
            });

            context.Genres.Add(new Genre
            {
                GenreId = 12,
                ParentGenreId = 6,
                GenreLocalizations = new List<GenreLocalization>
                {
                    new GenreLocalization
                    {
                        LanguageId = 1,
                        Language = context.Languages.Find(1), 
                        Name = "Tactical RPG",
                    },
                    new GenreLocalization
                    {
                        LanguageId = 2,
                        Language = context.Languages.Find(2), 
                        Name = "Тактическая РПГ",
                    },
                }
            });

            context.Genres.Add(new Genre
            {
                GenreId = 13,
                ParentGenreId = 9,
                GenreLocalizations = new List<GenreLocalization>
                {
                    new GenreLocalization
                    {
                        LanguageId = 1,
                        Language = context.Languages.Find(1), 
                        Name = "Racing",
                    },
                    new GenreLocalization
                    {
                        LanguageId = 2,
                        Language = context.Languages.Find(2), 
                        Name = "Гонки",
                    },
                }
            });

            context.Genres.Add(new Genre
            {
                GenreId = 14,
                ParentGenreId = 9,
                GenreLocalizations = new List<GenreLocalization>
                {
                    new GenreLocalization
                    {
                        LanguageId = 1,
                        Language = context.Languages.Find(1), 
                        Name = "Competitive",
                    },
                    new GenreLocalization
                    {
                        LanguageId = 2,
                        Language = context.Languages.Find(2), 
                        Name = "Соревнования",
                    },
                }
            });

            context.SaveChanges();
        }

        private static void InitPlatformTypes(GameStoreDbContext context)
        {
            context.PlatformTypes.Add(new PlatformType
            {
                PlatformTypeId = 1,
                PlatformTypeLocalizations = new List<PlatformTypeLocalization>
                {
                    new PlatformTypeLocalization
                    {
                        LanguageId = 1,
                        Language = context.Languages.Find(1),
                        Type = "Mobile",
                    },
                    new PlatformTypeLocalization
                    {
                        LanguageId = 2,
                        Language = context.Languages.Find(2),
                        Type = "Мобильные",
                    },
                }
            });

            context.PlatformTypes.Add(new PlatformType
            {
                PlatformTypeId = 2,
                PlatformTypeLocalizations = new List<PlatformTypeLocalization>
                {
                    new PlatformTypeLocalization
                    {
                        LanguageId = 1,
                        Language = context.Languages.Find(1),
                        Type = "Browser",
                    },
                    new PlatformTypeLocalization
                    {
                        LanguageId = 2,
                        Language = context.Languages.Find(2),
                        Type = "Браузерные",
                    },
                }
            });
            context.PlatformTypes.Add(new PlatformType
            {
                PlatformTypeId = 3,
                PlatformTypeLocalizations = new List<PlatformTypeLocalization>
                {
                    new PlatformTypeLocalization
                    {
                        LanguageId = 1,
                        Language = context.Languages.Find(1),
                        Type = "Desktop",
                    },
                    new PlatformTypeLocalization
                    {
                        LanguageId = 2,
                        Language = context.Languages.Find(2),
                        Type = "Десктопные",
                    },
                }
            });
            context.PlatformTypes.Add(new PlatformType
            {
                PlatformTypeId = 4,
                PlatformTypeLocalizations = new List<PlatformTypeLocalization>
                {
                    new PlatformTypeLocalization
                    {
                        LanguageId = 1,
                        Language = context.Languages.Find(1),
                        Type = "Console",
                    },
                    new PlatformTypeLocalization
                    {
                        LanguageId = 2,
                        Language = context.Languages.Find(2),
                        Type = "Консольные",
                    },
                }
            });

            context.SaveChanges();
        }

        private static void InitPublishers(GameStoreDbContext context)
        {
            context.Publishers.Add(new Publisher
            {
                PublisherId = 1,
                HomePage = "http://mysite.com",
                PublisherLocalizations = new List<PublisherLocalization>
                {
                    new PublisherLocalization
                    {
                        LanguageId = 1,
                        Language = context.Languages.Find(1),
                CompanyName = "Default",
                Description = "It is a default publisher. ",
                    },
                    new PublisherLocalization
                    {
                        LanguageId = 2,
                        Language = context.Languages.Find(2),
                CompanyName = "Неизвестно",
                Description = "Неизвестный издатель. ",
                    },
                }
            });

            context.Publishers.Add(new Publisher
            {
                PublisherId = 2,
                HomePage = "http://nintendo.com",
                PublisherLocalizations = new List<PublisherLocalization>
                {
                    new PublisherLocalization
                    {
                        LanguageId = 1,
                        Language = context.Languages.Find(1),
                CompanyName = "Nintendo",
                Description = "Nintendo description here. ",
                    },
                    new PublisherLocalization
                    {
                        LanguageId = 2,
                        Language = context.Languages.Find(2),
                CompanyName = "Нинтендо",
                Description = "Описание Нинтендо. ",
                    },
                }
            });

            context.Publishers.Add(new Publisher
            {
                PublisherId = 3,
                HomePage = "http://www.ubisoft.com",
                PublisherLocalizations = new List<PublisherLocalization>
                {
                    new PublisherLocalization
                    {
                        LanguageId = 1,
                        Language = context.Languages.Find(1),
                CompanyName = "Ubisoft",
                Description = "Ubisoft description here. ",
                    },
                    new PublisherLocalization
                    {
                        LanguageId =2,
                        Language = context.Languages.Find(2),
                CompanyName = "Юбисофт",
                Description = "Описание юбисофт. ",
                    },
                }
            });

            context.Publishers.Add(new Publisher
            {
                PublisherId = 4,
                HomePage = "http://www.ea.com",
                PublisherLocalizations = new List<PublisherLocalization>
                {
                    new PublisherLocalization
                    {
                        LanguageId = 1,
                        Language = context.Languages.Find(1),
                CompanyName = "EA",
                Description = "EA description here.",
                    },
                    new PublisherLocalization
                    {
                        LanguageId = 2,
                        Language = context.Languages.Find(2),
                CompanyName = "ЕА",
                Description = "Описание ЕА. ",
                    },
                }
            });

            context.Publishers.Add(new Publisher
            {
                PublisherId = 5,
                HomePage = "http://www.sony.com",
                PublisherLocalizations = new List<PublisherLocalization>
                {
                    new PublisherLocalization
                    {
                        LanguageId = 1,
                        Language = context.Languages.Find(1),
                CompanyName = "Sony",
                Description = "Sony description here.",
                    },
                    new PublisherLocalization
                    {
                        LanguageId = 2,
                        Language = context.Languages.Find(2),
                CompanyName = "Сони",
                Description = "Описание Сони. ",
                    },
                }
            });

            context.SaveChanges();
        }

        private static void InitComments(GameStoreDbContext context)
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

        private static void InitGames(GameStoreDbContext context)
        {
            context.Games.Add(new Game
            {
                Key = "nfs",
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
                GameLocalizations = new List<GameLocalization>
                {
                    new GameLocalization
                    {
                        
                        LanguageId = 1,
                        Language = context.Languages.Find(1),
                        Name = "Need for Speed",
                Description =
                    "Need for Speed is a series of racing video games published by Electronic Arts (EA) and developed by several studios including the Canadian company EA Black Box and the British company Criterion Games. ",
                
                    },
                    new GameLocalization
                    {
                        LanguageId = 2,
                        Language = context.Languages.Find(2),
                        Name = "Жажда скорости",
                Description =
                    "Жажда скорости — серия гоночных компьютерных игр, выпускаемая компанией Electronic Arts (EA) и разработанная в нескольких студиях, включая канадское отделение EA Black Box, британскую компанию Criterion Games и шведскую Ghost Games. В настоящее время разрабатывается под брендом EA Sports.",
                
                    }
                }
            });

            context.Games.Add(new Game
            {
                Key = "cs",
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
                AddedDate = DateTime.Now,
                GameLocalizations = new List<GameLocalization>
                {
                    new GameLocalization
                    {
                        
                        LanguageId = 1,
                        Language = context.Languages.Find(1),
                        Name = "Counter Strike",
                Description =
                    "Counter-Strike is a first-person shooter video game developed by Valve Corporation. It was initially developed and released as a Half-Life modification by Minh \"Gooseman\" Le and Jess \"Cliffe\" Cliffe in 1999, before Le and Cliffe were hired and the game's intellectual property acquired. Counter-Strike was first released by Valve on the Microsoft Windows platform in 2000. The game later spawned a franchise, and is the first installment in the Counter-Strike series. Several remakes and Ports of Counter-Strike have been released on the Xbox console, as well as OS X and Linux.",
                
                    },
                    new GameLocalization
                    {
                        
                        LanguageId = 2,
                        Language = context.Languages.Find(2),
                        Name = "Контрудар",
                Description =
                    "Контрудар — серия компьютерных игр в жанре командного шутера от первого лица, основанная на движке GoldSrc, изначально появившаяся как модификация игры Half-Life. Всего в основной серии вышло пять версий игры, самая популярная из которых — Counter-Strike 1.6. Существуют и отдельные части, например Counter-Strike: Condition Zero (мультиплеер с одиночными заданиями) и Condition Zero: Deleted Scenes (сюжетная игра), основанные на движке GoldSource, а также Counter-Strike: Source, основанная на более современном движке Source. 14 августа 2011 года Valve официально презентовала Counter-Strike: Global Offensive, которая вышла 21 августа 2012 года.",
                
                    },
                }
            });

            context.Games.Add(new Game
            {
                Key = "minesweeper",
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
                AddedDate = DateTime.Now,
                GameLocalizations = new List<GameLocalization>
                {
                    new GameLocalization
                    {
                        
                        LanguageId = 1,
                        Language = context.Languages.Find(1),
                        Name = "Minesweeper",
                Description =
                    "Minesweeper is a single-player puzzle video game. The objective of the game is to clear a rectangular board containing hidden \"mines\" without detonating any of them, with help from clues about the number of neighboring mines in each field. The game originates from the 1960s, and has been written for many computing platforms in use today. It has many variations and offshoots.",
                
                    },
                    new GameLocalization
                    {
                        
                        LanguageId = 2,
                        Language = context.Languages.Find(2),
                        Name = "Сапер",
                Description =
                    "Сапёр» — компьютерная игра-головоломка.",
                    },
                }
            });

            context.Games.Add(new Game
            {
                Key = "purio-3",
                Price = (decimal)5.99,
                UnitsInStock = 1000,
                Discontinued = false,
                PlatformTypes = new List<PlatformType>
                {
                    context.PlatformTypes.Find(3)
                },
                PublicationDate = DateTime.Now.AddDays(-40),
                AddedDate = DateTime.Now,
                GameLocalizations = new List<GameLocalization>
                {
                    new GameLocalization
                    {
                        
                        LanguageId = 1,
                        Language = context.Languages.Find(1),
                        Name = "Purio 3",
                Description =
                    "Purio 3 description here. ",
                
                    },
                    new GameLocalization
                    {
                        
                        LanguageId = 2,
                        Language = context.Languages.Find(2),
                        Name = "Пурио 3",
                Description =
                    "Описание Пурио 3.",
                    },
                }
            });

            context.SaveChanges();
        }

        private static void InitOrders(GameStoreDbContext context)
        {
            context.Orders.Add(new Order
            {
                OrderStatus = OrderStatus.New,
                OrderDate = DateTime.UtcNow,
                SessionKey = "someKey123",
                OrderItems = new List<OrderItem>(),
            });

            var random = new Random();
            for (int i = 0; i < 100; i++)
            {
                context.Orders.Add(new Order
                {
                    OrderStatus = (OrderStatus)(random.Next(3) + 1),
                    SessionKey = "sessionKey" + i,
                    OrderDate = DateTime.UtcNow.AddDays(-random.Next(70)),
                });
            }

            context.SaveChanges();
        }

        public void InitRoles(GameStoreDbContext context)
        {
            context.Roles.Add(new Role
            {
                RoleId = 1,
                RoleName = "Admin",
                IsReadonly = true,
            });

            context.Roles.Add(new Role
            {
                RoleId = 2,
                RoleName = "User",
                IsReadonly = true,
            });

            context.Roles.Add(new Role
            {
                RoleId = 3,
                RoleName = "Moderator",
                IsReadonly = true,
            });

            context.Roles.Add(new Role
            {
                RoleId = 4,
                RoleName = "Manager",
                IsReadonly = true,
            });

            context.SaveChanges();
        }

        private static void InitUsers(GameStoreDbContext context)
        {
            context.Users.Add(new User
            {
                UserName = "admin",
                Email = "admin@my.com",
                FirstName = "Admin",
                Password = "123123",
                CreateDate = DateTime.UtcNow,
                IsReadonly = true,
                Roles = new List<Role> { context.Roles.Find(1) }
            });

            context.Users.Add(new User
            {
                UserName = "moderator",
                Email = "moderator@my.com",
                FirstName = "Moderator",
                Password = "123123",
                IsReadonly = false,
                CreateDate = DateTime.UtcNow,
                Roles = new List<Role> { context.Roles.Find(3) }
            });

            context.Users.Add(new User
            {
                UserName = "manager",
                Email = "manager@my.com",
                FirstName = "Manager",
                Password = "123123",
                IsReadonly = false,
                CreateDate = DateTime.UtcNow,
                Roles = new List<Role> { context.Roles.Find(4) }
            });

            context.Users.Add(new User
            {
                UserName = "user1",
                Email = "user1@my.com",
                FirstName = "User1",
                Password = "123123",
                IsReadonly = false,
                CreateDate = DateTime.UtcNow,
                Roles = new List<Role> { context.Roles.Find(2) }
            });

            context.Users.Add(new User
            {
                UserName = "user2",
                Email = "user2@my.com",
                FirstName = "User2",
                Password = "123123",
                IsReadonly = false,
                CreateDate = DateTime.UtcNow,
                Roles = new List<Role> { context.Roles.Find(2) }
            });

            context.Users.Add(new User
            {
                UserName = "user3",
                Email = "user3@my.com",
                FirstName = "User3",
                Password = "123123",
                IsReadonly = false,
                CreateDate = DateTime.UtcNow,
                Roles = new List<Role> { context.Roles.Find(2) }
            });

            context.SaveChanges();
        }

    }
}