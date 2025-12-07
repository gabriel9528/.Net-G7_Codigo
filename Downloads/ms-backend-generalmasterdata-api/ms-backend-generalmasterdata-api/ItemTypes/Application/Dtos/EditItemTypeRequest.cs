namespace AnaPrevention.GeneralMasterData.Api.ItemTypes.Application.Dtos
{
    public class EditItemTypeRequest
    {
        public Guid Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public bool Status { get; set; }
    }
}
