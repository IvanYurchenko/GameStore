namespace PaymentWCFService.Models
{
    public class Client
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public Card AccountCard { get; set; }
    }
}