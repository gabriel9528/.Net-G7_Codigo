namespace AnaPrevention.GeneralMasterData.Api.Companies.Application.Dtos
{
    public class CompanyDto
    {
        public Guid Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public SettingDto Setting { get; set; } = new();
        public bool Status { get; set; }

    }
}
