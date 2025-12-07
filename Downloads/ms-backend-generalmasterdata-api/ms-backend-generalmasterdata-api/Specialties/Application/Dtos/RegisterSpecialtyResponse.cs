namespace AnaPrevention.GeneralMasterData.Api.Specialties.Application.Dtos
{
    public class RegisterSpecialtyResponse
    {
        public Guid Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public bool Status { get; set; }
    }
}
