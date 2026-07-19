using NPE.Core.Modules.Management.Center.Models;

namespace NPE.Core.Modules.Signup.Models
{
    public class CompanySignupRequest
    {
        public string CompanyName { get; set; } = "";
        public string Address { get; set; } = "";
        public int CityId { get; set; }
        public int CountryId { get; set; }

        public string ContactPerson { get; set; } = "";
        public string Phone { get; set; } = "";
        public string Email { get; set; } = "";

        public CenterDTO? Center { get; set; }

        public string Username { get; set; } = "";

        public string Password { get; set; } = "";
    }
}