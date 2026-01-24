using G7_Microservices.FrontEnd.Web.Models.Dto;
using G7_Microservices.FrontEnd.Web.Services.IServices;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using static G7_Microservices.FrontEnd.Web.Utility.SD;

namespace G7_Microservices.FrontEnd.Web.Services
{
    public class BaseService : IBaseService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ITokenProvider _tokenProvider;
        public BaseService(IHttpClientFactory httpClientFactory, ITokenProvider tokenProvider)
        {
            _httpClientFactory = httpClientFactory;
            _tokenProvider = tokenProvider;
        }

        public async Task<ResponseDto?> SendAsync(RequestDto requestDto, bool withBearer = true)
        {
            try
            {
                HttpClient client = _httpClientFactory.CreateClient("Microservices");
                HttpRequestMessage request = new HttpRequestMessage();
                request.Headers.Add("Accept", "application/json");

                //token
                if(withBearer)
                {
                    var token = _tokenProvider.GetToken();
                    request.Headers.Add("Authorization", $"Bearer {token}");
                }

                request.RequestUri = new Uri(requestDto.Url);
                request.Method = requestDto.API_TYPE switch
                {
                    API_TYPE.GET => HttpMethod.Get,
                    API_TYPE.POST => HttpMethod.Post,
                    API_TYPE.PUT => HttpMethod.Put,
                    API_TYPE.DELETE => HttpMethod.Delete,
                    _ => HttpMethod.Get
                };

                if (requestDto.Data != null)
                {
                    string jsonData = JsonConvert.SerializeObject(requestDto.Data);
                    request.Content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                }

                HttpResponseMessage responseMessage = await client.SendAsync(request);
                if (!responseMessage.IsSuccessStatusCode)
                {
                    var errorContent = await responseMessage.Content.ReadAsStringAsync();
                }

                switch (responseMessage.StatusCode)
                {
                    case HttpStatusCode.NotFound:
                        return new ResponseDto { IsSucess = false, Message = "Not Found" };
                    case HttpStatusCode.Forbidden:
                        return new ResponseDto { IsSucess = false, Message = "Access Denied" };
                    case HttpStatusCode.Unauthorized:
                        return new ResponseDto { IsSucess = false, Message = "Unauthorized" };
                    case HttpStatusCode.InternalServerError:
                        return new ResponseDto { IsSucess = false, Message = "Internal Server Error" };
                    case HttpStatusCode.BadRequest:
                        return new ResponseDto { IsSucess = false, Message = "Bad Request" };
                    case HttpStatusCode.MethodNotAllowed:
                        return new ResponseDto { IsSucess = false, Message = "Method Not Allowed" };
                    default:
                        var apiContent = await responseMessage.Content.ReadAsStringAsync();
                        var apiResponseDto = JsonConvert.DeserializeObject<ResponseDto>(apiContent);
                        return apiResponseDto;
                }
            }
            catch (Exception ex)
            {
                var newResponse = new ResponseDto { IsSucess = false, Message = ex.Message };
                return newResponse;
            }
        }
                    

    }
}
