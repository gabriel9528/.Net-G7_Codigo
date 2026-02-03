namespace G7_Microservices.FrontEnd.Web.Models.Dto.Auth
{
    public class LoginResponseDto
    {
        public UserDto User { get; set; }
        public string Token { get; set; }
    }
}
