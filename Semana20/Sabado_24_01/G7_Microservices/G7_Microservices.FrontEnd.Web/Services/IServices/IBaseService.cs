using G7_Microservices.FrontEnd.Web.Models.Dto;

namespace G7_Microservices.FrontEnd.Web.Services.IServices
{
    public interface IBaseService
    {
        Task<ResponseDto?> SendAsync (RequestDto requestDto, bool withBearer = true);
    }
}
