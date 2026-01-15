namespace SearchClassLibrary.Dto
{
    public class ServiceResponse
    {
        public record class GeneralResponse(bool flag, string message);
        public record class LoginResponse(bool flag, string token, string message);
    }
}
