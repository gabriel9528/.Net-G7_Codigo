namespace AnaPrevention.GeneralMasterData.Api.Companies.Application.Dtos
{
    public class InformationRequest
    {
        public string WebSite { get; set; } = string.Empty;
        public string Phones { get; set; } = string.Empty;
        public string Logo { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string DescriptionDisplay { get; set; } = string.Empty;
        public string WebResult { get; set; } = string.Empty;
        public Guid? BusinessId { get; set; }

    }
}
