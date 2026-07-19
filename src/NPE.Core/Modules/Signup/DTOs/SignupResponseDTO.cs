namespace NPE.Core.Modules.Signup.Models
{
    public class SignupResponseDTO
    {
        public int CompanyId { get; set; }
        public int CenterId { get; set; }
        public int UserId { get; set; }
        public string Message { get; set; } = "";
    }
}