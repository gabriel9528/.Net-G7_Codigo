namespace AnaPrevention.GeneralMasterData.Api.Companies.Application.Dtos
{
    public class EditCompanyRequest
    {
        public Guid Id { get; set; }
        public string Description { get; set; } = String.Empty;
        public SettingCompanyRequest? Setting { get; set; }
        public bool Status { get; set; }
    }
}
