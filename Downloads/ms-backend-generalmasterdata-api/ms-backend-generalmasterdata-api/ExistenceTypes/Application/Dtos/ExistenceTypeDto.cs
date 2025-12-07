namespace AnaPrevention.GeneralMasterData.Api.ExistenceTypes.Application.Dtos
{
    public class ExistenceTypeDto
    {
        public Guid Id { get; set; }
        public string Description { get; set; } = String.Empty;
        public string Code { get; set; } = String.Empty;
        public bool Status { get; set; }
    }
}
