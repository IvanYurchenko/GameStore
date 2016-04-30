using System.Linq;
using System.Security.Principal;

namespace GameStore.WebUI.Security
{
    public class CustomPrincipal : IPrincipal
    {
        public CustomPrincipal(string username)
        {
            Identity = new GenericIdentity(username);
        }

        public IIdentity Identity { get; private set; }

        public int UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string[] Roles { get; set; }

        public bool IsInRole(string role)
        {
            return Roles.Any(r => role.Contains(r));
        }
    }
}