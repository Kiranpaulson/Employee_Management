namespace EMPLOYEE_MANAGEMENT.Application.Dto
{
    public class LoginResponseDto
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string AccessToken { get; set; }
    }
}
