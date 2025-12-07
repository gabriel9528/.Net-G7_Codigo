namespace AnaPrevention.GeneralMasterData.Api.Companies.Application.Dtos
{
    public class RegisterCompanyRequest
    {
        public string Description { get; set; } = String.Empty;
        public SettingCompanyRequest? Setting { get; set; }
    }
}
