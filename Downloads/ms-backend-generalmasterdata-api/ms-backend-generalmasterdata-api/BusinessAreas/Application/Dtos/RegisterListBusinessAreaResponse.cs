namespace AnaPrevention.GeneralMasterData.Api.BusinessAreas.Application.Dtos
{
    public class RegisterListBusinessAreaResponse
    {
        public List<string> ListDescription { get; set; } = new List<string>();
        public Guid BusinessId { get; set; }
    }
}
