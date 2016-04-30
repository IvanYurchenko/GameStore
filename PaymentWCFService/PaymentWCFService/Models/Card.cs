namespace PaymentWCFService.Models
{
    public class Card
    {
        public string Number { get; set; }

        public int Cvv { get; set; }

        public int ExpirationYear { get; set; }

        public int ExpirationMonth { get; set; }

        public decimal Balance { get; set; }
    }
}