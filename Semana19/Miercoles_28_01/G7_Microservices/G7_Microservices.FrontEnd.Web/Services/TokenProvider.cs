using G7_Microservices.FrontEnd.Web.Services.IServices;
using G7_Microservices.FrontEnd.Web.Utility;

namespace G7_Microservices.FrontEnd.Web.Services
{
    public class TokenProvider : ITokenProvider
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public TokenProvider(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public void ClearToken()
        {
            _contextAccessor.HttpContext?.Response.Cookies.Delete(SD.TokenCookie);
        }

        public string? GetToken()
        {
            var context = _contextAccessor.HttpContext;
            if (context != null)
            {
                var cookie = context.Request.Cookies[SD.TokenCookie];
                return cookie;
            }
            return "No se encontro el token";
        }

        public void SetToken(string token)
        {
            _contextAccessor.HttpContext?.Response.Cookies.Append(SD.TokenCookie, token);
        }
    }
}
