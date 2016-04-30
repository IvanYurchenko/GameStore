using System;
using System.Linq;
using GameStore.Core;
using GameStore.DAL.Entities;
using GameStore.DAL.Entities.Localization;
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
                        string genreName =
                           genre.GenreLocalizations.First(
                               loc =>
                                   String.Equals(loc.Language.Code, Constants.EnglishLanguageCode,
                                       StringComparison.CurrentCultureIgnoreCase)).Name;

                        if (!game2.Genres.Any(x => x.GenreLocalizations
                            .First(loc =>
                                String.Equals(loc.Language.Code, Constants.EnglishLanguageCode, StringComparison.CurrentCultureIgnoreCase))
                                .Name == genreName))
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
                        string platformTypeType =
                            platformType.PlatformTypeLocalizations.First(
                                loc =>
                                    String.Equals(loc.Language.Code, Constants.EnglishLanguageCode,
                                        StringComparison.CurrentCultureIgnoreCase)).Type;

                        if (!game2.PlatformTypes.Any(x => x.PlatformTypeLocalizations
                            .First(loc => 
                                String.Equals(loc.Language.Code, Constants.EnglishLanguageCode, StringComparison.CurrentCultureIgnoreCase))
                                .Type == platformTypeType))
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
                string companyName1 = game1.Publisher.PublisherLocalizations.First(loc =>
                    String.Equals(loc.Language.Code, Constants.EnglishLanguageCode,
                        StringComparison.CurrentCultureIgnoreCase))
                    .CompanyName;

                string companyName2 = game2.Publisher.PublisherLocalizations.First(loc =>
                    String.Equals(loc.Language.Code, Constants.EnglishLanguageCode,
                        StringComparison.CurrentCultureIgnoreCase))
                    .CompanyName;

                publisherEquals = companyName1 == companyName2;
            }

            bool localizationsEquals = true;

            GameLocalization englishLocalization1 =
                game1.GameLocalizations.First(
                    loc =>
                        String.Equals(loc.Language.Code, Constants.EnglishLanguageCode,
                            StringComparison.CurrentCultureIgnoreCase));
            
            GameLocalization englishLocalization2 =
                game2.GameLocalizations.First(
                    loc =>
                        String.Equals(loc.Language.Code, Constants.EnglishLanguageCode,
                            StringComparison.CurrentCultureIgnoreCase));

            if (englishLocalization1.Name != englishLocalization2.Name ||
                englishLocalization1.Description != englishLocalization2.Description)
            {
                localizationsEquals = false;
            }

            bool result = game1.Key == game2.Key
                         && game1.Discontinued == game2.Discontinued
                         && game1.UnitsInStock == game2.UnitsInStock
                         && game1.Price == game2.Price
                         && game1.NorthwindId == game2.NorthwindId
                         && game1.IsReadonly == game2.IsReadonly
                         && genresEquals
                         && publisherEquals
                         && platformTypesEquals
                         && localizationsEquals;

            return result;
        }
    }
}