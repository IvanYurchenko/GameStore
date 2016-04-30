using System;
using System.Collections.Generic;
using PaymentWCFService.Models;

namespace PaymentWCFService.DataStoring
{
    public static class DataStore
    {
        static DataStore()
        {
            Clients = new List<Client>
            {
                new Client
                {
                    Id = 1,
                    Name = "GameStore",
                    AccountCard = new Card(),
                    Email = "lazer.ok@mail.ru",
                }
            };

            Payments = new List<Payment>();

            Tokens = new List<Guid>();

            Cards = new List<Card>
            {
                new Card
                {
                    Number = "1234567890123456",
                    ExpirationYear = 2015,
                    ExpirationMonth = 12,
                    Balance = 150,
                    Cvv = 100,
                }
            };
        }

        public static List<Client> Clients { get; set; }

        public static List<Payment> Payments { get; set; }

        public static List<Guid> Tokens { get; set; }

        public static List<Card> Cards { get; set; }
    }
}