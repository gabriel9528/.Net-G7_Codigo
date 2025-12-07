namespace AnaPrevention.GeneralMasterData.Api.Companies.Application.Dtos
{
    public class EditCompanyResponse
    {
        public Guid Id { get; set; }
        public string Description { get; set; } = String.Empty;
        public bool Status { get; set; }
    }
}
