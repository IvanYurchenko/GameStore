using System;
using System.Collections.Generic;
using GameStore.DAL.Comparing.Concrete;
using GameStore.DAL.Entities;
using GameStore.DAL.Entities.Localization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameStore.DAL.Tests.Comparing.Concrete
{
    [TestClass]
    public class GameComparerTest
    {
        [TestMethod]
        public void Check_That_Game_Comparer_Returns_True_For_Equal_Games()
        {
            var game1 = new Game
            {
                GameId = 1,
                NorthwindId = 1,
                OrderItems = new List<OrderItem>(),
                AddedDate = new DateTime(),
                BasketItems = new List<BasketItem>(),
                Comments = new List<Comment>(),
                Discontinued = false,
                Genres = new List<Genre>(),
                IsReadonly = true,
                Key = "key",
                PlatformTypes = new List<PlatformType>(),
                Price = 5,
                PublicationDate = DateTime.UtcNow,
                Publisher = new Publisher
                {
                    PublisherLocalizations = new List<PublisherLocalization>
                {
                    new PublisherLocalization
                    {
                        CompanyName = "name",
                        Description = "description",
                        LanguageId = 1,
                        Language = new Language
                        {
                            Name = "English",
                            Code = "en",
                            LanguageId = 1,
                        }
                    }
                }
                },
                PublisherId = 5,
                UnitsInStock = 6,
                GameLocalizations = new List<GameLocalization>
                    {
                        new GameLocalization
                        {
                            Language = new Language
                            {
                                Code = "en",
                                Name = "English",
                                LanguageId = 1
                            },
                            Name = "name",
                            Description = "description",
                        }
                    }
            };

            var game2 = new Game
            {
                GameId = 1,
                NorthwindId = 1,
                OrderItems = new List<OrderItem>(),
                AddedDate = new DateTime(),
                BasketItems = new List<BasketItem>(),
                Comments = new List<Comment>(),
                Discontinued = false,
                Genres = new List<Genre>(),
                IsReadonly = true,
                Key = "key",
                PlatformTypes = new List<PlatformType>(),
                Price = 5,
                PublicationDate = DateTime.UtcNow,
                Publisher = new Publisher
                {
                    PublisherLocalizations = new List<PublisherLocalization>
                {
                    new PublisherLocalization
                    {
                        CompanyName = "name",
                        Description = "description",
                        LanguageId = 1,
                        Language = new Language
                        {
                            Name = "English",
                            Code = "en",
                            LanguageId = 1,
                        }
                    }
                }
                },
                PublisherId = 5,
                UnitsInStock = 6,
                GameLocalizations = new List<GameLocalization>
                    {
                        new GameLocalization
                        {
                            Language = new Language
                            {
                                Code = "en",
                                Name = "English",
                                LanguageId = 1
                            },
                            Name = "name",
                            Description = "description",
                        }
                    }
            };

            var comparer = new GameComparer();

            bool result = comparer.AreEqual(game1, game2);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Check_That_Game_Comparer_Returns_False_For_Not_Equal_Games()
        {
            var game1 = new Game
            {
                GameId = 1,
                NorthwindId = 2,
                OrderItems = new List<OrderItem>(),
                AddedDate = new DateTime(),
                BasketItems = new List<BasketItem>(),
                Comments = new List<Comment>(),
                Discontinued = false,
                Genres = new List<Genre>(),
                IsReadonly = true,
                Key = "key",
                PlatformTypes = new List<PlatformType>(),
                Price = 5,
                PublicationDate = DateTime.UtcNow,
                Publisher = new Publisher
                {
                    PublisherLocalizations = new List<PublisherLocalization>
                {
                    new PublisherLocalization
                    {
                        CompanyName = "name",
                        Description = "description",
                        LanguageId = 1,
                        Language = new Language
                        {
                            Name = "English",
                            Code = "en",
                            LanguageId = 1,
                        }
                    }
                }
                },
                PublisherId = 5,
                UnitsInStock = 6,
                GameLocalizations = new List<GameLocalization>
                    {
                        new GameLocalization
                        {
                            Language = new Language
                            {
                                Code = "en",
                                Name = "English",
                                LanguageId = 1
                            },
                            Name = "name",
                            Description = "description",
                        }
                    }
            };

            var game2 = new Game
            {
                GameId = 1,
                NorthwindId = 1,
                OrderItems = new List<OrderItem>(),
                AddedDate = new DateTime(),
                BasketItems = new List<BasketItem>(),
                Comments = new List<Comment>(),
                Discontinued = false,
                Genres = new List<Genre>(),
                IsReadonly = true,
                Key = "key",
                PlatformTypes = new List<PlatformType>(),
                Price = 5,
                PublicationDate = DateTime.UtcNow,
                Publisher = new Publisher
                {
                    PublisherLocalizations = new List<PublisherLocalization>
                {
                    new PublisherLocalization
                    {
                        CompanyName = "name",
                        Description = "description",
                        LanguageId = 1,
                        Language = new Language
                        {
                            Name = "English",
                            Code = "en",
                            LanguageId = 1,
                        }
                    }
                }
                },
                PublisherId = 5,
                UnitsInStock = 6,
                GameLocalizations = new List<GameLocalization>
                    {
                        new GameLocalization
                        {
                            Language = new Language
                            {
                                Code = "en",
                                Name = "English",
                                LanguageId = 1
                            },
                            Name = "name",
                            Description = "description",
                        }
                    }
            };

            var comparer = new GameComparer();

            bool result = comparer.AreEqual(game1, game2);

            Assert.IsTrue(!result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Check_That_Game_Comparer_Throws_Exception_When_Obj1_Has_Wrong_Type()
        {
            var comparer = new GameComparer();

            var obj1 = new Object();
            var obj2 = new Game();

            comparer.AreEqual(obj1, obj2);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Check_That_Game_Comparer_Throws_Exception_When_Obj2_Has_Wrong_Type()
        {
            var comparer = new GameComparer();

            var obj1 = new Game();
            var obj2 = new Object();

            comparer.AreEqual(obj1, obj2);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Check_That_Game_Comparer_Throws_Exception_When_Obj1_And_Obj2_Have_Wrong_Type()
        {
            var comparer = new GameComparer();

            var obj1 = new Object();
            var obj2 = new Object();

            comparer.AreEqual(obj1, obj2);
        }

    }
}
