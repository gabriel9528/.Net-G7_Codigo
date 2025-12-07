namespace AnaPrevention.GeneralMasterData.Api.Companies.Application.Dtos
{
    public class RegisterCompanyResponse
    {
        public Guid Id { get; set; }
        public string Description { get; set; } = String.Empty;
        public bool Status { get; set; }
    }
}
