namespace GameStore.WebUI.Security
{
    public class CustomPrincipalSerializeModel
    {
        public int UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string[] Roles { get; set; }
    }
}