namespace AnaPrevention.GeneralMasterData.Api.BusinessProfiles.Application.Dtos
{
    public class BusinessProfileProtocolDto
    {
        public Guid Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public Guid BusinessId { get; set; }
        public bool Status { get; set; }
        public Guid ProtocolId { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateFinish { get; set; }
        public List<Guid> SubsidiaryIds { get; set; } = new();
    }
}
