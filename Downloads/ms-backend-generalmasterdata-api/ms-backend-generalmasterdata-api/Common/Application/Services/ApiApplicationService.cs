using AnaPrevention.GeneralMasterData.Api.Common.Application.Static;
using AnaPrevention.GeneralMasterData.Api.Common.Application.Dtos;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace AnaPrevention.GeneralMasterData.Api.Common.Application.Services
{
    public class ApiApplicationService
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _clientOccupationalMasterData;
        private readonly HttpClient _clientOccupationalAttention;

        public ApiApplicationService(IConfiguration configuration)
        {
            _configuration = configuration;
            try
            {
                string urlOccupationalMasterData = _configuration["Apis:OccupationalMasterData:Url"] ?? "";
                string urlOccupationalAttention = _configuration["Apis:OccupationalAttention:Url"] ?? "";

                string envioroment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "";
                if (string.IsNullOrWhiteSpace(envioroment))
                {
                    urlOccupationalMasterData = "http://internal-PULSCRP-PE01-ANAP-APD-ALB-739648191.us-east-1.elb.amazonaws.com:8082/v1/";
                    urlOccupationalAttention = "http://internal-PULSCRP-PE01-ANAP-APD-ALB-739648191.us-east-1.elb.amazonaws.com:8083/v1/";
                }

              

                _clientOccupationalMasterData = new HttpClient
                {
                    BaseAddress = new Uri(urlOccupationalMasterData)
                };
                _clientOccupationalAttention = new HttpClient
                {
                    BaseAddress = new Uri(urlOccupationalAttention)
                };
           

                if (!string.IsNullOrEmpty(envioroment))
                {
                    string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJhOTA1MzcxNS02YmE2LTQxMGItYTc2Yy1jZTY2MjY2M2NiYmEiLCJ1c2VyTmFtZSI6IkFETUlOIiwibWFpbCI6IiIsInVzZXJUeXBlSWQiOiJjOWRjMDI3OS1iZjRlLTRiMmUtYTc5MS1mZjliYmNiN2ZhMjIiLCJQZXJzb25JZCI6IjIxOWYwNGQzLWM1YTAtNDk1OC05NTQ0LTYxOGIyZWE1NjYxMCIsImNvbXBhbnlJZCI6IjcyMWIzMjdlLTgyYmUtNDM0NS1hYzMwLTNjOTgwYjgwNGYzZCIsInN1YnNpZGlhcnlJZCI6IjRiNmE1Yzk1LTE0YjMtNGZiZC1hNzQzLTdjYTlmYjhiN2Y4MSIsIkRvY3RvcklkIjoiYWEwMzY4MzgtMjRjZS00OWNkLTkzYWYtYWU1MTk5YjRmYWUxIiwiVXNlckxvZ2luVHlwZSI6WyIwIiwiMCJdLCJidXNpbmVzcyI6IlB1bHNvV2ViLkFQSS5TZWN1cml0eS5BcHBsaWNhdGlvbi5EdG9zLlVzZXJCdXNpbmVzc1JlbGF0aW9uc2hpcER0byIsIm5iZiI6MTcwOTA0NjA1OCwiZXhwIjoxODY2ODk4ODU4LCJpc3MiOiJodHRwczovL2xvY2FsaG9zdDo3MjM4IiwiYXVkIjoiY2xlYW5hcmNoaXRlY3R1cmVhcGkifQ.5CFWPNpEnjm3v60K0hYhvBp-HmhURA-DEAekv3NwXdw";
                    _clientOccupationalMasterData.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    _clientOccupationalAttention.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }
            }
            catch (Exception)
            {
            }
        }

        public async Task<T?> PostOccupationalAttention<T>(object request, string url) where T : class
        {
            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            var response = await _clientOccupationalAttention.PostAsync(url, content);

            EnvelopeDto? result = null;
            if (response.IsSuccessStatusCode)
            {
                string ContentResponse = await response.Content.ReadAsStringAsync();
                result = CommonStatic.ConvertJsonToDto<EnvelopeDto>(ContentResponse);
            }
            return CommonStatic.ConvertJsonToDto<T>(result?.Result?.ToString());
        }
        public async Task<T?> PostOccupationalMasterData<T>(object request, string url) where T : class
        {
            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            var response = await _clientOccupationalMasterData.PostAsync(url, content);

            EnvelopeDto? result = null;
            if (response.IsSuccessStatusCode)
            {
                string ContentResponse = await response.Content.ReadAsStringAsync();
                result = CommonStatic.ConvertJsonToDto<EnvelopeDto>(ContentResponse);
            }
            return CommonStatic.ConvertJsonToDto<T>(result?.Result?.ToString());
        }

        public async Task<T?> GetOccupationalMasterData<T>(string url) where T : class
        {
            var response = await _clientOccupationalMasterData.GetAsync(url);

            EnvelopeDto? result = null;
            if (response.IsSuccessStatusCode)
            {
                string ContentResponse = await response.Content.ReadAsStringAsync();
                result = CommonStatic.ConvertJsonToDto<EnvelopeDto>(ContentResponse);
            }
            return CommonStatic.ConvertJsonToDto<T>(result?.Result?.ToString());
        }
    }
}
