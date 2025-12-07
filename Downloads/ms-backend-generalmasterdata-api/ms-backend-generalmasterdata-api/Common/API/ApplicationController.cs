
using Microsoft.AspNetCore.Mvc;
using AnaPrevention.GeneralMasterData.Api.Common.Domain.Entities;
using System.Net;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace AnaPrevention.GeneralMasterData.Api.Common.API
{
    public class ApplicationController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;
        protected new ClaimsPrincipal User;

        public ApplicationController(IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
            var httpContext = _httpContextAccessor.HttpContext;

            // Accede al token JWT desde el encabezado de autorización
            string? token = httpContext?.Request.Headers["Authorization"];

            // Si el token es del tipo Bearer, elimina el prefijo "Bearer " para obtener solo el token
            if (!string.IsNullOrEmpty(token) && token.StartsWith("Bearer "))
            {
                token = token["Bearer ".Length..].Trim();
                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ValidateIssuerSigningKey = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Authentication:SecretForKey"] ?? "")),
                    ValidateLifetime = false
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters,out _);
                User = principal;
            }
            //}
            User ??= new();
        }

        protected new IActionResult Ok(object? result = null)
        {
            return new EnvelopeResult(Envelope.Ok(result), HttpStatusCode.OK);
        }

        protected IActionResult Created(object? result = null)
        {
            return new EnvelopeResult(Envelope.Ok(result), HttpStatusCode.Created);
        }

        protected new IActionResult NotFound()
        {
            return new EnvelopeResult(Envelope.NotFound(), HttpStatusCode.NotFound);
        }

        protected IActionResult Forbidden()
        {
            return new EnvelopeResult(Envelope.Forbidden(), HttpStatusCode.Forbidden);
        }

        protected IActionResult BadRequest(List<Error> errors)
        {
            return new EnvelopeResult(Envelope.BadRequest(errors), HttpStatusCode.BadRequest);
        }

        protected new IActionResult Unauthorized()
        {
            return new EnvelopeResult(Envelope.Unauthorized(), HttpStatusCode.Unauthorized);
        }

        protected IActionResult ServerError()
        {
            return new EnvelopeResult(Envelope.ServerError(), HttpStatusCode.InternalServerError);
        }
    }
}
