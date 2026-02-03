namespace G7_Microservices.Backend.AuthAPI.Models.Dto
{
    public class ResponseDto
    {
        public object? Result { get; set; }
        public bool IsSucess { get; set; } = true;
        public string Message { get; set; } = string.Empty;
    }
}
