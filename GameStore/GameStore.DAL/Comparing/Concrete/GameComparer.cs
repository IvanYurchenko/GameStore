using System;
using System.Linq;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;

namespace GameStore.DAL.Comparing.Concrete
{
    public class GameComparer : ICustomComparer
    {
        public bool AreEqual(Object object1, Object object2)
        {
            if (object1.GetType() != typeof(Game) || object2.GetType() != typeof(Game))
            {
                throw new ArgumentException(String.Format("An object is not an instance of a {0} class. ", typeof(Game)));
            }

            var game1 = (Game)object1;
            var game2 = (Game)object2;

            bool genresEquals = true;
            if (game1.Genres != null && game2.Genres != null)
            {
                if (game1.Genres.Count != game2.Genres.Count)
                {
                    genresEquals = false;
                }
                else
                {
                    foreach (Genre genre in game1.Genres)
                    {
                        if (!game2.Genres.Any(x => x.Name == genre.Name))
                        {
                            genresEquals = false;
                        }
                    }
                }
            }

            if ((game1.Genres == null && game2.Genres != null && game2.Genres.Count > 0)
                || (game1.Genres != null && game2.Genres == null && game1.Genres.Count > 0))
            {
                genresEquals = false;
            }

            bool platformTypesEquals = true;
            if (game1.PlatformTypes != null && game2.PlatformTypes != null)
            {
                if (game1.PlatformTypes.Count != game2.PlatformTypes.Count)
                {
                    platformTypesEquals = false;
                }
                else
                {
                    foreach (PlatformType platformType in game1.PlatformTypes)
                    {
                        if (!game2.PlatformTypes.Any(x => x.Type == platformType.Type))
                        {
                            platformTypesEquals = false;
                        }
                    }
                }
            }

            if ((game1.PlatformTypes == null && game2.PlatformTypes != null && game2.PlatformTypes.Count > 0)
                || (game1.PlatformTypes != null && game2.PlatformTypes == null && game1.PlatformTypes.Count > 0))
            {
                platformTypesEquals = false;
            }

            bool publisherEquals = true;
            if (game1.Publisher != null && game2.Publisher != null)
            {
                publisherEquals = game1.Publisher.CompanyName == game2.Publisher.CompanyName;
            }

            bool result = game1.Key == game2.Key
                         && game1.Name == game2.Name
                         && game1.Description == game2.Description
                         && game1.Discontinued == game2.Discontinued
                         && game1.UnitsInStock == game2.UnitsInStock
                         && game1.Price == game2.Price
                         && game1.NorthwindId == game2.NorthwindId
                         && game1.IsReadonly == game2.IsReadonly
                         && genresEquals
                         && publisherEquals
                         && platformTypesEquals;

            return result;
        }
    }
}