namespace AnaPrevention.GeneralMasterData.Api.Subsidiaries.Application.Dtos
{
    public class RegisterDistributionListEmailRequest
    {
        public Guid SubsidiaryId { get; set; }
        public string DistributionList { get; set; } = string.Empty;
        public string DistributionListLaboratory { get; set; } = string.Empty;
    }
}
