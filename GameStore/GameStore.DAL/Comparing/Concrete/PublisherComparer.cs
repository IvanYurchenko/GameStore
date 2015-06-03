using System;
using System.Linq;
using GameStore.Core;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;

namespace GameStore.DAL.Comparing.Concrete
{
    public class PublisherComparer : ICustomComparer
    {
        public bool AreEqual(Object object1, Object object2)
        {
            if (object1.GetType() != typeof(Publisher) || object2.GetType() != typeof(Publisher))
            {
                throw new ArgumentException(String.Format("An object is not an instance of a {0} class. ",
                    typeof(Publisher)));
            }

            var publisher1 = (Publisher)object1;
            var publisher2 = (Publisher)object2;

            bool localizationsEquals = true;

            var englishLocalization1 =
                publisher1.PublisherLocalizations.First(
                    loc =>
                        String.Equals(loc.Language.Code, Constants.EnglishLanguageCode,
                            StringComparison.CurrentCultureIgnoreCase));

            var englishLocalization2 =
                publisher2.PublisherLocalizations.First(
                    loc =>
                        String.Equals(loc.Language.Code, Constants.EnglishLanguageCode,
                            StringComparison.CurrentCultureIgnoreCase));

            if (englishLocalization1.CompanyName != englishLocalization2.CompanyName ||
                englishLocalization1.Description != englishLocalization2.Description)
            {
                localizationsEquals = false;
            }

            bool result = publisher1.HomePage == publisher2.HomePage
                         && publisher1.NorthwindId == publisher2.NorthwindId
                         && localizationsEquals;

            return result;
        }
    }
}