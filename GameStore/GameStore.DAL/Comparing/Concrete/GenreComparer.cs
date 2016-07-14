using System;
using System.Linq;
using GameStore.Core;
using GameStore.DAL.Entities;
using GameStore.DAL.Entities.Localization;
using GameStore.DAL.Interfaces;

namespace GameStore.DAL.Comparing.Concrete
{
    public class GenreComparer : ICustomComparer
    {
        public bool AreEqual(Object object1, Object object2)
        {
            if (object1.GetType() != typeof(Genre) || object2.GetType() != typeof(Genre))
            {
                throw new ArgumentException(String.Format("An object is not an instance of a {0} class. ",
                    typeof(Genre)));
            }

            var genre1 = (Genre)object1;
            var genre2 = (Genre)object2;

            bool localizationsEquals = true;

            GenreLocalization englishLocalization1 =
                genre1.GenreLocalizations.First(
                    loc =>
                        String.Equals(loc.Language.Code, Constants.EnglishLanguageCode,
                            StringComparison.CurrentCultureIgnoreCase));

            GenreLocalization englishLocalization2 =
                genre2.GenreLocalizations.First(
                    loc =>
                        String.Equals(loc.Language.Code, Constants.EnglishLanguageCode,
                            StringComparison.CurrentCultureIgnoreCase));

            if (englishLocalization1.Name != englishLocalization2.Name)
            {
                localizationsEquals = false;
            }

            bool result = genre1.IsReadonly == genre2.IsReadonly
                         && genre1.NorthwindId == genre2.NorthwindId
                         && localizationsEquals;

            return result;
        }
    }
}