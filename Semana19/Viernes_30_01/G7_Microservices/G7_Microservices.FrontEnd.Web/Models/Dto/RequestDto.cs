using static G7_Microservices.FrontEnd.Web.Utility.SD;

namespace G7_Microservices.FrontEnd.Web.Models.Dto
{
    public class RequestDto
    {
        public API_TYPE API_TYPE { get; set; } = API_TYPE.GET;
        public string Url { get; set; }
        public object Data { get; set; }
        public string JwtToken { get; set; }
    }
}
